package http

import (
	"github.com/gorilla/mux"
	"net/http"
	"schedule-educational-program-service/cmd/app/internal/servers/http/handler/term"
	term2 "schedule-educational-program-service/cmd/app/internal/usecases/term"
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
	s.initRoutes()
	return http.ListenAndServe(s.address, s.router)
}

func (s *server) initRoutes() {
	s.router.PathPrefix("/api")
	s.router.HandleFunc("/term", errorMiddleware(term.GetList(s.termUseCase)))
	s.router.HandleFunc("/term/{id}", errorMiddleware(term.GetById(s.termUseCase)))
}
