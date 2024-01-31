package models

type Student struct {
	ID        uint64 `gorm:"primaryKey"`
	GroupId   uint64
	AccountId uint64
}
