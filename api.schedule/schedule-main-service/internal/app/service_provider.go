package app

import (
	"Api/internal/config"
	"Api/internal/database"
	"Api/internal/repository"
	"github.com/jmoiron/sqlx"
	"log"
)

type ServiceProvider struct {
	postgres      *sqlx.DB
	dayRepository repository.Repository
}

func newServiceProvider() *ServiceProvider {
	return &ServiceProvider{}
}

func (s *ServiceProvider) Postgresql() *sqlx.DB {
	if s.postgres == nil {
		cfg, err := config.NewPostgresqlConfig()

		if err != nil {
			log.Fatalf("Failed to get postgresql config: %s", err.Error())
		}

		s.postgres, err = database.NewPostgresqlConnection(cfg)

		if err != nil {
			log.Fatalf("Failed to connect to postgresql: %s", err.Error())
		}
	}

	return s.postgres
}

func (s *ServiceProvider) DayRepository() repository.Repository {
	if s.dayRepository == nil {
		s.dayRepository = repository.NewDayRepository(s.postgres)
	}

	return s.dayRepository
}
