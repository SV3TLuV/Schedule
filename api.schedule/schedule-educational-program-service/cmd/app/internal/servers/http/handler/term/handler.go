package term

import (
	"encoding/json"
	"github.com/gorilla/mux"
	"net/http"
	"schedule-educational-program-service/cmd/app/internal/servers/http/handler"
	"schedule-educational-program-service/cmd/app/internal/usecases/term"
	"strconv"
)

func GetList(u term.UseCase) handler.AppHandler {
	return func(w http.ResponseWriter, r *http.Request) error {
		list, err := u.List(r.Context())
		if err != nil {
			return err
		}
		return json.NewEncoder(w).Encode(list)
	}
}

func GetById(u term.UseCase) handler.AppHandler {
	return func(w http.ResponseWriter, r *http.Request) error {
		params := mux.Vars(r)

		id, err := strconv.ParseInt(params["id"], 10, 64)
		if err != nil {
			return err
		}

		list, err := u.FindById(r.Context(), id)
		if err != nil {
			return err
		}
		return json.NewEncoder(w).Encode(list)
	}
}
