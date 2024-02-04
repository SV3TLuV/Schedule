--
-- PostgreSQL database dump
--

-- Dumped from database version 16.1 (Debian 16.1-1.pgdg120+1)
-- Dumped by pg_dump version 16.0

-- Started on 2024-02-03 22:50:03

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 3566 (class 1262 OID 24581)
-- Name: schedule; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE schedule WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE schedule OWNER TO postgres;

\connect schedule

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 24583)
-- Name: classrooms; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.classrooms (
    id bigint NOT NULL,
    cabinet text NOT NULL
);


ALTER TABLE public.classrooms OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 24582)
-- Name: classrooms_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.classrooms_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.classrooms_id_seq OWNER TO postgres;

--
-- TOC entry 3567 (class 0 OID 0)
-- Dependencies: 215
-- Name: classrooms_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.classrooms_id_seq OWNED BY public.classrooms.id;


--
-- TOC entry 218 (class 1259 OID 24592)
-- Name: days; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.days (
    id smallint NOT NULL,
    name text NOT NULL
);


ALTER TABLE public.days OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 24591)
-- Name: days_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.days_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.days_id_seq OWNER TO postgres;

--
-- TOC entry 3568 (class 0 OID 0)
-- Dependencies: 217
-- Name: days_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.days_id_seq OWNED BY public.days.id;


--
-- TOC entry 222 (class 1259 OID 24610)
-- Name: discipline_codes; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discipline_codes (
    id bigint NOT NULL,
    code text NOT NULL
);


ALTER TABLE public.discipline_codes OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 24609)
-- Name: discipline_codes_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.discipline_codes_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.discipline_codes_id_seq OWNER TO postgres;

--
-- TOC entry 3569 (class 0 OID 0)
-- Dependencies: 221
-- Name: discipline_codes_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.discipline_codes_id_seq OWNED BY public.discipline_codes.id;


--
-- TOC entry 224 (class 1259 OID 24619)
-- Name: discipline_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discipline_types (
    id integer NOT NULL,
    name text NOT NULL
);


ALTER TABLE public.discipline_types OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 24618)
-- Name: discipline_types_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.discipline_types_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.discipline_types_id_seq OWNER TO postgres;

--
-- TOC entry 3570 (class 0 OID 0)
-- Dependencies: 223
-- Name: discipline_types_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.discipline_types_id_seq OWNED BY public.discipline_types.id;


--
-- TOC entry 220 (class 1259 OID 24601)
-- Name: disciplines; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.disciplines (
    id bigint NOT NULL,
    name text NOT NULL
);


ALTER TABLE public.disciplines OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 24600)
-- Name: disciplines_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.disciplines_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.disciplines_id_seq OWNER TO postgres;

--
-- TOC entry 3571 (class 0 OID 0)
-- Dependencies: 219
-- Name: disciplines_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.disciplines_id_seq OWNED BY public.disciplines.id;


--
-- TOC entry 228 (class 1259 OID 24637)
-- Name: group_transfers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.group_transfers (
    id bigint NOT NULL,
    group_id bigint NOT NULL,
    term_id smallint NOT NULL,
    date date NOT NULL
);


ALTER TABLE public.group_transfers OWNER TO postgres;

--
-- TOC entry 227 (class 1259 OID 24636)
-- Name: group_transfers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.group_transfers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.group_transfers_id_seq OWNER TO postgres;

--
-- TOC entry 3572 (class 0 OID 0)
-- Dependencies: 227
-- Name: group_transfers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.group_transfers_id_seq OWNED BY public.group_transfers.id;


--
-- TOC entry 226 (class 1259 OID 24628)
-- Name: groups; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.groups (
    id bigint NOT NULL,
    name text NOT NULL,
    number integer NOT NULL,
    enrollment_year integer NOT NULL,
    speciality_id bigint NOT NULL,
    current_term_id smallint NOT NULL
);


ALTER TABLE public.groups OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 24627)
-- Name: groups_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.groups_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.groups_id_seq OWNER TO postgres;

