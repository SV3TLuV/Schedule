package http

import (
	"github.com/gorilla/mux"
	"net/http"
	"schedule-educational-program-service/cmd/app/internal/servers/http/handler/term"
	term2 "schedule-educational-program-service/cmd/app/internal/usecases/term"
	"schedule-educational-program-service/pkg/middleware"
)

type HttpServer interface {
	Run() error
}

type server struct {
	address     string
	router      *mux.Router
	termUseCase term2.UseCase
}

func NewHttpServer(address string, termUseCase term2.UseCase) HttpServer {
	return &server{
		router:      mux.NewRouter(),
		address:     address,
		termUseCase: termUseCase,
	}
}

func (s *server) Run() error {
	s.router.PathPrefix("/api")
	s.router.Use(middleware.ErrorMiddleware())
	s.initRoutes()
	return http.ListenAndServe(s.address, s.router)
}

func (s *server) initRoutes() {
	s.router.HandleFunc("/term", term.GetList(s.termUseCase))
	s.router.HandleFunc("/term/{id}", term.GetById(s.termUseCase))
}
