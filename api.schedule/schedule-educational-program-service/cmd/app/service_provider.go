package main

import (
	"schedule-educational-program-service/pkg/db/postgresql"
	"schedule-educational-program-service/pkg/migrator"
)

const migrationPath = "file://migrations/postgresql"

type serviceProvider struct {
	config           *Config
	postgresqlClient *postgresql.Client
	migrator         migrator.Migrator
}

func newServiceProvider() *serviceProvider {
	provider := &serviceProvider{}
	provider.init()
	return provider
}

func (p *serviceProvider) init() {
	p.config = newConfig()
	p.postgresqlClient = postgresql.New(
		p.config.Postgres.User,
		p.config.Postgres.Password,
		p.config.Postgres.Database,
		p.config.Postgres.Host,
		p.config.Postgres.Port)
	p.migrator = migrator.New(migrationPath, p.config.Postgres.URL())
}
