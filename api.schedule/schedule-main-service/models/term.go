package models

type Term struct {
	ID         uint `gorm:"primaryKey"`
	CourseId   uint `gorm:"required"`
	CourseTerm int8 `gorm:"required"`
	Course     Course
}
