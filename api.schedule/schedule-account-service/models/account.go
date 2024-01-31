package models

type Account struct {
	ID               uint `gorm:"primaryKey"`
	Login            string
	PasswordHash     string
	Email            string
	IsEmailConfirmed bool
	Name             string
	Surname          string
	MiddleName       string
	RoleId           uint
	Sessions         []Session
}
