package classroom

import (
	"Api/internal/middleware"
	"Api/internal/model"
	"database/sql"
	"errors"
	"github.com/jmoiron/sqlx"
)

type Repository interface {
	GetById(tx *sqlx.Tx, ID int64) (*model.Classroom, error)
	Get(tx *sqlx.Tx) (*[]model.Classroom, error)
	Create(tx *sqlx.Tx, c *model.Classroom) (int64, error)
	Update(tx *sqlx.Tx, c *model.Classroom) error
	Delete(tx *sqlx.Tx, ID int64) error
}

type ClassroomRepository struct {
	db *sqlx.DB
}

func NewClassroomRepository(db *sqlx.DB) *ClassroomRepository {
	return &ClassroomRepository{
		db: db,
	}
}

func (r *ClassroomRepository) Get(tx *sqlx.Tx) (*[]model.Classroom, error) {
	var classrooms []model.Classroom
	err := tx.Select(&classrooms, `SELECT * FROM "classrooms" ORDER BY "id"`)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, middleware.NotFound
	}

	if err != nil {
		return nil, err
	}

	return &classrooms, nil
}

func (r *ClassroomRepository) GetById(tx *sqlx.Tx, ID int64) (*model.Classroom, error) {
	var classroom model.Classroom
	err := tx.Get(&classroom, `SELECT * FROM "classrooms" WHERE "id" = $1`, ID)
	if errors.Is(err, sql.ErrNoRows) {
		return nil, middleware.NotFound
	}

	if err != nil {
		return nil, err
	}

	return &classroom, nil
}

func (r *ClassroomRepository) Create(tx *sqlx.Tx, c *model.Classroom) (int64, error) {
	query := `INSERT INTO "classrooms" ("cabinet") VALUES ($1)`
	res, err := tx.Exec(query, &c.Cabinet)
	if err != nil {
		return 0, err
	}

	return res.LastInsertId()
}

func (r *ClassroomRepository) Update(tx *sqlx.Tx, c *model.Classroom) error {
	query := `UPDATE "classrooms" SET "cabinet" = $1 WHERE "id" = $2`
	res, err := tx.Exec(query, &c.Cabinet, &c.ID)
	if err != nil {
		return err
	}

	count, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if count == 0 {
		return middleware.NotFound
	}

	return nil
}

func (r *ClassroomRepository) Delete(tx *sqlx.Tx, ID int64) error {
	query := `DELETE FROM "classrooms" WHERE "id" = $1`
	res, err := tx.Exec(query, ID)
	if err != nil {
		return err
	}

	count, err := res.RowsAffected()
	if err != nil {
		return err
	}

	if count == 0 {
		return middleware.NotFound
	}

	return nil
}
