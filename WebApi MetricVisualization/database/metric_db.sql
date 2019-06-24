# Host: localhost  (Version 8.0.11)
# Date: 2019-06-24 11:02:52
# Generator: MySQL-Front 6.1  (Build 1.26)


#
# Structure for table "metric_name"
#

DROP TABLE IF EXISTS `metric_name`;
CREATE TABLE `metric_name` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `MetricName` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

#
# Structure for table "metric_value"
#

DROP TABLE IF EXISTS `metric_value`;
CREATE TABLE `metric_value` (
  `Id` int(11) DEFAULT NULL,
  `MetricValue` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
