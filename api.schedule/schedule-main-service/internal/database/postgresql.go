package database

import (
	"Api/internal/config"
	"github.com/jackc/pgx"
)

func NewPostgresqlConnection(config *config.PostgresqlConfig) (*pgx.Conn, error) {
	cfg := pgx.ConnConfig{
		PreferSimpleProtocol: true,
		Host:                 config.Host,
		Port:                 config.Port,
		User:                 config.User,
		Password:             config.Password,
		Database:             config.Database,
	}

	conn, err := pgx.Connect(cfg)

	if err != nil {
		return nil, err
	}

	return conn, nil
}
