package discipline_type

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Service interface {
	List(ctx context.Context) (*[]model.DisciplineType, error)
	FindByID(ctx context.Context, id int64) (*model.DisciplineType, error)
	Save(ctx context.Context, t model.DisciplineType) error
	Remove(ctx context.Context, id int64) error
}
