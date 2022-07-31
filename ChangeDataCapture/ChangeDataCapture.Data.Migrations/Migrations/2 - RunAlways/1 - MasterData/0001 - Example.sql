INSERT INTO example
    (name, description)
VALUES
    ('name1', 'Unknown'),
    ('name2', 'Succeeded'),
    ('name3', 'Failed')
ON CONFLICT (name) DO UPDATE
SET
    name = EXCLUDED.name
