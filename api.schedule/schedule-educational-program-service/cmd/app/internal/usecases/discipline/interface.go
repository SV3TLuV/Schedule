package discipline

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type UseCase interface {
	List(ctx context.Context) (*[]model.Discipline, error)
	FindById(ctx context.Context, id int64) (*model.Discipline, error)
}
