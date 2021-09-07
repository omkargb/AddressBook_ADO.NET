create database AddressBookServiceDB

use AddressBookServiceDB

create table ContactsTable(
Id int identity(1,1) NOT NULL PRIMARY KEY,
FirstName varchar(16) ,
LastName varchar(16),
Address varchar(24),
City varchar(16),
State varchar(16),
ZipCode int,
PhoneNumber varchar(16),
EmailId varchar(24)
);

/* Insert contact */
Insert into ContactsTable(FirstName,LastName,Address,City,State,ZipCode,PhoneNumber,EmailId) values 
('Omkar','B','xyz Road','Mumbai','MH',400091,'9999888877','omkarb@mail.com'),
('Ramesh','M','Buliding abc','Sansad Marg','Delhi',110001,'9988558899','rameshm@mail.com'),
('Suresh','K','above showroom','Swargate','MH',411042,'8877995544','sureshk@mail.com'),
('Riya','D','Near temple','Chennai','Tamil Nadu',600005,'7744112233','riyad@mail.com'),
('Neha','G','Opp mall','Kolkata','West Bangal',700073,'7775553330','nehag@mail.com');

/* Edit contact */
UPDATE ContactsTable set State='Maharashtra' where FirstName='Omkar' or FirstName='Suresh'

/* Delete contact by name */
Delete from ContactsTable where FirstName='Neha'

/* Retrieve Person belonging to a City or State */
SELECT * from ContactsTable where State='Maharashtra' or City='Chennai';

/* Count of address book by City and State */
select City,COUNT(City) as countOfCity from ContactsTable group by City order by City;
select State,COUNT(State) as countOfStates from ContactsTable group by State order by State;

/* sort name entries based on city */
Insert into ContactsTable(FirstName,LastName,Address,City,State,ZipCode,PhoneNumber,EmailId) values 
('Raju','S','near store','Mumbai','Maharashtra',400092,'7895553330','rajus@mail.com'),
('Ketan','L','near school','Mumbai','Maharashtra',400089,'9877553030','ketanl@mail.com'),
('Nitya','P','Kalina','Mumbai','Maharashtra',400098,'7733400300','nityap@mail.com');
SELECT * from ContactsTable

SELECT * FROM ContactsTable WHERE City = 'Mumbai' ORDER BY FirstName ASC;	--ascending
SELECT * FROM ContactsTable WHERE City = 'Mumbai' ORDER BY FirstName DESC;	--descending

/* Ability to identify each Address Book with name and Type. */
ALTER table ContactsTable ADD AdrBookName varchar(8);
ALTER table ContactsTable ADD PersonType VARCHAR(16);

UPDATE ContactsTable set AdrBookName='AB1' where Id=1 OR Id=6 OR Id=8
UPDATE ContactsTable set AdrBookName='AB2' where Id=2 OR Id=3 
UPDATE ContactsTable set AdrBookName='AB3' where Id=4 OR Id=7

UPDATE ContactsTable set PersonType='Friends' where AdrBookName='AB1'
UPDATE ContactsTable set PersonType='Family' where AdrBookName='AB2'
UPDATE ContactsTable set PersonType='Profession' where AdrBookName='AB3'
SELECT * FROM ContactsTable

/* Ability to get number of contact persons i.e. count by type */
SELECT PersonType,COUNT(PersonType) as count from ContactsTable group by PersonType

/* Ability to add person to both Friend and Family */
create table PersonCategory (
	Id int identity(1,1) primary key,
	PersonId int foreign key references ContactsTable(Id),
	PersonType varchar(20) not null
	)
insert into PersonCategory(PersonId,PersonType) values (1,'Family'),(2,'Friend'),(3,'Family'),(4,'Friend'),(6,'Profession'),(7,'Profession'),(8,'Profession');
select * from PersonCategory;
----- adding id 4 as friend and family
insert into PersonCategory(PersonId,PersonType) values(1,'Profession'),(4,'Family');

/* ER diagram and normalisation */
Alter Table ContactsTable drop column PersonType
