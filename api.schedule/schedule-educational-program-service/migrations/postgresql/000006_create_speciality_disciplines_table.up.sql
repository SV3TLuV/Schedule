CREATE TABLE speciality_disciplines (
    speciality_id integer NOT NULL REFERENCES specialities (id),
    discipline_id integer NOT NULL REFERENCES disciplines (id),
    discipline_code_id integer NOT NULL REFERENCES discipline_codes (id),
    total_hours smallint NOT NULL,
    term_id integer NOT NULL REFERENCES terms (id),
    PRIMARY KEY (speciality_id, discipline_id),
    UNIQUE (speciality_id, discipline_id, discipline_code_id)
);