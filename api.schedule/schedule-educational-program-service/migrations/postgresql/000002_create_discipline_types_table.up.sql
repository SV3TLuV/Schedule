CREATE TABLE discipline_types (
    id serial NOT NULL PRIMARY KEY,
    name varchar(50) NOT NULL UNIQUE NULLS NOT DISTINCT
);

INSERT INTO discipline_types VALUES (1, 'Дисциплина');
INSERT INTO discipline_types VALUES (2, 'Практика');