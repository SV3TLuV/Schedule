package main

import (
	"Api/internal/app"
	"Api/internal/database"
	"github.com/jackc/pgx"
)

var DB *pgx.Conn

func main() {
	DB := database.InitDatabase()

	app.StartApp()
}
