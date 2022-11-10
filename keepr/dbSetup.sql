-- Active: 1666715469942@@SG-wood-cinema-3385-6834-mysql-master.servers.mongodirector.com@3306@garbagecollector

CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture',
        coverImg varchar(255) COMMENT 'User Cover Image'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS keeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        views INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id)
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaultkeeps(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        creatorId VARCHAR(255) NOT NULL,
        vaultId INT NOT NULL,
        keepId INT NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id),
        FOREIGN KEY (keepId) REFERENCES keeps(id) ON DELETE CASCADE,
        FOREIGN KEY (vaultId) REFERENCES vaults(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS vaults(
        id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
        creatorId VARCHAR(255) NOT NULL,
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(255) NOT NULL,
        description VARCHAR(500) NOT NULL,
        img VARCHAR(500) NOT NULL,
        isPrivate TINYINT NOT NULL DEFAULT 0,
        FOREIGN KEY (creatorId) REFERENCES accounts(id)
    ) default charset utf8 COMMENT '';

SELECT v.*, a.*
FROM vaults v
    JOIN accounts a ON a.id = @accountId
WHERE
    v.creatorId = "6351949e1a8c11f5daeb3eff";

ALTER TABLE accounts
ADD
    COLUMN coverImg varchar(255) COMMENT 'User Cover Image';