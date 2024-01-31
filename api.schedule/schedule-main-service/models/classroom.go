package models

type Classroom struct {
	ID        uint   `gorm:"primaryKey"`
	Cabinet   string `gorm:"required"`
	IsDeleted bool
}

type Lesson struct {
	ID     uint64 `gorm:"primaryKey"`
	Number int8   `gorm:"required"`
}
