package http

import (
	"context"
	"errors"
	"github.com/gorilla/mux"
	"net/http"
	"schedule-educational-program-service/cmd/main/internal/config"
	v1 "schedule-educational-program-service/cmd/main/internal/server/http/api/v1"
)

type (
	Server interface {
		Run() error
		Stop() error
		RegisterRoutes(r *mux.Router)
	}

	server struct {
		srv *http.Server
		u   v1.UseCase
	}
)

func NewServer(cfg *config.HttpConfig, useCase v1.UseCase) Server {
	return &server{
		srv: &http.Server{
			Addr: cfg.Address(),
		},
		u: useCase,
	}
}

func (s *server) Run() error {
	handler := v1.NewHandler(s.u)
	router := NewRouter()
	router.WithHandler(handler)
	s.RegisterRoutes(router.router)

	if s.srv.Handler == nil {
		return errors.New("no routes have registered")
	}

	return s.srv.ListenAndServe()
}

func (s *server) Stop() error {
	return s.srv.Shutdown(context.Background())
}

func (s *server) RegisterRoutes(r *mux.Router) {
	s.srv.Handler = r
}
