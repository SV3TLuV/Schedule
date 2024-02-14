package discipline

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/main/internal/entity"
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

func (i *interactor) List(ctx context.Context) (*[]entity.Discipline, error) {
	disciplines := &[]entity.Discipline{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		values, err := i.repo.GetAll(ctx)
		disciplines = values
		return err
	})
	return disciplines, err
}

func (i *interactor) FindByID(ctx context.Context, id int64) (*entity.Discipline, error) {
	d := &entity.Discipline{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := i.repo.GetOne(ctx, id)
		d = value
		return err
	})
	return d, err
}

func (i *interactor) Save(ctx context.Context, d entity.Discipline) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Save(ctx, d)
	})
}

func (i *interactor) Remove(ctx context.Context, id int64) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Delete(ctx, id)
	})
}
