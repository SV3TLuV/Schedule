package models

type Group struct {
	ID             uint64 `gorm:"primaryKey"`
	Number         uint16
	EnrollmentYear uint16
	SpecialityId   uint64
	CurrentTermId  uint8
	Name           string
}
