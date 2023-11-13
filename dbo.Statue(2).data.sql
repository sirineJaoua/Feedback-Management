SET IDENTITY_INSERT [dbo].[Statue] ON
INSERT INTO [dbo].[Statue] ([Id], [Name]) VALUES (1, N'Added')
SET IDENTITY_INSERT [dbo].[Statue] OFF
INSERT INTO Statue ([Name]) values ('Opened')
INSERT INTO Statue ([Name]) values ('Replied')
INSERT INTO Statue ([Name]) values ('Closed')


