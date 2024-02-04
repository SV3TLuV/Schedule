package day

import (
	"Api/internal/model"
	"database/sql"
	"github.com/jackc/pgx"
)

type Repository interface {
	GetById(tr *sql.Tx, ID uint8) *model.Day
	Get(tr *sql.Tx) *[]model.Day
}

type DayRepository struct{}

func NewDayRepository() *DayRepository {
	return &DayRepository{}
}

func (r *DayRepository) Get(tr *sql.Tx) *[]model.Day {
	rows, _ := tr.Query(`SELECT * FROM "days"`)

	var days []model.Day
	_ = rows.Scan(&days)
	return &days
}

func (r *DayRepository) GetById(tr *pgx.Tx, ID uint8) *model.Day {
	return nil
}
