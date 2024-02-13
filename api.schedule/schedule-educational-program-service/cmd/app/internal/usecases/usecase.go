package usecases

import (
	"schedule-educational-program-service/cmd/app/internal/services/term"
)

type UseCase struct {
	termService term.Service
}

func NewUseCase(termService term.Service) *UseCase {
	return &UseCase{
		termService: termService,
	}
}
