package app

import (
	"Api/internal/config"
	"Api/internal/database"
	"Api/internal/repository/day"
	"github.com/jmoiron/sqlx"
	"log"
)

type ServiceProvider struct {
	db            *sqlx.DB
	dayRepository day.Repository
}

func newServiceProvider() *ServiceProvider {
	return &ServiceProvider{}
}

func (s *ServiceProvider) Postgresql() *sqlx.DB {
	if s.db == nil {
		cfg, err := config.NewPostgresqlConfig()

		if err != nil {
			log.Fatalf("Failed to get postgresql config: %s", err.Error())
		}

		s.db = database.NewPostgresqlConnection(cfg)

		if err != nil {
			log.Fatalf("Failed to connect to postgresql: %s", err.Error())
		}
	}

	return s.db
}

func (s *ServiceProvider) DayRepository() *day.Repository {
	if s.dayRepository == nil {
		s.dayRepository = day.NewDayRepository()
	}

	return &s.dayRepository
}
