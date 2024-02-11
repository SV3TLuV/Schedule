package discipline

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Repository interface {
	GetAll(context.Context) (*[]model.Discipline, error)
	GetByID(context.Context, int64) (*model.Discipline, error)
	Save(context.Context, *model.Discipline) error
	Delete(context.Context, int64) error
}
