package discipline_code

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Repository interface {
	GetAll(context.Context, *GetAllOptions) (*[]model.DisciplineCode, error)
	GetByID(context.Context, int64) (*model.DisciplineCode, error)
	Save(context.Context, *model.DisciplineCode) error
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
