package models

type DisciplineType struct {
	ID   uint16 `gorm:"primaryKey"`
	Name string
}
