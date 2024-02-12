package discipline

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/adapter/discipline"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type service struct {
	trManager *manager.Manager
	repo      discipline.Repository
}

func NewTermService(trManager *manager.Manager, repo discipline.Repository) Service {
	return &service{
		trManager: trManager,
		repo:      repo,
	}
}

func (s *service) List(ctx context.Context) (*[]model.Discipline, error) {
	disciplines := &[]model.Discipline{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		options := discipline.NewGetAllOptions()
		values, err := s.repo.GetAll(ctx, options)
		disciplines = values
		return err
	})
	return disciplines, err
}

func (s *service) FindById(ctx context.Context, id int64) (*model.Discipline, error) {
	d := &model.Discipline{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := s.repo.GetByID(ctx, id)
		d = value
		return err
	})
	return d, err
}

func (s *service) Save(ctx context.Context, d *model.Discipline) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.repo.Save(ctx, d)
	})
}

func (s *service) Delete(ctx context.Context, id int64) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.repo.Delete(ctx, id)
	})
}
