package main

import (
	"Api/internal/app"
	"context"
	"log"
)

func main() {
	ctx := context.Background()
	application, err := app.NewApp(&ctx)

	if err != nil {
		log.Fatalf("Failed on initialization main: %s", err)
	}

	err = application.Run()

	if err != nil {
		log.Fatalf("Failed on started main: %s", err)
	}
}
