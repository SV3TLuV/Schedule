package speciality_discipline

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Key struct {
	specialityId int64
	disciplineId int64
}

type Repository interface {
	GetAll(context.Context) (*[]model.SpecialityDiscipline, error)
	GetByID(context.Context, Key) (*model.SpecialityDiscipline, error)
	Save(context.Context, *model.SpecialityDiscipline) error
	Delete(context.Context, Key) error
}
