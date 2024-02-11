package model

type SpecialityDiscipline struct {
	SpecialityID     int64 `db:"speciality_id"`
	DisciplineID     int64 `db:"discipline_id"`
	DisciplineCodeID int64 `db:"discipline_code_id"`
	TotalHours       int16 `db:"total_hours"`
	TermID           int64 `db:"term_id"`
}
