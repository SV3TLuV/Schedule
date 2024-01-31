package models

type Classroom struct {
	ID        uint   `gorm:"primaryKey"`
	Cabinet   string `gorm:"required"`
	IsDeleted bool
}

type Course struct {
	ID    uint `gorm:"primaryKey"`
	Terms []Term
}

type Term struct {
	ID         uint `gorm:"primaryKey"`
	CourseId   uint `gorm:"required"`
	CourseTerm int8 `gorm:"required"`
}
