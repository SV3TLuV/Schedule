package models

type Term struct {
	ID                 uint8 `gorm:"primaryKey"`
	Course             uint8
	CourseRelativeTerm uint8
}
