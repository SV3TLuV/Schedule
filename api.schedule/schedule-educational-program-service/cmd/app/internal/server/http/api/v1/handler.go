package v1

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/entity"
)

type UseCase interface {
	GetTerms(ctx context.Context) (*[]entity.Term, error)
	GetTermById(ctx context.Context, id int64) (*entity.Term, error)
}

type Handler struct {
	uc UseCase
}

func NewHandler(uc UseCase) *Handler {
	return &Handler{
		uc: uc,
	}
}