--
-- TOC entry 3573 (class 0 OID 0)
-- Dependencies: 225
-- Name: groups_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.groups_id_seq OWNED BY public.groups.id;


--
-- TOC entry 248 (class 1259 OID 24714)
-- Name: lesson_templates; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.lesson_templates (
    id bigint NOT NULL,
    number smallint NOT NULL,
    start timestamp with time zone,
    "end" timestamp with time zone,
    duration timestamp with time zone,
    template_id bigint NOT NULL,
    discipline_id bigint,
    teacher_ids bigint[],
    classroom_ids bigint[]
);


ALTER TABLE public.lesson_templates OWNER TO postgres;

--
-- TOC entry 247 (class 1259 OID 24713)
-- Name: lesson_templates_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.lesson_templates_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.lesson_templates_id_seq OWNER TO postgres;

--
-- TOC entry 3574 (class 0 OID 0)
-- Dependencies: 247
-- Name: lesson_templates_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.lesson_templates_id_seq OWNED BY public.lesson_templates.id;


--
-- TOC entry 246 (class 1259 OID 24707)
-- Name: lessons; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.lessons (
    id bigint NOT NULL,
    number smallint NOT NULL,
    start timestamp with time zone,
    "end" timestamp with time zone,
    duration timestamp with time zone,
    timetable_id bigint NOT NULL,
    discipline_id bigint,
    teacher_ids bigint[],
    classroom_ids bigint[],
    is_changed boolean DEFAULT false NOT NULL
);


ALTER TABLE public.lessons OWNER TO postgres;

--
-- TOC entry 245 (class 1259 OID 24706)
-- Name: lessons_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.lessons_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.lessons_id_seq OWNER TO postgres;

--
-- TOC entry 3575 (class 0 OID 0)
-- Dependencies: 245
-- Name: lessons_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.lessons_id_seq OWNED BY public.lessons.id;


--
-- TOC entry 230 (class 1259 OID 24644)
-- Name: specialities; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.specialities (
    id bigint NOT NULL,
    name text NOT NULL,
    code text NOT NULL,
    term_count smallint NOT NULL
);


ALTER TABLE public.specialities OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 24643)
-- Name: specialities_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.specialities_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.specialities_id_seq OWNER TO postgres;

--
-- TOC entry 3576 (class 0 OID 0)
-- Dependencies: 229
-- Name: specialities_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.specialities_id_seq OWNED BY public.specialities.id;


--
-- TOC entry 232 (class 1259 OID 24653)
-- Name: speciality_disciplines; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.speciality_disciplines (
    speciality_id bigint NOT NULL,
    discipline_id bigint NOT NULL,
    discipline_code_id bigint NOT NULL,
    total_hours bigint NOT NULL,
    term_id smallint NOT NULL
);


ALTER TABLE public.speciality_disciplines OWNER TO postgres;

--
-- TOC entry 231 (class 1259 OID 24652)
-- Name: speciality_disciplines_speciality_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.speciality_disciplines_speciality_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.speciality_disciplines_speciality_id_seq OWNER TO postgres;

--
-- TOC entry 3577 (class 0 OID 0)
-- Dependencies: 231
-- Name: speciality_disciplines_speciality_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.speciality_disciplines_speciality_id_seq OWNED BY public.speciality_disciplines.speciality_id;


--
-- TOC entry 234 (class 1259 OID 24660)
-- Name: students; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.students (
    id bigint NOT NULL,
    group_id bigint NOT NULL,
    account_id bigint NOT NULL
);


ALTER TABLE public.students OWNER TO postgres;

--
-- TOC entry 233 (class 1259 OID 24659)
-- Name: students_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.students_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.students_id_seq OWNER TO postgres;

--
-- TOC entry 3578 (class 0 OID 0)
-- Dependencies: 233
-- Name: students_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.students_id_seq OWNED BY public.students.id;


--
-- TOC entry 236 (class 1259 OID 24667)
-- Name: teachers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.teachers (
    id bigint NOT NULL,
    name text NOT NULL,
    surname text NOT NULL,
    middle_name text NOT NULL,
    account_id bigint NOT NULL
);


