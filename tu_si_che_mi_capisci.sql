-- phpMyAdmin SQL Dump
-- version 5.0.4
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Creato il: Mar 25, 2022 alle 19:51
-- Versione del server: 10.4.17-MariaDB
-- Versione PHP: 8.0.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
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
  `Tipo` int(1) NOT NULL,
  `Ip` varchar(16) NOT NULL,
  `Acceso` tinyint(1) NOT NULL,
  `IdUtente` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `dispositivo`
--

INSERT INTO `dispositivo` (`Id`, `Nome`, `Tipo`, `Ip`, `Acceso`, `IdUtente`) VALUES
(1, 'luce', 1, '192.168.1.200', 1, 1),
(3, 'ventilatore', 2, '192.168.1.100', 1, 2),
(4, 'mic', 0, 'accendi', 1, 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `emozione`
--

CREATE TABLE `emozione` (
  `Id` int(11) NOT NULL,
  `Tipo` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `emozione`
--

INSERT INTO `emozione` (`Id`, `Tipo`) VALUES
(1, 'Arrabbiato'),
(2, 'Felice'),
(3, 'Triste'),
(4, 'Felice');

-- --------------------------------------------------------

--
-- Struttura della tabella `emozionetrovata`
--

CREATE TABLE `emozionetrovata` (
  `Id` int(11) NOT NULL,
  `IdEmozione` int(11) NOT NULL,
  `DataRilevazione` date NOT NULL,
  `Ora` time NOT NULL,
  `IdDispositivo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `emozionetrovata`
--

INSERT INTO `emozionetrovata` (`Id`, `IdEmozione`, `DataRilevazione`, `Ora`, `IdDispositivo`) VALUES
(1, 1, '2022-03-16', '00:00:00', 1),
(2, 4, '2022-03-16', '07:17:44', 3),
(3, 1, '2022-05-02', '00:00:00', 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `obiettivo`
--

CREATE TABLE `obiettivo` (
  `Id` int(11) NOT NULL,
  `Immagine` varchar(64) NOT NULL,
  `Nome` varchar(64) NOT NULL,
  `Descrizione` varchar(128) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `permesso`
--

CREATE TABLE `permesso` (
  `IdTrovato` int(11) NOT NULL,
  `IdVoltoRegistrato` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `quest`
--

CREATE TABLE `quest` (
  `Id` int(11) NOT NULL,
  `Concluso` tinyint(1) NOT NULL,
  `IdUtente` int(11) NOT NULL,
  `IdObiettivo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `skill`
--

CREATE TABLE `skill` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(64) NOT NULL,
  `Azione` varchar(64) NOT NULL,
  `Descrizione` varchar(128) NOT NULL,
  `IdUtente` int(11) NOT NULL,
  `IdEmozione` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `utente`
--

CREATE TABLE `utente` (
  `Id` int(11) NOT NULL,
  `Username` varchar(64) NOT NULL,
  `Password` varchar(256) NOT NULL,
  `Email` varchar(128) NOT NULL,
  `Immagine` varchar(64) NOT NULL,
  `Xp` int(8) NOT NULL,
  `ApiKey` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Dump dei dati per la tabella `utente`
--

INSERT INTO `utente` (`Id`, `Username`, `Password`, `Email`, `Immagine`, `Xp`, `ApiKey`) VALUES
(1, 'admin', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'lory.pina.03@gmail.com', '1.png', 50000, 'e8de33ae-0964-48b2-abfc-307c14c6d441'),
(2, 'pippo', 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3', 'pippo@gmail.com', '2.png', 100, 'e8de33ae-0964-48b2-abfc-307c14c6d481');

-- --------------------------------------------------------

--
-- Struttura della tabella `voltoregistrato`
--

CREATE TABLE `voltoregistrato` (
  `Id` int(11) NOT NULL,
  `Nome` varchar(64) NOT NULL,
  `Immagine` varchar(64) NOT NULL,
  `VettoreVolto` varchar(256) NOT NULL,
  `IdUtente` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Struttura della tabella `voltotrovato`
--

CREATE TABLE `voltotrovato` (
  `Id` int(11) NOT NULL,
  `DataRilevazione` date NOT NULL,
  `Ora` time NOT NULL,
  `Immagine` varchar(64) NOT NULL,
  `VettoreImmagini` varchar(256) NOT NULL,
  `IdDispositivo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `dispositivo`
--
ALTER TABLE `dispositivo`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk5` (`IdUtente`);

--
-- Indici per le tabelle `emozione`
--
ALTER TABLE `emozione`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `emozionetrovata`
--
ALTER TABLE `emozionetrovata`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk6` (`IdDispositivo`),
  ADD KEY `fk7` (`IdEmozione`);

--
-- Indici per le tabelle `obiettivo`
--
ALTER TABLE `obiettivo`
  ADD PRIMARY KEY (`Id`);

--
-- Indici per le tabelle `permesso`
--
ALTER TABLE `permesso`
  ADD PRIMARY KEY (`IdVoltoRegistrato`,`IdTrovato`) USING BTREE,
  ADD KEY `fk3` (`IdTrovato`);

--
-- Indici per le tabelle `quest`
--
ALTER TABLE `quest`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk10` (`IdUtente`),
  ADD KEY `fk11` (`IdObiettivo`);

--
-- Indici per le tabelle `skill`
--
ALTER TABLE `skill`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk8` (`IdUtente`),
  ADD KEY `fk9` (`IdEmozione`);

--
-- Indici per le tabelle `utente`
--
ALTER TABLE `utente`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `ApiKey` (`ApiKey`),
  ADD UNIQUE KEY `Username` (`Username`);

--
-- Indici per le tabelle `voltoregistrato`
--
ALTER TABLE `voltoregistrato`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk4` (`IdUtente`);

--
-- Indici per le tabelle `voltotrovato`
--
ALTER TABLE `voltotrovato`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk1` (`IdDispositivo`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `dispositivo`
--
ALTER TABLE `dispositivo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT per la tabella `emozione`
--
ALTER TABLE `emozione`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT per la tabella `emozionetrovata`
--
ALTER TABLE `emozionetrovata`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT per la tabella `obiettivo`
--
ALTER TABLE `obiettivo`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `quest`
--
ALTER TABLE `quest`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `skill`
--
ALTER TABLE `skill`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `utente`
--
ALTER TABLE `utente`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=35;

--
-- AUTO_INCREMENT per la tabella `voltoregistrato`
--
ALTER TABLE `voltoregistrato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `voltotrovato`
--
ALTER TABLE `voltotrovato`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `dispositivo`
--
ALTER TABLE `dispositivo`
  ADD CONSTRAINT `fk5` FOREIGN KEY (`IdUtente`) REFERENCES `utente` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `emozionetrovata`
--
ALTER TABLE `emozionetrovata`
  ADD CONSTRAINT `fk6` FOREIGN KEY (`IdDispositivo`) REFERENCES `dispositivo` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk7` FOREIGN KEY (`IdEmozione`) REFERENCES `emozione` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `permesso`
--
ALTER TABLE `permesso`
  ADD CONSTRAINT `fk2` FOREIGN KEY (`IdVoltoRegistrato`) REFERENCES `voltoregistrato` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk3` FOREIGN KEY (`IdTrovato`) REFERENCES `voltotrovato` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `quest`
--
ALTER TABLE `quest`
  ADD CONSTRAINT `fk10` FOREIGN KEY (`IdUtente`) REFERENCES `utente` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk11` FOREIGN KEY (`IdObiettivo`) REFERENCES `obiettivo` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `skill`
--
ALTER TABLE `skill`
  ADD CONSTRAINT `fk8` FOREIGN KEY (`IdUtente`) REFERENCES `utente` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `fk9` FOREIGN KEY (`IdEmozione`) REFERENCES `emozione` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `voltoregistrato`
--
ALTER TABLE `voltoregistrato`
  ADD CONSTRAINT `fk4` FOREIGN KEY (`IdUtente`) REFERENCES `utente` (`Id`) ON UPDATE CASCADE;

--
-- Limiti per la tabella `voltotrovato`
--
ALTER TABLE `voltotrovato`
  ADD CONSTRAINT `fk1` FOREIGN KEY (`IdDispositivo`) REFERENCES `dispositivo` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
