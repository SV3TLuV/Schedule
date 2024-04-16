--
-- PostgreSQL database dump
--

-- Dumped from database version 15.5
-- Dumped by pg_dump version 15.5

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
-- Name: schedule; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE schedule WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = icu LOCALE = 'en_US.UTF-8' ICU_LOCALE = 'en-US';


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
-- Name: account; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.account (
    account_id integer NOT NULL,
    login character varying(50) NOT NULL,
    password_hash character varying(512) NOT NULL,
    name character varying(40) NOT NULL,
    surname character varying(40) NOT NULL,
    middle_name character varying(40),
    email character varying(200) NOT NULL,
    role_id integer NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.account OWNER TO postgres;

--
-- Name: account_account_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.account ALTER COLUMN account_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.account_account_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: classroom; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.classroom (
    classroom_id integer NOT NULL,
    cabinet character varying(10) NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.classroom OWNER TO postgres;

--
-- Name: classroom_classroom_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.classroom ALTER COLUMN classroom_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.classroom_classroom_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: day; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.day (
    day_id integer NOT NULL,
    name character varying(20) NOT NULL,
    is_study boolean DEFAULT true NOT NULL
);


ALTER TABLE public.day OWNER TO postgres;

--
-- Name: day_day_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.day ALTER COLUMN day_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.day_day_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: discipline; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discipline (
    discipline_id integer NOT NULL,
    discipline_name_id integer NOT NULL,
    discipline_code_id integer NOT NULL,
    total_hours integer NOT NULL,
    term_id integer NOT NULL,
    speciality_id integer NOT NULL,
    discipline_type_id integer NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.discipline OWNER TO postgres;

--
-- Name: discipline_code; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discipline_code (
    discipline_code_id integer NOT NULL,
    code character varying(20) NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.discipline_code OWNER TO postgres;

--
-- Name: discipline_code_discipline_code_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.discipline_code ALTER COLUMN discipline_code_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.discipline_code_discipline_code_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: discipline_discipline_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.discipline ALTER COLUMN discipline_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.discipline_discipline_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: discipline_name; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discipline_name (
    discipline_name_id integer NOT NULL,
    name character varying(50) NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.discipline_name OWNER TO postgres;

--
-- Name: discipline_name_discipline_name_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.discipline_name ALTER COLUMN discipline_name_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.discipline_name_discipline_name_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: discipline_type; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.discipline_type (
    discipline_type_id integer NOT NULL,
    name character varying(30) NOT NULL
);


ALTER TABLE public.discipline_type OWNER TO postgres;

--
-- Name: discipline_type_discipline_type_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.discipline_type ALTER COLUMN discipline_type_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.discipline_type_discipline_type_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: employee; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.employee (
    employee_id integer NOT NULL,
    account_id integer NOT NULL
);


ALTER TABLE public.employee OWNER TO postgres;

--
-- Name: employee_employee_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.employee ALTER COLUMN employee_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.employee_employee_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: employee_permission; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.employee_permission (
    employee_id integer NOT NULL,
    permission_id integer NOT NULL
);


ALTER TABLE public.employee_permission OWNER TO postgres;

--
-- Name: group; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."group" (
    group_id integer NOT NULL,
    number character varying(2) NOT NULL,
    speciality_id integer NOT NULL,
    term_id integer NOT NULL,
    enrollment_year integer NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL,
    is_after_eleven boolean DEFAULT false NOT NULL
);


ALTER TABLE public."group" OWNER TO postgres;

--
-- Name: group_group_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."group" ALTER COLUMN group_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.group_group_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: group_transfer; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.group_transfer (
    next_term_id integer NOT NULL,
    group_id integer NOT NULL,
    is_transferred boolean NOT NULL,
    transfer_date date NOT NULL
);


ALTER TABLE public.group_transfer OWNER TO postgres;

--
-- Name: lesson; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.lesson (
    lesson_id integer NOT NULL,
    discipline_id integer NOT NULL,
    number integer NOT NULL,
    subgroup integer,
    timetable_id integer NOT NULL,
    teacher_ids integer[] NOT NULL,
    classroom_ids integer[] NOT NULL,
    lesson_change_id integer,
    time_start time without time zone,
    time_end time without time zone
);


ALTER TABLE public.lesson OWNER TO postgres;

--
-- Name: lesson_change; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.lesson_change (
    lesson_change_id integer NOT NULL,
    number integer NOT NULL,
    subgroup integer,
    lesson_id integer NOT NULL,
    discipline_id integer NOT NULL,
    teacher_ids integer[] NOT NULL,
    classroom_ids integer[] NOT NULL,
    date date NOT NULL,
    time_start time without time zone,
    time_end time without time zone
);


ALTER TABLE public.lesson_change OWNER TO postgres;

--
-- Name: lesson_change_lesson_change_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.lesson_change ALTER COLUMN lesson_change_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.lesson_change_lesson_change_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: lesson_lesson_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.lesson ALTER COLUMN lesson_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.lesson_lesson_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: middle_name; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.middle_name (
    value character varying(40) NOT NULL
);


ALTER TABLE public.middle_name OWNER TO postgres;

--
-- Name: name; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.name (
    value character varying(40) NOT NULL
);


ALTER TABLE public.name OWNER TO postgres;

--
-- Name: permission; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.permission (
    permission_id integer NOT NULL,
    name character varying(40) NOT NULL
);


ALTER TABLE public.permission OWNER TO postgres;

--
-- Name: permission_permission_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.permission ALTER COLUMN permission_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.permission_permission_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.role (
    role_id integer NOT NULL,
    name character varying(30) NOT NULL
);


ALTER TABLE public.role OWNER TO postgres;

--
-- Name: role_role_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.role ALTER COLUMN role_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.role_role_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: session; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.session (
    refresh_token character varying(512) NOT NULL,
    created date NOT NULL,
    updated date,
    account_id integer NOT NULL,
    session_id uuid DEFAULT gen_random_uuid() NOT NULL
);


ALTER TABLE public.session OWNER TO postgres;

--
-- Name: speciality; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.speciality (
    speciality_id integer NOT NULL,
    code character varying(20) NOT NULL,
    name character varying(20) NOT NULL,
    max_term_id integer NOT NULL,
    is_deleted boolean DEFAULT false NOT NULL
);


ALTER TABLE public.speciality OWNER TO postgres;

--
-- Name: speciality_speciality_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.speciality ALTER COLUMN speciality_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.speciality_speciality_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: student; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.student (
    student_id integer NOT NULL,
    group_id integer NOT NULL,
    account_id integer NOT NULL
);


ALTER TABLE public.student OWNER TO postgres;

--
-- Name: student_student_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.student ALTER COLUMN student_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.student_student_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: surname; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.surname (
    value character varying(40) NOT NULL
);


ALTER TABLE public.surname OWNER TO postgres;

--
-- Name: teacher; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.teacher (
    teacher_id integer NOT NULL,
    account_id integer NOT NULL
);


ALTER TABLE public.teacher OWNER TO postgres;

--
-- Name: teacher_teacher_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.teacher ALTER COLUMN teacher_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.teacher_teacher_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: term; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.term (
    term_id integer NOT NULL,
    course integer NOT NULL
);


ALTER TABLE public.term OWNER TO postgres;

--
-- Name: timetable; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.timetable (
    timetable_id integer NOT NULL,
    group_id integer NOT NULL,
    created date DEFAULT now() NOT NULL,
    ended date,
    day_id integer NOT NULL,
    week_type_id integer NOT NULL
);


ALTER TABLE public.timetable OWNER TO postgres;

--
-- Name: timetable_timetable_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.timetable ALTER COLUMN timetable_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.timetable_timetable_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Name: week_type; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.week_type (
    week_type_id integer NOT NULL,
    name character varying(20) NOT NULL
);


ALTER TABLE public.week_type OWNER TO postgres;

--
-- Name: week_type_week_type_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.week_type ALTER COLUMN week_type_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.week_type_week_type_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- Data for Name: account; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.account (account_id, login, password_hash, name, surname, middle_name, email, role_id, is_deleted) FROM stdin;
1	Admin	$2a$11$/AKGJmbjT9.J/pdMmIk7S.VItgYYrknXhoPAUsTRIUqzIUXVw25zq	Admin	Admin	Admin	admin	1	f
2	Editor	$2a$11$qtS1HuNq4Q/9/gnERQJunu9U0wEYvtxbN2Z8senRvOLUF1gn/OV3i	Editor	Editor	Editor	editor	2	f
\.


--
-- Data for Name: classroom; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.classroom (classroom_id, cabinet, is_deleted) FROM stdin;
1	0108	f
2	0109	f
3	0110	f
4	0111	f
5	0114	f
6	0115	f
7	0200	f
8	0201	f
9	0201а	f
10	0202	f
11	0204	f
12	0205	f
13	0207	f
14	0209	f
15	0209а	f
16	0300	f
17	0301	f
18	0302	f
19	0303	f
20	0305	f
21	0306	f
22	0307	f
23	0308	f
24	0309	f
25	104	f
26	105	f
27	215	f
28	219	f
29	220	f
30	221	f
31	222	f
32	226	f
33	228	f
34	230	f
35	300	f
36	301	f
37	303	f
38	304	f
39	305	f
40	306	f
41	306а	f
42	307	f
43	308	f
44	309	f
45	311	f
46	312	f
47	314	f
48	315	f
49	317	f
50	401	f
51	402	f
52	403	f
53	404	f
54	404а	f
55	405	f
56	406	f
57	407	f
58	408	f
59	409	f
60	411	f
61	411а	f
62	413	f
63	414	f
64	416	f
65	417	f
66	418	f
\.


--
-- Data for Name: day; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.day (day_id, name, is_study) FROM stdin;
1	Понедельник	t
2	Вторник	t
3	Среда	t
4	Четверг	t
5	Пятница	t
6	Суббота	t
7	Воскресенье	f
\.


--
-- Data for Name: discipline; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.discipline (discipline_id, discipline_name_id, discipline_code_id, total_hours, term_id, speciality_id, discipline_type_id, is_deleted) FROM stdin;
\.


--
-- Data for Name: discipline_code; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.discipline_code (discipline_code_id, code, is_deleted) FROM stdin;
1	ЕН.01	f
2	ЕН.02	f
3	ЕН.03	f
4	ОГСЭ.01	f
5	ОГСЭ.02	f
6	ОГСЭ.03	f
7	ОГСЭ.04	f
8	ОГСЭ.05	f
9	ОП.01	f
10	ОП.02	f
11	ОП.03	f
12	ОП.04	f
13	ОП.05	f
14	ОП.06	f
15	ОП.07	f
16	ОП.08	f
17	ОП.09	f
18	ОП.10	f
19	ОП.11	f
20	ОП.12	f
21	ОП.13	f
22	ОП.14	f
23	ОУД.01	f
24	ОУД.02	f
25	ОУД.03	f
26	ОУД.04	f
27	ОУД.05	f
28	ОУД.06	f
29	ОУД.07	f
30	ОУД.08	f
31	ОУД.09	f
32	ОУД.10	f
33	ОУД.11	f
34	ОУД.12	f
35	ОУД.13	f
36	СГ.01	f
37	СГ.02	f
38	СГ.04	f
39	СГ.05	f
40	СГ.06	f
41	СГ.07	f
\.


--
-- Data for Name: discipline_name; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.discipline_name (discipline_name_id, name, is_deleted) FROM stdin;
1	ААС	f
2	БЖД	f
3	БИОЛОГИЯ	f
4	ВТ	f
5	ГЕОГРАФИЯ	f
6	ДМ	f
7	ДМ С ЭМЛ	f
8	И И КГ	f
9	ИБ	f
10	ИКГ	f
11	ИНДИВИДУАЛЬНЫЙ ПРОЕКТ	f
12	ИНЖЕНЕРНАЯ ГРАФИКА	f
13	ИНОСТРАННЫЙ ЯЗЫК	f
14	ИНОСТРАННЫЙ ЯЗЫК В ПД	f
15	ИНФОРМАТИКА	f
16	ИП	f
17	ИСТОРИЯ	f
18	ИСТОРИЯ РОССИИ	f
19	ИТ	f
20	КМ	f
21	КС	f
22	ЛИТЕРАТУРА	f
23	МАТЕМАТИКА	f
24	МДК.01.01	f
25	МДК.01.02	f
26	МДК.01.03	f
27	МДК.01.04	f
28	МДК.01.05	f
29	МДК.02.01	f
30	МДК.02.02	f
31	МДК.02.03	f
32	МДК.03.01	f
33	МДК.03.02	f
34	МДК.03.03	f
35	МДК.03.04	f
36	МДК.03.05	f
37	МДК.04.01	f
38	МДК.04.02	f
39	МДК.05.01	f
40	МДК.05.02	f
41	МДК.05.03	f
42	МДК.06.01	f
43	МДК.08.01	f
44	МДК.08.02	f
45	МДК.09.01	f
46	МДК.09.02	f
47	МДК.09.03	f
48	МДК.11.01	f
49	МЕНЕДЖМЕНТ	f
50	МЕНЕДЖМЕНТ В ПД	f
51	МС И С	f
52	О И ПОИБ	f
53	ОА И П	f
54	ОБЖ	f
55	ОБП	f
56	ОБЩЕСТВОЗНАНИЕ	f
57	ОИБ	f
58	ОПБД	f
59	ОС И С	f
60	ОСНОВЫ БЕРЕЖЛИВОГО ПРОИЗВОДСТВА	f
61	ОСНОВЫ ФИЛОСОФИИ	f
62	ОСНОВЫ ФИНАНСОВОЙ ГРАМОТНОСТИ	f
63	ОСНОВЫ ЭТ	f
64	ОСНОВЫ ЭТХ	f
65	ОТИ	f
66	ОТК	f
67	ОФГ	f
68	ОЭ И ВТ	f
69	ОЭТХ	f
70	ПОКС И WEB-СЕРВЕРОВ	f
71	ПОПД	f
72	ПП	f
73	ПП.01	f
74	ПП.02	f
75	ПП.03	f
76	ПП.04	f
77	ПП.05	f
78	ПП.06	f
79	ПП.08	f
80	ПП.09	f
81	ПП.11	f
82	ППОПД	f
83	ПРИКЛАДНАЯ ЭЛЕКТРОНИКА	f
84	ПСИХОЛОГИЯ ОБЩЕНИЯ	f
85	РУССКИЙ ЯЗЫК	f
86	РЯ И КР	f
87	СС И ТД	f
88	ТВИМС	f
89	ТФУПД	f
90	ТЭС	f
91	ТЭЦ	f
92	УП.01	f
93	УП.02	f
94	УП.03	f
95	УП.04	f
96	УП.05	f
97	УП.06	f
98	УП.08	f
99	УП.09	f
100	УП.11	f
101	ФИЗИКА	f
102	ФИЗИЧЕСКАЯ КУЛЬТУРА	f
103	ХИМИЯ	f
104	ЧИСЛЕННЫЕ МЕТОДЫ	f
105	Э И СХТ	f
106	ЭВМ	f
107	ЭКОНОМИКА И УПРАВЛЕНИЕ	f
108	ЭКОНОМИКА ОТРАСЛИ	f
109	ЭЛЕКТРОННАЯ ТЕХНИКА	f
110	ЭЛЕКТРОТЕХНИКА	f
111	ЭРИ	f
112	ЭТ	f
113	ЭТИ	f
114	ЭТС	f
\.


--
-- Data for Name: discipline_type; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.discipline_type (discipline_type_id, name) FROM stdin;
1	Дисциплина
2	Практика
3	Внекласная деятельность
\.


--
-- Data for Name: employee; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.employee (employee_id, account_id) FROM stdin;
\.


--
-- Data for Name: employee_permission; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.employee_permission (employee_id, permission_id) FROM stdin;
\.


--
-- Data for Name: group; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."group" (group_id, number, speciality_id, term_id, enrollment_year, is_deleted, is_after_eleven) FROM stdin;
\.


--
-- Data for Name: group_transfer; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.group_transfer (next_term_id, group_id, is_transferred, transfer_date) FROM stdin;
\.


--
-- Data for Name: lesson; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.lesson (lesson_id, discipline_id, number, subgroup, timetable_id, teacher_ids, classroom_ids, lesson_change_id, time_start, time_end) FROM stdin;
\.


--
-- Data for Name: lesson_change; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.lesson_change (lesson_change_id, number, subgroup, lesson_id, discipline_id, teacher_ids, classroom_ids, date, time_start, time_end) FROM stdin;
\.


--
-- Data for Name: middle_name; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.middle_name (value) FROM stdin;
Admin
Editor
\.


--
-- Data for Name: name; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.name (value) FROM stdin;
Admin
Editor
\.


--
-- Data for Name: permission; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.permission (permission_id, name) FROM stdin;
\.


--
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.role (role_id, name) FROM stdin;
1	Admin
2	Editor
3	Teacher
4	Student
\.


--
-- Data for Name: session; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.session (refresh_token, created, updated, account_id, session_id) FROM stdin;
\.


--
-- Data for Name: speciality; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.speciality (speciality_id, code, name, max_term_id, is_deleted) FROM stdin;
11	09.02.01	КСК	8	f
12	09.02.03	ПКС	8	f
13	09.02.06	ССА	8	f
14	09.02.07	ИСПП	8	f
15	09.02.07	ИСПВ	8	f
16	10.02.04	ОИБ	8	f
17	11.02.15	ИСС	8	f
18	11.02.18	РМТ	8	f
19	11.02.10	Р	8	f
20	11.02.11	С	8	f
\.


--
-- Data for Name: student; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.student (student_id, group_id, account_id) FROM stdin;
\.


--
-- Data for Name: surname; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.surname (value) FROM stdin;
Admin
Editor
\.


--
-- Data for Name: teacher; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.teacher (teacher_id, account_id) FROM stdin;
\.


--
-- Data for Name: term; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.term (term_id, course) FROM stdin;
1	1
2	1
3	2
4	2
5	3
6	3
7	4
8	4
9	5
10	5
\.


--
-- Data for Name: timetable; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.timetable (timetable_id, group_id, created, ended, day_id, week_type_id) FROM stdin;
\.


--
-- Data for Name: week_type; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.week_type (week_type_id, name) FROM stdin;
1	Знаменатель
2	Числитель
\.


--
-- Name: account_account_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.account_account_id_seq', 2, true);


--
-- Name: classroom_classroom_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.classroom_classroom_id_seq', 66, true);


--
-- Name: day_day_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.day_day_id_seq', 7, true);


--
-- Name: discipline_code_discipline_code_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discipline_code_discipline_code_id_seq', 41, true);


--
-- Name: discipline_discipline_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discipline_discipline_id_seq', 1, false);


--
-- Name: discipline_name_discipline_name_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discipline_name_discipline_name_id_seq', 114, true);


--
-- Name: discipline_type_discipline_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.discipline_type_discipline_type_id_seq', 3, true);


--
-- Name: employee_employee_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.employee_employee_id_seq', 1, false);


--
-- Name: group_group_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.group_group_id_seq', 1, false);


--
-- Name: lesson_change_lesson_change_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.lesson_change_lesson_change_id_seq', 1, false);


--
-- Name: lesson_lesson_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.lesson_lesson_id_seq', 1, false);


--
-- Name: permission_permission_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.permission_permission_id_seq', 1, false);


--
-- Name: role_role_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.role_role_id_seq', 5, true);


--
-- Name: speciality_speciality_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.speciality_speciality_id_seq', 20, true);


--
-- Name: student_student_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.student_student_id_seq', 1, false);


--
-- Name: teacher_teacher_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.teacher_teacher_id_seq', 1, false);


--
-- Name: timetable_timetable_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.timetable_timetable_id_seq', 1, false);


--
-- Name: week_type_week_type_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.week_type_week_type_id_seq', 2, true);


--
-- Name: account account_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT account_pk PRIMARY KEY (account_id);


--
-- Name: classroom classroom_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.classroom
    ADD CONSTRAINT classroom_pk PRIMARY KEY (classroom_id);


--
-- Name: day day_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.day
    ADD CONSTRAINT day_pk PRIMARY KEY (day_id);


--
-- Name: discipline_code discipline_code_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_code
    ADD CONSTRAINT discipline_code_pk PRIMARY KEY (discipline_code_id);


--
-- Name: discipline_name discipline_name_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_name
    ADD CONSTRAINT discipline_name_pk PRIMARY KEY (discipline_name_id);


--
-- Name: discipline discipline_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline
    ADD CONSTRAINT discipline_pk PRIMARY KEY (discipline_id);


--
-- Name: discipline_type discipline_type_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline_type
    ADD CONSTRAINT discipline_type_pk PRIMARY KEY (discipline_type_id);


--
-- Name: employee_permission employee_permission_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee_permission
    ADD CONSTRAINT employee_permission_pk PRIMARY KEY (employee_id, permission_id);


--
-- Name: employee employee_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee
    ADD CONSTRAINT employee_pk PRIMARY KEY (employee_id);


--
-- Name: group group_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."group"
    ADD CONSTRAINT group_pk PRIMARY KEY (group_id);


--
-- Name: group_transfer group_transfer_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_transfer
    ADD CONSTRAINT group_transfer_pk PRIMARY KEY (next_term_id, group_id);


--
-- Name: lesson_change lesson_change_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_change
    ADD CONSTRAINT lesson_change_pk PRIMARY KEY (lesson_change_id);


--
-- Name: lesson lesson_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson
    ADD CONSTRAINT lesson_pk PRIMARY KEY (lesson_id);


--
-- Name: middle_name middle_name_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.middle_name
    ADD CONSTRAINT middle_name_pk PRIMARY KEY (value);


--
-- Name: permission permission_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.permission
    ADD CONSTRAINT permission_pk PRIMARY KEY (permission_id);


--
-- Name: role role_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT role_pk PRIMARY KEY (role_id);


--
-- Name: session session_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.session
    ADD CONSTRAINT session_pk PRIMARY KEY (session_id);


--
-- Name: speciality speciality_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality
    ADD CONSTRAINT speciality_pk PRIMARY KEY (speciality_id);


--
-- Name: student student_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_pk PRIMARY KEY (student_id);


--
-- Name: surname surname_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.surname
    ADD CONSTRAINT surname_pk PRIMARY KEY (value);


--
-- Name: teacher teacher_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.teacher
    ADD CONSTRAINT teacher_pk PRIMARY KEY (teacher_id);


--
-- Name: term term_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.term
    ADD CONSTRAINT term_pk PRIMARY KEY (term_id);


--
-- Name: timetable timetable_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetable
    ADD CONSTRAINT timetable_pk PRIMARY KEY (timetable_id);


--
-- Name: name value_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.name
    ADD CONSTRAINT value_pk PRIMARY KEY (value);


--
-- Name: week_type week_type_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.week_type
    ADD CONSTRAINT week_type_pk PRIMARY KEY (week_type_id);


--
-- Name: account_email_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX account_email_index ON public.account USING btree (email);


--
-- Name: account_login_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX account_login_index ON public.account USING btree (login);


--
-- Name: classroom_cabinet_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX classroom_cabinet_index ON public.classroom USING btree (cabinet);


--
-- Name: day_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX day_name_index ON public.day USING btree (name);


--
-- Name: discipline_code_code_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX discipline_code_code_index ON public.discipline_code USING btree (code);


--
-- Name: discipline_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX discipline_index ON public.discipline USING btree (discipline_code_id, discipline_name_id, speciality_id, term_id);


--
-- Name: discipline_name_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX discipline_name_name_index ON public.discipline_name USING btree (name);


--
-- Name: group_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX group_index ON public."group" USING btree (number, enrollment_year, speciality_id);


--
-- Name: permission_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX permission_name_index ON public.permission USING btree (name);


--
-- Name: role_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX role_name_index ON public.role USING btree (name);


--
-- Name: speciality_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX speciality_name_index ON public.speciality USING btree (name);


--
-- Name: timetable_created_group_id_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX timetable_created_group_id_index ON public.timetable USING btree (created, group_id);


--
-- Name: timetable_group_id_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX timetable_group_id_index ON public.timetable USING btree (group_id);


--
-- Name: week_type_name_index; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX week_type_name_index ON public.week_type USING btree (name);


--
-- Name: account account_middle_name_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT account_middle_name_fk FOREIGN KEY (middle_name) REFERENCES public.middle_name(value);


--
-- Name: account account_name_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT account_name_fk FOREIGN KEY (name) REFERENCES public.name(value);


--
-- Name: account account_role_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT account_role_id_fk FOREIGN KEY (role_id) REFERENCES public.role(role_id) ON DELETE CASCADE;


--
-- Name: account account_surname_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.account
    ADD CONSTRAINT account_surname_fk FOREIGN KEY (surname) REFERENCES public.surname(value);


--
-- Name: discipline discipline_code_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline
    ADD CONSTRAINT discipline_code_id_fk FOREIGN KEY (discipline_code_id) REFERENCES public.discipline_code(discipline_code_id) ON DELETE CASCADE;


--
-- Name: discipline discipline_name_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline
    ADD CONSTRAINT discipline_name_id_fk FOREIGN KEY (discipline_name_id) REFERENCES public.discipline_name(discipline_name_id) ON DELETE CASCADE;


--
-- Name: discipline discipline_speciality_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline
    ADD CONSTRAINT discipline_speciality_id_fk FOREIGN KEY (speciality_id) REFERENCES public.speciality(speciality_id) ON DELETE CASCADE;


--
-- Name: discipline discipline_term_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline
    ADD CONSTRAINT discipline_term_id_fk FOREIGN KEY (term_id) REFERENCES public.term(term_id) ON DELETE CASCADE;


--
-- Name: discipline discipline_type_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.discipline
    ADD CONSTRAINT discipline_type_id_fk FOREIGN KEY (discipline_type_id) REFERENCES public.discipline_type(discipline_type_id) ON DELETE CASCADE;


--
-- Name: employee employee_account_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee
    ADD CONSTRAINT employee_account_id_fk FOREIGN KEY (account_id) REFERENCES public.account(account_id);


--
-- Name: employee_permission employee_permission_employee_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee_permission
    ADD CONSTRAINT employee_permission_employee_id_fk FOREIGN KEY (employee_id) REFERENCES public.employee(employee_id) ON DELETE CASCADE;


--
-- Name: employee_permission employee_permission_permission_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.employee_permission
    ADD CONSTRAINT employee_permission_permission_id_fk FOREIGN KEY (permission_id) REFERENCES public.permission(permission_id) ON DELETE CASCADE;


--
-- Name: group group_speciality_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."group"
    ADD CONSTRAINT group_speciality_id_fk FOREIGN KEY (speciality_id) REFERENCES public.speciality(speciality_id) ON DELETE CASCADE;


--
-- Name: group group_term_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."group"
    ADD CONSTRAINT group_term_id_fk FOREIGN KEY (term_id) REFERENCES public.term(term_id) ON DELETE CASCADE;


--
-- Name: group_transfer group_transfer_group_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_transfer
    ADD CONSTRAINT group_transfer_group_id_fk FOREIGN KEY (group_id) REFERENCES public."group"(group_id) ON DELETE CASCADE;


--
-- Name: group_transfer group_transfer_next_term_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.group_transfer
    ADD CONSTRAINT group_transfer_next_term_id_fk FOREIGN KEY (next_term_id) REFERENCES public.term(term_id) ON DELETE CASCADE;


--
-- Name: lesson_change lesson_change_discipline_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_change
    ADD CONSTRAINT lesson_change_discipline_id_fk FOREIGN KEY (discipline_id) REFERENCES public.discipline(discipline_id) ON DELETE CASCADE;


--
-- Name: lesson_change lesson_change_lesson_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson_change
    ADD CONSTRAINT lesson_change_lesson_id_fk FOREIGN KEY (lesson_id) REFERENCES public.lesson(lesson_id) ON DELETE CASCADE;


--
-- Name: lesson lesson_discipline_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson
    ADD CONSTRAINT lesson_discipline_id_fk FOREIGN KEY (discipline_id) REFERENCES public.discipline(discipline_id) ON DELETE CASCADE;


--
-- Name: lesson lesson_lesson_change_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson
    ADD CONSTRAINT lesson_lesson_change_id_fk FOREIGN KEY (lesson_change_id) REFERENCES public.lesson_change(lesson_change_id) ON DELETE CASCADE;


--
-- Name: lesson lesson_timetable_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.lesson
    ADD CONSTRAINT lesson_timetable_id_fk FOREIGN KEY (timetable_id) REFERENCES public.timetable(timetable_id) ON DELETE CASCADE;


--
-- Name: session session_account_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.session
    ADD CONSTRAINT session_account_id_fk FOREIGN KEY (account_id) REFERENCES public.account(account_id) ON DELETE CASCADE;


--
-- Name: speciality speciality_max_term_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.speciality
    ADD CONSTRAINT speciality_max_term_id_fk FOREIGN KEY (max_term_id) REFERENCES public.term(term_id) ON DELETE CASCADE;


--
-- Name: student student_account_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_account_id_fk FOREIGN KEY (account_id) REFERENCES public.account(account_id) ON DELETE CASCADE;


--
-- Name: student student_group_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.student
    ADD CONSTRAINT student_group_id_fk FOREIGN KEY (group_id) REFERENCES public."group"(group_id) ON DELETE CASCADE;


--
-- Name: teacher teacher_account_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.teacher
    ADD CONSTRAINT teacher_account_id_fk FOREIGN KEY (account_id) REFERENCES public.account(account_id) ON DELETE CASCADE;


--
-- Name: timetable timetable_day_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetable
    ADD CONSTRAINT timetable_day_id_fk FOREIGN KEY (day_id) REFERENCES public.day(day_id) ON DELETE CASCADE;


--
-- Name: timetable timetable_group_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetable
    ADD CONSTRAINT timetable_group_id_fk FOREIGN KEY (group_id) REFERENCES public."group"(group_id) ON DELETE CASCADE;


--
-- Name: timetable timetable_week_type_id_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.timetable
    ADD CONSTRAINT timetable_week_type_id_fk FOREIGN KEY (week_type_id) REFERENCES public.week_type(week_type_id) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

