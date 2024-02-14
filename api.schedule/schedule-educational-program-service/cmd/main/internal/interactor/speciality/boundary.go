package speciality

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type Interactor interface {
	List(ctx context.Context) (*[]entity.Speciality, error)
	FindByID(ctx context.Context, id int64) (*entity.Speciality, error)
	Save(context.Context, entity.Speciality) error
	Remove(context.Context, int64) error
}

type Repository interface {
	GetAll(ctx context.Context) (*[]entity.Speciality, error)
	GetOne(ctx context.Context, id int64) (*entity.Speciality, error)
	Save(context.Context, entity.Speciality) error
	Delete(context.Context, int64) error
}
