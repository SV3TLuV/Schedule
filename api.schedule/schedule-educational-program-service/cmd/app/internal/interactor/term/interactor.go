package term

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/entity"
)

type interactor struct {
	trManager *manager.Manager
	repo      Repository
}

func NewInteractor(trManager *manager.Manager, repo Repository) Interactor {
	return &interactor{
		trManager: trManager,
		repo:      repo,
	}
}

func (i *interactor) List(ctx context.Context) (*[]entity.Term, error) {
	terms := &[]entity.Term{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		values, err := i.repo.GetAll(ctx)
		terms = values
		return err
	})
	return terms, err
}

func (i *interactor) FindByID(ctx context.Context, id int64) (*entity.Term, error) {
	t := &entity.Term{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := i.repo.GetOne(ctx, id)
		t = value
		return err
	})
	return t, err
}
