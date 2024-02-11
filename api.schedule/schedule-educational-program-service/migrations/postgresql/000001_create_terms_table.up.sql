CREATE TABLE terms (
    id serial NOT NULL PRIMARY KEY,
    course smallint NOT NULL CHECK (course >= 1 and course <= 5),
    course_relative_term smallint NOT NULL CHECK (course_relative_term >= 1 and course_relative_term <= 2),
    UNIQUE (course, course_relative_term)
);

INSERT INTO terms VALUES (1, 1, 1);
INSERT INTO terms VALUES (2, 1, 2);
INSERT INTO terms VALUES (3, 2, 1);
INSERT INTO terms VALUES (4, 2, 2);
INSERT INTO terms VALUES (5, 3, 1);
INSERT INTO terms VALUES (6, 3, 2);
INSERT INTO terms VALUES (7, 4, 1);
INSERT INTO terms VALUES (8, 4, 2);