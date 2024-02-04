package model

type DisciplineCode struct {
	ID   uint64 `gorm:"primaryKey"`
	Code string
}
