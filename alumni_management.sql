-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Dec 13, 2023 at 08:02 AM
-- Server version: 10.4.28-MariaDB
-- PHP Version: 8.0.28

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `alumni_management`
--

-- --------------------------------------------------------

--
-- Table structure for table `alumni`
--

CREATE TABLE `alumni` (
  `id` int(20) NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `course_graduated` varchar(50) NOT NULL,
  `year_graduated` year(4) NOT NULL,
  `working_status` tinyint(1) NOT NULL,
  `current_work` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `alumni`
--

INSERT INTO `alumni` (`id`, `first_name`, `last_name`, `username`, `password`, `course_graduated`, `year_graduated`, `working_status`, `current_work`) VALUES
(1, 'Niño', 'Abao', 'oninsan', '12345', 'BSIT', '2024', 1, 'Data Science Engineer'),
(2, 'Benry', 'Olidiana', 'ben', 'ry', 'BSIT', '2024', 1, 'IT Specialist'),
(3, 'Micheal Jay', 'Sinadjan', 'mj', 'sinad', 'BSIT', '2024', 1, 'IT Specialist'),
(4, 'Leslie', 'Balabat', 'leslie', 'balabat', 'BSIT', '2024', 1, 'Teacher'),
(5, 'jesshuacutie', 'cutelang', 'hehehe', 'huhuhu', 'bsit', '2024', 0, 'n/a'),
(6, 'manjorie', 'hehehe', 'hihihi', 'hahaha', 'bscrim', '2021', 1, 'teacher');

-- --------------------------------------------------------

--
-- Table structure for table `announcement`
--

CREATE TABLE `announcement` (
  `id` int(20) NOT NULL,
  `announcement_title` varchar(50) NOT NULL,
  `announcement_description` varchar(2000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `announcement`
--

INSERT INTO `announcement` (`id`, `announcement_title`, `announcement_description`) VALUES
(2, 'Alumni Homecoming', 'The grand alumni homecoming event that is the most anticipated has become a reality.'),
(3, 'What to wear during alumni homecoming?', 'Anything blue, basta dili green, red, white, black, violet and etc. Wear everything you like'),
(4, 'Alumni Bingo', 'Tag 25 lang ang each card. Pede ra i baligya.'),
(5, 'Unifast requirements', 'Valid ID and yourself, Please proceed to the recieving station. Line properly and accordingly.'),
(6, 'Mangaligo dagat ig human capstone', 'Mayta madayon na, Dili kay puro drawing salamat!'),
(7, 'Mga dad onon sa pangaligo ug dagat', 'Kaugalingon, lawas ug kalag.'),
(11, 'Pundok Ila Lucas', 'budol fight'),
(12, 'Kaon', 'unya');

-- --------------------------------------------------------

--
-- Table structure for table `event`
--

CREATE TABLE `event` (
  `id` int(20) NOT NULL,
  `event_name` varchar(50) NOT NULL,
  `event_description` varchar(2000) NOT NULL,
  `event_date` date NOT NULL,
  `event_time` time NOT NULL,
  `attendees_list` varchar(2000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `event`
--

INSERT INTO `event` (`id`, `event_name`, `event_description`, `event_date`, `event_time`, `attendees_list`) VALUES
(1, 'Alumni Homecoming', 'Everyone is invited to the 87th grand homecoming. A lot of surprises aside from pa bingo and all.', '2023-12-21', '20:59:00', '1.Oninsama\r\n2.Koky\r\n3.Baldo'),
(2, 'Alumni Bingo', 'To celebrate the founding anniversary of CRMC, the alumni committee decide to have bingo games with many prizes.', '2023-12-27', '03:58:00', 'All alumni'),
(3, 'Banda2', 'Manganta tang tanan', '2023-12-15', '06:15:00', '1. Kamekaze\r\n2. Parokya ni Edgar\r\n3. Ben and Ben'),
(4, 'ccs night', 'secret', '2023-12-11', '18:40:00', 'bsit stud, faculty and members');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `alumni`
--
ALTER TABLE `alumni`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `announcement`
--
ALTER TABLE `announcement`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `event`
--
ALTER TABLE `event`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `alumni`
--
ALTER TABLE `alumni`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `announcement`
--
ALTER TABLE `announcement`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `event`
--
ALTER TABLE `event`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
