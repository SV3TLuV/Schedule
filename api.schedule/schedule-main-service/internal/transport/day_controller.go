package transport

import (
	"Api/internal/middleware"
	"Api/internal/repository/day"
	"encoding/json"
	"github.com/gorilla/mux"
	"github.com/jmoiron/sqlx"
	"net/http"
	"strconv"
)

type DayController struct {
}

func NewDayController() *DayController {
	return &DayController{}
}

func (c *DayController) Init(r *mux.Router, rep day.Repository, db *sqlx.DB) {
	router := r.PathPrefix("/day").Subrouter()

	router.HandleFunc("", getDays(db, rep)).Methods("GET")
	router.HandleFunc("/{id}", getDay(db, rep)).Methods("GET")
}

func getDays(db *sqlx.DB, rep day.Repository) http.HandlerFunc {
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

type getDayQuery struct {
	ID int64
}

func (q *getDayQuery) Validate() error {
	if q.ID <= 0 {
		return middleware.BadRequest
	}

	return nil
}

func fromRequest(r *http.Request) (*getDayQuery, error) {
	q := &getDayQuery{}

	params := mux.Vars(r)
	id, err := strconv.ParseInt(params["id"], 10, 64)
	if err != nil {
		return nil, middleware.BadRequest
	}

	q.ID = id

	return q, nil
}

func getDay(db *sqlx.DB, rep day.Repository) http.HandlerFunc {
	return middleware.ErrorMiddleware(func(writer http.ResponseWriter, request *http.Request) error {
		q, err := fromRequest(request)
		if err != nil {
			return err
		}

		err = q.Validate()
		if err != nil {
			return err
		}

		tr := db.MustBegin()
		day, err := rep.GetById(tr, q.ID)
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
