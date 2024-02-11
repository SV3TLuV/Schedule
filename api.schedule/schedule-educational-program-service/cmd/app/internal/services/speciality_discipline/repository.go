package speciality_discipline

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Repository interface {
	GetAll(context.Context) (*[]model.SpecialityDiscipline, error)
	GetById(context.Context, int) (*model.SpecialityDiscipline, error)
	Save(context.Context, *model.SpecialityDiscipline) error
	Delete(context.Context, int) error
}
