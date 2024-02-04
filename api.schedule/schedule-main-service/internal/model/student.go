package model

type Student struct {
	ID        uint64 `gorm:"primaryKey"`
	GroupID   uint64
	AccountID uint64
}