ALTER TABLE public.teachers OWNER TO postgres;

--
-- TOC entry 235 (class 1259 OID 24666)
-- Name: teachers_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.teachers_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.teachers_id_seq OWNER TO postgres;

--
-- TOC entry 3579 (class 0 OID 0)
-- Dependencies: 235
-- Name: teachers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.teachers_id_seq OWNED BY public.teachers.id;


--
-- TOC entry 238 (class 1259 OID 24676)
-- Name: templates; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.templates (
    id bigint NOT NULL,
    group_id bigint NOT NULL,
    week_type_id smallint NOT NULL,
    day_id smallint NOT NULL,
    term_id smallint NOT NULL
);


ALTER TABLE public.templates OWNER TO postgres;

--
-- TOC entry 237 (class 1259 OID 24675)
-- Name: templates_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.templates_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.templates_id_seq OWNER TO postgres;

--
-- TOC entry 3580 (class 0 OID 0)
-- Dependencies: 237
-- Name: templates_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.templates_id_seq OWNED BY public.templates.id;


--
-- TOC entry 240 (class 1259 OID 24683)
-- Name: terms; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.terms (
    id smallint NOT NULL,
    course smallint NOT NULL,
    course_relative_term smallint NOT NULL
);


ALTER TABLE public.terms OWNER TO postgres;

--
-- TOC entry 239 (class 1259 OID 24682)
-- Name: terms_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.terms_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.terms_id_seq OWNER TO postgres;

--
-- TOC entry 3581 (class 0 OID 0)
-- Dependencies: 239
-- Name: terms_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.terms_id_seq OWNED BY public.terms.id;


--
-- TOC entry 242 (class 1259 OID 24690)
-- Name: timetables; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.timetables (
    id bigint NOT NULL,
    group_id bigint NOT NULL,
    date date NOT NULL
);


ALTER TABLE public.timetables OWNER TO postgres;

--
-- TOC entry 241 (class 1259 OID 24689)
-- Name: timetables_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.timetables_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.timetables_id_seq OWNER TO postgres;

--
-- TOC entry 3582 (class 0 OID 0)
-- Dependencies: 241
-- Name: timetables_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.timetables_id_seq OWNED BY public.timetables.id;


--
-- TOC entry 244 (class 1259 OID 24697)
-- Name: week_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.week_types (
    id smallint NOT NULL,
    name text NOT NULL
);


ALTER TABLE public.week_types OWNER TO postgres;

--
-- TOC entry 243 (class 1259 OID 24696)
-- Name: week_types_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.week_types_id_seq
    AS smallint
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.week_types_id_seq OWNER TO postgres;

--
-- TOC entry 3583 (class 0 OID 0)
-- Dependencies: 243
-- Name: week_types_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.week_types_id_seq OWNED BY public.week_types.id;


--
-- TOC entry 3283 (class 2604 OID 24586)
-- Name: classrooms id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.classrooms ALTER COLUMN id SET DEFAULT nextval('public.classrooms_id_seq'::regclass);


--
-- TOC entry 3284 (class 2604 OID 24595)
-- Name: days id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.days ALTER COLUMN id SET DEFAULT nextval('public.days_id_seq'::regclass);


--
-- TOC entry 3286 (class 2604 OID 24613)
-- Name: discipline_codes id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_codes ALTER COLUMN id SET DEFAULT nextval('public.discipline_codes_id_seq'::regclass);


--
-- TOC entry 3287 (class 2604 OID 24622)
-- Name: discipline_types id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_types ALTER COLUMN id SET DEFAULT nextval('public.discipline_types_id_seq'::regclass);


--
-- TOC entry 3285 (class 2604 OID 24604)
-- Name: disciplines id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.disciplines ALTER COLUMN id SET DEFAULT nextval('public.disciplines_id_seq'::regclass);


