package models

type Template struct {
	ID         uint64 `gorm:"primaryKey"`
	GroupID    uint64
	WeekTypeID uint8
	DayID      uint8
	TermID     uint8
}
