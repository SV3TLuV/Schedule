package models

type Teacher struct {
	ID         uint64 `gorm:"primaryKey"`
	Name       string
	Surname    string
	MiddleName string
	AccountId  uint64
}
