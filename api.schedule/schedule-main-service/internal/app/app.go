package app

import (
	"Api/internal/config"
	"context"
)

type App struct {
	serviceProvider *ServiceProvider
}

//*(*[]byte)(unsafe.Pointer(&line))

func NewApp(ctx *context.Context) (*App, error) {
	a := &App{}

	err := a.initDeps(ctx)
	if err != nil {
		return nil, err
	}

	return a, nil
}

func (a *App) Run() error {
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
