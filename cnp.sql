create database cnp;
use cnp;

create table Employee(
employeeId int primary key,
employeeName varchar(20),
phoneNumber varchar(10),
gender varchar(7),
email  varchar(40),
password varchar(25),
approval varchar(5) 

);

 create table News(
 newsId int primary key,
 title varchar(30),
 description varchar(200),
 approval varchar(5) 
 );
 select * from News;