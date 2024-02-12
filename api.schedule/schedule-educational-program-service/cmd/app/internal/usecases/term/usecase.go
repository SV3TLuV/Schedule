package term

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/model"
	"schedule-educational-program-service/cmd/app/internal/repository/term"
)

type useCase struct {
	trManager *manager.Manager
	repo      term.Repository
}

func NewTermUseCase(repo term.Repository, trManager *manager.Manager) UseCase {
	return &useCase{
		repo:      repo,
		trManager: trManager,
	}
}

func (u *useCase) List(ctx context.Context) (*[]model.Term, error) {
	terms := &[]model.Term{}
	err := u.trManager.Do(ctx, func(ctx context.Context) error {
		values, err := u.repo.GetAll(ctx)
		terms = values
		return err
	})
	return terms, err
}

func (u *useCase) FindById(ctx context.Context, id int64) (*model.Term, error) {
	t := &model.Term{}
	err := u.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := u.repo.GetByID(ctx, id)
		t = value
		return err
	})
	return t, err
}
