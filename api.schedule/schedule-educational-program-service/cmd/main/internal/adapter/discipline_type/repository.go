package discipline_type

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type (
	Repository interface {
		GetAll(ctx context.Context, search string, limit, offset int64) (*[]entity.DisciplineType, error)
		GetOne(ctx context.Context, id int64) (*entity.DisciplineType, error)
		Save(ctx context.Context, e *entity.DisciplineType) error
		Delete(ctx context.Context, id int64) error
	}
)
