--Mod Log
-- 03/07/2022 by Rebecca Plowman
--	changed item.release_date to not null
--03/08/2022 by Rebecca Plowman
--	added surrogate key and index to borrowed_item table
--03/10/2022 by Rebecca Plowman
--	corrected data type of genre.genre_name from into to nvarchar(50)
--03/11/2022 by Rebecca Plowman
--	added drop statements to normalize id values between databases (for ease of use by instructor)
--	populating tables with data
--03/14/2022 by Rebecca Plowman
--	bugfix: add borrowed_date in select statement for borrowed_item rows with null values for returned_date
--	added select statements to show all disks, all borrowed disks, all disks borrowed more than once, and all disks currently on loan
--03/17/2022 by Rebecca Plowman
--	added create View_Borrower_No_Loans view
--	added select statement for borrowers who have borrowed multiple items
--03/28/2022 by Rebecca Plowman
--	added procedure sp_insertBorrowedItem and granted exec permission
--	added procedure sp_updateBorrowedItem and granted exec permission

use master;
Go

--create diskinventory_rp
if db_id ('diskinventory_rp') is null
	create database diskinventory_rp;
Go

use diskinventory_rp;
Go

--drop existing tables in order that won't conflict with foreign key constraint
--drop borrowed_item and borrower tables
if object_id ('borrowed_item') is not null
	drop table borrowed_item;
Go
if object_id ('borrower') is not null
	drop table borrower;
Go
--drop item table
if object_id ('item') is not null
	drop table item;
Go
--drop status_type, genre, and item_type tables
if object_id ('status_type') is not null
	drop table status_type;
Go
if object_id ('genre') is not null
	drop table genre;
Go
if object_id ('item_type') is not null
	drop table item_type;
Go

--create borrower table
if object_id ('borrower') is null
	create table borrower 
		(borrower_id int not null identity primary key,
		borrower_name nvarchar(100) not null,
		borrower_phone_number char(10) not null);
Go

--populate borrower table
insert into borrower
	(borrower_name, borrower_phone_number)
values
	('Miguel Ewing', '2025550126'),
	('Anabel Perkins', '2025550146'),
	('Ross Brooks', '2025550184'),
	('Sheldon McLean', '2025550124'),
	('Ashlyn Leblanc', '2025550182'),
	('Brittany Gonzales', '2025550176'),
	('Simon Ortega', '6175550184'),
	('Megan Marsh', '6175550117'),
	('Kierra Webb', '6175550163'),
	('Maxwell Adkins', '6175550112'),
	('Nico Gregory', '6175550129'),
	('Emily Carson', '6175550163'),
	('Tania Ryan', '2085550158'),
	('Patrick Brock', '2085550121'),
	('Eleanor Castro', '2085550154'),
	('Lucas Hardy', '2085550122'),
	('Athena Forbes', '2085550176'),
	('Ernest Flowers', '6135550142'),
	('Eliezer Mann', '6135550124'),
	('Sam Middleton', '6135550173'),
	('Sophie Sherman', '6135550182'),
	('Jack Ware', '6135550130');
Go

--delete one borrower row
delete borrower
where borrower_name = 'Eleanor Castro';
Go

--create item_type table
if object_id('item_type') is null
	create table item_type
		(item_type_id int not null identity primary key,
		item_type_name nvarchar(50) not null);
Go

--populate item_type table
insert into item_type
	(item_type_name)
values
	('Album'),
	('DVD'),
	('Book'),
	('Video Game'),
	('Tabletop Game');
Go

--create genre table
if object_id ('genre') is null
	create table genre
		(genre_id int not null identity primary key,
		genre_name nvarchar(50) not null);
Go

--populate genre table
insert into genre
	(genre_name)
values
	('Pop'),
	('Indie'),
	('Fantasy'),
	('Sci-Fi'),
	('Shmup'),
	('Iyashikei'),
	('Deck Builder');
Go

--create status_type table
if object_id ('status_type') is null
	create table status_type
		(status_id int not null identity primary key,
		status_name nvarchar(50) not null);
Go

--populate status_type table
insert into status_type
	(status_name)
values
	('Available'),
	('On Loan'),
	('Damaged'),
	('In Repair'),
	('Lost');
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

--populate T.Swift albums in item table
insert into item
	(item_name, release_date, status_id, item_type_id, genre_id)
values
	('Red (Taylor''s Version)', '2021-11-12', 1, 1, 1), --meets criteria for d.5 in Project 3
	('Fearless (Taylor''s Version)', '2021-04-09', 1, 1, 1),
	('Lover', '2019-08-23', 1, 1, 1), --meets criteria for d.3 in Project 3
	('Folklore', '2020-07-24', 1, 1, 2),
	('Evermore', '2020-12-11', 1, 1, 2);
