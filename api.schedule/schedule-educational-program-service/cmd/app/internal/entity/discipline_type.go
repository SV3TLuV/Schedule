package entity

type DisciplineType struct {
	ID          int64  `db:"id" json:"id"`
	Name        string `db:"name" json:"name"`
	IsDeletable bool   `db:"is_deletable" json:"is_deletable"`
}
