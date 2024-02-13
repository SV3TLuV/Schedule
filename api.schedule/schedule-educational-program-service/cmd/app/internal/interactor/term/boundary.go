package term

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/entity"
)

type Interactor interface {
	List(ctx context.Context) (*[]entity.Term, error)
	FindByID(ctx context.Context, id int64) (*entity.Term, error)
}

type Repository interface {
	GetAll(ctx context.Context) (*[]entity.Term, error)
	GetOne(ctx context.Context, id int64) (*entity.Term, error)
}
