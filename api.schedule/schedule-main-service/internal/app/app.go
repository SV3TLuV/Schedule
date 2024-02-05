package app

import (
	"Api/internal/config"
	"Api/internal/controller/day"
	"context"
	"github.com/gorilla/mux"
	"net/http"
)

type App struct {
	serviceProvider *ServiceProvider
}

func NewApp(ctx *context.Context) (*App, error) {
	a := &App{}

	err := a.initDeps(ctx)
	if err != nil {
		return nil, err
	}

	return a, nil
}

func (a *App) Run() error {
	cfg, err := config.NewApiConfiguration()
	if err != nil {
		return err
	}

	router, err := initRouter(a)
	if err != nil {
		return err
	}

	err = http.ListenAndServe(cfg.Address(), router)
	if err != nil {
		return err
	}

	return nil
}

func (a *App) initDeps(ctx *context.Context) error {
	inits := []func(*context.Context) error{
		a.initEnvConfig,
		a.initServiceProvider,
	}

	for _, f := range inits {
		err := f(ctx)
		if err != nil {
			return err
		}
	}

	return nil
}

func (a *App) initEnvConfig(_ *context.Context) error {
	err := config.Load(".env.local")
	if err != nil {
		return err
	}

	return nil
}

func (a *App) initServiceProvider(_ *context.Context) error {
	a.serviceProvider = newServiceProvider()
	return nil
}

func initRouter(a *App) (*mux.Router, error) {
	router := mux.NewRouter()
	postgres := a.serviceProvider.Postgresql()

	(&day.DayController{}).Init(router, a.serviceProvider.DayRepository(), postgres)

	return router, nil
}
