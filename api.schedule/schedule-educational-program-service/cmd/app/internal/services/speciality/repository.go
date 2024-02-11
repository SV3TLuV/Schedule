package speciality

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Repository interface {
	GetAll(context.Context) (*[]model.Speciality, error)
	GetById(context.Context, int) (*model.Speciality, error)
	Save(context.Context, *model.Speciality) error
	Delete(context.Context, int) error
}
