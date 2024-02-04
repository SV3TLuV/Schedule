package handlers

import (
	"Api/internal/app"
	"Api/internal/database"
	"encoding/json"
	"github.com/gorilla/mux"
	"net/http"
)

func NewDayHandler() *mux.Router {
	r := mux.NewRouter()
	r.HandleFunc("/day", handleDay).Methods("GET")
	return r
}

func handleDay(w http.ResponseWriter, r *http.Request) {
	tr, _ := app.DB.Begin()
	dr := database.NewDayRepository()
	days := dr.Get(tr)
	err := json.NewEncoder(w).Encode(days)

	if err != nil {
		return
	}
}
