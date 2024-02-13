package v1

import (
	"encoding/json"
	"github.com/gorilla/mux"
	"net/http"
	"strconv"
)

func (h *Handler) GetTermList(w http.ResponseWriter, r *http.Request) {
	list, err := h.uc.GetTerms(r.Context())
	if err != nil {
		panic(err)
	}

	err = json.NewEncoder(w).Encode(list)
	if err != nil {
		panic(err)
	}
}

func (h *Handler) GetTermById(w http.ResponseWriter, r *http.Request) {
	params := mux.Vars(r)

	id, err := strconv.ParseInt(params["id"], 10, 64)
	if err != nil {
		panic(err)
	}

	list, err := h.uc.GetTermById(r.Context(), id)
	if err != nil {
		panic(err)
	}

	err = json.NewEncoder(w).Encode(list)
	if err != nil {
		panic(err)
	}
}
