package database

import (
	"database/sql"
	"github.com/jackc/pgx"
	"github.com/jackc/pgx/stdlib"
)

func InitDatabase() *sql.DB {
	cfg := pgx.ConnConfig{
		PreferSimpleProtocol: true,
		Host:                 "localhost",
		User:                 "postgres",
		Password:             "P@ssw0rd",
		Database:             "schedule",
		Port:                 5432,
	}

	return stdlib.OpenDB(cfg)
}
