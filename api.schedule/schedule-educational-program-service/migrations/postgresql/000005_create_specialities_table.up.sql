CREATE TABLE specialities (
    id integer NOT NULL PRIMARY KEY,
    name varchar(50) NOT NULL,
    code varchar(50) NOT NULL,
    term_count integer NOT NULL REFERENCES terms (id),
    UNIQUE (name, code)
);