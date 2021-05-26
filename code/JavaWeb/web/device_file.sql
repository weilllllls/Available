    /*
    Navicat MySQL Data Transfer

    Source Server         : localhost
    Source Server Version : 50151
    Source Host           : localhost:3306
    Source Database       : ylxdb

    Target Server Type    : MYSQL
    Target Server Version : 50151
    File Encoding         : 65001

    Date: 2016-03-26 23:42:43
    */

    SET FOREIGN_KEY_CHECKS=0;
    -- ----------------------------
    -- Table structure for `video_file`
    -- ----------------------------
    DROP TABLE IF EXISTS `zhuangqibin_device_file`;
    CREATE TABLE `zhuangqibin_device_file` (
      `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
      `device_id` varchar(255) DEFAULT '',
      `device_name` varchar(255) DEFAULT NULL,
      `attachment_file_id` varchar(255) DEFAULT NULL,
      `create_time` datetime DEFAULT NULL,
      PRIMARY KEY (`id`)
    ) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;


    DROP TABLE IF EXISTS `zhuangqibin_attach_file`;
    CREATE TABLE `zhuangqibin_attach_file` (
      `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
      `attachment_file_id` varchar(255) DEFAULT '',
      `attachment_file_url` varchar(255) DEFAULT NULL,
      PRIMARY KEY (`id`)
    ) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8;

