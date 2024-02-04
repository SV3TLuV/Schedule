package model

import "time"

type GroupTransfer struct {
	ID      uint64 `gorm:"primaryKey"`
	GroupID uint64
	TermID  uint8
	Date    time.Time
}
