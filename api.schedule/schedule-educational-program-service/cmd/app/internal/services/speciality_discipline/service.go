package speciality_discipline

import (
	"context"
	"errors"
	trmsqlx "github.com/avito-tech/go-transaction-manager/drivers/sqlx/v2"
	"github.com/jmoiron/sqlx"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type service struct {
	db     *sqlx.DB
	getter *trmsqlx.CtxGetter
}

func NewRepo(db *sqlx.DB, getter *trmsqlx.CtxGetter) Repository {
	return &service{
		db:     db,
		getter: getter,
	}
}

func (s *service) GetAll(ctx context.Context) (*[]model.SpecialityDiscipline, error) {
	query := `SELECT * FROM speciality_disciplines;`
	specialityDisciplines := []model.SpecialityDiscipline{}
	return &specialityDisciplines, s.getter.DefaultTrOrDB(ctx, s.db).SelectContext(ctx, &specialityDisciplines, query)
}

func (s *service) GetByID(ctx context.Context, key PK) (*model.SpecialityDiscipline, error) {
	query := `SELECT * FROM speciality_disciplines 
         	  WHERE speciality_id = ? AND discipline_id = ?;`
	speciality := model.SpecialityDiscipline{}
	return &speciality, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &speciality,
		s.db.Rebind(query), key.specialityId, key.disciplineId)
}

func (s *service) Save(ctx context.Context, sd *model.SpecialityDiscipline) error {
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
