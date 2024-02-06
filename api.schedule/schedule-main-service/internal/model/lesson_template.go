package model

import "time"

type LessonTemplate struct {
	ID           int64
	Number       int64
	Start        time.Time
	End          time.Time
	Duration     time.Time
	TemplateID   int64
	DisciplineID int64
	TeacherIDs   []int64
	ClassroomIDs []int64
}
