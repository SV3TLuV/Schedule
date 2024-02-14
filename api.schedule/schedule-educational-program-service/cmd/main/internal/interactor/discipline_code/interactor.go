package discipline_code

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

func (i *interactor) List(ctx context.Context) (*[]entity.DisciplineCode, error) {
	codes := &[]entity.DisciplineCode{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		values, err := i.repo.GetAll(ctx)
		codes = values
		return err
	})
	return codes, err
}

func (i *interactor) FindByID(ctx context.Context, id int64) (*entity.DisciplineCode, error) {
	c := &entity.DisciplineCode{}
	err := i.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := i.repo.GetOne(ctx, id)
		c = value
		return err
	})
	return c, err
}

func (i *interactor) Save(ctx context.Context, dc entity.DisciplineCode) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Save(ctx, dc)
	})
}

func (i *interactor) Remove(ctx context.Context, id int64) error {
	return i.trManager.Do(ctx, func(ctx context.Context) error {
		return i.repo.Delete(ctx, id)
	})
}
