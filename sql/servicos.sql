-- MySQL dump 10.13  Distrib 8.0.18, for Win64 (x86_64)
--
-- Host: localhost    Database: serviços
-- ------------------------------------------------------
-- Server version	8.0.18

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `clientes`
--

DROP TABLE IF EXISTS `clientes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `clientes` (
  `Id_Clientes` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(45) NOT NULL,
  `Nif` varchar(45) NOT NULL,
  `Morada` varchar(45) NOT NULL,
  `Telefone` varchar(15) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Id_Conta` int(11) NOT NULL,
  PRIMARY KEY (`Id_Clientes`),
  KEY `fk_Clientes_Login1_idx` (`Id_Conta`),
  CONSTRAINT `fk_Clientes_Login1` FOREIGN KEY (`Id_Conta`) REFERENCES `login` (`Id_Conta`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `clientes`
--

LOCK TABLES `clientes` WRITE;
/*!40000 ALTER TABLE `clientes` DISABLE KEYS */;
INSERT INTO `clientes` VALUES (3,'Diogo','123456789','Braga','9654321678','diogo@gmail.com',5);
/*!40000 ALTER TABLE `clientes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `empresas`
--

DROP TABLE IF EXISTS `empresas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `empresas` (
  `Id_Empresa` int(11) NOT NULL AUTO_INCREMENT,
  `Nome` varchar(45) NOT NULL,
  `Nif` varchar(45) NOT NULL,
  `Morada` varchar(45) NOT NULL,
  `Telefone` varchar(15) NOT NULL,
  `Email` varchar(45) NOT NULL,
  `Id_Conta` int(11) NOT NULL,
  PRIMARY KEY (`Id_Empresa`),
  KEY `fk_Empresas_Login1_idx` (`Id_Conta`),
  CONSTRAINT `fk_Empresas_Login1` FOREIGN KEY (`Id_Conta`) REFERENCES `login` (`Id_Conta`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `empresas`
--

LOCK TABLES `empresas` WRITE;
/*!40000 ALTER TABLE `empresas` DISABLE KEYS */;
INSERT INTO `empresas` VALUES (5,'EDP','123456789','Braga','808 535 353','edp@gmail.com',6),(6,'houseshine','123456789','Braga','1234567890','houseshine@gmail.com',7);
/*!40000 ALTER TABLE `empresas` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `login`
--

DROP TABLE IF EXISTS `login`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `login` (
  `Id_Conta` int(11) NOT NULL AUTO_INCREMENT,
  `Nome_Conta` varchar(45) NOT NULL,
  `Email_conta` varchar(45) NOT NULL,
  `Password_Conta` varchar(45) NOT NULL,
  PRIMARY KEY (`Id_Conta`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `login`
--

LOCK TABLES `login` WRITE;
/*!40000 ALTER TABLE `login` DISABLE KEYS */;
INSERT INTO `login` VALUES (5,'Diogo','diogo@gmail.com','123'),(6,'Pedro','pedro@gmail.com','123'),(7,'Rui','rui@gmail.com','123');
/*!40000 ALTER TABLE `login` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pedido`
--

DROP TABLE IF EXISTS `pedido`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedido` (
  `Id_Pedido` int(11) NOT NULL AUTO_INCREMENT,
  `Data_Realizar` date NOT NULL,
  `Id_Clientes` int(11) NOT NULL,
  `Id_Servicos_Empresa` int(11) NOT NULL,
  PRIMARY KEY (`Id_Pedido`),
  KEY `fk_Pedido_Clientes1_idx` (`Id_Clientes`),
  KEY `fk_Pedido_Servicos_Empresa1_idx` (`Id_Servicos_Empresa`),
  CONSTRAINT `fk_Pedido_Clientes1` FOREIGN KEY (`Id_Clientes`) REFERENCES `clientes` (`Id_Clientes`),
  CONSTRAINT `fk_Pedido_Servicos_Empresa1` FOREIGN KEY (`Id_Servicos_Empresa`) REFERENCES `servicos_empresa` (`Id_Servico_Empresa`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedido`
--

LOCK TABLES `pedido` WRITE;
/*!40000 ALTER TABLE `pedido` DISABLE KEYS */;
INSERT INTO `pedido` VALUES (1,'2020-03-31',3,2),(6,'2020-04-05',3,2);
/*!40000 ALTER TABLE `pedido` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servico`
--

DROP TABLE IF EXISTS `servico`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servico` (
  `Id_Servico` int(11) NOT NULL AUTO_INCREMENT,
  `Nome_Servico` varchar(45) NOT NULL,
  PRIMARY KEY (`Id_Servico`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servico`
--

LOCK TABLES `servico` WRITE;
/*!40000 ALTER TABLE `servico` DISABLE KEYS */;
INSERT INTO `servico` VALUES (1,'Eletrecista'),(2,'canalizador'),(3,'pintor');
/*!40000 ALTER TABLE `servico` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `servicos_empresa`
--

DROP TABLE IF EXISTS `servicos_empresa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `servicos_empresa` (
  `Id_Servico_Empresa` int(11) NOT NULL AUTO_INCREMENT,
  `Id_Empresa` int(11) NOT NULL,
  `Id_Servico` int(11) NOT NULL,
  PRIMARY KEY (`Id_Servico_Empresa`),
  KEY `fk_Servicos_Empresa_Empresas_idx` (`Id_Empresa`),
  KEY `fk_Servicos_Empresa_Servico1_idx` (`Id_Servico`),
  CONSTRAINT `fk_Servicos_Empresa_Empresas` FOREIGN KEY (`Id_Empresa`) REFERENCES `empresas` (`Id_Empresa`),
  CONSTRAINT `fk_Servicos_Empresa_Servico1` FOREIGN KEY (`Id_Servico`) REFERENCES `servico` (`Id_Servico`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `servicos_empresa`
--

LOCK TABLES `servicos_empresa` WRITE;
/*!40000 ALTER TABLE `servicos_empresa` DISABLE KEYS */;
INSERT INTO `servicos_empresa` VALUES (2,5,1),(3,5,3),(4,6,3),(5,6,2);
/*!40000 ALTER TABLE `servicos_empresa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'serviços'
--

--
-- Dumping routines for database 'serviços'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-04-01 17:22:15
