package database

import (
	"Api/internal/models"
	"github.com/jackc/pgx"
)

type Repository interface {
	GetById(tr *pgx.Tx, ID uint8) *models.Day
	Get(tr *pgx.Tx) *[]models.Day
}

type DayRepository struct{}

func NewDayRepository() *DayRepository {
	return &DayRepository{}
}

func (r *DayRepository) Get(tr *pgx.Tx) *[]models.Day {
	rows, _ := tr.Query(`SELECT * FROM "days"`)

	var days []models.Day
	_ = rows.Scan(&days)
	return &days
}
