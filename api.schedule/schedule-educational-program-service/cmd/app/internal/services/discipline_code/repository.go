package discipline_code

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Repository interface {
	GetAll(context.Context, *getAllOptions) (*[]model.DisciplineCode, error)
	GetByID(context.Context, int64) (*model.DisciplineCode, error)
	Save(context.Context, *model.DisciplineCode) error
	Delete(context.Context, int64) error
}

type getAllOptions struct {
	search string
	limit  int64
	offset int64
}
