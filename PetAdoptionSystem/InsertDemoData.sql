INSERT INTO Users (Id, Username, PasswordHash) VALUES
    (NEWID(), 'admin', '123'),
    (NEWID(), 'user1', '123'),
    (NEWID(), 'user2', '123');

INSERT INTO Pets (Name, Type, Breed, Sex) VALUES
    ('Buddy', 'Dog', 'Golden Retriever', 'Male'),
    ('Mittens', 'Cat', 'Siamese', 'Female'),
    ('Charlie', 'Dog', 'Beagle', 'Male'),
    ('Lucy', 'Cat', 'Persian', 'Female'),
    ('Max', 'Dog', 'Bulldog', 'Male');

    SELECT * FROM Pets