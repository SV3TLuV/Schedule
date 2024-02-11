package discipline_type

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Repository interface {
	GetAll(context.Context) (*[]model.DisciplineType, error)
	GetById(context.Context, int) (*model.DisciplineType, error)
	Save(context.Context, *model.DisciplineType) error
	Delete(context.Context, int) error
}
