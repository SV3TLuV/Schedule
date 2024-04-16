package discipline_code

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

func (s *service) GetAll(ctx context.Context, search string, limit, offset int64) (*[]entity.DisciplineCode, error) {
	params := struct {
		search string
		limit  int64
		offset int64
	}{search: search, limit: limit, offset: offset}

	query := `SELECT * FROM discipline_codes
			  WHERE starts_with(name, :search)
			  LIMIT :limit
			  OFFSET :offset;`
	codes := []entity.DisciplineCode{}

	rows, err := s.getter.DefaultTrOrDB(ctx, s.db).NamedQuery(s.db.Rebind(query), params)
	if err != nil {
		return nil, err
	}

	return &codes, rows.StructScan(&codes)
}

func (s *service) GetOne(ctx context.Context, id int64) (*entity.DisciplineCode, error) {
	query := `SELECT * FROM discipline_codes WHERE id = ?;`
	code := entity.DisciplineCode{}
	return &code, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &code, s.db.Rebind(query), id)
}

func (s *service) Save(ctx context.Context, dc *entity.DisciplineCode) error {
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
		return errors.New("disciplineCode not found")
	}

	return err
}
