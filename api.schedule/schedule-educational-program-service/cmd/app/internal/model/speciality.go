package model

type Speciality struct {
	ID        int64  `db:"id"`
	Name      string `db:"name"`
	Code      string `db:"code"`
	TermCount int64  `db:"term_count"`
}
