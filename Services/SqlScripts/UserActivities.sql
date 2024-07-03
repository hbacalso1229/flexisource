/*
SQLyog Ultimate v9.02 
MySQL - 5.5.5-10.1.48-MariaDB 
*********************************************************************
*/
/*!40101 SET NAMES utf8 */;

create table `UserActivities` (
	`Id` char (114),
	`UserId` char (114),
	`Location` varchar (450),
	`TimeStarted` datetime ,
	`TimeEnded` datetime ,
	`Distance` double ,
	`Duration` double ,
	`AveragePace` double 
); 
