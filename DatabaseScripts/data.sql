INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'c091590a-34c4-40f7-af9b-565236117732', N'admin@jobbank.com', N'ADMIN@JOBBANK.COM', N'admin@jobbank.com', N'ADMIN@JOBBANK.COM', 0, N'AQAAAAEAACcQAAAAEEBvs5Ko5/dEjYUwfQbcezVGijBRrFDB3UJciLSTtGTHeQgj26jZRCyMDeOs1D27KA==', N'PEU6GNRAPZDOAUG5IS6G37RIL46X3VDQ', N'6a2d424d-2e08-47b6-ac80-76c04c333885', NULL, 0, 0, NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[Candidate] ON
INSERT INTO [dbo].[Candidate] ([Id], [Name], [ContactNumber]) VALUES (1, N'John Smith', N'021345655990')
INSERT INTO [dbo].[Candidate] ([Id], [Name], [ContactNumber]) VALUES (2, N'Harry Newton ', N'021345677990')
SET IDENTITY_INSERT [dbo].[Candidate] OFF
SET IDENTITY_INSERT [dbo].[Employer] ON
INSERT INTO [dbo].[Employer] ([Id], [Name], [ContactNumber], [WebSite]) VALUES (1, N'ABC Software  Pvt Ltd', N'02134567899', N'http://abcsoftware.com')
INSERT INTO [dbo].[Employer] ([Id], [Name], [ContactNumber], [WebSite]) VALUES (2, N'Networking Systems', N'021899912345', N'http://networkingsystems.com')
INSERT INTO [dbo].[Employer] ([Id], [Name], [ContactNumber], [WebSite]) VALUES (3, N'Data Works ', N'02197623456', N'http://dataworks.com')
SET IDENTITY_INSERT [dbo].[Employer] OFF
SET IDENTITY_INSERT [dbo].[Advertisement] ON
INSERT INTO [dbo].[Advertisement] ([Id], [Title], [Description], [EmployerId], [SalaryInformation], [JobType]) VALUES (1, N'Software Developer', N'Software Developer Wanted', 1, N'70000 p.a', 0)
INSERT INTO [dbo].[Advertisement] ([Id], [Title], [Description], [EmployerId], [SalaryInformation], [JobType]) VALUES (2, N'Network Engineer', N'Network Engineer wanted', 2, N'80000', 1)
SET IDENTITY_INSERT [dbo].[Advertisement] OFF
SET IDENTITY_INSERT [dbo].[Application] ON
INSERT INTO [dbo].[Application] ([Id], [AdvertisementId], [CandidateId]) VALUES (1, 1, 2)
INSERT INTO [dbo].[Application] ([Id], [AdvertisementId], [CandidateId]) VALUES (2, 2, 2)
SET IDENTITY_INSERT [dbo].[Application] OFF
