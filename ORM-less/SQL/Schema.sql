CREATE TABLE dbo.Transactions (
    ID int IDENTITY,
    Created DATETIME DEFAULT GETDATE(),
    Amount MONEY NOT NULL
)

create table dbo.Person
(
    ID          int identity,
    FirstName   varchar(50),
    LastName    varchar(50),
    Created     datetime,
    CreatedByID int
)

DECLARE @Counter INT = 1
WHILE @Counter <= 1000
    BEGIN
        INSERT INTO Transactions(Amount)
        SELECT RAND() * 100
        SET @Counter = @Counter + 1
    end


INSERT INTO Person (FirstName, LastName, Created, CreatedByID)
    SELECT 'Ryan', 'Williams', GETDATE(), 1337
    UNION ALL
    SELECT 'Rhino', 'Ware', GETDATE(), 9001