--
-- TOC entry 3289 (class 2604 OID 24640)
-- Name: group_transfers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_transfers ALTER COLUMN id SET DEFAULT nextval('public.group_transfers_id_seq'::regclass);


--
-- TOC entry 3288 (class 2604 OID 24631)
-- Name: groups id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.groups ALTER COLUMN id SET DEFAULT nextval('public.groups_id_seq'::regclass);


--
-- TOC entry 3300 (class 2604 OID 24717)
-- Name: lesson_templates id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_templates ALTER COLUMN id SET DEFAULT nextval('public.lesson_templates_id_seq'::regclass);


--
-- TOC entry 3298 (class 2604 OID 24710)
-- Name: lessons id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lessons ALTER COLUMN id SET DEFAULT nextval('public.lessons_id_seq'::regclass);


--
-- TOC entry 3290 (class 2604 OID 24647)
-- Name: specialities id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specialities ALTER COLUMN id SET DEFAULT nextval('public.specialities_id_seq'::regclass);


--
-- TOC entry 3291 (class 2604 OID 24656)
-- Name: speciality_disciplines speciality_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality_disciplines ALTER COLUMN speciality_id SET DEFAULT nextval('public.speciality_disciplines_speciality_id_seq'::regclass);


--
-- TOC entry 3292 (class 2604 OID 24663)
-- Name: students id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students ALTER COLUMN id SET DEFAULT nextval('public.students_id_seq'::regclass);


--
-- TOC entry 3293 (class 2604 OID 24670)
-- Name: teachers id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.teachers ALTER COLUMN id SET DEFAULT nextval('public.teachers_id_seq'::regclass);


--
-- TOC entry 3294 (class 2604 OID 24679)
-- Name: templates id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.templates ALTER COLUMN id SET DEFAULT nextval('public.templates_id_seq'::regclass);


--
-- TOC entry 3295 (class 2604 OID 24686)
-- Name: terms id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.terms ALTER COLUMN id SET DEFAULT nextval('public.terms_id_seq'::regclass);


--
-- TOC entry 3296 (class 2604 OID 24693)
-- Name: timetables id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetables ALTER COLUMN id SET DEFAULT nextval('public.timetables_id_seq'::regclass);


--
-- TOC entry 3297 (class 2604 OID 24700)
-- Name: week_types id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.week_types ALTER COLUMN id SET DEFAULT nextval('public.week_types_id_seq'::regclass);


--
-- TOC entry 3528 (class 0 OID 24583)
-- Dependencies: 216
-- Data for Name: classrooms; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3530 (class 0 OID 24592)
-- Dependencies: 218
-- Data for Name: days; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.days VALUES (7, 'Воскресенье');
INSERT INTO public.days VALUES (6, 'Суббота');
INSERT INTO public.days VALUES (5, 'Пятница');
INSERT INTO public.days VALUES (4, 'Четверг');
INSERT INTO public.days VALUES (3, 'Среда');
INSERT INTO public.days VALUES (2, 'Вторник');
INSERT INTO public.days VALUES (1, 'Понедельник');


--
-- TOC entry 3534 (class 0 OID 24610)
-- Dependencies: 222
-- Data for Name: discipline_codes; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3536 (class 0 OID 24619)
-- Dependencies: 224
-- Data for Name: discipline_types; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3532 (class 0 OID 24601)
-- Dependencies: 220
-- Data for Name: disciplines; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3540 (class 0 OID 24637)
-- Dependencies: 228
-- Data for Name: group_transfers; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3538 (class 0 OID 24628)
-- Dependencies: 226
-- Data for Name: groups; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3560 (class 0 OID 24714)
-- Dependencies: 248
-- Data for Name: lesson_templates; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3558 (class 0 OID 24707)
-- Dependencies: 246
-- Data for Name: lessons; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3542 (class 0 OID 24644)
-- Dependencies: 230
-- Data for Name: specialities; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3544 (class 0 OID 24653)
-- Dependencies: 232
-- Data for Name: speciality_disciplines; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3546 (class 0 OID 24660)
-- Dependencies: 234
-- Data for Name: students; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3548 (class 0 OID 24667)
-- Dependencies: 236
-- Data for Name: teachers; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3550 (class 0 OID 24676)
-- Dependencies: 238
-- Data for Name: templates; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3552 (class 0 OID 24683)
-- Dependencies: 240
-- Data for Name: terms; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.terms VALUES (8, 4, 2);
INSERT INTO public.terms VALUES (7, 4, 1);
INSERT INTO public.terms VALUES (6, 3, 2);
INSERT INTO public.terms VALUES (5, 3, 1);
INSERT INTO public.terms VALUES (4, 2, 2);
INSERT INTO public.terms VALUES (3, 2, 1);
INSERT INTO public.terms VALUES (2, 1, 2);
INSERT INTO public.terms VALUES (1, 1, 1);


