CREATE DATABASE crud;
USE crud;

CREATE TABLE usuario (
	id int PRIMARY KEY AUTO_INCREMENT,
    nome varchar(30),
    login varchar(30),
    senha varchar(30)
);
