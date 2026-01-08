/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
-- Déclarations des identifiants des destinations
DECLARE @DestFrance UNIQUEIDENTIFIER = NEWID();
DECLARE @DestItaly UNIQUEIDENTIFIER = NEWID();
DECLARE @DestSpain UNIQUEIDENTIFIER = NEWID();

-- Insertion des destinations
INSERT INTO [dbo].[Destination] ([Id], [Country], [City], [Description])
VALUES
(@DestFrance, 'France', 'Paris', 'Ville lumière et capitale culturelle'),
(@DestItaly, 'Italy', 'Rome', 'Ville historique au patrimoine antique'),
(@DestSpain, 'Spain', 'Barcelona', 'Ville côtière dynamique et culturelle');

-------------------------------------------------
-- Déclarations des identifiants des activités
DECLARE @ActTourEiffel UNIQUEIDENTIFIER = NEWID();
DECLARE @ActCroisiereSeine UNIQUEIDENTIFIER = NEWID();
DECLARE @ActColisee UNIQUEIDENTIFIER = NEWID();
DECLARE @ActSagrada UNIQUEIDENTIFIER = NEWID();

-- Insertion des activités
INSERT INTO [dbo].[Activity] ([Id], [Title], [Price], [Description], [DestinationId])
VALUES
(@ActTourEiffel, 'Tour Eiffel', 35.00, 'Visite guidée de la Tour Eiffel', @DestFrance),
(@ActCroisiereSeine, 'Croisière sur la Seine', 25.50, 'Balade en bateau sur la Seine', @DestFrance),
(@ActColisee, 'Colisée', 40.00, 'Visite du Colisée et du Forum', @DestItaly),
(@ActSagrada, 'Sagrada Familia', 30.00, 'Visite de la basilique emblématique', @DestSpain);

-------------------------------------------------
-- Déclarations des identifiants des bookings
DECLARE @BookJean UNIQUEIDENTIFIER = NEWID();
DECLARE @BookMaria UNIQUEIDENTIFIER = NEWID();
DECLARE @BookCarlos UNIQUEIDENTIFIER = NEWID();

-- Insertion des bookings
INSERT INTO [dbo].[Booking] ([Id], [BookingDate], [ClientName])
VALUES
(@BookJean, CAST(GETDATE() AS DATE), 'Jean Dupont'),
(@BookMaria, DATEADD(DAY, 5, CAST(GETDATE() AS DATE)), 'Maria Rossi'),
(@BookCarlos, DATEADD(DAY, 10, CAST(GETDATE() AS DATE)), 'Carlos Gomez');

-------------------------------------------------
-- Insertion des relations activités-bookings
INSERT INTO [dbo].[ActivityBooked] ([BookId], [ActivityId])
VALUES
(@BookJean, @ActTourEiffel),
(@BookJean, @ActCroisiereSeine),
(@BookMaria, @ActColisee),
(@BookCarlos, @ActSagrada);