--
-- TOC entry 3554 (class 0 OID 24690)
-- Dependencies: 242
-- Data for Name: timetables; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3556 (class 0 OID 24697)
-- Dependencies: 244
-- Data for Name: week_types; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.week_types VALUES (1, 'Нечётная');
INSERT INTO public.week_types VALUES (2, 'Чётная');


--
-- TOC entry 3584 (class 0 OID 0)
-- Dependencies: 215
-- Name: classrooms_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.classrooms_id_seq', 1, false);


--
-- TOC entry 3585 (class 0 OID 0)
-- Dependencies: 217
-- Name: days_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.days_id_seq', 1, false);


--
-- TOC entry 3586 (class 0 OID 0)
-- Dependencies: 221
-- Name: discipline_codes_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discipline_codes_id_seq', 1, false);


--
-- TOC entry 3587 (class 0 OID 0)
-- Dependencies: 223
-- Name: discipline_types_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discipline_types_id_seq', 1, false);


--
-- TOC entry 3588 (class 0 OID 0)
-- Dependencies: 219
-- Name: disciplines_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.disciplines_id_seq', 1, false);


--
-- TOC entry 3589 (class 0 OID 0)
-- Dependencies: 227
-- Name: group_transfers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.group_transfers_id_seq', 1, false);


--
-- TOC entry 3590 (class 0 OID 0)
-- Dependencies: 225
-- Name: groups_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.groups_id_seq', 1, false);


--
-- TOC entry 3591 (class 0 OID 0)
-- Dependencies: 247
-- Name: lesson_templates_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.lesson_templates_id_seq', 1, false);


--
-- TOC entry 3592 (class 0 OID 0)
-- Dependencies: 245
-- Name: lessons_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.lessons_id_seq', 1, false);


--
-- TOC entry 3593 (class 0 OID 0)
-- Dependencies: 229
-- Name: specialities_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.specialities_id_seq', 1, false);


--
-- TOC entry 3594 (class 0 OID 0)
-- Dependencies: 231
-- Name: speciality_disciplines_speciality_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.speciality_disciplines_speciality_id_seq', 1, false);


--
-- TOC entry 3595 (class 0 OID 0)
-- Dependencies: 233
-- Name: students_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.students_id_seq', 1, false);


--
-- TOC entry 3596 (class 0 OID 0)
-- Dependencies: 235
-- Name: teachers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.teachers_id_seq', 1, false);


--
-- TOC entry 3597 (class 0 OID 0)
-- Dependencies: 237
-- Name: templates_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.templates_id_seq', 1, false);


--
-- TOC entry 3598 (class 0 OID 0)
-- Dependencies: 239
-- Name: terms_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.terms_id_seq', 1, false);


--
-- TOC entry 3599 (class 0 OID 0)
-- Dependencies: 241
-- Name: timetables_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.timetables_id_seq', 1, false);


--
-- TOC entry 3600 (class 0 OID 0)
-- Dependencies: 243
-- Name: week_types_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.week_types_id_seq', 2, true);


--
-- TOC entry 3303 (class 2606 OID 24725)
-- Name: classrooms cabinet_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.classrooms
    ADD CONSTRAINT cabinet_unique UNIQUE NULLS NOT DISTINCT (cabinet);


