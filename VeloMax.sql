-- CREATE database VeloMax;
use VeloMax;

DROP TABLE IF EXISTS Fourniture;
DROP TABLE IF EXISTS Fournisseur ;
DROP TABLE IF EXISTS Commande ;
DROP TABLE IF EXISTS Piece ;
DROP TABLE IF EXISTS Modele;
DROP TABLE IF EXISTS Client_Boutique ;
DROP TABLE IF EXISTS Client_Individu ;
DROP TABLE IF EXISTS Programme ;
DROP TABLE IF EXISTS Assemblage ;

CREATE TABLE Modele (
modele_num INT NOT NULL,
modele_nom VARCHAR(40) NOT NULL,
grandeur VARCHAR(40) NOT NULL,
modele_prix INT NOT NULL,
modele_date_intro VARCHAR(40) NULL,
modele_dateDiscon VARCHAR(40) NULL,
ligne_produit VARCHAR(40) NOT NULL,
assemblage_num INT NOT NULL,
PRIMARY KEY (modele_num)) ;

CREATE TABLE Piece (
piece_num VARCHAR(5) NOT NULL,
piece_description VARCHAR(40) NOT NULL,
PRIMARY KEY (piece_num)) ;

CREATE TABLE Fourniture (
piece_numProduitF VARCHAR(40) NOT NULL,
fournisseur_nom VARCHAR(40) NOT NULL,
piece_prix REAL NOT NULL,
piece_dateDiscon VARCHAR(40) NOT NULL,
piece_date_intro VARCHAR(40) NOT NULL,
piece_delai_appro VARCHAR(40) NOT NULL,
piece_num VARCHAR(5) NOT NULL,
PRIMARY KEY (piece_numProduitF));

CREATE TABLE Commande (
commande_num INT AUTO_INCREMENT NOT NULL,
commande_dateC VARCHAR(40) NOT NULL,
commande_date_livraison VARCHAR(40) NOT NULL,
quantite INT NOT NULL,
boutique_nom VARCHAR(40) NULL,
individu_nom VARCHAR(40) NULL,
modele_num INT NOT NULL,
piece_num VARCHAR(5) NOT NULL,
PRIMARY KEY (commande_num)) ;

CREATE TABLE Fournisseur (
siret INT NOT NULL,
fournisseur_nom VARCHAR(40) NOT NULL,
fournisseur_contact VARCHAR(40) NOT NULL,
fournisseur_adresse VARCHAR(40) NOT NULL,
libelle VARCHAR(40) NULL,
CONSTRAINT AK_fournisseur_nom UNIQUE(fournisseur_nom),
PRIMARY KEY (siret)) ;

CREATE TABLE Client_Boutique (
boutique_nom VARCHAR(40) NOT NULL,
boutique_adresse VARCHAR(40) NOT NULL,
boutique_tel VARCHAR(40) NOT NULL,
boutique_courriel VARCHAR(40) NOT NULL,
boutique_contact VARCHAR(40) NOT NULL,
boutique_remise VARCHAR(40) NULL,
volumeTot INT NULL,
PRIMARY KEY (boutique_nom)) ;

CREATE TABLE Client_Individu (
individu_nom VARCHAR(40) NOT NULL,
individu_prenom VARCHAR(40) NOT NULL,
individu_adresse VARCHAR(40) NOT NULL,
individu_courriel VARCHAR(40) NOT NULL,
individu_tel VARCHAR(40) NOT NULL,
programme_num INT NOT NULL,
PRIMARY KEY (individu_nom)) ;

CREATE TABLE Programme (
programme_num INT NOT NULL,
programme_nom VARCHAR(40) NOT NULL,
programme_prix REAL NOT NULL,
programme_duree VARCHAR(40) NOT NULL,
programme_rabais VARCHAR(40) NULL,
PRIMARY KEY (programme_num)) ;

-- ALTER TABLE Assemblage DROP modele_num;
-- ALTER TABLE Assemblage
-- DROP CONSTRAINT FK_modele_num;

CREATE TABLE Assemblage (
assemblage_num INT AUTO_INCREMENT NOT NULL,
Cadre VARCHAR(5) NULL,
Guidon VARCHAR(5) NULL,
Freins VARCHAR(5) NULL,
Selle VARCHAR(5) NULL,
Derailleur_avant VARCHAR(5) NULL,
Derailleur_arriere VARCHAR(5) NULL,
Roue_avant VARCHAR(5) NULL,
Roue_arriere VARCHAR(5) NULL, 
Reflecteur VARCHAR(5) NULL,
Pedalier VARCHAR(5) NULL,
Ordinateur VARCHAR(5) NULL,
Panier VARCHAR(5) NULL,
PRIMARY KEY (assemblage_num)) ;

ALTER TABLE Modele ADD CONSTRAINT FK_assemblage_num FOREIGN KEY (assemblage_num) REFERENCES Assemblage (assemblage_num);
ALTER TABLE Commande ADD CONSTRAINT FK_boutique_nom FOREIGN KEY (boutique_nom) REFERENCES Client_Boutique (boutique_nom);
ALTER TABLE Commande ADD CONSTRAINT FK_individu_nom FOREIGN KEY (individu_nom) REFERENCES Client_individu (individu_nom);
ALTER TABLE Commande ADD CONSTRAINT FK_modele_num FOREIGN KEY (modele_num) REFERENCES Modele (modele_num );
ALTER TABLE Commande ADD CONSTRAINT FK_piece_num FOREIGN KEY (piece_num) REFERENCES Piece (piece_num);
ALTER TABLE Client_individu ADD CONSTRAINT FK_programme_num FOREIGN KEY (programme_num) REFERENCES Programme (programme_num);
ALTER TABLE Fourniture ADD CONSTRAINT FK_piece_num_2 FOREIGN KEY (piece_num) REFERENCES Piece (piece_num);
ALTER TABLE Fourniture ADD CONSTRAINT FK_fournisseur_nom FOREIGN KEY (fournisseur_nom) REFERENCES Fournisseur (fournisseur_nom);

-- Chargement des données depuis des dossiers CSV --
