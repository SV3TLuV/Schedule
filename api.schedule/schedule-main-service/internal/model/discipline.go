package model

type Discipline struct {
	ID   uint64 `gorm:"primaryKey"`
	Name string
}
