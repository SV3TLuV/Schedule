package model

import "time"

type GroupTransfer struct {
	ID      uint64
	GroupID uint64
	TermID  uint8
	Date    time.Time
}
