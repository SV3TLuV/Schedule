package models

import "time"

type Timetable struct {
	ID      uint64 `gorm:"primaryKey"`
	GroupId uint64
	Date    time.Time
	Lessons []Lesson
}
