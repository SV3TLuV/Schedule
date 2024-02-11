package migrator

import (
	"errors"
	"github.com/golang-migrate/migrate/v4"
	_ "github.com/golang-migrate/migrate/v4/database/postgres"
	_ "github.com/golang-migrate/migrate/v4/source/file"
	"log"
)

type Migrator interface {
	Migrate()
}

type migrator struct {
	path        string
	databaseURL string
}

func New(path, databaseURL string) Migrator {
	return &migrator{
		path:        path,
		databaseURL: databaseURL,
	}
}

func (migrator *migrator) Migrate() {
	m, err := migrate.New(migrator.path, migrator.databaseURL)
	if err != nil {
		panic(err)
	}

	if err := m.Up(); err != nil {
		if errors.Is(err, migrate.ErrNoChange) {
			log.Fatalf("%s", err)
		} else {
			panic(err)
		}
	}
}
