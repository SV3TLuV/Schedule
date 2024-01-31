package models

type Course struct {
	ID    uint `gorm:"primaryKey"`
	Terms []Term
}
