IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'TicketDb')
  BEGIN
    CREATE DATABASE [TicketDb]


    END
    GO
       USE [TicketDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='tickets' and xtype='U')
BEGIN
    CREATE TABLE tickets (
	ticket_id INT IDENTITY (1, 1) PRIMARY KEY,
	car_id INT NOT NULL UNIQUE,
	expiration_date datetime NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
)
END