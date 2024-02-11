package model

type Term struct {
	ID                 int64 `db:"id"`
	Course             int16 `db:"course"`
	CourseRelativeTerm int16 `db:"course_relative_term"`
}
