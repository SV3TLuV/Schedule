CREATE TABLE discipline_types (
    id serial NOT NULL PRIMARY KEY,
    name varchar(50) NOT NULL UNIQUE NULLS NOT DISTINCT,
    is_deletable bool NOT NULL DEFAULT(true)
);

INSERT INTO discipline_types VALUES (1, 'Дисциплина', false);
INSERT INTO discipline_types VALUES (2, 'Практика', false);