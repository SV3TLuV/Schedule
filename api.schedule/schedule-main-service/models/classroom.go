package models

type Classroom struct {
	ID        uint `gorm:"primaryKey"`
	Cabinet   string
	IsDeleted bool `gorm:"default:false"`
}

type Discipline struct {
	ID uint `gorm:"primaryKey"`
}
