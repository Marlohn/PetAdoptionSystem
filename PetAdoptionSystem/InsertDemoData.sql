INSERT INTO Users (Id, Username, PasswordHash) VALUES
    (NEWID(), 'admin', '123'),
    (NEWID(), 'user1', '123'),
    (NEWID(), 'user2', '123');

INSERT INTO Pets (Id, Name, Type, Breed, Sex) VALUES
    (NEWID(), 'Buddy', 'Dog', 'Golden Retriever', 'Male'),
    (NEWID(), 'Mittens', 'Cat', 'Siamese', 'Female'),
    (NEWID(), 'Charlie', 'Dog', 'Beagle', 'Male'),
    (NEWID(), 'Lucy', 'Cat', 'Persian', 'Female'),
    (NEWID(), 'Max', 'Dog', 'Bulldog', 'Male');

    SELECT * FROM Pets