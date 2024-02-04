package model

type Day struct {
	ID   uint8 `gorm:"primaryKey"`
	Name string
}
