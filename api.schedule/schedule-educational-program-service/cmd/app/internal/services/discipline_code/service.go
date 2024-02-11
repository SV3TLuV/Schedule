package discipline_code

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

func (s *service) GetAll(ctx context.Context) (*[]model.DisciplineCode, error) {
	query := `SELECT * FROM discipline_codes;`
	codes := []model.DisciplineCode{}
	return &codes, s.getter.DefaultTrOrDB(ctx, s.db).SelectContext(ctx, &codes, query)
}

func (s *service) GetByID(ctx context.Context, id int64) (*model.DisciplineCode, error) {
	query := `SELECT * FROM discipline_codes WHERE id = ?;`
	code := model.DisciplineCode{}
	return &code, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &code, s.db.Rebind(query), id)
}

func (s *service) Save(ctx context.Context, dc *model.DisciplineCode) error {
	query := `UPDATE discipline_codes SET name = :name WHERE id = :id;`
	if dc.ID == 0 {
		query = `INSERT INTO discipline_codes (name) VALUES (:name);`
	}

	res, err := sqlx.NamedExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), dc)

	if err != nil {
		return err
	} else if dc.ID != 0 {
		return nil
	} else if dc.ID, err = res.LastInsertId(); err != nil {
		return err
	}

	return err
}

func (s *service) Delete(ctx context.Context, id int64) error {
	query := `DELETE FROM discipline_codes WHERE id = ?;`

	res := sqlx.MustExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), id)

	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return fmt.Errorf("disciplineCode not found")
	}

	return err
}
