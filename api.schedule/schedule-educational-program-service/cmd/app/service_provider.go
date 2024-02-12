package main

import (
	trmsqlx "github.com/avito-tech/go-transaction-manager/drivers/sqlx/v2"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/repository/discipline"
	"schedule-educational-program-service/cmd/app/internal/repository/discipline_code"
	"schedule-educational-program-service/cmd/app/internal/repository/discipline_type"
	"schedule-educational-program-service/cmd/app/internal/repository/speciality"
	"schedule-educational-program-service/cmd/app/internal/repository/speciality_discipline"
	"schedule-educational-program-service/cmd/app/internal/repository/term"
	term2 "schedule-educational-program-service/cmd/app/internal/usecases/term"
	"schedule-educational-program-service/pkg/db/postgresql"
	"schedule-educational-program-service/pkg/migrator"
)

type serviceProvider struct {
	config                         *Config
	postgresqlClient               *postgresql.Client
	migrator                       migrator.Migrator
	trManager                      *manager.Manager
	termRepository                 term.Repository
	termUseCase                    term2.UseCase
	disciplineRepository           discipline.Repository
	disciplineCodeRepository       discipline_code.Repository
	disciplineTypeRepository       discipline_type.Repository
	specialityRepository           speciality.Repository
	specialityDisciplineRepository speciality_discipline.Repository
}

func newServiceProvider() *serviceProvider {
	provider := &serviceProvider{}
	provider.init()
	return provider
}

func (p *serviceProvider) init() {
	p.initConfig()
	p.initPostgresClient()
	p.initMigrator()
	p.initTrManager()
	p.initRepositories()
	p.initUseCases()
}

func (p *serviceProvider) initConfig() {
	p.config = newConfig()
}

func (p *serviceProvider) initPostgresClient() {
	p.postgresqlClient = postgresql.New(
		p.config.Postgres.User,
		p.config.Postgres.Password,
		p.config.Postgres.Database,
		p.config.Postgres.Host,
		p.config.Postgres.Port)
}

func (p *serviceProvider) initMigrator() {
	const migrationPath = "file://migrations/postgresql"
	p.migrator = migrator.New(migrationPath, p.config.Postgres.URL())
}

func (p *serviceProvider) initTrManager() {
	p.trManager = manager.Must(trmsqlx.NewDefaultFactory(p.postgresqlClient.Connection))
}

func (p *serviceProvider) initRepositories() {
	db, getter := p.postgresqlClient.Connection, trmsqlx.DefaultCtxGetter

	p.termRepository = term.NewRepo(db, getter)
	p.disciplineRepository = discipline.NewRepo(db, getter)
	p.disciplineCodeRepository = discipline_code.NewRepo(db, getter)
	p.disciplineTypeRepository = discipline_type.NewRepo(db, getter)
	p.specialityRepository = speciality.NewRepo(db, getter)
	p.specialityDisciplineRepository = speciality_discipline.NewRepo(db, getter)
}

func (p *serviceProvider) initUseCases() {
	p.termUseCase = term2.NewTermUseCase(p.termRepository, p.trManager)
}
