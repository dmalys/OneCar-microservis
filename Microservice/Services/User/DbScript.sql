IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'UserDb')
  BEGIN
    CREATE DATABASE [UserDb]


    END
    GO
       USE [UserDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='users' and xtype='U')
BEGIN
    CREATE TABLE users (
	user_id INT IDENTITY (1, 1) PRIMARY KEY,
	account_id INT NOT NULL UNIQUE,
	car_id INT DEFAULT NULL UNIQUE,
	first_name VARCHAR (255) NOT NULL,
	last_name VARCHAR (255) NOT NULL,
	gender VARCHAR (1) NOT NULL DEFAULT 'u',
	phone VARCHAR (25) DEFAULT NULL,
	email VARCHAR (255) NOT NULL UNIQUE,
	street VARCHAR (255) DEFAULT NULL,
	city VARCHAR (255) DEFAULT NULL,
	state VARCHAR (255) DEFAULT NULL,
	country VARCHAR (255) DEFAULT NULL,
	zip_code VARCHAR (5) DEFAULT NULL,
	available_credit DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	driving_license_id VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	update_date datetime DEFAULT NULL
)
END