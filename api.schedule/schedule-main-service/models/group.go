package models

type Group struct {
	ID             uint64
	Name           string
	Number         uint16
	EnrollmentYear uint16
	SpecialityId   uint64
	CurrentTermId  uint8
}
