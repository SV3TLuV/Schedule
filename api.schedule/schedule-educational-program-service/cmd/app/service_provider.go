package main

import (
	trmsqlx "github.com/avito-tech/go-transaction-manager/drivers/sqlx/v2"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/services/term"
	"schedule-educational-program-service/pkg/db/postgresql"
	"schedule-educational-program-service/pkg/migrator"
)

const migrationPath = "file://migrations/postgresql"

type serviceProvider struct {
	config           *Config
	postgresqlClient *postgresql.Client
	migrator         migrator.Migrator
	trManager        *manager.Manager
	termRepository   term.Repository
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
	p.trManager = manager.Must(trmsqlx.NewDefaultFactory(p.postgresqlClient.Connection))
	p.termRepository = term.NewRepo(p.postgresqlClient.Connection, trmsqlx.DefaultCtxGetter)
}
