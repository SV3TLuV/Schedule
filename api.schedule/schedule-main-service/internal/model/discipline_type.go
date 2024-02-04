package model

type DisciplineType struct {
	ID   uint16 `gorm:"primaryKey"`
	Name string
}
