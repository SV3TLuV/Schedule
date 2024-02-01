package models

import "time"

type Lesson struct {
	ID           uint64 `gorm:"primaryKey"`
	Number       uint8
	Start        time.Time
	End          time.Time
	Duration     time.Time
	TeacherId    uint64
	DisciplineId uint64
	IsChanged    bool `gorm:"default:false"`
}
