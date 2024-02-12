package speciality

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type Service interface {
	List(ctx context.Context) (*[]model.Speciality, error)
	FindByID(ctx context.Context, id int64) (*model.Speciality, error)
	Save(ctx context.Context, s model.Speciality) error
	Remove(ctx context.Context, id int64) error
}
