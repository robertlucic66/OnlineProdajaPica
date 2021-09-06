namespace OnlineProdajaPica.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'0b900e65-eee5-4f94-b0f7-ffa6797484f7', N'admin@opp.com', 0, N'AGwrfcLDwCkVd8gZVdCcuxOFnTDy6hkjcdXVgFmEK/VCyLLiyqnzd5oofQGgs0JBjg==', N'91ef87b2-cece-4b2a-bbe8-03e9ba44cee2', NULL, 0, 0, NULL, 1, 0, N'admin@opp.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2663d82f-ae32-47a5-8ada-6aaa2124e899', N'guest1@opp.com', 0, N'ABRP0kGMf7BdPVyRYZ0V14/Kj/36MwbNGKtwC6eDZx/yMRknlmIVcufmf8L5jOY9Qw==', N'22b42a73-5a83-42c3-82e1-e3ddcbd09478', NULL, 0, 0, NULL, 1, 0, N'guest1@opp.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'd61df15b-42d9-43d1-bac9-7776e4473048', N'Admin')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'0b900e65-eee5-4f94-b0f7-ffa6797484f7', N'd61df15b-42d9-43d1-bac9-7776e4473048')
");
        }
        
        public override void Down()
        {
        }
    }
}
