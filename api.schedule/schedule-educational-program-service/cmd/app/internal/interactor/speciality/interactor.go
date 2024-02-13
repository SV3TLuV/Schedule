package speciality

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

func (i *interactor) List(ctx context.Context) (*[]entity.Speciality, error) {
	specialities := &[]entity.Speciality{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		values, err := i.repo.GetAll(ctx)
		specialities = values
		return err
	})
	return specialities, err
}

func (i *interactor) FindByID(ctx context.Context, id int64) (*entity.Speciality, error) {
	sp := &entity.Speciality{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := i.repo.GetOne(ctx, id)
		sp = value
		return err
	})
	return sp, err
}

func (i *interactor) Save(ctx context.Context, sp entity.Speciality) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Save(ctx, sp)
	})
}

func (i *interactor) Remove(ctx context.Context, id int64) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Delete(ctx, id)
	})
}
