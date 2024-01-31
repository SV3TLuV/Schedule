package models

type Account struct {
	ID           uint   `gorm:"primaryKey"`
	Login        string `gorm:"required"`
	PasswordHash string `gorm:"required"`
	Email        string `gorm:"required"`
	RoleId       uint   `gorm:"required"`
	Sessions     []Session
}
