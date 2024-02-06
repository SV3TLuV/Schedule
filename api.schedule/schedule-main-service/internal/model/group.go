package model

type Group struct {
	ID             int64
	Name           string
	Number         uint16
	EnrollmentYear uint16
	SpecialityID   int64
	CurrentTermID  int64
}
