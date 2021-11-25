IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CarModelDb')
  BEGIN
    CREATE DATABASE [CarModelDb]


    END
    GO
       USE [CarModelDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='car_models' and xtype='U')
BEGIN
    CREATE TABLE car_models (
	car_model_id INT IDENTITY (1, 1) PRIMARY KEY,
	car_model_name VARCHAR (255) NOT NULL,
	brand_id INT NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
)
END