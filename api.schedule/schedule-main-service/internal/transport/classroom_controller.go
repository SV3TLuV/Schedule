package transport

import (
	"Api/internal/repository/classroom"
	"github.com/gorilla/mux"
	"github.com/jmoiron/sqlx"
)

type ClassroomController struct{}

func NewClassroomController() *ClassroomController {
	return &ClassroomController{}
}

func (c *ClassroomController) Init(r *mux.Router, rep classroom.Repository, db *sqlx.DB) {
	router := r.PathPrefix("/classroom").Subrouter()

	router.HandleFunc("", (db, rep)).Methods("GET")
	router.HandleFunc("/{id}", (db, rep)).Methods("GET")
}
