package models

import (
	"github.com/google/uuid"
	"time"
)

type Session struct {
	ID           uuid.UUID `gorm:"primaryKey;type:uuid;default:uuid_generate_v4()"`
	AccountId    uint64
	RefreshToken string
	CreatedAt    time.Time
	UpdatedAt    *time.Time
}
