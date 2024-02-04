package main

import (
	"Api/internal/app"
	"context"
)

func main() {
	ctx := context.Background()
	app, err := app.NewApp(&ctx)

	if err != nil {
		return
	}

	app.Run()
}
