package day

import (
	"Api/internal/model"
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
	if err != nil {
		return nil, err
	}

	return &days, nil
}

func (r *DayRepository) GetById(tx *sqlx.Tx, ID uint8) (*model.Day, error) {
	var day model.Day
	query := r.db.Rebind(`SELECT * FROM "days" WHERE "id" = ?`)
	err := tx.Select(&day, query, ID)
	if err != nil {
		return nil, err
	}

	return &day, nil
}