--
-- TOC entry 3305 (class 2606 OID 24590)
-- Name: classrooms classrooms_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.classrooms
    ADD CONSTRAINT classrooms_pkey PRIMARY KEY (id);


--
-- TOC entry 3307 (class 2606 OID 24733)
-- Name: days day_name_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.days
    ADD CONSTRAINT day_name_unique UNIQUE NULLS NOT DISTINCT (name);


--
-- TOC entry 3309 (class 2606 OID 24599)
-- Name: days days_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.days
    ADD CONSTRAINT days_pkey PRIMARY KEY (id);


--
-- TOC entry 3315 (class 2606 OID 24729)
-- Name: discipline_codes discipline_code_code_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_codes
    ADD CONSTRAINT discipline_code_code_unique UNIQUE NULLS NOT DISTINCT (code);


--
-- TOC entry 3317 (class 2606 OID 24617)
-- Name: discipline_codes discipline_codes_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_codes
    ADD CONSTRAINT discipline_codes_pkey PRIMARY KEY (id);


--
-- TOC entry 3311 (class 2606 OID 24735)
-- Name: disciplines discipline_name_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.disciplines
    ADD CONSTRAINT discipline_name_unique UNIQUE NULLS NOT DISTINCT (name);


--
-- TOC entry 3319 (class 2606 OID 24731)
-- Name: discipline_types discipline_type_name_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_types
    ADD CONSTRAINT discipline_type_name_unique UNIQUE NULLS NOT DISTINCT (name);


--
-- TOC entry 3321 (class 2606 OID 24626)
-- Name: discipline_types discipline_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_types
    ADD CONSTRAINT discipline_types_pkey PRIMARY KEY (id);


--
-- TOC entry 3313 (class 2606 OID 24608)
-- Name: disciplines disciplines_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.disciplines
    ADD CONSTRAINT disciplines_pkey PRIMARY KEY (id);


--
-- TOC entry 3327 (class 2606 OID 24741)
-- Name: group_transfers group_transfer_group_id_term_id_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_transfers
    ADD CONSTRAINT group_transfer_group_id_term_id_unique UNIQUE NULLS NOT DISTINCT (group_id, term_id);


--
-- TOC entry 3329 (class 2606 OID 24642)
-- Name: group_transfers group_transfers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_transfers
    ADD CONSTRAINT group_transfers_pkey PRIMARY KEY (id);


--
-- TOC entry 3323 (class 2606 OID 24751)
-- Name: groups groups_name_enrollment_year_speciality_id_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.groups
    ADD CONSTRAINT groups_name_enrollment_year_speciality_id_unique UNIQUE NULLS NOT DISTINCT (number, enrollment_year, speciality_id);


--
-- TOC entry 3325 (class 2606 OID 24635)
-- Name: groups groups_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.groups
    ADD CONSTRAINT groups_pkey PRIMARY KEY (id);


--
-- TOC entry 3359 (class 2606 OID 24793)
-- Name: lessons lesson_number_timetable_id_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lessons
    ADD CONSTRAINT lesson_number_timetable_id_unique UNIQUE (number, timetable_id);


--
-- TOC entry 3363 (class 2606 OID 24780)
-- Name: lesson_templates lesson_template_number_template_id_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_templates
    ADD CONSTRAINT lesson_template_number_template_id_unique UNIQUE NULLS NOT DISTINCT (number, template_id);


--
-- TOC entry 3365 (class 2606 OID 24719)
-- Name: lesson_templates lesson_templates_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_templates
    ADD CONSTRAINT lesson_templates_pkey PRIMARY KEY (id);


--
-- TOC entry 3361 (class 2606 OID 24712)
-- Name: lessons lessons_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lessons
    ADD CONSTRAINT lessons_pkey PRIMARY KEY (id);


--
-- TOC entry 3331 (class 2606 OID 24651)
-- Name: specialities specialities_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specialities
    ADD CONSTRAINT specialities_pkey PRIMARY KEY (id);


