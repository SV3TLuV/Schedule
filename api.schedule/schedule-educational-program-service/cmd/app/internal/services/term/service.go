package term

import (
	"context"
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

func (s *service) GetAll(ctx context.Context) (*[]model.Term, error) {
	query := `SELECT * FROM terms;`
	terms := []model.Term{}
	return &terms, s.getter.DefaultTrOrDB(ctx, s.db).SelectContext(ctx, &terms, query)
}

func (s *service) GetByID(ctx context.Context, id int64) (*model.Term, error) {
	query := `SELECT * FROM terms WHERE id = ?;`
	term := model.Term{}
	return &term, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &term, s.db.Rebind(query), id)
}
