CREATE TABLE discipline_codes (
    id integer NOT NULL PRIMARY KEY,
    name varchar(50) NOT NULL UNIQUE NULLS NOT DISTINCT
);