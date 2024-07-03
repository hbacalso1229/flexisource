/*
SQLyog Ultimate v9.02 
MySQL - 5.5.5-10.1.48-MariaDB 
*********************************************************************
*/
/*!40101 SET NAMES utf8 */;

create table `User` (
	`Id` char (114),
	`Name` varchar (150),
	`Height` Decimal (11),
	`Weight` Decimal (11),
	`BirthDate` datetime ,
	`Age` int (11),
	`BMI` Decimal (11),
	`EmailAddress` varchar (450),
	`Address` varchar (450),
	`Created` datetime ,
	`CreatedBy` varchar (150),
	`LastModified` datetime ,
	`LastModifiedBy` varchar (150)
); 
