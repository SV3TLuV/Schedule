package models

import "time"

type Timetable struct {
	ID      uint64
	GroupId uint64
	Date    time.Time
	Lessons []Lesson
}
