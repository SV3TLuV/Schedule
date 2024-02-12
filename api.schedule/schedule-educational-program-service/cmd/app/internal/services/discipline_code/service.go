package discipline_code

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/adapter/discipline_code"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type service struct {
	trManager *manager.Manager
	repo      discipline_code.Repository
}

func NewDisciplineCodeService(trManager *manager.Manager, repo discipline_code.Repository) Service {
	return &service{
		trManager: trManager,
		repo:      repo,
	}
}

func (s *service) List(ctx context.Context) (*[]model.DisciplineCode, error) {
	codes := &[]model.DisciplineCode{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		options := discipline_code.NewGetAllOptions()
		values, err := s.repo.GetAll(ctx, options)
		codes = values
		return err
	})
	return codes, err
}

func (s *service) FindByID(ctx context.Context, id int64) (*model.DisciplineCode, error) {
	c := &model.DisciplineCode{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := s.repo.GetByID(ctx, id)
		c = value
		return err
	})
	return c, err
}

func (s *service) Save(ctx context.Context, c model.DisciplineCode) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.repo.Save(ctx, &c)
	})
}

func (s *service) Remove(ctx context.Context, id int64) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.repo.Delete(ctx, id)
	})
}