--
-- TOC entry 3335 (class 2606 OID 24812)
-- Name: speciality_disciplines speciality_discipline_speciality_id_discipline_id_term_id_uniqu; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality_disciplines
    ADD CONSTRAINT speciality_discipline_speciality_id_discipline_id_term_id_uniqu UNIQUE (discipline_id, term_id, speciality_id);


--
-- TOC entry 3337 (class 2606 OID 24658)
-- Name: speciality_disciplines speciality_disciplines_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality_disciplines
    ADD CONSTRAINT speciality_disciplines_pkey PRIMARY KEY (speciality_id);


--
-- TOC entry 3333 (class 2606 OID 24805)
-- Name: specialities speciality_name_code_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specialities
    ADD CONSTRAINT speciality_name_code_unique UNIQUE (name, code);


--
-- TOC entry 3339 (class 2606 OID 24665)
-- Name: students students_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT students_pkey PRIMARY KEY (id);


--
-- TOC entry 3341 (class 2606 OID 24674)
-- Name: teachers teachers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.teachers
    ADD CONSTRAINT teachers_pkey PRIMARY KEY (id);


--
-- TOC entry 3343 (class 2606 OID 24839)
-- Name: templates templates_group_id_week_type_id_day_id_term_id_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.templates
    ADD CONSTRAINT templates_group_id_week_type_id_day_id_term_id_unique UNIQUE (group_id, week_type_id, day_id, term_id);


--
-- TOC entry 3345 (class 2606 OID 24681)
-- Name: templates templates_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.templates
    ADD CONSTRAINT templates_pkey PRIMARY KEY (id);


--
-- TOC entry 3347 (class 2606 OID 24861)
-- Name: terms term_course_course_relative_term_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.terms
    ADD CONSTRAINT term_course_course_relative_term_unique UNIQUE (course, course_relative_term);


--
-- TOC entry 3301 (class 2606 OID 24862)
-- Name: terms terms_course_relative_term_check; Type: CHECK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE public.terms
    ADD CONSTRAINT terms_course_relative_term_check CHECK (((course_relative_term > 0) AND (course_relative_term <= 2))) NOT VALID;


--
-- TOC entry 3349 (class 2606 OID 24688)
-- Name: terms terms_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.terms
    ADD CONSTRAINT terms_pkey PRIMARY KEY (id);


--
-- TOC entry 3351 (class 2606 OID 24868)
-- Name: timetables timetable_group_id_date_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetables
    ADD CONSTRAINT timetable_group_id_date_unique UNIQUE NULLS NOT DISTINCT (group_id, date);


--
-- TOC entry 3353 (class 2606 OID 24695)
-- Name: timetables timetables_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetables
    ADD CONSTRAINT timetables_pkey PRIMARY KEY (id);


--
-- TOC entry 3355 (class 2606 OID 24875)
-- Name: week_types week_type_name_unique; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.week_types
    ADD CONSTRAINT week_type_name_unique UNIQUE (name);


--
-- TOC entry 3357 (class 2606 OID 24704)
-- Name: week_types week_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.week_types
    ADD CONSTRAINT week_types_pkey PRIMARY KEY (id);


--
-- TOC entry 3375 (class 2606 OID 24850)
-- Name: templates days_day_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.templates
    ADD CONSTRAINT days_day_id_fk FOREIGN KEY (day_id) REFERENCES public.days(id) NOT VALID;


--
-- TOC entry 3370 (class 2606 OID 24823)
-- Name: speciality_disciplines discipline_codes_discipline_code_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality_disciplines
    ADD CONSTRAINT discipline_codes_discipline_code_id_fk FOREIGN KEY (discipline_code_id) REFERENCES public.discipline_codes(id) NOT VALID;


--
-- TOC entry 3382 (class 2606 OID 24786)
-- Name: lesson_templates disciplines_discipline_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_templates
    ADD CONSTRAINT disciplines_discipline_id_fk FOREIGN KEY (discipline_id) REFERENCES public.disciplines(id) NOT VALID;


