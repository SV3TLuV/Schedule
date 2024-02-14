package discipline_type

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type Interactor interface {
	List(ctx context.Context) (*[]entity.DisciplineType, error)
	FindByID(ctx context.Context, id int64) (*entity.DisciplineType, error)
	Save(context.Context, entity.DisciplineType) error
	Remove(context.Context, int64) error
}

type Repository interface {
	GetAll(ctx context.Context) (*[]entity.DisciplineType, error)
	GetOne(ctx context.Context, id int64) (*entity.DisciplineType, error)
	Save(context.Context, entity.DisciplineType) error
	Delete(context.Context, int64) error
}
