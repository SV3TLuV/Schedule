package usecases

import (
	"context"
	"schedule-educational-program-service/cmd/app/internal/model"
)

func (u *UseCase) GetTerms(ctx context.Context) (*[]model.Term, error) {
	return u.termService.List(ctx)
}

func (u *UseCase) GetTermById(ctx context.Context, id int64) (*model.Term, error) {
	return u.termService.FindByID(ctx, id)
}
