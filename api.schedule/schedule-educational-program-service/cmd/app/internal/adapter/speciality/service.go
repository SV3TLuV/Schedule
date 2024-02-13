package speciality

import (
	"context"
	"errors"
	trmsqlx "github.com/avito-tech/go-transaction-manager/drivers/sqlx/v2"
	"github.com/jmoiron/sqlx"
	"schedule-educational-program-service/cmd/app/internal/entity"
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

func (s *service) GetAll(ctx context.Context, o *GetAllOptions) (*[]entity.Speciality, error) {
	query := `SELECT * FROM specialities
			  WHERE starts_with(name, :search)
			  LIMIT :limit
			  OFFSET :offset;`
	specialities := []entity.Speciality{}

	rows, err := s.getter.DefaultTrOrDB(ctx, s.db).NamedQuery(s.db.Rebind(query), o)
	if err != nil {
		return nil, err
	}

	return &specialities, rows.StructScan(&specialities)
}

func (s *service) GetOne(ctx context.Context, id int64) (*entity.Speciality, error) {
	query := `SELECT * FROM specialities WHERE id = ?;`
	speciality := entity.Speciality{}
	return &speciality, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &speciality, s.db.Rebind(query), id)
}

func (s *service) Save(ctx context.Context, speciality *entity.Speciality) error {
	query := `UPDATE specialities SET name = :name, code = :code, term_count = :term_count WHERE id = :id;`
	if speciality.ID == 0 {
		query = `INSERT INTO specialities (name, code, term_count) VALUES (:name, :code, :term_count);`
	}

	res, err := sqlx.NamedExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), speciality)

	if err != nil {
		return err
	} else if speciality.ID != 0 {
		return nil
	} else if speciality.ID, err = res.LastInsertId(); err != nil {
		return err
	}

	return err
}

func (s *service) Delete(ctx context.Context, id int64) error {
	query := `DELETE FROM specialities WHERE id = ?;`

	res := sqlx.MustExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), id)

	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return errors.New("speciality not found")
	}

	return err
}
