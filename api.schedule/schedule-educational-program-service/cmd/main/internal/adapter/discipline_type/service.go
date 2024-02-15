package discipline_type

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

func (s *service) GetAll(ctx context.Context, o *GetAllOptions) (*[]entity.DisciplineType, error) {
	query := `SELECT * FROM discipline_types
			  WHERE starts_with(name, :search)
			  LIMIT :limit
			  OFFSET :offset;`
	types := []entity.DisciplineType{}

	rows, err := s.getter.DefaultTrOrDB(ctx, s.db).NamedQuery(s.db.Rebind(query), o)
	if err != nil {
		return nil, err
	}

	return &types, rows.StructScan(&types)
}

func (s *service) GetOne(ctx context.Context, id int64) (*entity.DisciplineType, error) {
	query := `SELECT * FROM discipline_types WHERE id = ?;`
	dType := entity.DisciplineType{}
	return &dType, s.getter.DefaultTrOrDB(ctx, s.db).GetContext(ctx, &dType, s.db.Rebind(query), id)
}

func (s *service) Save(ctx context.Context, dt *entity.DisciplineType) error {
	query := `UPDATE discipline_types SET name = :name WHERE id = :id;`
	if dt.ID == 0 {
		query = `INSERT INTO discipline_types (name) VALUES (:name);`
	}

	res, err := sqlx.NamedExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), dt)

	if err != nil {
		return err
	} else if dt.ID != 0 {
		return nil
	} else if dt.ID, err = res.LastInsertId(); err != nil {
		return err
	}

	return err
}

func (s *service) Delete(ctx context.Context, id int64) error {
	query := `DELETE FROM discipline_types WHERE id = ?;`

	res := sqlx.MustExecContext(ctx, s.getter.DefaultTrOrDB(ctx, s.db), s.db.Rebind(query), id)

	affected, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if affected == 0 {
		return errors.New("disciplineType not found")
	}

	return err
}
