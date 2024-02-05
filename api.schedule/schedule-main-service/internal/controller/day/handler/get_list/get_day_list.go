package get_list

import (
	"Api/internal/middleware"
	"Api/internal/repository"
	"encoding/json"
	"github.com/gorilla/schema"
	"github.com/jmoiron/sqlx"
	"net/http"
)

type getDayListRequest struct {
	ID uint8
}

func (r *getDayListRequest) Validate() error {
	if r.ID <= 0 || r.ID > 255 {
		return middleware.BadRequest
	}

	return nil
}

func fromRequest(r *http.Request) (*getDayListRequest, error) {
	var request getDayListRequest
	decoder := schema.NewDecoder()
	err := decoder.Decode(&request, r.URL.Query())
	if err != nil {
		return nil, middleware.BadRequest
	}

	return &request, nil
}

func GetDays(db *sqlx.DB, rep repository.Repository) http.HandlerFunc {
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
