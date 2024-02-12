package discipline_code

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Service interface {
	List(ctx context.Context) (*[]model.DisciplineCode, error)
	FindByID(ctx context.Context, id int64) (*model.DisciplineCode, error)
	Save(ctx context.Context, code model.DisciplineCode) error
	Remove(ctx context.Context, id int64) error
}
