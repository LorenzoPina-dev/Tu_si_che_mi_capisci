-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Mar 24, 2022 alle 09:52
-- Versione del server: 10.4.6-MariaDB
-- Versione PHP: 7.3.8

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `tu_si_che_mi_capisci`
--

-- --------------------------------------------------------

--
-- Struttura della tabella `dispositivo`
--

CREATE TABLE `dispositivo` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(64) NOT NULL,
  `Tipo` tinyint(1) NOT NULL,
  `Ip` int(15) NOT NULL,
  `Acceso` tinyint(1) NOT NULL,
  `IdUtente` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `dispositivo`
--

INSERT INTO `dispositivo` (`Id`, `Nome`, `Tipo`, `Ip`, `Acceso`, `IdUtente`) VALUES
(1, 'disp1', 1, 172168, 1, 6),
(2, 'disp2', 1, 172165, 1, 6);

-- --------------------------------------------------------

--
-- Struttura della tabella `emozionetrovata`
--

CREATE TABLE `emozionetrovata` (
  `Id` int(11) NOT NULL,
  `DataRilevazione` date NOT NULL,
  `Ora` time NOT NULL,
  `IdDispositivo` int(11) NOT NULL,
  `IdEmozione` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `emozionetrovata`
--

INSERT INTO `emozionetrovata` (`Id`, `DataRilevazione`, `Ora`, `IdDispositivo`, `IdEmozione`) VALUES
(1, '2022-03-24', '08:29:00', 2, 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `permesso`
--

CREATE TABLE `permesso` (
  `IdVoltoTrovato` int(11) NOT NULL,
  `IdVoltoRegistrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `permesso`
--

INSERT INTO `permesso` (`IdVoltoTrovato`, `IdVoltoRegistrato`) VALUES
(2, 4);

-- --------------------------------------------------------

--
-- Struttura della tabella `skill`
--

CREATE TABLE `skill` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(64) NOT NULL,
  `Azione` varchar(128) NOT NULL,
  `Descrizione` varchar(128) NOT NULL,
  `IdUtente` int(11) NOT NULL,
  `IdEmozione` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Struttura della tabella `utente`
--

CREATE TABLE `utente` (
  `Id` int(11) NOT NULL,
  `Username` varchar(11) NOT NULL,
  `Password` varchar(256) NOT NULL,
  `Email` varchar(40) NOT NULL,
  `Immagine` varchar(50) NOT NULL,
  `Xp` int(4) NOT NULL,
  `ApiKey` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `utente`
--

INSERT INTO `utente` (`Id`, `Username`, `Password`, `Email`, `Immagine`, `Xp`, `ApiKey`) VALUES
(1, 'admin', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'lory.pina.03@gmail.com', '1.png', 0, 'a2444840-cb9a-479d-bd3f-a4fa4a2f23f8'),
(6, 'pippo', 'f8638b979b2f4f793ddb6dbd197e0ee25a7a6ea32b0ae22f5e3c5d119d839e75', 'buongiorno@gmail.png', '', 0, '9d3a6a1f-45f3-4144-9d50-b90bfce7c90a');

-- --------------------------------------------------------

--
-- Struttura della tabella `voltoregistrato`
--

CREATE TABLE `voltoregistrato` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(32) NOT NULL,
  `Immagine` varchar(128) NOT NULL,
  `IdUtente` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `voltoregistrato`
--

INSERT INTO `voltoregistrato` (`Id`, `Nome`, `Immagine`, `IdUtente`) VALUES
(1, 'gianni', 'R:/Documents/Immagini/Screenshots/img.jpg', 6);

-- --------------------------------------------------------

--
-- Struttura della tabella `voltotrovato`
--

CREATE TABLE `voltotrovato` (
  `Id` int(11) NOT NULL,
  `DataRilevazione` date NOT NULL,
  `Ora` time NOT NULL,
  `Immagine` varchar(128) NOT NULL,
  `IdDispositivo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dump dei dati per la tabella `voltotrovato`
--

INSERT INTO `voltotrovato` (`Id`, `DataRilevazione`, `Ora`, `Immagine`, `IdDispositivo`) VALUES
(1, '0000-00-00', '08:29:00', 'R:/Documents/Immagini/Screenshots/img.jpg', 2),
(2, '2022-03-24', '08:29:00', 'R:/Documents/Immagini/Screenshots/img.jpg', 2);

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `dispositivo`
--
ALTER TABLE `dispositivo`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `emozionetrovata`
--
ALTER TABLE `emozionetrovata`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `permesso`
--
ALTER TABLE `permesso`
  ADD PRIMARY KEY (`IdVoltoTrovato`,`IdVoltoRegistrato`);

--
-- Indici per le tabelle `utente`
--
ALTER TABLE `utente`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `Username` (`Username`),
  ADD UNIQUE KEY `ApiKey` (`ApiKey`);

--
-- Indici per le tabelle `voltoregistrato`
--
ALTER TABLE `voltoregistrato`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `voltotrovato`
--
ALTER TABLE `voltotrovato`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `dispositivo`
--
ALTER TABLE `dispositivo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT per la tabella `emozionetrovata`
--
ALTER TABLE `emozionetrovata`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT per la tabella `utente`
--
ALTER TABLE `utente`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT per la tabella `voltoregistrato`
--
ALTER TABLE `voltoregistrato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT per la tabella `voltotrovato`
--
ALTER TABLE `voltotrovato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
