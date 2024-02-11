package model

type Discipline struct {
	ID     int64  `db:"id"`
	Name   string `db:"name"`
	TypeID int64  `db:"type_id"`
}
