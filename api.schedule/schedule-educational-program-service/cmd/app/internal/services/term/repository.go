package term

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Repository interface {
	GetAll(context.Context) (*[]model.Term, error)
	GetByID(context.Context, int) (*model.Term, error)
}
