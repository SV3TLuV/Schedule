package model

import "time"

type Lesson struct {
	ID           uint64 `gorm:"primaryKey"`
	Number       uint8
	Start        time.Time
	End          time.Time
	Duration     time.Time
	TimetableID  uint64
	DisciplineID uint64
	TeacherIDs   []uint64
	ClassroomIDs []uint64
	IsChanged    bool
}
