package model

import "time"

type Timetable struct {
	ID      uint64 `gorm:"primaryKey"`
	GroupID uint64
	Date    time.Time
}
