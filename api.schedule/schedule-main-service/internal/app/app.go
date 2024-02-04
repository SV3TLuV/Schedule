package app

import (
	"context"
)

type App struct {
	provider *ServiceProvider
}

//*(*[]byte)(unsafe.Pointer(&line))

func NewApp(ctx *context.Context) (*App, error) {
	a := &App{}

	a.provider = newServiceProvider()

	return a, nil
}

func (a *App) Run() error {

}
