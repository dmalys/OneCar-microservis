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