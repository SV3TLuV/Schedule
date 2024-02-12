package term

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/adapter/term"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type service struct {
	trManager *manager.Manager
	repo      term.Repository
}

func NewTermService(trManager *manager.Manager, repo term.Repository) Service {
	return &service{
		trManager: trManager,
		repo:      repo,
	}
}

func (s *service) List(ctx context.Context) (*[]model.Term, error) {
	terms := &[]model.Term{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		values, err := s.repo.GetAll(ctx)
		terms = values
		return err
	})
	return terms, err
}

func (s *service) FindById(ctx context.Context, id int64) (*model.Term, error) {
	t := &model.Term{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := s.repo.GetByID(ctx, id)
		t = value
		return err
	})
	return t, err
}
