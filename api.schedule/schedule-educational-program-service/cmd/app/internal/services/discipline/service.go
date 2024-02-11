package discipline

import (
	"context"
	"fmt"
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

func (s *service) GetAll(ctx context.Context) (*[]model.Discipline, error) {
	query := `SELECT * FROM disciplines;`
	disciplines := []model.Discipline{}
	return &disciplines, s.getter.DefaultTrOrDB(ctx, s.db).SelectContext(ctx, &disciplines, query)
}

func (s *service) GetByID(ctx context.Context, id int) (*model.Discipline, error) {
	query := `SELECT * FROM disciplines WHERE id = ?;`
	discipline := model.Discipline{}
	return &discipline, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &discipline, s.db.Rebind(query), id)
}

func (s *service) Save(ctx context.Context, discipline *model.Discipline) error {
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

func (s *service) Delete(ctx context.Context, id int) error {
	query := `DELETE FROM disciplines WHERE id = ?;`

	res := sqlx.MustExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), id)

	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return fmt.Errorf("Discipline not found.")
	}

	return err
}
