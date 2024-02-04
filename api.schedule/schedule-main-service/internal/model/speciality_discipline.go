package model

type SpecialityDiscipline struct {
	SpecialityID     uint64 `gorm:"primaryKey"`
	DisciplineID     uint64
	DisciplineCodeID uint
	TotalHours       uint
	TermID           uint8
}
