package term

import (
	"encoding/json"
	"github.com/gorilla/mux"
	"net/http"
	"schedule-educational-program-service/cmd/app/internal/usecases/term"
	"strconv"
)

func GetList(u term.UseCase) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		list, err := u.List(r.Context())
		if err != nil {
			panic(err)
		}

		err = json.NewEncoder(w).Encode(list)
		if err != nil {
			panic(err)
		}
	}
}

func GetById(u term.UseCase) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		params := mux.Vars(r)

		id, err := strconv.ParseInt(params["id"], 10, 64)
		if err != nil {
			panic(err)
		}

		list, err := u.FindById(r.Context(), id)
		if err != nil {
			panic(err)
		}

		err = json.NewEncoder(w).Encode(list)
		if err != nil {
			panic(err)
		}
	}
}
