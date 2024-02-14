package discipline

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

func NewRepo(db *sqlx.DB, getter *trmsqlx.CtxGetter) Repository {
	return &service{
		db:     db,
		getter: getter,
	}
}

func (s *service) GetAll(ctx context.Context, o *GetAllOptions) (*[]entity.Discipline, error) {
	query := `SELECT * FROM disciplines
			  WHERE starts_with(name, :search)
			  LIMIT :limit
			  OFFSET :offset;`
	disciplines := []entity.Discipline{}

	rows, err := s.getter.DefaultTrOrDB(ctx, s.db).NamedQuery(s.db.Rebind(query), o)
	if err != nil {
		return nil, err
	}

	return &disciplines, rows.StructScan(&disciplines)
}

func (s *service) GetOne(ctx context.Context, id int64) (*entity.Discipline, error) {
	query := `SELECT * FROM disciplines WHERE id = ?;`
	discipline := entity.Discipline{}
	return &discipline, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &discipline, s.db.Rebind(query), id)
}

func (s *service) Save(ctx context.Context, discipline *entity.Discipline) error {
	query := `UPDATE disciplines SET name = :name, type_id = :type_id WHERE id = :id;`
	if discipline.ID == 0 {
		query = `INSERT INTO disciplines (name, type_id) VALUES (:name, :type_id);`
	}

	res, err := sqlx.NamedExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), discipline)

	if err != nil {
		return err
	} else if discipline.ID != 0 {
		return nil
	} else if discipline.ID, err = res.LastInsertId(); err != nil {
		return err
	}

	return err
}

func (s *service) Delete(ctx context.Context, id int64) error {
	query := `DELETE FROM disciplines WHERE id = ?;`

	res := sqlx.MustExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), id)

	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return errors.New("discipline not found")
	}

	return err
}
