IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'BrandDb')
  BEGIN
    CREATE DATABASE [BrandDb]


    END
    GO
       USE [BrandDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='brands' and xtype='U')
BEGIN
    CREATE TABLE brands (
	brand_id INT IDENTITY (1, 1) PRIMARY KEY,
	brand_name VARCHAR (255) NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
)
END