package http

import (
	"github.com/gorilla/mux"
	"net/http"
	"schedule-educational-program-service/cmd/app/internal/servers/http/handler/term"
	term2 "schedule-educational-program-service/cmd/app/internal/services/term"
	"schedule-educational-program-service/pkg/middleware"
)

type HttpServer interface {
	Run() error
}

type server struct {
	address     string
	router      *mux.Router
	termService term2.Service
}

func NewHttpServer(address string, termUseCase term2.Service) HttpServer {
	return &server{
		router:      mux.NewRouter(),
		address:     address,
		termService: termUseCase,
	}
}

func (s *server) Run() error {
	s.router.PathPrefix("/api")
	s.router.Use(middleware.ErrorMiddleware())
	s.initRoutes()
	return http.ListenAndServe(s.address, s.router)
}

func (s *server) initRoutes() {
	s.router.HandleFunc("/term", term.GetList(s.termService))
	s.router.HandleFunc("/term/{id}", term.GetById(s.termService))
}
