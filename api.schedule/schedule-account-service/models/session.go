package models

import (
	"github.com/google/uuid"
	"time"
)

type Session struct {
	ID           uuid.UUID
	AccountID    int64
	RefreshToken string
	CreatedAt    time.Time
	UpdatedAt    *time.Time
	ExpiredAt    time.Time
}
