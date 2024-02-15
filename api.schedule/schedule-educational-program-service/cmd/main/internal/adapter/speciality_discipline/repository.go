package speciality_discipline

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type (
	PK struct {
		specialityId int64
		disciplineId int64
	}

	GetAllOptions struct {
		specialityID *int64
		disciplineID *int64
		termID       *int64
		limit        int64
		offset       int64
	}

	Option func(options *GetAllOptions)
)

type Repository interface {
	GetAll(context.Context, *GetAllOptions) (*[]entity.SpecialityDiscipline, error)
	GetOne(context.Context, PK) (*entity.SpecialityDiscipline, error)
	Save(context.Context, *entity.SpecialityDiscipline) error
	Delete(context.Context, PK) error
}

func NewGetAllOptions() *GetAllOptions {
	return &GetAllOptions{}
}

func (o *GetAllOptions) SpecialityID(specialityId *int64) Option {
	return func(options *GetAllOptions) {
		options.specialityID = specialityId
	}
}

func (o *GetAllOptions) DisciplineID(disciplineId *int64) Option {
	return func(options *GetAllOptions) {
		options.disciplineID = disciplineId
	}
}

func (o *GetAllOptions) TermID(termId *int64) Option {
	return func(options *GetAllOptions) {
		options.termID = termId
	}
}

func (o *GetAllOptions) Limit(limit int64) Option {
	return func(options *GetAllOptions) {
		options.limit = limit
	}
}

func (o *GetAllOptions) Offset(offset int64) Option {
	return func(options *GetAllOptions) {
		options.offset = offset
	}
}
