package http

import (
	"github.com/gorilla/mux"
	"net/http"
	"net/http/pprof"
	"schedule-educational-program-service/pkg/middleware"
)

type HandlerRouter interface {
	AddRoutes(r *mux.Router)
	GetVersion() string
	GetContentType() string
}

type Router struct {
	router *mux.Router
}

func NewRouter() *Router {
	return &Router{router: mux.NewRouter()}
}

func (r *Router) WithSwagger() *Router {
	fs := http.FileServer(http.Dir("./swagger/"))
	r.router.PathPrefix("/swagger/").Handler(http.StripPrefix("/swagger/", fs))
	return r
}

func (r *Router) WithHandler(h HandlerRouter) *Router {
	api := r.router.PathPrefix("/api/" + h.GetVersion()).Subrouter()

	api.Use(middleware.ErrorMiddleware())

	h.AddRoutes(api)

	return r
}

func (r *Router) WithProfiler() *Router {
	r.router.HandleFunc("/debug/pprof/", pprof.Index)
	r.router.HandleFunc("/debug/pprof/profile", pprof.Profile)
	r.router.HandleFunc("/debug/pprof/symbol", pprof.Symbol)
	r.router.HandleFunc("/debug/pprof/trace", pprof.Trace)

	r.router.Handle("/debug/pprof/goroutine", pprof.Handler("goroutine"))
	r.router.Handle("/debug/pprof/heap", pprof.Handler("heap"))
	r.router.Handle("/debug/pprof/threadcreate", pprof.Handler("threadcreate"))
	r.router.Handle("/debug/pprof/block", pprof.Handler("block"))
	r.router.Handle("/debug/pprof/allocs", pprof.Handler("allocs"))
	r.router.Handle("/debug/pprof/mutex", pprof.Handler("mutex"))

	return r
}
