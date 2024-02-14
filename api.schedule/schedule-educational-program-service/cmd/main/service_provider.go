package main

import (
	trmsqlx "github.com/avito-tech/go-transaction-manager/drivers/sqlx/v2"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/main/internal/adapter/discipline"
	"schedule-educational-program-service/cmd/main/internal/adapter/discipline_code"
	"schedule-educational-program-service/cmd/main/internal/adapter/discipline_type"
	"schedule-educational-program-service/cmd/main/internal/adapter/speciality"
	"schedule-educational-program-service/cmd/main/internal/adapter/speciality_discipline"
	"schedule-educational-program-service/cmd/main/internal/adapter/term"
	config2 "schedule-educational-program-service/cmd/main/internal/config"
	discipline2 "schedule-educational-program-service/cmd/main/internal/interactor/discipline"
	discipline_code2 "schedule-educational-program-service/cmd/main/internal/interactor/discipline_code"
	discipline_type2 "schedule-educational-program-service/cmd/main/internal/interactor/discipline_type"
	speciality2 "schedule-educational-program-service/cmd/main/internal/interactor/speciality"
	term2 "schedule-educational-program-service/cmd/main/internal/interactor/term"
	"schedule-educational-program-service/cmd/main/internal/server/http"
	"schedule-educational-program-service/pkg/db/postgresql"
	"schedule-educational-program-service/pkg/migrator"
)

type serviceProvider struct {
	config *config2.Config

	migrator         migrator.Migrator
	postgresqlClient *postgresql.Client

	httpServer http.HttpServer

	trManager *manager.Manager

	termRepository term.Repository
	termInteractor term2.Interactor

	disciplineRepository discipline.Repository
	disciplineInteractor discipline2.Interactor

	disciplineCodeRepository discipline_code.Repository
	disciplineCodeInteractor discipline_code2.Interactor

	disciplineTypeRepository discipline_type.Repository
	disciplineTypeInteractor discipline_type2.Interactor

	specialityRepository speciality.Repository
	specialityInteractor speciality2.Interactor

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
	p.initInteractors()
	p.initHttpServer()
}

func (p *serviceProvider) initConfig() {
	p.config = config2.NewConfig()
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

func (p *serviceProvider) initInteractors() {
	p.termInteractor = term2.NewInteractor(p.trManager, p.termRepository)
	p.disciplineInteractor = discipline2.NewInteractor(p.trManager, p.disciplineRepository)
	p.disciplineCodeInteractor = discipline_code2.NewInteractor(p.trManager, p.disciplineCodeRepository)
	p.disciplineTypeInteractor = discipline_type2.NewInteractor(p.trManager, p.disciplineTypeRepository)
	p.specialityInteractor = speciality2.NewInteractor(
		p.trManager, p.specialityRepository, p.specialityDisciplineRepository)
}

func (p *serviceProvider) initHttpServer() {
	p.httpServer = http.NewServer(p.config.Http)
}
