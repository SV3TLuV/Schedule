package database

import (
	"github.com/jackc/pgx"
)

func InitDatabase() *pgx.Conn {
	cfg := pgx.ConnConfig{
		PreferSimpleProtocol: true,
		Host:                 "localhost",
		User:                 "postgres",
		Password:             "P@ssw0rd",
		Database:             "schedule",
		Port:                 5432,
	}

	conn, _ := pgx.Connect(cfg)

	return conn
}
