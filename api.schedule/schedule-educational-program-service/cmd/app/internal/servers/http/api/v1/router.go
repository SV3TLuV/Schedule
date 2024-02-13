package v1

import (
	"github.com/gorilla/mux"
	"net/http"
)

func (h *Handler) GetVersion() string {
	return "v1"
}

func (h *Handler) GetContentType() string {
	return ""
}

func (h *Handler) AddRoutes(r *mux.Router) {
	r.HandleFunc("/term", h.GetTermList).Methods(http.MethodGet)
	r.HandleFunc("/term/{id}", h.GetTermById).Methods(http.MethodGet)
}
