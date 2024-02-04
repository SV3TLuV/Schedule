package model

type Teacher struct {
	ID         uint64 `gorm:"primaryKey"`
	Name       string
	Surname    string
	MiddleName string
	AccountID  uint64
}
