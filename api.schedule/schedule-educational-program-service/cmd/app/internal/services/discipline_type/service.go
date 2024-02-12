package discipline_type

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/adapter/discipline_type"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type service struct {
	trManager *manager.Manager
	repo      discipline_type.Repository
}

func NewDisciplineTypeService(trManager *manager.Manager, repo discipline_type.Repository) Service {
	return &service{
		trManager: trManager,
		repo:      repo,
	}
}

func (s *service) List(ctx context.Context) (*[]model.DisciplineType, error) {
	types := &[]model.DisciplineType{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		options := discipline_type.NewGetAllOptions()
		values, err := s.repo.GetAll(ctx, options)
		types = values
		return err
	})
	return types, err
}

func (s *service) FindByID(ctx context.Context, id int64) (*model.DisciplineType, error) {
	t := &model.DisciplineType{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := s.repo.GetByID(ctx, id)
		t = value
		return err
	})
	return t, err
}

func (s *service) Save(ctx context.Context, t model.DisciplineType) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.repo.Save(ctx, &t)
	})
}

func (s *service) Remove(ctx context.Context, id int64) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.repo.Delete(ctx, id)
	})
}
