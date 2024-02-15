package speciality

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type (
	Repository interface {
		GetAll(context.Context, *GetAllOptions) (*[]entity.Speciality, error)
		GetOne(context.Context, int64) (*entity.Speciality, error)
		Save(context.Context, *entity.Speciality) error
		Delete(context.Context, int64) error
	}

	GetAllOptions struct {
		search string
		limit  int64
		offset int64
	}

	Option func(options *GetAllOptions)
)

func NewGetAllOptions() *GetAllOptions {
	return &GetAllOptions{}
}

func (o *GetAllOptions) Search(search string) Option {
	return func(options *GetAllOptions) {
		options.search = search
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
