package get

import (
	"Api/internal/middleware"
	"Api/internal/repository/day"
	"encoding/json"
	"github.com/gorilla/mux"
	"github.com/jmoiron/sqlx"
	"net/http"
	"strconv"
)

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
	request := &getDayQuery{}

	params := mux.Vars(r)
	id, err := strconv.ParseInt(params["id"], 10, 64)
	if err != nil {
		return nil, middleware.BadRequest
	}

	request.ID = id

	return request, nil
}

func GetDay(db *sqlx.DB, rep day.Repository) http.HandlerFunc {
	return middleware.ErrorMiddleware(func(writer http.ResponseWriter, request *http.Request) error {
		query, err := fromRequest(request)
		if err != nil {
			return err
		}

		err = query.Validate()
		if err != nil {
			return err
		}

		tr := db.MustBegin()
		day, err := rep.GetById(tr, query.ID)
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
