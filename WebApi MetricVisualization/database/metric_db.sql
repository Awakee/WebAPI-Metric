# Host: localhost  (Version 8.0.11)
# Date: 2019-06-26 10:45:33
# Generator: MySQL-Front 6.1  (Build 1.26)


#
# Structure for table "metric_name"
#

CREATE TABLE `metric_name` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `metric_name` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=25 DEFAULT CHARSET=utf8;

#
# Structure for table "metric_value"
#

CREATE TABLE `metric_value` (
  `Id` int(11) DEFAULT NULL,
  `metric_date` datetime DEFAULT '0000-00-00 00:00:00'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
