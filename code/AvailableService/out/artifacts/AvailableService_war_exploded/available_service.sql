SET FOREIGN_KEY_CHECKS=0;


DROP TABLE IF EXISTS `request_info`;
CREATE TABLE `request_info` (
  `id` int(10) NOT NULL AUTO_INCREMENT,
  `teacher_id` varchar(255) DEFAULT NULL,
  `student_id` varchar(255) DEFAULT NULL,
  `course_id` varchar(255) DEFAULT NULL,
  `section_id` varchar(255) DEFAULT NULL,
  `test_type` varchar(255) DEFAULT NULL,
  `test_time` varchar(255) DEFAULT NULL,
  `test_room` varchar(255) DEFAULT NULL,
  `test_building` varchar(255) DEFAULT NULL,
  `request_time` varchar(255) DEFAULT NULL,
  `is_valid` int DEFAULT 0,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=819 DEFAULT CHARSET=utf8;
