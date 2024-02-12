package discipline

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/model"
	"schedule-educational-program-service/cmd/app/internal/repository/discipline"
)

type useCase struct {
	repo      discipline.Repository
	trManager *manager.Manager
}

func NewDisciplineUseCase(repo discipline.Repository, trManager *manager.Manager) UseCase {
	return &useCase{
		repo:      repo,
		trManager: trManager,
	}
}

func (u *useCase) List(ctx context.Context) (*[]model.Discipline, error) {
	disciplines := &[]model.Discipline{}
	err := u.trManager.Do(ctx, func(ctx context.Context) error {
		options := discipline.NewGetAllOptions()
		values, err := u.repo.GetAll(ctx, options)
		disciplines = values
		return err
	})
	return disciplines, err
}

func (u *useCase) FindById(ctx context.Context, id int64) (*model.Discipline, error) {
	d := &model.Discipline{}
	err := u.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := u.repo.GetByID(ctx, id)
		d = value
		return err
	})
	return d, err
}
