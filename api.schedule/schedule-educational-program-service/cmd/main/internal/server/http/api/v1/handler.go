package v1

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type (
	Handler struct {
		uc UseCase
	}

	UseCase interface {
		GetTerms(ctx context.Context) (*[]entity.Term, error)
		GetTermById(ctx context.Context, id int64) (*entity.Term, error)
	}
)

func NewHandler(uc UseCase) *Handler {
	return &Handler{
		uc: uc,
	}
}
