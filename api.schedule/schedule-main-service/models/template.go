package models

type Template struct {
	ID              uint64
	GroupId         uint64
	WeekTypeId      uint8
	DayId           uint8
	LessonTemplates []LessonTemplate
}
