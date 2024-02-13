package entity

type Speciality struct {
	ID        int64  `db:"id" json:"id"`
	Name      string `db:"name" json:"name"`
	Code      string `db:"code" json:"code"`
	TermCount int64  `db:"term_count" json:"term_count"`
}
