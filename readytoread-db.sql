-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Creato il: Mag 17, 2026 alle 19:10
-- Versione del server: 8.2.0
-- Versione PHP: 8.3.0

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `readytoread`
--

-- --------------------------------------------------------

--
-- Struttura della tabella `admins`
--

CREATE TABLE `admins` (
  `ID` int NOT NULL,
  `utenteID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `autori`
--

CREATE TABLE `autori` (
  `ID` int NOT NULL,
  `verificato` tinyint(1) NOT NULL,
  `nome_arte` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `data_morte` date DEFAULT NULL,
  `utenteID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `caratterizzare`
--

CREATE TABLE `caratterizzare` (
  `ID` int NOT NULL,
  `libroISBN` char(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `genereID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3;

-- --------------------------------------------------------

--
-- Struttura della tabella `censire`
--

CREATE TABLE `censire` (
  `ID` int NOT NULL,
  `data` date NOT NULL,
  `adminID` int NOT NULL,
  `genereID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `clienti`
--

CREATE TABLE `clienti` (
  `ID` int NOT NULL,
  `indirizzo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `cap` char(5) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `utenteID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dump dei dati per la tabella `clienti`
--

INSERT INTO `clienti` (`ID`, `indirizzo`, `cap`, `utenteID`) VALUES
(1, '', '', 1);

-- --------------------------------------------------------

--
-- Struttura della tabella `controllare`
--

CREATE TABLE `controllare` (
  `ID` int NOT NULL,
  `data` date NOT NULL,
  `adminID` int NOT NULL,
  `prodottoID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `generare`
--

CREATE TABLE `generare` (
  `ID` int NOT NULL,
  `data` date NOT NULL,
  `autoreID` int NOT NULL,
  `adminID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `generi`
--

CREATE TABLE `generi` (
  `ID` int NOT NULL,
  `nome` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `descrizione` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `target` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `tipologia` enum('narrativo','musicale') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dump dei dati per la tabella `generi`
--

INSERT INTO `generi` (`ID`, `nome`, `descrizione`, `target`, `tipologia`) VALUES
(1, 'Giallo', 'incentrato sulla risoluzione di un enigma o crimine (delitto, furto, mistero) attraverso le indagini di un detective o investigatore', '14+', 'narrativo'),
(2, 'Distopico', 'descrive una società futura, immaginaria o alternativa, organizzata in modo repressivo, totalitario o spaventoso', 'Giovani Adulti', 'narrativo'),
(4, 'Psicologico', 'si concentra sull\'esplorazione profonda della mente, delle emozioni e dei meccanismi interiori dei personaggi, piuttosto che sull\'azione esterna', 'Giovani Adulti', 'narrativo'),
(5, 'Battle', 'incentrato su combattimenti spettacolari, crescita dei personaggi e il classico \"viaggio dell\'eroe\"', 'Shoenen', 'narrativo'),
(6, 'Romantico', 'incentrato su relazioni amorose, crescita emotiva e vita scolastica, si distingue per uno stile artistico elegante, introspettivo ed emozionale, spesso focalizzato sulle insicurezze dei primi amori', 'Shojo', 'narrativo'),
(7, 'Demenziale', 'forma di comicità basata sull\'assurdo, il nonsense e la parodia estrema, caratterizzata da un\'ironia dissacrante che stravolge le regole logiche per creare risate', 'Kodomo', 'narrativo'),
(9, 'Storico', 'un tipo di racconto, romanzo o film ambientato in un\'epoca passata, che ricostruisce fedelmente atmosfere, usi e costumi di quel tempo', '14+', 'narrativo'),
(10, 'Fantasy', 'genere narrativo ambientato in mondi immaginari, caratterizzato da forti elementi magici, creature mitologiche e la lotta tra bene e male', 'Tutte le età', 'narrativo'),
(11, 'Fantascientifico', 'filone narrativo, sviluppatosi principalmente nel XX secolo, che esplora l\'impatto della scienza, della tecnologia e di futuri ipotetici sulla società e sull\'individuo', 'Tutte le età', 'narrativo'),
(12, 'Dark Fantasy', 'sottogenere del fantasy che fonde ambientazioni magiche e soprannaturali con temi oscuri, macabri e horror', 'Giovani Adulti', 'narrativo'),
(13, 'Horror', 'mira a suscitare nel lettore paura, ansia, inquietudine o disgusto attraverso storie che esplorano il lato oscuro, il soprannaturale, la morte o il male', 'Giovani Adulti', 'narrativo'),
(14, 'Politico', '', 'Giovani Adulti', 'narrativo');

-- --------------------------------------------------------

--
-- Struttura della tabella `gift_cards`
--

CREATE TABLE `gift_cards` (
  `ID` int NOT NULL,
  `nome_destinatario` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `dedica` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `prodottoID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `houses`
--

CREATE TABLE `houses` (
  `ID` int NOT NULL,
  `indirizzo_sede_legale` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `indirizzo_sede_operativo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `tipo_azienda` enum('SRL','SNC','SAS','SPA') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ragione_sociale` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `esclusiva` tinyint(1) NOT NULL,
  `tipologia` enum('editrice') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `utenteID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `libri`
--

CREATE TABLE `libri` (
  `isbn` char(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `path_copertina` blob,
  `numero_pagine` smallint NOT NULL,
  `sinossi` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `path_file` blob,
  `tipo` enum('fisico','ebook') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `prodottoID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `ordinare`
--

CREATE TABLE `ordinare` (
  `ID` int NOT NULL,
  `stato_ordine` enum('arrivato','spedito','non spedito','') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `totale` decimal(10,0) NOT NULL,
  `indirizzo_destinazione` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `data` date NOT NULL,
  `clienteID` int NOT NULL,
  `prodottoID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `prodotti`
--

CREATE TABLE `prodotti` (
  `ID` int NOT NULL,
  `nome` int NOT NULL,
  `stato_disponibilita` enum('disponibile','preordine','esaurito','') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `prezzo` decimal(10,0) NOT NULL,
  `descrizione` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `pubblicare`
--

CREATE TABLE `pubblicare` (
  `ID` int NOT NULL,
  `anno_pubblicazione` date NOT NULL,
  `edizione` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `casa_editriceID` int NOT NULL,
  `libroISBN` varchar(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `recensire`
--

CREATE TABLE `recensire` (
  `ID` int NOT NULL,
  `titolo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `descrizione` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `valutazione` enum('1','2','3','4','5') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `data_pubblicazione` date NOT NULL,
  `clienteID` int NOT NULL,
  `prodottoID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `scrivere`
--

CREATE TABLE `scrivere` (
  `ID` int NOT NULL,
  `data` date NOT NULL,
  `autoreID` int NOT NULL,
  `libroISBN` char(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `super_admins`
--

CREATE TABLE `super_admins` (
  `ID` int NOT NULL,
  `utenteID` int NOT NULL,
  `adminID` int NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Struttura della tabella `utenti`
--

CREATE TABLE `utenti` (
  `ID` int NOT NULL,
  `nome` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `cognome` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `comune_nascita` enum('Jesi','Ancona','Chiaravalle','Senigallia','Rotella') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `data_nascita` date DEFAULT NULL,
  `genere` char(1) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `username` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `password` char(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `foto_profilo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dump dei dati per la tabella `utenti`
--

INSERT INTO `utenti` (`ID`, `nome`, `cognome`, `comune_nascita`, `data_nascita`, `genere`, `username`, `password`, `email`, `foto_profilo`) VALUES
(1, '', '', 'Chiaravalle', '2025-11-13', 'f', '', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', '', '');

--
-- Indici per le tabelle scaricate
--

--
-- Indici per le tabelle `admins`
--
ALTER TABLE `admins`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `utenteID` (`utenteID`);

--
-- Indici per le tabelle `autori`
--
ALTER TABLE `autori`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `utenteID` (`utenteID`) USING BTREE;

--
-- Indici per le tabelle `caratterizzare`
--
ALTER TABLE `caratterizzare`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `genereID` (`genereID`),
  ADD KEY `libroISBN` (`libroISBN`);

--
-- Indici per le tabelle `censire`
--
ALTER TABLE `censire`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `adminID` (`adminID`),
  ADD KEY `genereID` (`genereID`);

--
-- Indici per le tabelle `clienti`
--
ALTER TABLE `clienti`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `utenteID` (`utenteID`);

--
-- Indici per le tabelle `controllare`
--
ALTER TABLE `controllare`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `adminID` (`adminID`),
  ADD KEY `prodottoID` (`prodottoID`);

--
-- Indici per le tabelle `generare`
--
ALTER TABLE `generare`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `autoreID` (`autoreID`),
  ADD KEY `adminID` (`adminID`);

--
-- Indici per le tabelle `generi`
--
ALTER TABLE `generi`
  ADD PRIMARY KEY (`ID`);

--
-- Indici per le tabelle `gift_cards`
--
ALTER TABLE `gift_cards`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `prodottoID` (`prodottoID`);

--
-- Indici per le tabelle `houses`
--
ALTER TABLE `houses`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `utenteID` (`utenteID`);

--
-- Indici per le tabelle `libri`
--
ALTER TABLE `libri`
  ADD PRIMARY KEY (`isbn`),
  ADD KEY `prodottoID` (`prodottoID`);

--
-- Indici per le tabelle `ordinare`
--
ALTER TABLE `ordinare`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `prodottoID` (`prodottoID`),
  ADD KEY `clienteID` (`clienteID`);

--
-- Indici per le tabelle `prodotti`
--
ALTER TABLE `prodotti`
  ADD PRIMARY KEY (`ID`);

--
-- Indici per le tabelle `pubblicare`
--
ALTER TABLE `pubblicare`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `casa_editriceID` (`casa_editriceID`),
  ADD KEY `libroID` (`libroISBN`);

--
-- Indici per le tabelle `recensire`
--
ALTER TABLE `recensire`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `clienteID` (`clienteID`),
  ADD KEY `prodottoID` (`prodottoID`);

--
-- Indici per le tabelle `scrivere`
--
ALTER TABLE `scrivere`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `autoreID` (`autoreID`),
  ADD KEY `libroISBN` (`libroISBN`);

--
-- Indici per le tabelle `super_admins`
--
ALTER TABLE `super_admins`
  ADD PRIMARY KEY (`ID`),
  ADD KEY `utenteID` (`utenteID`),
  ADD KEY `adminID` (`adminID`);

--
-- Indici per le tabelle `utenti`
--
ALTER TABLE `utenti`
  ADD PRIMARY KEY (`ID`),
  ADD UNIQUE KEY `username` (`username`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT per le tabelle scaricate
--

--
-- AUTO_INCREMENT per la tabella `admins`
--
ALTER TABLE `admins`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `autori`
--
ALTER TABLE `autori`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `caratterizzare`
--
ALTER TABLE `caratterizzare`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `censire`
--
ALTER TABLE `censire`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `clienti`
--
ALTER TABLE `clienti`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT per la tabella `controllare`
--
ALTER TABLE `controllare`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `generare`
--
ALTER TABLE `generare`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `generi`
--
ALTER TABLE `generi`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=15;

--
-- AUTO_INCREMENT per la tabella `gift_cards`
--
ALTER TABLE `gift_cards`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `houses`
--
ALTER TABLE `houses`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `ordinare`
--
ALTER TABLE `ordinare`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `prodotti`
--
ALTER TABLE `prodotti`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `pubblicare`
--
ALTER TABLE `pubblicare`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `recensire`
--
ALTER TABLE `recensire`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `scrivere`
--
ALTER TABLE `scrivere`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `super_admins`
--
ALTER TABLE `super_admins`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT per la tabella `utenti`
--
ALTER TABLE `utenti`
  MODIFY `ID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Limiti per le tabelle scaricate
--

--
-- Limiti per la tabella `caratterizzare`
--
ALTER TABLE `caratterizzare`
  ADD CONSTRAINT `genereID` FOREIGN KEY (`genereID`) REFERENCES `generi` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `libroISBN` FOREIGN KEY (`libroISBN`) REFERENCES `libri` (`isbn`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `censire`
--
ALTER TABLE `censire`
  ADD CONSTRAINT `censire_admin` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `censire_genere` FOREIGN KEY (`genereID`) REFERENCES `generi` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `clienti`
--
ALTER TABLE `clienti`
  ADD CONSTRAINT `clientiID` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `controllare`
--
ALTER TABLE `controllare`
  ADD CONSTRAINT `controllare_admin` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `controllare_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `generare`
--
ALTER TABLE `generare`
  ADD CONSTRAINT `generare_admins` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  ADD CONSTRAINT `generare_autore` FOREIGN KEY (`adminID`) REFERENCES `autori` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `gift_cards`
--
ALTER TABLE `gift_cards`
  ADD CONSTRAINT `card_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `houses`
--
ALTER TABLE `houses`
  ADD CONSTRAINT `houses_utente` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `libri`
--
ALTER TABLE `libri`
  ADD CONSTRAINT `libro_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `ordinare`
--
ALTER TABLE `ordinare`
  ADD CONSTRAINT `ordine_cliente` FOREIGN KEY (`clienteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `ordine_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `pubblicare`
--
ALTER TABLE `pubblicare`
  ADD CONSTRAINT `pubblicare_houses` FOREIGN KEY (`casa_editriceID`) REFERENCES `houses` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `pubblicare_libro` FOREIGN KEY (`libroISBN`) REFERENCES `libri` (`isbn`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `recensire`
--
ALTER TABLE `recensire`
  ADD CONSTRAINT `recensione_cliente` FOREIGN KEY (`clienteID`) REFERENCES `clienti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `recensione_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `scrivere`
--
ALTER TABLE `scrivere`
  ADD CONSTRAINT `scrivere_autore` FOREIGN KEY (`autoreID`) REFERENCES `autori` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `scrivere_libri` FOREIGN KEY (`libroISBN`) REFERENCES `libri` (`isbn`) ON DELETE CASCADE ON UPDATE CASCADE;

--
-- Limiti per la tabella `super_admins`
--
ALTER TABLE `super_admins`
  ADD CONSTRAINT `super_adminsID` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  ADD CONSTRAINT `super_utentiID` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
