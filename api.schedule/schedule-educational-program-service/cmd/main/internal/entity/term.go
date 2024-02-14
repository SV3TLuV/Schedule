package entity

type Term struct {
	ID                 int64 `db:"id" json:"id"`
	Course             int16 `db:"course" json:"course"`
	CourseRelativeTerm int16 `db:"course_relative_term" json:"course_relative_term"`
}
