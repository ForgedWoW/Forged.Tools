/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `spell_empower_stage` (
  `ID` INT NOT NULL,
  `SpellEmpowerStage` INT NOT NULL,
  `OtherValue` INT NOT NULL,
  `VerifiedBuild` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`,`VerifiedBuild`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=UTF8MB4_UNICODE_CI;
/*!40101 SET character_set_client = @saved_cs_client */;