Go
--populate Tolkien books in item table
insert into item
	(item_name, release_date, status_id, item_type_id, genre_id)
values
	('Beowulf: A Translation and Commentary, together with Sellic Spell', '2014-05-01', 1, 3, 3),
	('The Silmarillion', '1977-09-15', 1, 3, 3), --meets criteria for d.4 in Project 3
	('The Fellowship of the Ring', '1954-07-29', 1, 3, 3),
	('The Two Towers', '1954-11-11', 1, 3, 3),
	('The Return of the King', '1955-10-20', 1, 3, 3);
Go
--populate The Expanse books in item table
insert into item
	(item_name, release_date, status_id, item_type_id, genre_id)
values
	('Leviathan Wakes', '2011-06-02', 1, 3, 4),
	('Caliban''s War', '2012-06-26', 1, 3, 4),
	('Abaddon''s Gate', '2013-06-04', 1, 3, 4),
	('Cibola Burn', '2014-06-05', 1, 3, 4),
	('Nemesis Games', '2015-06-02', 1, 3, 4),
	('Babylon''s Ashes', '2016-12-06', 1, 3, 4),
	('Persepolis Rising', '2017-12-05', 1, 3, 4),
	('Tiamat''s Wrath', '2019-03-26', 1, 3, 4);
Go
--populate movie in item table
insert into item
	(item_name, release_date, status_id, item_type_id, genre_id)
values
	('The Wrath of Khan', '1982-06-04', 1, 2, 4);
Go
--populate video games in item table
insert into item
	(item_name, release_date, status_id, item_type_id, genre_id)
values
	('Animal Crossing: New Leaf', '2012-11-08', 1, 4, 6),
	('Animal Crossing: New Horizons', '2020-03-20', 1, 4, 6),
	('Ikaruga', '2001-12-20', 1, 4, 5);
Go
--populate tabletop games in item table
insert into item
	(item_name, release_date, status_id, item_type_id, genre_id)
values
	('Dominion', '2016-08-01', 1, 5, 7),
	('Kittens in a Blender', '2019-02-01', 1, 5, 7);
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

--populate borrowed_item table
insert into borrowed_item
	(borrower_id, item_id, borrowed_date, returned_date)
values
	(1, 9, dateadd(month, -3, GETDATE()), null),
	(5, 9, dateadd(month, -7, GETDATE()), dateadd(month, -8, GETDATE())),
	(18, 11, dateadd(day, -11, GETDATE()), dateadd(day, -1, GETDATE())),
	(18, 11, dateadd(month, -3, GETDATE()), DATEADD(month, -1, GETDATE())),
	(22, 1, dateadd(month, -1, GETDATE()), null),
	(22, 5, dateadd(month, -6, GETDATE()), null),
	(18, 12, getdate(), null),
	(8, 5, '2021-04-03', '2021-08-15'),
	(19, 6, '2019-06-08', '2020-01-14'),
	(19, 7, '2020-11-02', '2021-09-27'),
	(12, 20, '2017-08-02', '2018-02-21'),
	(12, 21, '2021-05-30', '2021-12-28'),
	(16, 24, '2020-12-31', '2021-01-01'),
	(16, 23, '2020-12-31', '2021-01-01'),
	(3, 19, '2016-11-29', '2016-12-24'), --item status is damaged
	(20, 18, '2020-07-27', '2020-08-15'),
	(3, 8, '2017-04-09', null), --item status is lost
	(9, 5, '2021-01-02', '2021-01-11'),
	(5, 7, dateadd(day, -1, GETDATE()), null),
	(5, 10, dateadd(day, -1, GETDATE()), null),
	(17, 3, dateadd(day, -11, GETDATE()), dateadd(day, -3, getdate()));
Go

--drop sp if exists
drop proc if exists sp_insertBorrowedItem;
go

--sp to insert into borrowed_item
create proc sp_insertBorrowedItem
	@borrower_id int,
	@item_id int,
	@borrowed_date datetime2,
	@returned_date datetime2 = NULL
as
begin try
	insert into borrowed_item
	(borrower_id, item_id, borrowed_date, returned_date)
	values
	(@borrower_id, @item_id, @borrowed_date, @returned_date);
end try
begin catch
	PRINT 'An error occurred.';
	PRINT 'Message: ' + CONVERT(varchar(250), ERROR_MESSAGE());
	IF ERROR_NUMBER() >= 50000
        PRINT 'This is a custom error message.';
end catch
Go
--exec w/o error
declare @datenow datetime2 = getdate();
exec sp_insertBorrowedItem 18, 12, @datenow, @datenow;
--exec w/ error
exec sp_insertBorrowedItem 18, 45, @datenow, @datenow;
go

--drop sp if exists
drop proc if exists sp_updateBorrowedItem;
go

