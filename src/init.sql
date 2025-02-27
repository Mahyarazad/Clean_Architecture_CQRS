IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'NadineSoft')
BEGIN
    CREATE DATABASE NadineSoft;
END

IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'NadineSoft_Identity')
BEGIN
    CREATE DATABASE NadineSoft_Identity;
END