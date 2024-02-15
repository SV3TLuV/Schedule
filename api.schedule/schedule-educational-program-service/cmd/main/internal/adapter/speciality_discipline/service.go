package speciality_discipline

import (
	"context"
	"errors"
	trmsqlx "github.com/avito-tech/go-transaction-manager/drivers/sqlx/v2"
	"github.com/jmoiron/sqlx"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type service struct {
	db     *sqlx.DB
	getter *trmsqlx.CtxGetter
}

func NewRepository(db *sqlx.DB, getter *trmsqlx.CtxGetter) Repository {
	return &service{
		db:     db,
		getter: getter,
	}
}

func (s *service) GetAll(ctx context.Context, o *GetAllOptions) (*[]entity.SpecialityDiscipline, error) {
	query := `SELECT * FROM speciality_disciplines
			  WHERE (:speciality_id IS NULL OR speciality_id = :speciality_id)
			  AND (:discipline_id IS NULL OR discipline_id = :discipline_id)
			  LIMIT :limit
			  OFFSET :offset;`
	specialityDisciplines := []entity.SpecialityDiscipline{}

	rows, err := s.getter.DefaultTrOrDB(ctx, s.db).NamedQuery(s.db.Rebind(query), o)
	if err != nil {
		return nil, err
	}

	return &specialityDisciplines, rows.StructScan(&specialityDisciplines)
}

func (s *service) GetOne(ctx context.Context, key PK) (*entity.SpecialityDiscipline, error) {
	query := `SELECT * FROM speciality_disciplines 
         	  WHERE speciality_id = ? AND discipline_id = ?;`
	speciality := entity.SpecialityDiscipline{}
	return &speciality, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &speciality,
		s.db.Rebind(query), key.specialityId, key.disciplineId)
}

func (s *service) Save(ctx context.Context, sd *entity.SpecialityDiscipline) error {
	query := `INSERT INTO speciality_disciplines (speciality_id, discipline_id, discipline_code_id, total_hours, term_id)
			  VALUES (:speciality_id, :discipline_id, :discipline_code_id, :total_hours, :term_id)
			  ON CONFLICT (speciality_id, discipline_id)
			  DO UPDATE 
			  SET discipline_code_id = :discipline_code_id,
			      total_hours = :total_hours,
			      term_id = :term_id;`

	res, err := sqlx.NamedExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), sd)
	if err != nil {
		return err
	}

	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return errors.New("unknown error")
	}

	return err
}

func (s *service) Delete(ctx context.Context, key PK) error {
	query := `DELETE FROM speciality_disciplines 
       		  WHERE speciality_id = ? AND discipline_id = ?;`

	res := sqlx.MustExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db),
		s.db.Rebind(query), key.specialityId, key.disciplineId)

	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return errors.New("specialityDiscpline not found")
	}

	return err
}
