create table dbo.Person
(
    ID          int primary key identity,
    FirstName   varchar(50),
    LastName    varchar(50),
    Created     datetime,
    CreatedByID int
)

INSERT INTO Person (FirstName, LastName, Created, CreatedByID)
    SELECT 'Ryan', 'Williams', GETDATE(), 1337
    UNION ALL
    SELECT 'Rhino', 'Ware', GETDATE(), 9001

CREATE TABLE dbo.Transactions (
    ID int PRIMARY KEY IDENTITY,
    Created DATETIME DEFAULT GETDATE(),
    Amount MONEY NOT NULL,
    PersonID INT FOREIGN KEY REFERENCES Person
)

DECLARE @Counter INT = 1
WHILE @Counter <= 1000
    BEGIN
        INSERT INTO Transactions(Amount, PersonID)
        SELECT RAND() * 100, ROUND(RAND(), 0) + 1
        SET @Counter = @Counter + 1
    end