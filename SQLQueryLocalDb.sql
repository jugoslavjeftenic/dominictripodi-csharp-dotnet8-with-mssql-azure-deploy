USE DotNetCourseDatabase
GO

SELECT [UserId]
    ,[FirstName]
    ,[LastName]
    ,[Email]
    ,[Gender]
    ,[Active]
FROM [DotNetCourseDatabase].[TutorialAppSchema].[Users]
ORDER BY UserId DESC
GO

SELECT [Email]
    ,[PasswordHash]
    ,[PasswordSalt]
FROM [DotNetCourseDatabase].[TutorialAppSchema].[Auth]
GO

SELECT [UserId]
    ,[JobTitle]
    ,[Department]
FROM [DotNetCourseDatabase].[TutorialAppSchema].[UserJobInfo]
ORDER BY UserId DESC
GO

SELECT [UserId]
    ,[Salary]
FROM [DotNetCourseDatabase].[TutorialAppSchema].[UserSalary]
ORDER BY UserId DESC
GO

SELECT [PostId]
    ,[UserId]
    ,[PostTitle]
    ,[PostContent]
    ,[PostCreated]
    ,[PostUpdated]
FROM [DotNetCourseDatabase].[TutorialAppSchema].[Posts]
ORDER BY UserId DESC
GO
