package day

import (
	"Api/internal/middleware"
	"Api/internal/model"
	"database/sql"
	"errors"
	"github.com/jmoiron/sqlx"
)

type Repository interface {
	GetById(tx *sqlx.Tx, ID uint8) (*model.Day, error)
	Get(tx *sqlx.Tx) (*[]model.Day, error)
}

type DayRepository struct {
	db *sqlx.DB
}

func NewDayRepository(db *sqlx.DB) *DayRepository {
	return &DayRepository{
		db: db,
	}
}

func (r *DayRepository) Get(tx *sqlx.Tx) (*[]model.Day, error) {
	var days []model.Day
	err := tx.Select(&days, `SELECT * FROM "days" ORDER BY "id"`)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, middleware.NotFound
	}

	if err != nil {
		return nil, err
	}

	return &days, nil
}

func (r *DayRepository) GetById(tx *sqlx.Tx, ID uint8) (*model.Day, error) {
	var day model.Day
	err := tx.Get(&day, `SELECT * FROM "days" WHERE "id" = $1`, ID)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, middleware.NotFound
	}

	if err != nil {
		return nil, err
	}

	return &day, nil
}
