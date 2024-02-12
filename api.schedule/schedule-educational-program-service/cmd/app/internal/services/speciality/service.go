package speciality

import (
	"context"
	"github.com/avito-tech/go-transaction-manager/trm/v2/manager"
	"schedule-educational-program-service/cmd/app/internal/adapter/speciality"
	"schedule-educational-program-service/cmd/app/internal/adapter/speciality_discipline"
	"schedule-educational-program-service/cmd/app/internal/model"
)

type service struct {
	trManager *manager.Manager
	sRepo     speciality.Repository
	sdRepo    speciality_discipline.Repository
}

func NewSpecialityService(trManager *manager.Manager,
	sRepo speciality.Repository,
	sdRepo speciality_discipline.Repository) Service {
	return &service{
		trManager: trManager,
		sRepo:     sRepo,
		sdRepo:    sdRepo,
	}
}

func (s *service) List(ctx context.Context) (*[]model.Speciality, error) {
	specialities := &[]model.Speciality{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		options := speciality.NewGetAllOptions()
		values, err := s.sRepo.GetAll(ctx, options)
		specialities = values
		return err
	})
	return specialities, err
}

func (s *service) FindByID(ctx context.Context, id int64) (*model.Speciality, error) {
	sp := &model.Speciality{}
	err := s.trManager.Do(ctx, func(ctx context.Context) error {
		value, err := s.sRepo.GetByID(ctx, id)
		sp = value
		return err
	})
	return sp, err
}

func (s *service) Save(ctx context.Context, sp model.Speciality) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.sRepo.Save(ctx, &sp)
	})
}

func (s *service) Remove(ctx context.Context, id int64) error {
	return s.trManager.Do(ctx, func(ctx context.Context) error {
		return s.sRepo.Delete(ctx, id)
	})
}
