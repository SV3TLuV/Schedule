package postgresql

import (
	"github.com/jackc/pgx"
	"github.com/jackc/pgx/stdlib"
	"github.com/jmoiron/sqlx"
)

type Client struct {
	Connection *sqlx.DB
}

func New(user, password, database, host string, port uint16) *Client {
	cfg := pgx.ConnConfig{
		PreferSimpleProtocol: true,
		User:                 user,
		Password:             password,
		Database:             database,
		Host:                 host,
		Port:                 port,
	}

	db := stdlib.OpenDB(cfg)
	err := db.Ping()
	if err != nil {
		panic(err)
	}

	return &Client{
		Connection: sqlx.NewDb(db, "pgx"),
	}
}
