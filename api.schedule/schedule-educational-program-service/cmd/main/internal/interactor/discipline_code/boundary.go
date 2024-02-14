package discipline_code

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type Interactor interface {
	List(ctx context.Context) (*[]entity.DisciplineCode, error)
	FindByID(ctx context.Context, id int64) (*entity.DisciplineCode, error)
	Save(context.Context, entity.DisciplineCode) error
	Remove(context.Context, int64) error
}

type Repository interface {
	GetAll(ctx context.Context) (*[]entity.DisciplineCode, error)
	GetOne(ctx context.Context, id int64) (*entity.DisciplineCode, error)
	Save(context.Context, entity.DisciplineCode) error
	Delete(context.Context, int64) error
}
