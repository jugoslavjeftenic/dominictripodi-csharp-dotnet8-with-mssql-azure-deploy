SELECT [UserId]
    ,[FirstName]
    ,[LastName]
    ,[Email]
    ,[Gender]
    ,[Active]
FROM [TutorialAppSchema].[Users]
ORDER BY UserId DESC
GO

SELECT [Email]
    ,[PasswordHash]
    ,[PasswordSalt]
FROM [TutorialAppSchema].[Auth]
GO

SELECT [UserId]
    ,[JobTitle]
    ,[Department]
FROM [TutorialAppSchema].[UserJobInfo]
ORDER BY UserId DESC
GO

SELECT [UserId]
    ,[Salary]
FROM [TutorialAppSchema].[UserSalary]
ORDER BY UserId DESC
GO

SELECT [PostId]
    ,[UserId]
    ,[PostTitle]
    ,[PostContent]
    ,[PostCreated]
    ,[PostUpdated]
FROM [TutorialAppSchema].[Posts]
ORDER BY UserId DESC
GO
