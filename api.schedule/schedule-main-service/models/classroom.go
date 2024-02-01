package models

type Classroom struct {
	ID        uint64 `gorm:"primaryKey"`
	Cabinet   string
	IsDeleted bool `gorm:"default:false"`
}
