--Mod Log
-- 03/07/2021 by Rebecca Plowman
--	changed item.release_date to not null
--03/08/2021 by Rebecca Plowman
--	added surrogate key and index to borrowed_item table

use master;
Go

--create diskinventory_rp if not exists
if db_id ('diskinventory_rp') is null
	create database diskinventory_rp;
Go

use diskinventory_rp;
Go

--create borrower table
if object_id ('borrower') is null
	create table borrower 
		(borrower_id int not null identity primary key,
		borrower_name nvarchar(100) not null,
		borrower_phone_number char(10) not null);
Go

--create item_type table
if object_id('item_type') is null
	create table item_type
		(item_type_id int not null identity primary key,
		item_type_name nvarchar(50) not null);
Go

--create genre table
if object_id ('genre') is null
	create table genre
		(genre_id int not null identity primary key,
		genre_name int not null);
Go

--create status_type table
if object_id ('status_type') is null
	create table status_type
		(status_id int not null identity primary key,
		status_name nvarchar(50) not null);
Go

--create item table
if object_id ('item') is null
	create table item
		(item_id int not null identity primary key,
		item_name nvarchar(100) not null,
		release_date date not null,
		status_id int not null references status_type (status_id),
		item_type_id int not null references item_type (item_type_id),
		genre_id int not null references genre (genre_id));
Go

--create borrowed_item table
if object_id ('borrowed_item') is null
	create table borrowed_item
		(borrowed_id int not null identity primary key,
		borrower_id int not null references borrower (borrower_id),
		item_id int not null references item (item_id),
		borrowed_date datetime2 not null default getdate(),
		returned_date datetime2 null);
Go

use master;
Go

--create login for diskUserRP
if suser_id ('diskUserRP') is null
	create login diskUserRP with password = 'S3cur3Pw0rd',
		default_database = diskinventory_rp;
Go

use diskinventory_rp;
Go

--create database user for diskUserRP
if user_id ('diskUserRP') is null
	create user diskUserRP
		from login diskUserRP;
Go

--grant read perms to diskUserRP
alter role db_datareader
	add member diskUserRP;
Go