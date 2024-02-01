package models

type SpecialityDiscipline struct {
	SpecialityID     uint64 `gorm:"primaryKey"`
	DisciplineID     uint64 `gorm:"primaryKey"`
	DisciplineCodeId uint
	TotalHours       uint
	TermId           uint8
}
