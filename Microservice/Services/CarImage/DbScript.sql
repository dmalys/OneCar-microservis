IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CarImageDb')
  BEGIN
    CREATE DATABASE [CarImageDb]


    END
    GO
       USE [CarImageDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='car_images' and xtype='U')
BEGIN
    CREATE TABLE car_images (
	car_image_id INT IDENTITY (1, 1) PRIMARY KEY,
	filename VARCHAR (255) NOT NULL,
	content VARBINARY(MAX) NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
)
END