DO $$ BEGIN
	IF NOT EXISTS(select 1 from pg_publication WHERE pubname = 'example_publication') THEN
		CREATE PUBLICATION example_publication FOR TABLE example WITH (publish = 'insert');
	END IF;

	IF NOT EXISTS(select 1 from pg_replication_slots WHERE slot_name = 'example_slot') THEN
		SELECT * FROM pg_create_logical_replication_slot('example_slot', 'pgoutput');
	END IF;
END $$;