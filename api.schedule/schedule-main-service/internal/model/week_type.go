package model

type WeekType struct {
	ID   uint8 `gorm:"primaryKey"`
	Name string
}
