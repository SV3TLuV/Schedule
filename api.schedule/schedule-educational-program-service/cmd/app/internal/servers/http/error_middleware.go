package http

import (
	"encoding/json"
	"fmt"
	"net/http"
	"schedule-educational-program-service/cmd/app/internal/servers/http/handler"
)

func errorMiddleware(h handler.AppHandler) http.HandlerFunc {
	return func(w http.ResponseWriter, r *http.Request) {
		err := h(w, r)
		if err != nil {
			err := json.NewEncoder(w).Encode(err)
			if err != nil {
				fmt.Printf("%s", err)
			}
		}
	}
}
