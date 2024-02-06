package model

import "time"

type Lesson struct {
	ID           int64
	Number       int64
	Start        time.Time
	End          time.Time
	Duration     time.Time
	TimetableID  int64
	DisciplineID int64
	TeacherIDs   []int64
	ClassroomIDs []int64
	IsChanged    bool
}
