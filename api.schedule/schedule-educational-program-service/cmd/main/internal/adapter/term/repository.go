package term

import (
	"context"
	"schedule-educational-program-service/cmd/main/internal/entity"
)

type Repository interface {
	GetAll(context.Context) (*[]entity.Term, error)
	GetOne(context.Context, int64) (*entity.Term, error)
}
