package models

type Account struct {
	ID               int64
	Login            string
	PasswordHash     string
	Email            string
	IsEmailConfirmed bool
	Name             string
	Surname          string
	MiddleName       string
	RoleID           int64
}
