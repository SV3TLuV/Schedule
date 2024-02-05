package controller

import (
	"Api/internal/repository/day"
	"encoding/json"
	"github.com/gorilla/mux"
	"github.com/jmoiron/sqlx"
	"net/http"
	"strconv"
)

type DayController struct {
}

func (c *DayController) Init(r *mux.Router, rep day.Repository, db *sqlx.DB) {
	router := r.PathPrefix("/day").Subrouter()

	router.HandleFunc("", getDays(db, rep)).Methods("GET")
	router.HandleFunc("/{id}", getDay(db, rep)).Methods("GET")
}

func getDays(db *sqlx.DB, rep day.Repository) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		tr := db.MustBegin()
		days, _ := rep.Get(tr)
		_ = json.NewEncoder(writer).Encode(days)
	}
}

func getDay(db *sqlx.DB, rep day.Repository) http.HandlerFunc {
	return func(writer http.ResponseWriter, request *http.Request) {
		params := mux.Vars(request)
		id, _ := strconv.ParseUint(params["id"], 0, 64)
		tr := db.MustBegin()
		day, _ := rep.GetById(tr, uint8(id))
		_ = json.NewEncoder(writer).Encode(day)
	}
}
