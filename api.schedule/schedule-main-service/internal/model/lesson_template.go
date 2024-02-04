package model

import "time"

type LessonTemplate struct {
	ID           uint64
	Number       uint8
	Start        time.Time
	End          time.Time
	Duration     time.Time
	TemplateID   uint64
	DisciplineID uint64
	TeacherIDs   []uint64
	ClassroomIDs []uint64
}
