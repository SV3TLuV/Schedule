package model

import "time"

type Timetable struct {
	ID      int64
	GroupID int64
	Date    time.Time
}
