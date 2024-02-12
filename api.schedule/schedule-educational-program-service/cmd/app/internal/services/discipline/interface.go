package discipline

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Service interface {
	List(ctx context.Context) (*[]model.Discipline, error)
	FindById(ctx context.Context, id int64) (*model.Discipline, error)
	Save(ctx context.Context, discipline *model.Discipline) error
	Delete(ctx context.Context, id int64) error
}
