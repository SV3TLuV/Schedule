package discipline_type

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

func (i *interactor) List(ctx context.Context) (*[]entity.DisciplineType, error) {
	types := &[]entity.DisciplineType{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		values, err := i.repo.GetAll(ctx)
		types = values
		return err
	})
	return types, err
}

func (i *interactor) FindByID(ctx context.Context, id int64) (*entity.DisciplineType, error) {
	t := &entity.DisciplineType{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := i.repo.GetOne(ctx, id)
		t = value
		return err
	})
	return t, err
}

func (i *interactor) Save(ctx context.Context, dt entity.DisciplineType) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Save(ctx, dt)
	})
}

func (i *interactor) Remove(ctx context.Context, id int64) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Delete(ctx, id)
	})
}
