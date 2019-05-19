USE [master]
GO
/****** Object:  Database [DBAccount]    Script Date: 2019/4/25 22:29:15 ******/
CREATE DATABASE [DBAccount]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBAccount', FILENAME = N'E:\SQLData\Data\DBAccount.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBAccount_log', FILENAME = N'E:\SQLData\Data\DBAccount_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBAccount] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBAccount].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBAccount] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBAccount] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBAccount] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBAccount] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBAccount] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBAccount] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBAccount] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBAccount] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBAccount] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBAccount] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBAccount] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBAccount] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBAccount] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBAccount] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBAccount] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBAccount] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBAccount] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBAccount] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBAccount] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBAccount] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBAccount] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBAccount] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBAccount] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBAccount] SET RECOVERY FULL 
GO
ALTER DATABASE [DBAccount] SET  MULTI_USER 
GO
ALTER DATABASE [DBAccount] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBAccount] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBAccount] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBAccount] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBAccount', N'ON'
GO
USE [DBAccount]
GO
/****** Object:  User [NT AUTHORITY\SYSTEM]    Script Date: 2019/4/25 22:29:15 ******/
CREATE USER [NT AUTHORITY\SYSTEM] FOR LOGIN [NT AUTHORITY\SYSTEM] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [NT AUTHORITY\SYSTEM]
GO
/****** Object:  StoredProcedure [dbo].[Account_Create]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-08 00:51:11
-- Description:添加
-- =============================================
CREATE PROCEDURE [dbo].[Account_Create]
(
    @Id int Output,
	@Status tinyint,
	@UserName varchar(50),
	@Pwd varchar(32),
	@Mobile varchar(11),
	@Mail varchar(200),
	@Money int,
	@ChannelId varchar(50),
	@LastLogOnServerId int,
	@LastLogOnServerName nvarchar(40),
	@LastLogOnServerTime datetime,
	@LastLogOnRoleId int,
	@LastLogOnRoleNickName nvarchar(40),
	@LastLogOnRoleJobId int,
	@UpdateTime datetime,
	@CreateTime datetime,
	@DeviceIdentifier varchar(100),
	@DeviceModel varchar(50),
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结
		
        INSERT INTO Account
			(Status,UserName,Pwd,Mobile,Mail,Money,ChannelId,LastLogOnServerId,LastLogOnServerName,LastLogOnServerTime,LastLogOnRoleId,LastLogOnRoleNickName,LastLogOnRoleJobId,UpdateTime,CreateTime,DeviceIdentifier,DeviceModel)
		VALUES
			(@Status,@UserName,@Pwd,@Mobile,@Mail,@Money,@ChannelId,@LastLogOnServerId,@LastLogOnServerName,@LastLogOnServerTime,@LastLogOnRoleId,@LastLogOnRoleNickName,@LastLogOnRoleJobId,@UpdateTime,@CreateTime,@DeviceIdentifier,@DeviceModel)
	
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
	            SET @Id = IDENT_CURRENT('Account')
				SET @RetMsg = '添加成功';
				RETURN 1;
			END
		ELSE
			BEGIN
				SET @RetMsg = '添加失败';
				RETURN -2;
			END
    
		SET NOCOUNT OFF;
	END TRY
	BEGIN CATCH
		SET @RetMsg = ERROR_MESSAGE();
		RETURN -1;
	END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[Account_Delete]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-08 00:51:11
-- Description:删除
-- =============================================
CREATE PROCEDURE [dbo].[Account_Delete]
(
    @Id int,
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结束
		UPDATE Account SET Status=0 WHERE Id=@Id
			
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
				SET @RetMsg = '删除成功';
				RETURN 1;
			END
		ELSE
			BEGIN
				SET @RetMsg = '删除失败';
				RETURN -2;
			END
			
		SET NOCOUNT OFF;
	END TRY
	BEGIN CATCH
		SET @RetMsg = ERROR_MESSAGE();
		RETURN -1;
	END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[Account_GetEntity]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-08 00:51:11
-- Description:查询实体
-- =============================================
CREATE PROCEDURE [dbo].[Account_GetEntity]
(
    @Id int
)
AS
BEGIN
	SET NOCOUNT ON;
	
		SELECT * FROM Account WHERE Id=@Id
	
	SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[Account_Update]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-08 00:51:11
-- Description:修改
-- =============================================
CREATE PROCEDURE [dbo].[Account_Update]
(
    @Id int,
    @Status tinyint,
	@UserName varchar(50),
	@Pwd varchar(32),
	@Mobile varchar(11),
	@Mail varchar(200),
	@Money int,
	@ChannelId varchar(50),
	@LastLogOnServerId int,
	@LastLogOnServerName nvarchar(40),
	@LastLogOnServerTime datetime,
	@LastLogOnRoleId int,
	@LastLogOnRoleNickName nvarchar(40),
	@LastLogOnRoleJobId int,
	@UpdateTime datetime,
	@CreateTime datetime,
	@DeviceIdentifier varchar(100),
	@DeviceModel varchar(50),
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结束
        
		UPDATE
			Account
		SET
		    Status=@Status,
		    UserName=@UserName,
		    Pwd=@Pwd,
		    Mobile=@Mobile,
		    Mail=@Mail,
		    Money=@Money,
		    ChannelId=@ChannelId,
		    LastLogOnServerId=@LastLogOnServerId,
		    LastLogOnServerName=@LastLogOnServerName,
		    LastLogOnServerTime=@LastLogOnServerTime,
		    LastLogOnRoleId=@LastLogOnRoleId,
		    LastLogOnRoleNickName=@LastLogOnRoleNickName,
		    LastLogOnRoleJobId=@LastLogOnRoleJobId,
		    UpdateTime=@UpdateTime,
		    CreateTime=@CreateTime,
		    DeviceIdentifier=@DeviceIdentifier,
		    DeviceModel=@DeviceModel
		WHERE
		    Id=@Id
			
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
				SET @RetMsg = '修改成功';
				RETURN 1;
			END
		ELSE
			BEGIN
				SET @RetMsg = '修改失败';
				RETURN -2;
			END
			
		SET NOCOUNT OFF;
	END TRY
	BEGIN CATCH
		SET @RetMsg = ERROR_MESSAGE();
		RETURN -1;
	END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[GameServer_Create]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-07 14:18:46
-- Description:添加
-- =============================================
CREATE PROCEDURE [dbo].[GameServer_Create]
(
    @Id int Output,
	@Status tinyint,
	@RunStatus tinyint,
	@IsCommand bit,
	@IsNew bit,
	@Name nvarchar(40),
	@Ip varchar(20),
	@Port int,
	@CreateTime datetime,
	@UpdateTime datetime,
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结
		
        INSERT INTO GameServer
			(Status,RunStatus,IsCommand,IsNew,Name,Ip,Port,CreateTime,UpdateTime)
		VALUES
			(@Status,@RunStatus,@IsCommand,@IsNew,@Name,@Ip,@Port,@CreateTime,@UpdateTime)
	
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
	            SET @Id = IDENT_CURRENT('GameServer')
				SET @RetMsg = '添加成功';
				RETURN 1;
			END
		ELSE
			BEGIN
				SET @RetMsg = '添加失败';
				RETURN -2;
			END
    
		SET NOCOUNT OFF;
	END TRY
	BEGIN CATCH
		SET @RetMsg = ERROR_MESSAGE();
		RETURN -1;
	END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[GameServer_Delete]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-07 14:18:46
-- Description:删除
-- =============================================
CREATE PROCEDURE [dbo].[GameServer_Delete]
(
    @Id int,
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结束
		UPDATE GameServer SET Status=0 WHERE Id=@Id
			
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
				SET @RetMsg = '删除成功';
				RETURN 1;
			END
		ELSE
			BEGIN
				SET @RetMsg = '删除失败';
				RETURN -2;
			END
			
		SET NOCOUNT OFF;
	END TRY
	BEGIN CATCH
		SET @RetMsg = ERROR_MESSAGE();
		RETURN -1;
	END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[GameServer_GetEntity]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-07 14:18:46
-- Description:查询实体
-- =============================================
CREATE PROCEDURE [dbo].[GameServer_GetEntity]
(
    @Id int
)
AS
BEGIN
	SET NOCOUNT ON;
	
		SELECT * FROM GameServer WHERE Id=@Id
	
	SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[GameServer_Update]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-11-07 14:18:46
-- Description:修改
-- =============================================
CREATE PROCEDURE [dbo].[GameServer_Update]
(
    @Id int,
    @Status tinyint,
	@RunStatus tinyint,
	@IsCommand bit,
	@IsNew bit,
	@Name nvarchar(40),
	@Ip varchar(20),
	@Port int,
	@CreateTime datetime,
	@UpdateTime datetime,
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结束
        
		UPDATE
			GameServer
		SET
		    Status=@Status,
		    RunStatus=@RunStatus,
		    IsCommand=@IsCommand,
		    IsNew=@IsNew,
		    Name=@Name,
		    Ip=@Ip,
		    Port=@Port,
		    CreateTime=@CreateTime,
		    UpdateTime=@UpdateTime
		WHERE
		    Id=@Id
			
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
				SET @RetMsg = '修改成功';
				RETURN 1;
			END
		ELSE
			BEGIN
				SET @RetMsg = '修改失败';
				RETURN -2;
			END
			
		SET NOCOUNT OFF;
	END TRY
	BEGIN CATCH
		SET @RetMsg = ERROR_MESSAGE();
		RETURN -1;
	END CATCH
END



GO
/****** Object:  StoredProcedure [dbo].[GetPageList]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[GetPageList] 

@TableName varchar(200), --表名 

@Fields varchar(1000) = '*', --字段名(全部字段为*) 

@OrderField varchar(500), --排序字段(必须!支持多字段) 

@SqlWhere varchar(5000) = Null,--条件语句(不用加where) 

@PageSize int, --每页多少条记录 

@PageIndex int = 1 , --指定当前为第几页 

@TotalCount int output --返回总记录数 

as 

begin 

Begin Tran --开始事务 

Declare @sql nvarchar(4000); 

Declare @TotalPage int;

set @TotalCount = 0;

set @TableName = @TableName;

if(@PageIndex is null)
	set @PageIndex = -1

if(@PageSize is null)
	set @PageSize = -1

if (@PageIndex<1 and @PageSize <1) 
	begin
		if (@SqlWhere='' or @SqlWhere=NULL) 
			set @sql = 'select '+@Fields+' from 

'+@TableName+' order by '+@OrderField
		else
			set @sql = 'select '+@Fields+' from 

'+@TableName+ ' where ' + @SqlWhere +' order by '+@OrderField
	end
else
	begin
		--计算总记录数 

		if (@SqlWhere='' or @SqlWhere=NULL) 

		set @sql = 'select @TotalCount = count(1) from ' + 

@TableName 

		else 

		set @sql = 'select @TotalCount = count(1) from ' + 

@TableName + ' where ' + @SqlWhere 

		EXEC sp_executesql @sql,N'@TotalCount int 

OUTPUT',@TotalCount OUTPUT--计算总记录数 

		--计算总页数 

		select @TotalPage=CEILING((@TotalCount+0.0)/@PageSize) 

		if (@SqlWhere='' or @SqlWhere=NULL) 

		set @sql = 'Select * FROM (select ROW_NUMBER() Over

(order by ' + @OrderField + ') as rowId,' + @Fields + ' from ' + 

@TableName 

		else 

		set @sql = 'Select * FROM (select ROW_NUMBER() Over

(order by ' + @OrderField + ') as rowId,' + @Fields + ' from ' + 

@TableName + ' where ' + @SqlWhere 

		--处理页数超出范围情况 

		if @PageIndex<=0 

		Set @PageIndex = 1 

		if @PageIndex>@TotalPage 

		Set @PageIndex = @TotalPage 

		--处理开始点和结束点 

		Declare @StartRecord int 

		Declare @EndRecord int 

		set @StartRecord = (@PageIndex-1)*@PageSize + 1 

		set @EndRecord = @StartRecord + @PageSize - 1 

		--继续合成sql语句 

		set @Sql = @Sql + ') as _pagelisttable_ where rowId 

between ' + Convert(varchar(50),@StartRecord) + ' and ' + Convert

(varchar(50),@EndRecord) 

	end
	
print @Sql

Exec(@Sql) 

-------------------------------------------------- - 

If @@Error <> 0 

Begin 

RollBack Tran 

Return -1 

End 

Else 

Begin 

Commit Tran 

Return @TotalPage ---返回总页数 

End 

end






GO
/****** Object:  StoredProcedure [dbo].[GetPageList_High]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[GetPageList_High]
	(
		@TableName VARCHAR(255), -- 表名
		@Fields varchar(1000) = '*', -- 需要返回的列
		@OrderField VARCHAR(255)='', -- 排序的字段名
		@SqlWhere VARCHAR(2000) = '', -- 查询条件 (注意: 不要加 WHERE)
		@PageSize INT = 10, -- 页尺寸
		@PageIndex INT = 1, -- 页码
		@OrderType BIT = 0, -- 设置排序类型, 非 0 值则降序
		@TotalCount int output --返回总记录数 
	)
AS

	SET NOCOUNT ON
	
	begin tran
	begin try

		DECLARE @strSQL VARCHAR(5000) -- 主语句
		DECLARE @strTmp VARCHAR(1000) -- 临时变量
		DECLARE @strOrder VARCHAR(400) -- 排序类型
		declare @sql nvarchar(1000)

		if @PageIndex<1 and @PageSize<1
		begin
			if (@SqlWhere='' or @SqlWhere=NULL) 
				set @strSQL = 'select '+@Fields+' from '+@TableName+' order by '+@OrderField
			else
				set @strSQL = 'select '+@Fields+' from '+@TableName+ ' where ' + @SqlWhere +' order by '+@OrderField
				
			if @OrderType!=0
				set @strSQL=@strSQL+' desc '
				
		end
		else
		begin
			--计算总记录数 

			if (@SqlWhere='' or @SqlWhere=NULL) 
				set @sql = 'select @TotalCount = count(1) from ' + @TableName 
			else 
				set @sql = 'select @TotalCount = count(1) from ' + @TableName + ' where ' + @SqlWhere 


			EXEC sp_executesql @sql,N'@TotalCount int OUTPUT',@TotalCount OUTPUT--计算总记录数 

			IF @OrderType != 0
				BEGIN
					SET @strTmp = '<(SELECT MIN'
					SET @strOrder = ' ORDER BY ' + @OrderField +' DESC'
					--如果@OrderType不是0，就执行降序，这句很重要
				END
			ELSE
				BEGIN
					SET @strTmp = '>(SELECT MAX'
					SET @strOrder = ' ORDER BY ' + @OrderField +' ASC'
				END

			IF @PageIndex = 1
				BEGIN
					IF @SqlWhere != ''
						SET @strSQL = 'SELECT TOP ' + str(@PageSize) +' '+@Fields+ ' FROM ' + @TableName + ' WHERE ' + @SqlWhere + ' ' + @strOrder
					ELSE
						SET @strSQL = 'SELECT TOP ' + str(@PageSize) +' '+@Fields+ ' FROM '+ @TableName + ' '+ @strOrder
					--如果是第一页就执行以上代码，这样会加快执行速度
				END
			ELSE
				BEGIN
				declare @OrderFi varchar(50)

				set @OrderFi=SUBSTRING(@OrderField,CHARINDEX('.',@OrderField)+1,LEN(@OrderField));
					--以下代码赋予了@strSQL以真正执行的SQL代码
					SET @strSQL = 'SELECT TOP ' + str(@PageSize) +' '+@Fields+ ' FROM '
					+ @TableName + ' WHERE ' + @OrderField + '' + @strTmp + '('+ @OrderFi + ') FROM (SELECT TOP ' + str((@PageIndex-1)*@PageSize) + ' '+ @OrderField + ' FROM ' + @TableName + '' + @strOrder + ') AS tblTmp)'+ @strOrder
					IF @SqlWhere != ''
					SET @strSQL = 'SELECT TOP ' + str(@PageSize) +' '+@Fields+ ' FROM '
					+ @TableName + ' WHERE ' + @OrderField + '' + @strTmp + '('
					+ @OrderFi + ') FROM (SELECT TOP ' + str((@PageIndex-1)*@PageSize) + ' '
					+ @OrderField + ' FROM ' + @TableName + ' WHERE ' + @SqlWhere + ' '
					+ @strOrder + ') AS tblTmp) AND ' + @SqlWhere + ' ' + @strOrder

				END
		end

		EXEC (@strSQL)
		commit tran
	end try
	begin catch
		rollback tran
	end catch
	
	SET NOCOUNT OFF






GO
/****** Object:  Table [dbo].[Account]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NULL,
	[UserName] [varchar](50) NULL,
	[Pwd] [varchar](32) NULL,
	[Mobile] [varchar](11) NULL,
	[Mail] [varchar](200) NULL,
	[Money] [int] NULL,
	[ChannelId] [varchar](50) NULL,
	[LastLogOnServerId] [int] NULL,
	[LastLogOnServerName] [nvarchar](20) NULL,
	[LastLogOnServerTime] [datetime] NULL,
	[LastLogOnRoleId] [int] NULL,
	[LastLogOnRoleNickName] [nvarchar](20) NULL,
	[LastLogOnRoleJobId] [int] NULL,
	[UpdateTime] [datetime] NULL,
	[CreateTime] [datetime] NULL,
	[DeviceIdentifier] [varchar](100) NULL,
	[DeviceModel] [varchar](50) NULL,
 CONSTRAINT [PK_ACCOUNT] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[GameServer]    Script Date: 2019/4/25 22:29:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GameServer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NULL,
	[RunStatus] [tinyint] NULL,
	[IsCommand] [bit] NULL,
	[IsNew] [bit] NULL,
	[Name] [nvarchar](20) NULL,
	[Ip] [varchar](20) NULL,
	[Port] [int] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_GAMESERVER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Account] ON 

INSERT [dbo].[Account] ([Id], [Status], [UserName], [Pwd], [Mobile], [Mail], [Money], [ChannelId], [LastLogOnServerId], [LastLogOnServerName], [LastLogOnServerTime], [LastLogOnRoleId], [LastLogOnRoleNickName], [LastLogOnRoleJobId], [UpdateTime], [CreateTime], [DeviceIdentifier], [DeviceModel]) VALUES (1, 1, N'fan', N'fan123', NULL, NULL, 0, N'0', 0, NULL, CAST(0x0000AA32016CF417 AS DateTime), 0, NULL, 0, CAST(0x0000AA32016CF417 AS DateTime), CAST(0x0000A9920011298D AS DateTime), N'028ef1a32ed1fde2d5250260c3e6b70979122a12', N'SVF15A18SCB (SONY)')
INSERT [dbo].[Account] ([Id], [Status], [UserName], [Pwd], [Mobile], [Mail], [Money], [ChannelId], [LastLogOnServerId], [LastLogOnServerName], [LastLogOnServerTime], [LastLogOnRoleId], [LastLogOnRoleNickName], [LastLogOnRoleJobId], [UpdateTime], [CreateTime], [DeviceIdentifier], [DeviceModel]) VALUES (2, 1, N'aaa', N'aaa', NULL, NULL, 0, N'0', 0, NULL, CAST(0x0000A99901575199 AS DateTime), 0, NULL, 0, CAST(0x0000A99901575199 AS DateTime), CAST(0x0000A992014BB9A8 AS DateTime), N'028ef1a32ed1fde2d5250260c3e6b70979122a12', N'SVF15A18SCB (SONY)')
INSERT [dbo].[Account] ([Id], [Status], [UserName], [Pwd], [Mobile], [Mail], [Money], [ChannelId], [LastLogOnServerId], [LastLogOnServerName], [LastLogOnServerTime], [LastLogOnRoleId], [LastLogOnRoleNickName], [LastLogOnRoleJobId], [UpdateTime], [CreateTime], [DeviceIdentifier], [DeviceModel]) VALUES (3, 1, N'fan001', N'111111', NULL, NULL, 0, N'0', 0, NULL, CAST(0x0000A993001CB0BF AS DateTime), 0, NULL, 0, CAST(0x0000A993001CB0BF AS DateTime), CAST(0x0000A993001CB0BF AS DateTime), N'028ef1a32ed1fde2d5250260c3e6b70979122a12', N'SVF15A18SCB (SONY)')
INSERT [dbo].[Account] ([Id], [Status], [UserName], [Pwd], [Mobile], [Mail], [Money], [ChannelId], [LastLogOnServerId], [LastLogOnServerName], [LastLogOnServerTime], [LastLogOnRoleId], [LastLogOnRoleNickName], [LastLogOnRoleJobId], [UpdateTime], [CreateTime], [DeviceIdentifier], [DeviceModel]) VALUES (4, 1, N'fan002', N'123456', NULL, NULL, 0, N'0', 0, NULL, CAST(0x0000A99300296C3D AS DateTime), 0, NULL, 0, CAST(0x0000A99300296C3D AS DateTime), CAST(0x0000A99300296C3D AS DateTime), N'028ef1a32ed1fde2d5250260c3e6b70979122a12', N'SVF15A18SCB (SONY)')
SET IDENTITY_INSERT [dbo].[Account] OFF
SET IDENTITY_INSERT [dbo].[GameServer] ON 

INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (1, 1, 1, 0, 0, N'琅嬛福地1', N'192.168.1.100', 1001, CAST(0x0000A99300F77C4B AS DateTime), CAST(0x0000A99300F77C4B AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (2, 2, 1, 0, 0, N'琅嬛福地2', N'192.168.1.100', 1002, CAST(0x0000A99300F77C85 AS DateTime), CAST(0x0000A99300F77C85 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (3, 3, 1, 0, 0, N'琅嬛福地3', N'192.168.1.100', 1003, CAST(0x0000A99300F77C86 AS DateTime), CAST(0x0000A99300F77C86 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (4, 1, 1, 0, 0, N'琅嬛福地4', N'192.168.1.100', 1004, CAST(0x0000A99300F77C87 AS DateTime), CAST(0x0000A99300F77C87 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (5, 3, 1, 0, 0, N'琅嬛福地5', N'192.168.1.100', 1005, CAST(0x0000A99300F77C89 AS DateTime), CAST(0x0000A99300F77C89 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (6, 1, 1, 0, 0, N'琅嬛福地6', N'192.168.1.100', 1006, CAST(0x0000A99300F77C89 AS DateTime), CAST(0x0000A99300F77C89 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (7, 3, 1, 0, 0, N'琅嬛福地7', N'192.168.1.100', 1007, CAST(0x0000A99300F77C8B AS DateTime), CAST(0x0000A99300F77C8B AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (8, 2, 1, 0, 0, N'琅嬛福地8', N'192.168.1.100', 1008, CAST(0x0000A99300F77C8C AS DateTime), CAST(0x0000A99300F77C8C AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (9, 1, 1, 0, 0, N'琅嬛福地9', N'192.168.1.100', 1009, CAST(0x0000A99300F77C8C AS DateTime), CAST(0x0000A99300F77C8C AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (10, 2, 1, 0, 0, N'琅嬛福地10', N'192.168.1.100', 1010, CAST(0x0000A99300F77C8D AS DateTime), CAST(0x0000A99300F77C8D AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (11, 1, 1, 0, 0, N'琅嬛福地11', N'192.168.1.100', 1011, CAST(0x0000A99300F77C8E AS DateTime), CAST(0x0000A99300F77C8E AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (12, 2, 1, 0, 0, N'琅嬛福地12', N'192.168.1.100', 1012, CAST(0x0000A99300F77C8F AS DateTime), CAST(0x0000A99300F77C8F AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (13, 2, 1, 0, 0, N'琅嬛福地13', N'192.168.1.100', 1013, CAST(0x0000A99300F77C90 AS DateTime), CAST(0x0000A99300F77C90 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (14, 2, 1, 0, 0, N'琅嬛福地14', N'192.168.1.100', 1014, CAST(0x0000A99300F77C91 AS DateTime), CAST(0x0000A99300F77C91 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (15, 3, 1, 0, 0, N'琅嬛福地15', N'192.168.1.100', 1015, CAST(0x0000A99300F77C92 AS DateTime), CAST(0x0000A99300F77C92 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (16, 1, 1, 0, 0, N'琅嬛福地16', N'192.168.1.100', 1016, CAST(0x0000A99300F77C93 AS DateTime), CAST(0x0000A99300F77C93 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (17, 2, 1, 0, 0, N'琅嬛福地17', N'192.168.1.100', 1017, CAST(0x0000A99300F77C94 AS DateTime), CAST(0x0000A99300F77C94 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (18, 3, 1, 0, 0, N'琅嬛福地18', N'192.168.1.100', 1018, CAST(0x0000A99300F77C95 AS DateTime), CAST(0x0000A99300F77C95 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (19, 1, 1, 0, 0, N'琅嬛福地19', N'192.168.1.100', 1019, CAST(0x0000A99300F77C96 AS DateTime), CAST(0x0000A99300F77C96 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (20, 3, 1, 0, 0, N'琅嬛福地20', N'192.168.1.100', 1020, CAST(0x0000A99300F77C97 AS DateTime), CAST(0x0000A99300F77C97 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (21, 1, 1, 0, 0, N'琅嬛福地21', N'192.168.1.100', 1021, CAST(0x0000A99300F77C98 AS DateTime), CAST(0x0000A99300F77C98 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (22, 2, 1, 0, 0, N'琅嬛福地22', N'192.168.1.100', 1022, CAST(0x0000A99300F77C99 AS DateTime), CAST(0x0000A99300F77C99 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (23, 3, 1, 0, 0, N'琅嬛福地23', N'192.168.1.100', 1023, CAST(0x0000A99300F77C9A AS DateTime), CAST(0x0000A99300F77C9A AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (24, 3, 1, 0, 0, N'琅嬛福地24', N'192.168.1.100', 1024, CAST(0x0000A99300F77C9B AS DateTime), CAST(0x0000A99300F77C9B AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (25, 3, 1, 0, 0, N'琅嬛福地25', N'192.168.1.100', 1025, CAST(0x0000A99300F77C9B AS DateTime), CAST(0x0000A99300F77C9B AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (26, 2, 1, 0, 0, N'琅嬛福地26', N'192.168.1.100', 1026, CAST(0x0000A99300F77C9C AS DateTime), CAST(0x0000A99300F77C9C AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (27, 1, 1, 0, 0, N'琅嬛福地27', N'192.168.1.100', 1027, CAST(0x0000A99300F77C9D AS DateTime), CAST(0x0000A99300F77C9D AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (28, 1, 1, 0, 0, N'琅嬛福地28', N'192.168.1.100', 1028, CAST(0x0000A99300F77C9F AS DateTime), CAST(0x0000A99300F77C9F AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (29, 1, 1, 0, 0, N'琅嬛福地29', N'192.168.1.100', 1029, CAST(0x0000A99300F77CA0 AS DateTime), CAST(0x0000A99300F77CA0 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (30, 1, 1, 0, 0, N'琅嬛福地30', N'192.168.1.100', 1030, CAST(0x0000A99300F77CA1 AS DateTime), CAST(0x0000A99300F77CA1 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (31, 3, 1, 0, 0, N'琅嬛福地31', N'192.168.1.100', 1031, CAST(0x0000A99300F77CA2 AS DateTime), CAST(0x0000A99300F77CA2 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (32, 2, 1, 0, 0, N'琅嬛福地32', N'192.168.1.100', 1032, CAST(0x0000A99300F77CA3 AS DateTime), CAST(0x0000A99300F77CA3 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (33, 0, 1, 0, 0, N'琅嬛福地33', N'192.168.1.100', 1033, CAST(0x0000A99300F77CA3 AS DateTime), CAST(0x0000A99300F77CA3 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (34, 3, 1, 1, 1, N'琅嬛福地34', N'192.168.1.100', 1034, CAST(0x0000A99300F77CA4 AS DateTime), CAST(0x0000A99300F77CA4 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (35, 3, 1, 1, 1, N'琅嬛福地35', N'192.168.1.100', 1035, CAST(0x0000A99300F77CA5 AS DateTime), CAST(0x0000A99300F77CA5 AS DateTime))
INSERT [dbo].[GameServer] ([Id], [Status], [RunStatus], [IsCommand], [IsNew], [Name], [Ip], [Port], [CreateTime], [UpdateTime]) VALUES (36, 3, 1, 1, 1, N'琅嬛福地36', N'192.168.1.100', 1036, CAST(0x0000A99300F77CA5 AS DateTime), CAST(0x0000A99300F77CA5 AS DateTime))
SET IDENTITY_INSERT [dbo].[GameServer] OFF
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'UserName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'密码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'Pwd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'手机' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'邮箱' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'Mail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'元宝' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'渠道号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'ChannelId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录服务器Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'LastLogOnServerId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录服务器名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'LastLogOnServerName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录服务器时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'LastLogOnServerTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'LastLogOnRoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录角色名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'LastLogOnRoleNickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后登录角色职业Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'LastLogOnRoleJobId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备标识符' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'DeviceIdentifier'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设备型号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Account', @level2type=N'COLUMN',@level2name=N'DeviceModel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'运行状态 0=维护 1=流畅 2=爆满' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'RunStatus'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否推荐' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'IsCommand'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否新服' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'IsNew'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Ip' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'Ip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Port' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'Port'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'GameServer', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
USE [master]
GO
ALTER DATABASE [DBAccount] SET  READ_WRITE 
GO
