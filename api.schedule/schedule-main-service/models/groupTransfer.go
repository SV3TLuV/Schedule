package models

import "time"

type GroupTransfer struct {
	ID      uint64
	GroupId uint64
	TermId  uint8
	Date    time.Time
}
