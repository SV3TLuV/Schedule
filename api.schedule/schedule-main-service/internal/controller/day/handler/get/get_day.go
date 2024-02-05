package get

import (
	"Api/internal/middleware"
	"Api/internal/repository"
	"encoding/json"
	"github.com/gorilla/mux"
	"github.com/jmoiron/sqlx"
	"net/http"
	"strconv"
)

type getDayQuery struct {
	ID uint8
}

func fromRequest(r *http.Request) (*getDayQuery, error) {
	request := &getDayQuery{}

	params := mux.Vars(r)
	id, err := strconv.ParseUint(params["id"], 0, 64)
	if err != nil || id <= 0 || id > 255 {
		return nil, middleware.BadRequest
	}

	request.ID = uint8(id)

	return request, nil
}

func GetDay(db *sqlx.DB, rep repository.Repository) http.HandlerFunc {
	return middleware.ErrorMiddleware(func(writer http.ResponseWriter, request *http.Request) error {
		query, err := fromRequest(request)
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
