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
	car_image_id INT DEFAULT NULL,
	car_model_id INT NOT NULL,
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

IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'CouponDb')
  BEGIN
    CREATE DATABASE [CouponDb]


    END
    GO
       USE [CouponDb]
    GO
	
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='coupons' and xtype='U')
BEGIN
    CREATE TABLE coupons (
	coupon_id INT IDENTITY (1, 1) PRIMARY KEY,
	code VARCHAR (255) NOT NULL,
	money_value DECIMAL(10,2) NOT NULL DEFAULT '0.00',
	enabled BIT NOT NULL,
	expiration_date datetime NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
)
END

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
	car_id INT NOT NULL,
	expiration_date datetime NOT NULL,
	created_by VARCHAR (255) NOT NULL,
	create_date datetime NOT NULL,
	updated_by VARCHAR (255) DEFAULT NULL,
	update_date datetime DEFAULT NULL
)
END

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
	account_id INT NOT NULL,
	car_id INT DEFAULT NULL,
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