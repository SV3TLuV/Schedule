package models

type Group struct {
	ID             uint64 `gorm:"primaryKey"`
	Name           string
	Number         uint16
	EnrollmentYear uint16
	SpecialityID   uint64
	CurrentTermID  uint8
}
