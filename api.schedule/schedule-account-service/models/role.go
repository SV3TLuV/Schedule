package models

type Role struct {
	ID       uint   `gorm:"primaryKey"`
	Name     string `gorm:"required"`
	Accounts []Account
}
