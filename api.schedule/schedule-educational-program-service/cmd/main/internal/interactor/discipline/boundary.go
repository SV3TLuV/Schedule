package discipline

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type (
	Interactor interface {
		List(ctx context.Context) (*[]entity.Discipline, error)
		FindByID(ctx context.Context, id int64) (*entity.Discipline, error)
		Save(context.Context, entity.Discipline) error
		Remove(context.Context, int64) error
	}

	Repository interface {
		GetAll(ctx context.Context) (*[]entity.Discipline, error)
		GetOne(ctx context.Context, id int64) (*entity.Discipline, error)
		Save(context.Context, entity.Discipline) error
		Delete(context.Context, int64) error
	}
)
