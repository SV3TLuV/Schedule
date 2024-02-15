package migrator

import (
	"errors"
	"github.com/golang-migrate/migrate/v4"
	_ "github.com/golang-migrate/migrate/v4/database/postgres"
	_ "github.com/golang-migrate/migrate/v4/source/file"
)

type Migrator interface {
	Migrate() error
}

type migrator struct {
	path        string
	databaseURL string
}

func NewMigrator(path, databaseURL string) Migrator {
	return &migrator{
		path:        path,
		databaseURL: databaseURL,
	}
}

func (migrator *migrator) Migrate() error {
	m, err := migrate.New(migrator.path, migrator.databaseURL)
	if err != nil {
		return err
	}

	err = m.Up()
	if errors.Is(err, migrate.ErrNoChange) {
		return nil
	}

	return err
}
