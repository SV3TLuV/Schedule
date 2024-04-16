package speciality

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type (
	Repository interface {
		GetAll(ctx context.Context, search string, limit, offset int64) (*[]entity.Speciality, error)
		GetOne(ctx context.Context, id int64) (*entity.Speciality, error)
		Save(ctx context.Context, e *entity.Speciality) error
		Delete(ctx context.Context, id int64) error
	}
)
