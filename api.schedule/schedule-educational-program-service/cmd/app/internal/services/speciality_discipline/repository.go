package speciality_discipline

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type PK struct {
	specialityId int64
	disciplineId int64
}

type Repository interface {
	GetAll(context.Context) (*[]model.SpecialityDiscipline, error)
	GetByID(context.Context, PK) (*model.SpecialityDiscipline, error)
	Save(context.Context, *model.SpecialityDiscipline) error
	Delete(context.Context, PK) error
}
