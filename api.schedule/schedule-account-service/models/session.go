package models

import (
	"github.com/google/uuid"
	"time"
)

type Session struct {
	ID           uuid.UUID `gorm:"primaryKey;type:uuid;default:uuid_generate_v4()"`
	AccountId    uint64    `gorm:"required"`
	RefreshToken string    `gorm:"required"`
	CreatedAt    time.Time
	UpdatedAt    *time.Time
}
