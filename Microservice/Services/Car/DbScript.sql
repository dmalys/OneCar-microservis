IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CarDb')
  BEGIN
    CREATE DATABASE [CarDb]


    END
    GO
       USE [CarDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='cars' and xtype='U')
BEGIN
    CREATE TABLE cars (
	car_id INT IDENTITY (1, 1) PRIMARY KEY,
	car_image_id INT DEFAULT NULL UNIQUE,
	car_model_id INT NOT NULL UNIQUE,
	production_date datetime NOT NULL,	
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL,
	user_rating INT DEFAULT NULL,
	mileage INT NOT NULL,
	price_per_hour DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	localization VARCHAR (255) NOT NULL
)
END