package model

import "time"

type Timetable struct {
	ID      uint64
	GroupID uint64
	Date    time.Time
}
