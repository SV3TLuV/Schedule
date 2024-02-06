package get_list

import (
	"Api/internal/middleware"
	"Api/internal/repository/day"
	"encoding/json"
	"github.com/jmoiron/sqlx"
	"net/http"
)

func GetDays(db *sqlx.DB, rep day.Repository) http.HandlerFunc {
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
