package entity

type Discipline struct {
	ID     int64  `db:"id" json:"id"`
	Name   string `db:"name" json:"name"`
	TypeID int64  `db:"type_id" json:"type_id"`
}
