package term

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Service interface {
	List(ctx context.Context) (*[]model.Term, error)
	FindById(ctx context.Context, id int64) (*model.Term, error)
}
