package v1

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type UseCase interface {
	GetTerms(ctx context.Context) (*[]model.Term, error)
	GetTermById(ctx context.Context, id int64) (*model.Term, error)
}

type Handler struct {
	uc UseCase
}

func NewHandler(uc UseCase) *Handler {
	return &Handler{
		uc: uc,
	}
}
