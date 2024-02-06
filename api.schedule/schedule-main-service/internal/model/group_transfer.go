package model

import "time"

type GroupTransfer struct {
	ID      int64
	GroupID int64
	TermID  int64
	Date    time.Time
}
