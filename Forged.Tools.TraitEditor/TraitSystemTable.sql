/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `trait_system` (
  `ID` INT NOT NULL,
  `Field_10_0_0_44795_001` INT NOT NULL,
  `WidgetSetID` INT NOT NULL,
  `VerifiedBuild` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`VerifiedBuild`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=UTF8MB4_UNICODE_CI;
/*!40101 SET character_set_client = @saved_cs_client */;