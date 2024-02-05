package controller

import (
	"Api/internal/middleware"
	"Api/internal/repository"
	"encoding/json"
	"github.com/gorilla/mux"
	"github.com/jmoiron/sqlx"
	"net/http"
	"strconv"
)

type DayController struct {
}

func (c *DayController) Init(r *mux.Router, rep repository.Repository, db *sqlx.DB) {
	router := r.PathPrefix("/day").Subrouter()

	router.HandleFunc("", getDays(db, rep)).Methods("GET")
	router.HandleFunc("/{id}", getDay(db, rep)).Methods("GET")
}

func getDays(db *sqlx.DB, rep repository.Repository) http.HandlerFunc {
	return middleware.ErrorMiddleware(func(writer http.ResponseWriter, request *http.Request) error {
		tr := db.MustBegin()
		days, err := rep.Get(tr)
		if err != nil {
			return err
		}

		err = json.NewEncoder(writer).Encode(days)
		if err != nil {
			return err
		}

		return nil
	})
}

func getDay(db *sqlx.DB, rep repository.Repository) http.HandlerFunc {
	return middleware.ErrorMiddleware(func(writer http.ResponseWriter, request *http.Request) error {
		params := mux.Vars(request)
		id, err := strconv.ParseUint(params["id"], 0, 64)
		if err != nil {
			return err
		}

		tr := db.MustBegin()
		day, err := rep.GetById(tr, uint8(id))
		if err != nil {
			return err
		}

		err = json.NewEncoder(writer).Encode(day)
		if err != nil {
			return err
		}

		return nil
	})
}
