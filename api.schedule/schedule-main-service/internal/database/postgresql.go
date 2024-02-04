package database

import (
	"Api/internal/config"
	"github.com/jackc/pgx"
	"github.com/jackc/pgx/stdlib"
	"github.com/jmoiron/sqlx"
)

func NewPostgresqlConnection(config *config.PostgresqlConfig) *sqlx.DB {
	cfg := pgx.ConnConfig{
		PreferSimpleProtocol: true,
		Host:                 config.Host,
		Port:                 config.Port,
		User:                 config.User,
		Password:             config.Password,
		Database:             config.Database,
	}

	db := stdlib.OpenDB(cfg)
	return sqlx.NewDb(db, "pgx")
}
