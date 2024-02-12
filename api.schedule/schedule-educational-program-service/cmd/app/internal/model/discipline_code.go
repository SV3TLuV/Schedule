package model

type DisciplineCode struct {
	ID   int64  `db:"id" json:"id"`
	Code string `db:"code" json:"code"`
}
