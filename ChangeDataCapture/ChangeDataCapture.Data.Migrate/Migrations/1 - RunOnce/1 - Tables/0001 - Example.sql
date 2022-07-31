--Initialise PostgreSQL database

CREATE TABLE example (
	example_id  int PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
	name        varchar(50) UNIQUE NOT NULL,
	description varchar(100) NULL
);
