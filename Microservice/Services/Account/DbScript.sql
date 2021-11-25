IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'AccountDb')
  BEGIN
    CREATE DATABASE [AccountDb]


    END
    GO
       USE [AccountDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='accounts' and xtype='U')
BEGIN
    CREATE TABLE accounts (
	account_id INT IDENTITY (1, 1) PRIMARY KEY,
	account_type VARCHAR (255) NOT NULL,
	dicount INT DEFAULT NULL,
	price_per_month DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	create_date datetime NOT NULL,
	update_date datetime DEFAULT NULL
)
END