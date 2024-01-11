-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Jan 10, 2024 at 09:58 AM
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
(4, 'Leslie', 'Balabat', 'leslie', 'balabat', 'COMMERCE', '2024', 1, 'Teacher'),
(5, 'jesshuacutie', 'cutelang', 'hehehe', 'huhuhu', 'BSIT', '2023', 1, 'Wala'),
(6, 'Kaloy', 'Eh', 'kaloy', 'eh', 'BSCRIM', '2021', 1, 'IT personel'),
(7, 'Cresil', 'Rondina', 'cres', 'sil', 'BSED', '2024', 1, 'Teacher'),
(8, 'Lucas', 'Catadman', 'luc', 'cas', 'PSYCHOLOGY', '2024', 1, 'Councilor');

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
-- Table structure for table `announcement_comment`
--

CREATE TABLE `announcement_comment` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `announcement_id` int(11) NOT NULL,
  `comment` varchar(2000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

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
(4, 'ccs night', 'secret', '2023-12-11', '18:40:00', 'bsit stud, faculty and members'),
(5, 'Dinner Date', 'Dinner Date with Benry Olidiana', '2024-01-22', '19:31:00', 'wala');

-- --------------------------------------------------------

--
-- Table structure for table `event_comment`
--

CREATE TABLE `event_comment` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `event_id` int(11) NOT NULL,
  `comment` varchar(2000) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `event_comment`
--

INSERT INTO `event_comment` (`id`, `user_id`, `event_id`, `comment`) VALUES
(1, 6, 5, 'Asa isul ob ani admin?'),
(2, 6, 4, 'Puti isul ob?'),
(3, 6, 5, 'Naa benry'),
(4, 6, 5, 'Nag add ko ug comment'),
(5, 6, 3, 'Mokanta si neil chris?'),
(6, 6, 2, 'Nag add ug comment');

-- --------------------------------------------------------

--
-- Table structure for table `event_like`
--

CREATE TABLE `event_like` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `event_id` int(11) NOT NULL,
  `status` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `event_like`
--

INSERT INTO `event_like` (`id`, `user_id`, `event_id`, `status`) VALUES
(1, 6, 5, 0),
(2, 6, 4, 1),
(3, 6, 1, 1),
(4, 3, 4, 1),
(5, 3, 5, 1),
(6, 3, 2, 0),
(7, 3, 3, 1),
(8, 6, 2, 1),
(9, 6, 3, 1);

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
-- Indexes for table `announcement_comment`
--
ALTER TABLE `announcement_comment`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `event`
--
ALTER TABLE `event`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `event_comment`
--
ALTER TABLE `event_comment`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `event_like`
--
ALTER TABLE `event_like`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `alumni`
--
ALTER TABLE `alumni`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `announcement`
--
ALTER TABLE `announcement`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT for table `announcement_comment`
--
ALTER TABLE `announcement_comment`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT for table `event`
--
ALTER TABLE `event`
  MODIFY `id` int(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT for table `event_comment`
--
ALTER TABLE `event_comment`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT for table `event_like`
--
ALTER TABLE `event_like`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