--
-- TOC entry 3380 (class 2606 OID 24799)
-- Name: lessons disciplines_discipline_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lessons
    ADD CONSTRAINT disciplines_discipline_id_fk FOREIGN KEY (discipline_id) REFERENCES public.disciplines(id) NOT VALID;


--
-- TOC entry 3371 (class 2606 OID 24813)
-- Name: speciality_disciplines disciplines_discipline_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality_disciplines
    ADD CONSTRAINT disciplines_discipline_id_fk FOREIGN KEY (discipline_id) REFERENCES public.disciplines(id) NOT VALID;


--
-- TOC entry 3374 (class 2606 OID 24833)
-- Name: students groups_group_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.students
    ADD CONSTRAINT groups_group_id_fk FOREIGN KEY (group_id) REFERENCES public.groups(id) NOT VALID;


--
-- TOC entry 3376 (class 2606 OID 24840)
-- Name: templates groups_group_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.templates
    ADD CONSTRAINT groups_group_id_fk FOREIGN KEY (group_id) REFERENCES public.groups(id) NOT VALID;


--
-- TOC entry 3379 (class 2606 OID 24869)
-- Name: timetables groups_group_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetables
    ADD CONSTRAINT groups_group_id_fk FOREIGN KEY (group_id) REFERENCES public.groups(id) NOT VALID;


--
-- TOC entry 3368 (class 2606 OID 24762)
-- Name: group_transfers groups_group_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_transfers
    ADD CONSTRAINT groups_group_id_fkey FOREIGN KEY (group_id) REFERENCES public.groups(id) NOT VALID;


--
-- TOC entry 3366 (class 2606 OID 24752)
-- Name: groups groups_speciality_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.groups
    ADD CONSTRAINT groups_speciality_id_fkey FOREIGN KEY (speciality_id) REFERENCES public.specialities(id) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3367 (class 2606 OID 24757)
-- Name: groups groups_term_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.groups
    ADD CONSTRAINT groups_term_id_fkey FOREIGN KEY (current_term_id) REFERENCES public.terms(id) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3372 (class 2606 OID 24818)
-- Name: speciality_disciplines specialities_speciality_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality_disciplines
    ADD CONSTRAINT specialities_speciality_id_fk FOREIGN KEY (speciality_id) REFERENCES public.specialities(id) NOT VALID;


--
-- TOC entry 3383 (class 2606 OID 24781)
-- Name: lesson_templates templates_template_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_templates
    ADD CONSTRAINT templates_template_id_fk FOREIGN KEY (template_id) REFERENCES public.templates(id) ON DELETE CASCADE NOT VALID;


--
-- TOC entry 3369 (class 2606 OID 24806)
-- Name: specialities terms_term_count_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.specialities
    ADD CONSTRAINT terms_term_count_fk FOREIGN KEY (term_count) REFERENCES public.terms(id) NOT VALID;


--
-- TOC entry 3373 (class 2606 OID 24828)
-- Name: speciality_disciplines terms_term_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality_disciplines
    ADD CONSTRAINT terms_term_id_fk FOREIGN KEY (term_id) REFERENCES public.terms(id) NOT VALID;


--
-- TOC entry 3377 (class 2606 OID 24855)
-- Name: templates terms_term_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.templates
    ADD CONSTRAINT terms_term_id_fk FOREIGN KEY (term_id) REFERENCES public.terms(id) NOT VALID;


--
-- TOC entry 3381 (class 2606 OID 24794)
-- Name: lessons timetables_timetable_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lessons
    ADD CONSTRAINT timetables_timetable_id_fk FOREIGN KEY (timetable_id) REFERENCES public.timetables(id) NOT VALID;


--
-- TOC entry 3378 (class 2606 OID 24845)
-- Name: templates week_types_week_type_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.templates
    ADD CONSTRAINT week_types_week_type_id_fk FOREIGN KEY (week_type_id) REFERENCES public.week_types(id) NOT VALID;


-- Completed on 2024-02-03 22:50:03

--
-- PostgreSQL database dump complete
--