--sp to update borrowed_item
create proc sp_updateBorrowedItem
	@borrowed_id int,
	@borrower_id int,
	@item_id int,
	@borrowed_date datetime2,
	@returned_date datetime2 = NULL
as
begin try
	update borrowed_item
	set borrower_id = @borrower_id,
		item_id = @item_id,
		borrowed_date = @borrowed_date,
		returned_date = @returned_date
	where borrowed_id = @borrowed_id;
end try
begin catch
	PRINT 'An error occurred.';
	PRINT 'Message: ' + CONVERT(varchar(250), ERROR_MESSAGE());
	IF ERROR_NUMBER() >= 50000
        PRINT 'This is a custom error message.';
end catch
go
--exec w/o error
declare @datenow datetime2 = getdate();
exec sp_updateBorrowedItem 22, 5, 7, @datenow, null;
--exec w/ error
exec sp_updateBorrowedItem 22, 5, 7, null, null;
go

--update Wrath of Khan status to damaged
update item
set status_id = 3
where item_id = 19;
Go

--update Fellowship of the Ring status to lost
update item
set status_id = 5
where item_id = 8;
Go

--update status_id of items that haven't been returned
update item
set status_id = 2
where item_id = any(
	select item_id
	from borrowed_item
	where returned_date is null)
	and status_id = 1;
Go

--list items that are on loan and haven't been returned
select borrowed_item.borrower_id, borrower_name, borrowed_item.item_id, item.item_name, borrowed_date, returned_date
from borrowed_item
	join borrower on borrowed_item.borrower_id = borrower.borrower_id
	join item on borrowed_item.item_id = item.item_id
where returned_date is null
order by borrowed_date desc;
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
--grant exec permissions to diskUserRP
grant exec on sp_insertBorrowedItem to diskUserRP;
grant exec on sp_updateBorrowedItem to diskUserRP;
go

use diskinventory_rp;
Go

--show all items in database
select item_name as 'Disk Name', 
	concat(month(release_date), '/', day(release_date), '/', year(release_date)) as 'Release Date', 
	item_type_name as Type, 
	genre_name as Genre, status_name as Status
from item, item_type, genre, status_type
where item.item_type_id = item_type.item_type_id
	and item.genre_id = genre.genre_id
	and item.status_id = status_type.status_id
order by item_name;
Go

--show all borrowed items
select right(borrower_name, len(borrower_name) - charindex(' ', borrower_name)) as 'Last',
	left(borrower_name, CHARINDEX(' ', borrower_name)) as 'First', 
	item_name as 'Disk Name', 
	cast(borrowed_date as date) as 'Borrowed Date', 
	cast(returned_date as date) as 'Returned Date'
from borrowed_item, borrower, item
where borrowed_item.item_id = item.item_id
	and borrowed_item.borrower_id = borrower.borrower_id
order by Last, First;
Go

--show items borrowed more than once
select item_name as 'Disk Name', count(borrowed_item.item_id) as 'Times Borrowed'
from borrowed_item, item
where item.item_id = borrowed_item.item_id
group by item.item_name
having count(borrowed_item.item_id) > 1
order by 'Times Borrowed' desc;
Go

--show items currently on loan
select distinct item_name as 'Disk Name', 
	cast(borrowed_date as date) as 'Borrowed', 
	cast(returned_date as date) as 'Returned', 
	right(borrower_name, len(borrower_name) - charindex(' ', borrower_name)) as 'Last Name',
	left(borrower_name, charindex(' ', borrower_name)) as 'First Name'
from item
	join borrowed_item
	on borrowed_item.item_id = item.item_id
	join borrower
	on borrowed_item.borrower_id = borrower.borrower_id
where item.status_id = 2 and
	borrowed_item.returned_date is null
order by 'Disk Name', 'Last Name', 'First Name';
Go

--drop view of borrowers with no borrowed_item, if exists
drop view if exists View_Borrower_No_Loans;
go
--create view of borrowers with no borrowed_item
create view View_Borrower_No_Loans
as
select borrower_id, 
	right(borrower_name, len(borrower_name) - charindex(' ', borrower_name)) as LastName,
	left(borrower_name, charindex(' ', borrower_name)) as FirstName
from borrower
where borrower_id not in
	(select distinct borrower_id
	from borrowed_item);
go
--select borrowers with no borrowed item
select LastName, FirstName from View_Borrower_No_Loans
order by LastName, FirstName;
go

--show borrowers who've borrowed multiple items
select 	right(borrower_name, len(borrower_name) - charindex(' ', borrower_name)) as LastName,
	left(borrower_name, charindex(' ', borrower_name)) as FirstName,
	count(distinct item_id) as 'Disks Borrowed'
from borrowed_item, borrower
where borrowed_item.borrower_id = borrower.borrower_id
group by borrower.borrower_name
having count(distinct item_id) > 1
order by LastName, FirstName;
go