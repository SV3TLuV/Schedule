package day

import (
	"Api/internal/controller/day/handler/get"
	"Api/internal/controller/day/handler/get_list"
	"Api/internal/repository/day"
	"github.com/gorilla/mux"
	"github.com/jmoiron/sqlx"
)

type DayController struct {
}

func (c *DayController) Init(r *mux.Router, rep day.Repository, db *sqlx.DB) {
	router := r.PathPrefix("/day").Subrouter()

	router.HandleFunc("", get_list.GetDays(db, rep)).Methods("GET")
	router.HandleFunc("/{id}", get.GetDay(db, rep)).Methods("GET")
}
