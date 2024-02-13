package discipline_type

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/entity"
)

type Repository interface {
	GetAll(context.Context, *GetAllOptions) (*[]entity.DisciplineType, error)
	GetOne(context.Context, int64) (*entity.DisciplineType, error)
	Save(context.Context, *entity.DisciplineType) error
	Delete(context.Context, int64) error
}

type GetAllOptions struct {
	search string
	limit  int64
	offset int64
}

func NewGetAllOptions() *GetAllOptions {
	return &GetAllOptions{}
}

type Option func(options *GetAllOptions)

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
