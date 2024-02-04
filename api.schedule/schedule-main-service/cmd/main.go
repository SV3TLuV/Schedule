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
		log.Fatalf("Failed on initialization app: %s", err)
	}

	err = application.Run()

	if err != nil {
		log.Fatalf("Failed on started app: %s", err)
	}
}
