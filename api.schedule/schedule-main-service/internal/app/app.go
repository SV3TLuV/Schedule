package app

import (
	"Api/internal/handlers"
	"net/http"
)

func StartApp() {
	err := http.ListenAndServe(":8080", handlers.NewDayHandler())

	if err != nil {
		return
	}
}
