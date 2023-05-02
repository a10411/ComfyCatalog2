CREATE TABLE Product (
  productID    int IDENTITY NOT NULL, 
  brandID      int NOT NULL, 
  estadoID     int NOT NULL, 
  productName  varchar(255) NOT NULL, 
  sport        varchar(255) NOT NULL, 
  composition  varchar(255) NOT NULL, 
  colour       varchar(255) NOT NULL, 
  clientNumber int NOT NULL, 
  productType  varchar(255) NOT NULL, 
  PRIMARY KEY (productID));
CREATE TABLE Brand (
  brandID   int IDENTITY NOT NULL, 
  brandName varchar(255) NOT NULL, 
  PRIMARY KEY (brandID));
CREATE TABLE [User] (
  userID        int IDENTITY NOT NULL, 
  username      varchar(255) NOT NULL, 
  password_hash varchar(255) NOT NULL, 
  password_salt varchar(255) NOT NULL, 
  PRIMARY KEY (userID));
CREATE TABLE Image (
  imageID   int IDENTITY NOT NULL, 
  imagePath varchar(255) NOT NULL, 
  imageName varchar(255) NOT NULL, 
  PRIMARY KEY (imageID));
CREATE TABLE Observation (
  obsID     int IDENTITY NOT NULL, 
  productID int NOT NULL, 
  userID    int NOT NULL, 
  title     varchar(255) NULL, 
  body      varchar(255) NOT NULL, 
  date_hour datetime NOT NULL, 
  PRIMARY KEY (obsID));
CREATE TABLE Admin (
  adminID       int IDENTITY NOT NULL, 
  username      varchar(255) NOT NULL, 
  password_hash varchar(255) NOT NULL, 
  password_salt varchar(255) NOT NULL, 
  PRIMARY KEY (adminID));
CREATE TABLE Product_Image (
  product_imageID int IDENTITY NOT NULL, 
  imageID         int NOT NULL, 
  productID       int NOT NULL, 
  PRIMARY KEY (product_imageID));
CREATE TABLE Favourite (
  favouriteID int IDENTITY NOT NULL, 
  userID      int NOT NULL, 
  productID   int NOT NULL, 
  PRIMARY KEY (favouriteID));
CREATE TABLE Estado (
  estadoID int IDENTITY NOT NULL, 
  estado   varchar(255) NOT NULL, 
  PRIMARY KEY (estadoID));
ALTER TABLE Observation ADD CONSTRAINT FKObservatio732992 FOREIGN KEY (productID) REFERENCES Product (productID);
ALTER TABLE Product ADD CONSTRAINT FKProduct81625 FOREIGN KEY (brandID) REFERENCES Brand (brandID);
ALTER TABLE Product_Image ADD CONSTRAINT FKProduct_Im664323 FOREIGN KEY (imageID) REFERENCES Image (imageID);
ALTER TABLE Product_Image ADD CONSTRAINT FKProduct_Im522595 FOREIGN KEY (productID) REFERENCES Product (productID);
ALTER TABLE Favourite ADD CONSTRAINT FKFavourite659751 FOREIGN KEY (userID) REFERENCES [User] (userID);
ALTER TABLE Favourite ADD CONSTRAINT FKFavourite725154 FOREIGN KEY (productID) REFERENCES Product (productID);
ALTER TABLE Product ADD CONSTRAINT FKProduct499223 FOREIGN KEY (estadoID) REFERENCES Estado (estadoID);
ALTER TABLE Observation ADD CONSTRAINT FKObservatio798395 FOREIGN KEY (userID) REFERENCES [User] (userID);
