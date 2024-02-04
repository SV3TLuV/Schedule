package handlers

import (
	"Api/internal/models"
	"github.com/gorilla/mux"
	"github.com/jackc/pgx"
	"net/http"
)

func newDayHandler() *mux.Router {
	r := mux.NewRouter()
	r.HandleFunc("/day", handleDay).Methods("GET")
}

func handleDay(w http.ResponseWriter, r *http.Request) {
}

type Repository interface {
	GetById(tr *pgx.Tx, ID uint8) *models.Day
	Get(tr *pgx.Tx) *[]models.Day
}

type DayRepository struct{}

func (r *DayRepository) Get(tr *pgx.Tx) *[]models.Day {
	rows, _ := tr.Query(`SELECT * FROM "days"`)

	var days []models.Day
	_ = rows.Scan(&days)
	return &days
}
