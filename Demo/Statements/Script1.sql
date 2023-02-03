
DECLARE @CountryName AS NVARCHAR(MAX) = 'Mexico';

SELECT C.CustomerIdentifier,
       C.CompanyName,
       CT.ContactTitle,
       Contact.FirstName,
       Contact.LastName,
       C.ContactIdentifier,
       C.ContactTypeIdentifier,
       C.CountryIdentfier
FROM dbo.Customers AS C
    INNER JOIN dbo.Countries
        ON C.CountryIdentfier = id
    INNER JOIN dbo.Contact AS Contact
        ON C.ContactIdentifier = Contact.ContactIdentifier
    INNER JOIN dbo.ContactType AS CT
        ON C.ContactTypeIdentifier = CT.ContactTypeIdentifier
WHERE (CountryName = @CountryName);

