--ApiUsers Table
CREATE TABLE `IntToNetCore`.`ApiUsers` 
(   `Username` VARCHAR(50) NOT NULL , 
    `Password` VARCHAR(50) NOT NULL , 
    `CreatedTime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP, 
    `ModifiedTime` TIMESTAMP NOT NULL , 
    PRIMARY KEY (`Username`)
) ENGINE = InnoDB;


--Customer Table
CREATE TABLE `IntToNetCore`.`Customer` 
(   `CustomerNum` INT NOT NULL AUTO_INCREMENT , 
    `Name` VARCHAR(100) NOT NULL , 
    `Surname` VARCHAR(100) NOT NULL , 
    `Email` VARCHAR(100) NOT NULL , 
    `EmailVerified` BOOLEAN NOT NULL DEFAULT '0' , 
    `CreatedTime` TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP , 
    `ModifiedTime` TIMESTAMP NULL , 
    `CreatedUser` VARCHAR(50) NOT NULL , 
    `ModifiedUser` VARCHAR(50) NULL , 
    PRIMARY KEY (`CustomerNum`)
) ENGINE = InnoDB;
