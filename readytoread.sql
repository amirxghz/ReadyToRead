-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1:3306
-- Versione del server: 8.2.0
-- Versione PHP: 8.3.0
--
-- FIX applicati rispetto alla versione originale:
-- 1. prodotti.nome: int -> varchar(255)
-- 2. libri.path_copertina e path_file: blob -> varchar(500) (salvano il path, non il file)
-- 3. libri: aggiunte colonne edizione, lingua, anno_pubblicazione
-- 4. libri: isbn non puo' essere PK se piu' copie condividono lo stesso ISBN;
--           aggiunto campo ID AUTO_INCREMENT come PK, isbn diventa campo normale
-- 5. autori: aggiunta colonna citta
-- 6. generare: FK generare_autore corretagiusta (puntava a autori.ID usando adminID)
-- 7. ordinare: FK ordine_cliente corretta (puntava a utenti.ID, ora a clienti.ID)
-- 8. admins: aggiunta FK verso utenti

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";
SET FOREIGN_KEY_CHECKS = 0;

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

-- Database: `readytoread`

-- --------------------------------------------------------
-- utenti (creata prima perche' referenziata da quasi tutte)
-- --------------------------------------------------------
CREATE TABLE `utenti` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `cognome` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `comune_nascita` enum('Jesi','Ancona','Chiaravalle','Senigallia','Rotella','Nessuno') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `data_nascita` date DEFAULT NULL,
  `genere` char(1) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `username` varchar(25) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `password` char(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `email` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `foto_profilo` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  PRIMARY KEY (`ID`),
  UNIQUE KEY `username` (`username`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `utenti` (`ID`, `nome`, `cognome`, `comune_nascita`, `data_nascita`, `genere`, `username`, `password`, `email`, `foto_profilo`) VALUES
(1, '', '', 'Chiaravalle', '2025-11-13', 'f', '', 'e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855', '', ''),
(2, 'ewrew', 'erwrr', 'Nessuno', '2025-11-01', 'm', 'ciccio', 'c23a9f0b40aa39799d80255487ec8f167dc4e6aef5155ba429f6e5192d3e7deb', 'ewrwe@', NULL);

-- --------------------------------------------------------
-- admins
-- --------------------------------------------------------
CREATE TABLE `admins` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `utenteID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `utenteID` (`utenteID`),
  CONSTRAINT `admins_utente` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- autori (FIX: aggiunta colonna citta)
-- --------------------------------------------------------
CREATE TABLE `autori` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `verificato` tinyint(1) NOT NULL,
  `nome_arte` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `data_morte` date DEFAULT NULL,
  `citta` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `utenteID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `utenteID` (`utenteID`) USING BTREE,
  CONSTRAINT `autori_utente` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- clienti
-- --------------------------------------------------------
CREATE TABLE `clienti` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `indirizzo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `cap` char(5) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `utenteID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `utenteID` (`utenteID`),
  CONSTRAINT `clientiID` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

INSERT INTO `clienti` (`ID`, `indirizzo`, `cap`, `utenteID`) VALUES
(1, '', '', 1),
(2, '', '', 2);

-- --------------------------------------------------------
-- houses (case editrici)
-- --------------------------------------------------------
CREATE TABLE `houses` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `indirizzo_sede_legale` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `indirizzo_sede_operativo` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `tipo_azienda` enum('SRL','SNC','SAS','SPA') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `ragione_sociale` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `esclusiva` tinyint(1) NOT NULL,
  `tipologia` enum('editrice') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `utenteID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `utenteID` (`utenteID`),
  CONSTRAINT `houses_utente` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- super_admins
-- --------------------------------------------------------
CREATE TABLE `super_admins` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `utenteID` int NOT NULL,
  `adminID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `utenteID` (`utenteID`),
  KEY `adminID` (`adminID`),
  CONSTRAINT `super_adminsID` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `super_utentiID` FOREIGN KEY (`utenteID`) REFERENCES `utenti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- generi
-- --------------------------------------------------------
CREATE TABLE `generi` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `descrizione` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `target` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `tipologia` enum('narrativo','musicale') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci AUTO_INCREMENT=15;

INSERT INTO `generi` (`ID`, `nome`, `descrizione`, `target`, `tipologia`) VALUES
(1, 'Giallo', 'incentrato sulla risoluzione di un enigma o crimine (delitto, furto, mistero) attraverso le indagini di un detective o investigatore', '14+', 'narrativo'),
(2, 'Distopico', 'descrive una società futura, immaginaria o alternativa, organizzata in modo repressivo, totalitario o spaventoso', 'Giovani Adulti', 'narrativo'),
(4, 'Psicologico', 'si concentra sull\'esplorazione profonda della mente, delle emozioni e dei meccanismi interiori dei personaggi, piuttosto che sull\'azione esterna', 'Giovani Adulti', 'narrativo'),
(5, 'Battle', 'incentrato su combattimenti spettacolari, crescita dei personaggi e il classico "viaggio dell\'eroe"', 'Shoenen', 'narrativo'),
(6, 'Romantico', 'incentrato su relazioni amorose, crescita emotiva e vita scolastica, si distingue per uno stile artistico elegante, introspettivo ed emozionale, spesso focalizzato sulle insicurezze dei primi amori', 'Shojo', 'narrativo'),
(7, 'Demenziale', 'forma di comicità basata sull\'assurdo, il nonsense e la parodia estrema, caratterizzata da un\'ironia dissacrante che stravolge le regole logiche per creare risate', 'Kodomo', 'narrativo'),
(9, 'Storico', 'un tipo di racconto, romanzo o film ambientato in un\'epoca passata, che ricostruisce fedelmente atmosfere, usi e costumi di quel tempo', '14+', 'narrativo'),
(10, 'Fantasy', 'genere narrativo ambientato in mondi immaginari, caratterizzato da forti elementi magici, creature mitologiche e la lotta tra bene e male', 'Tutte le età', 'narrativo'),
(11, 'Fantascientifico', 'filone narrativo, sviluppatosi principalmente nel XX secolo, che esplora l\'impatto della scienza, della tecnologia e di futuri ipotetici sulla società e sull\'individuo', 'Tutte le età', 'narrativo'),
(12, 'Dark Fantasy', 'sottogenere del fantasy che fonde ambientazioni magiche e soprannaturali con temi oscuri, macabri e horror', 'Giovani Adulti', 'narrativo'),
(13, 'Horror', 'mira a suscitare nel lettore paura, ansia, inquietudine o disgusto attraverso storie che esplorano il lato oscuro, il soprannaturale, la morte o il male', 'Giovani Adulti', 'narrativo'),
(14, 'Politico', '', 'Giovani Adulti', 'narrativo');

-- --------------------------------------------------------
-- prodotti (FIX: nome era int -> ora varchar(255))
-- --------------------------------------------------------
CREATE TABLE `prodotti` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `nome` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `stato_disponibilita` enum('disponibile','preordine','esaurito','') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `prezzo` decimal(10,2) NOT NULL,
  `descrizione` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- libri
-- FIX 1: path_copertina e path_file: blob -> varchar(500) (si salva il path del file)
-- FIX 2: aggiunge colonne edizione, lingua, anno_pubblicazione
-- FIX 3: isbn non e' piu' PRIMARY KEY (piu' copie fisiche condividono lo stesso ISBN);
--         aggiunto ID AUTO_INCREMENT come PK; isbn diventa campo normale con indice
-- --------------------------------------------------------
CREATE TABLE `libri` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `isbn` char(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `path_copertina` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `numero_pagine` smallint NOT NULL,
  `sinossi` text CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci,
  `path_file` varchar(500) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `tipo` enum('fisico','ebook') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `edizione` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `lingua` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `anno_pubblicazione` date DEFAULT NULL,
  `prodottoID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `isbn` (`isbn`),
  KEY `prodottoID` (`prodottoID`),
  CONSTRAINT `libro_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- gift_cards
-- --------------------------------------------------------
CREATE TABLE `gift_cards` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `nome_destinatario` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `dedica` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `prodottoID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `prodottoID` (`prodottoID`),
  CONSTRAINT `card_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- caratterizzare: libro <-> genere
-- --------------------------------------------------------
CREATE TABLE `caratterizzare` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `libroISBN` char(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `genereID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `genereID` (`genereID`),
  KEY `libroISBN` (`libroISBN`),
  CONSTRAINT `genereID` FOREIGN KEY (`genereID`) REFERENCES `generi` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `libroISBN` FOREIGN KEY (`libroISBN`) REFERENCES `libri` (`isbn`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- censire: admin <-> genere
-- --------------------------------------------------------
CREATE TABLE `censire` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `data` date NOT NULL,
  `adminID` int NOT NULL,
  `genereID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `adminID` (`adminID`),
  KEY `genereID` (`genereID`),
  CONSTRAINT `censire_admin` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `censire_genere` FOREIGN KEY (`genereID`) REFERENCES `generi` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- controllare: admin <-> prodotto
-- --------------------------------------------------------
CREATE TABLE `controllare` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `data` date NOT NULL,
  `adminID` int NOT NULL,
  `prodottoID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `adminID` (`adminID`),
  KEY `prodottoID` (`prodottoID`),
  CONSTRAINT `controllare_admin` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `controllare_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- generare: autore <-> admin
-- FIX: la FK originale "generare_autore" puntava ad autori.ID usando adminID (sbagliato)
-- --------------------------------------------------------
CREATE TABLE `generare` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `data` date NOT NULL,
  `autoreID` int NOT NULL,
  `adminID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `autoreID` (`autoreID`),
  KEY `adminID` (`adminID`),
  CONSTRAINT `generare_autore` FOREIGN KEY (`autoreID`) REFERENCES `autori` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `generare_admins` FOREIGN KEY (`adminID`) REFERENCES `admins` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- ordinare: cliente <-> prodotto
-- FIX: ordine_cliente puntava a utenti.ID, ora punta correttamente a clienti.ID
-- --------------------------------------------------------
CREATE TABLE `ordinare` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `stato_ordine` enum('arrivato','spedito','non spedito','') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `totale` decimal(10,2) NOT NULL,
  `indirizzo_destinazione` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `data` date NOT NULL,
  `clienteID` int NOT NULL,
  `prodottoID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `prodottoID` (`prodottoID`),
  KEY `clienteID` (`clienteID`),
  CONSTRAINT `ordine_cliente` FOREIGN KEY (`clienteID`) REFERENCES `clienti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `ordine_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- pubblicare: casa editrice <-> libro
-- --------------------------------------------------------
CREATE TABLE `pubblicare` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `anno_pubblicazione` date NOT NULL,
  `edizione` varchar(80) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `casa_editriceID` int NOT NULL,
  `libroISBN` varchar(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `casa_editriceID` (`casa_editriceID`),
  KEY `libroID` (`libroISBN`),
  CONSTRAINT `pubblicare_houses` FOREIGN KEY (`casa_editriceID`) REFERENCES `houses` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `pubblicare_libro` FOREIGN KEY (`libroISBN`) REFERENCES `libri` (`isbn`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- recensire: cliente <-> prodotto
-- --------------------------------------------------------
CREATE TABLE `recensire` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `titolo` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `descrizione` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci DEFAULT NULL,
  `valutazione` enum('1','2','3','4','5') CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  `data_pubblicazione` date NOT NULL,
  `clienteID` int NOT NULL,
  `prodottoID` int NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `clienteID` (`clienteID`),
  KEY `prodottoID` (`prodottoID`),
  CONSTRAINT `recensione_cliente` FOREIGN KEY (`clienteID`) REFERENCES `clienti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `recensione_prodotto` FOREIGN KEY (`prodottoID`) REFERENCES `prodotti` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------
-- scrivere: autore <-> libro
-- --------------------------------------------------------
CREATE TABLE `scrivere` (
  `ID` int NOT NULL AUTO_INCREMENT,
  `data` date NOT NULL,
  `autoreID` int NOT NULL,
  `libroISBN` char(13) CHARACTER SET utf8mb4 COLLATE utf8mb4_general_ci NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `autoreID` (`autoreID`),
  KEY `libroISBN` (`libroISBN`),
  CONSTRAINT `scrivere_autore` FOREIGN KEY (`autoreID`) REFERENCES `autori` (`ID`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `scrivere_libri` FOREIGN KEY (`libroISBN`) REFERENCES `libri` (`isbn`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

SET FOREIGN_KEY_CHECKS = 1;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
