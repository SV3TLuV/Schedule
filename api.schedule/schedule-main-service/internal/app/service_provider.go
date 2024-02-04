package app

import (
	"Api/internal/config"
	"Api/internal/database"
	"github.com/jackc/pgx"
	"log"
)

type ServiceProvider struct {
	db *pgx.Conn
}

func newServiceProvider() *ServiceProvider {
	return &ServiceProvider{}
}

func (s *ServiceProvider) Postgresql() *pgx.Conn {
	if s.db == nil {
		cfg, err := config.NewPostgresqlConfig()

		if err != nil {
			log.Fatalf("Failed to get postgresql config: %s", err.Error())
		}

		s.db, err = database.NewPostgresqlConnection(cfg)

		if err != nil {
			log.Fatalf("Failed to connect to postgresql: %s", err.Error())
		}
	}

	return s.db
}
