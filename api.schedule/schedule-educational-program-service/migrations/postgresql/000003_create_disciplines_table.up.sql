CREATE TABLE disciplines (
    id serial NOT NULL PRIMARY KEY,
    name varchar(50) NOT NULL UNIQUE NULLS NOT DISTINCT,
    type_id integer NOT NULL DEFAULT (1) REFERENCES discipline_types (id)
);