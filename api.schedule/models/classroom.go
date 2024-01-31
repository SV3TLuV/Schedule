package models

type Classroom struct {
	ID        uint   `gorm:"primaryKey"`
	Cabinet   string `gorm:"required"`
	IsDeleted bool
}
