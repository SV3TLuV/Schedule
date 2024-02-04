package models

type Speciality struct {
	ID        uint64 `gorm:"primaryKey"`
	Name      string
	Code      string
	TermCount uint8
}
