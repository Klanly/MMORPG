USE [master]
GO
/****** Object:  Database [DBGameServer]    Script Date: 2019/4/25 22:30:04 ******/
CREATE DATABASE [DBGameServer]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBGameServer', FILENAME = N'E:\SQLData\Data\DBGameServer.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DBGameServer_log', FILENAME = N'E:\SQLData\Data\DBGameServer_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [DBGameServer] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBGameServer].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBGameServer] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBGameServer] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBGameServer] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBGameServer] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBGameServer] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBGameServer] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBGameServer] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [DBGameServer] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBGameServer] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBGameServer] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBGameServer] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBGameServer] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBGameServer] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBGameServer] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBGameServer] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBGameServer] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBGameServer] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBGameServer] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBGameServer] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBGameServer] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBGameServer] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBGameServer] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBGameServer] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBGameServer] SET RECOVERY FULL 
GO
ALTER DATABASE [DBGameServer] SET  MULTI_USER 
GO
ALTER DATABASE [DBGameServer] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBGameServer] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBGameServer] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBGameServer] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBGameServer', N'ON'
GO
USE [DBGameServer]
GO
/****** Object:  StoredProcedure [dbo].[GetPageList]    Script Date: 2019/4/25 22:30:04 ******/
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
/****** Object:  StoredProcedure [dbo].[GetPageList_High]    Script Date: 2019/4/25 22:30:04 ******/
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
/****** Object:  StoredProcedure [dbo].[Role_Create]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-12-07 14:06:22
-- Description:添加
-- =============================================
CREATE PROCEDURE [dbo].[Role_Create]
(
    @Id int Output,
	@Status int,
	@AccountId int,
	@JobId int,
	@NickName nvarchar(50),
	@Sex tinyint,
	@Level int,
	@Money int,
	@Gold int,
	@Exp int,
	@MaxHP int,
	@CurrHP int,
	@MaxMP int,
	@CurrMP int,
	@Attack int,
	@AttackAddition int,
	@Defense int,
	@DefenseAddition int,
	@Res int,
	@ResAddition int,
	@Hit int,
	@HitAddition int,
	@Dodge int,
	@DodgeAddition int,
	@Cri int,
	@CriAddition int,
	@Fighting int,
	@FightingAddition int,
	@CreateTime datetime,
	@UpdateTime datetime,
	@LastInWorldMapId int,
	@LastInWorldMapPos varchar(100),
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结
		
        INSERT INTO Role
			(Status,AccountId,JobId,NickName,Sex,Level,Money,Gold,Exp,MaxHP,CurrHP,MaxMP,CurrMP,Attack,AttackAddition,Defense,DefenseAddition,Res,ResAddition,Hit,HitAddition,Dodge,DodgeAddition,Cri,CriAddition,Fighting,FightingAddition,CreateTime,UpdateTime,LastInWorldMapId,LastInWorldMapPos)
		VALUES
			(@Status,@AccountId,@JobId,@NickName,@Sex,@Level,@Money,@Gold,@Exp,@MaxHP,@CurrHP,@MaxMP,@CurrMP,@Attack,@AttackAddition,@Defense,@DefenseAddition,@Res,@ResAddition,@Hit,@HitAddition,@Dodge,@DodgeAddition,@Cri,@CriAddition,@Fighting,@FightingAddition,@CreateTime,@UpdateTime,@LastInWorldMapId,@LastInWorldMapPos)
	
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
	            SET @Id = IDENT_CURRENT('Role')
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
/****** Object:  StoredProcedure [dbo].[Role_Delete]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-12-07 14:06:22
-- Description:删除
-- =============================================
CREATE PROCEDURE [dbo].[Role_Delete]
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
		UPDATE Role SET Status=0 WHERE Id=@Id
			
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
/****** Object:  StoredProcedure [dbo].[Role_GetEntity]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-12-07 14:06:22
-- Description:查询实体
-- =============================================
CREATE PROCEDURE [dbo].[Role_GetEntity]
(
    @Id int
)
AS
BEGIN
	SET NOCOUNT ON;
	
		SELECT * FROM Role WHERE Id=@Id
	
	SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[Role_Update]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2018-12-07 14:06:22
-- Description:修改
-- =============================================
CREATE PROCEDURE [dbo].[Role_Update]
(
    @Id int,
    @Status tinyint,
	@AccountId int,
	@JobId int,
	@NickName nvarchar(50),
	@Sex tinyint,
	@Level int,
	@Money int,
	@Gold int,
	@Exp int,
	@MaxHP int,
	@CurrHP int,
	@MaxMP int,
	@CurrMP int,
	@Attack int,
	@AttackAddition int,
	@Defense int,
	@DefenseAddition int,
	@Res int,
	@ResAddition int,
	@Hit int,
	@HitAddition int,
	@Dodge int,
	@DodgeAddition int,
	@Cri int,
	@CriAddition int,
	@Fighting int,
	@FightingAddition int,
	@CreateTime datetime,
	@UpdateTime datetime,
	@LastInWorldMapId int,
	@LastInWorldMapPos varchar(100),
    @RetMsg nvarchar(255) Output
)
AS
BEGIN
	BEGIN TRY
		SET NOCOUNT ON;
		--自定义逻辑开始
		
		--自定义逻辑结束
        
		UPDATE
			Role
		SET
		    Status=@Status,
		    AccountId=@AccountId,
		    JobId=@JobId,
		    NickName=@NickName,
		    Sex=@Sex,
		    Level=@Level,
		    Money=@Money,
		    Gold=@Gold,
		    Exp=@Exp,
		    MaxHP=@MaxHP,
		    CurrHP=@CurrHP,
		    MaxMP=@MaxMP,
		    CurrMP=@CurrMP,
		    Attack=@Attack,
		    AttackAddition=@AttackAddition,
		    Defense=@Defense,
		    DefenseAddition=@DefenseAddition,
		    Res=@Res,
		    ResAddition=@ResAddition,
		    Hit=@Hit,
		    HitAddition=@HitAddition,
		    Dodge=@Dodge,
		    DodgeAddition=@DodgeAddition,
		    Cri=@Cri,
		    CriAddition=@CriAddition,
		    Fighting=@Fighting,
		    FightingAddition=@FightingAddition,
		    CreateTime=@CreateTime,
		    UpdateTime=@UpdateTime,
		    LastInWorldMapId=@LastInWorldMapId,
		    LastInWorldMapPos=@LastInWorldMapPos
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
/****** Object:  StoredProcedure [dbo].[RoleSkill_Create]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2019-01-10 15:11:41
-- Description:添加
-- =============================================
CREATE PROCEDURE [dbo].[RoleSkill_Create]
(
    @Id int Output,
	@Status tinyint,
	@RoleId int,
	@SkillId int,
	@SkillLevel int,
	@SlotsNode tinyint,
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
		
        INSERT INTO RoleSkill
			(Status,RoleId,SkillId,SkillLevel,SlotsNode,CreateTime,UpdateTime)
		VALUES
			(@Status,@RoleId,@SkillId,@SkillLevel,@SlotsNode,@CreateTime,@UpdateTime)
	
		IF(@@ERROR = 0 AND @@ROWCOUNT > 0)
			BEGIN
	            SET @Id = IDENT_CURRENT('RoleSkill')
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
/****** Object:  StoredProcedure [dbo].[RoleSkill_Delete]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2019-01-10 15:11:41
-- Description:删除
-- =============================================
CREATE PROCEDURE [dbo].[RoleSkill_Delete]
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
		UPDATE RoleSkill SET Status=0 WHERE Id=@Id
			
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
/****** Object:  StoredProcedure [dbo].[RoleSkill_GetEntity]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2019-01-10 15:11:41
-- Description:查询实体
-- =============================================
CREATE PROCEDURE [dbo].[RoleSkill_GetEntity]
(
    @Id int
)
AS
BEGIN
	SET NOCOUNT ON;
	
		SELECT * FROM RoleSkill WHERE Id=@Id
	
	SET NOCOUNT OFF;
END



GO
/****** Object:  StoredProcedure [dbo].[RoleSkill_Update]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:北京-边涯
-- Create Date:2019-01-10 15:11:41
-- Description:修改
-- =============================================
CREATE PROCEDURE [dbo].[RoleSkill_Update]
(
    @Id int,
    @Status tinyint,
	@RoleId int,
	@SkillId int,
	@SkillLevel int,
	@SlotsNode tinyint,
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
			RoleSkill
		SET
		    Status=@Status,
		    RoleId=@RoleId,
		    SkillId=@SkillId,
		    SkillLevel=@SkillLevel,
		    SlotsNode=@SlotsNode,
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
/****** Object:  Table [dbo].[InitConfig]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[InitConfig](
	[Id] [int] NULL,
	[Status] [tinyint] NULL,
	[Key] [varchar](64) NULL,
	[Value] [varchar](50) NULL,
	[Desc] [varchar](100) NULL,
	[CreateTime] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LogGameLevel]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogGameLevel](
	[Id] [int] NOT NULL,
	[Status] [tinyint] NULL,
	[GameLevelId] [int] NULL,
	[Grade] [tinyint] NULL,
	[Action] [tinyint] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_LOGGAMELEVEL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogKillMonster]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogKillMonster](
	[Id] [int] NOT NULL,
	[Status] [tinyint] NULL,
	[RoleId] [int] NULL,
	[PlayType] [tinyint] NULL,
	[PlaySceneId] [int] NULL,
	[Grade] [tinyint] NULL,
	[GoodsType] [tinyint] NULL,
	[SpriteId] [int] NULL,
	[SpriteCount] [int] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_LOGKILLMONSTER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LogReceiveGoods]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LogReceiveGoods](
	[Id] [int] NOT NULL,
	[Status] [tinyint] NULL,
	[RoleId] [int] NULL,
	[PlayType] [tinyint] NULL,
	[PlaySceneId] [int] NULL,
	[Grade] [tinyint] NULL,
	[GoodsType] [tinyint] NULL,
	[GoodsId] [int] NULL,
	[GoodsCount] [int] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_LOGRECEIVEGOODS] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NULL,
	[AccountId] [int] NULL,
	[JobId] [int] NULL,
	[NickName] [nvarchar](25) NULL,
	[Sex] [tinyint] NULL,
	[Level] [int] NULL,
	[Money] [int] NULL,
	[Gold] [int] NULL,
	[Exp] [int] NULL,
	[MaxHP] [int] NULL,
	[CurrHP] [int] NULL,
	[MaxMP] [int] NULL,
	[CurrMP] [int] NULL,
	[Attack] [int] NULL,
	[AttackAddition] [int] NULL,
	[Defense] [int] NULL,
	[DefenseAddition] [int] NULL,
	[Res] [int] NULL,
	[ResAddition] [int] NULL,
	[Hit] [int] NULL,
	[HitAddition] [int] NULL,
	[Dodge] [int] NULL,
	[DodgeAddition] [int] NULL,
	[Cri] [int] NULL,
	[CriAddition] [int] NULL,
	[Fighting] [int] NULL,
	[FightingAddition] [int] NULL,
	[LastPassGameLevelId] [int] NULL,
	[LastInWorldMapId] [int] NULL,
	[LastInWorldMapPos] [varchar](100) NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ROLE] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role_Backpack]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role_Backpack](
	[Id] [int] NOT NULL,
	[Status] [int] NULL,
	[RoleId] [int] NULL,
	[GoodsType] [tinyint] NULL,
	[GoodsId] [int] NULL,
	[GoodsCount] [int] NULL,
	[GoodsSvrId] [int] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_ROLE_BACKPACK] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleEquip]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleEquip](
	[Id] [int] NOT NULL,
	[Status] [int] NULL,
	[RoleId] [int] NULL,
	[EquipId] [int] NULL,
	[Type] [tinyint] NULL,
	[StrengthenLevel] [int] NULL,
	[StrengthenCount] [int] NULL,
	[BaseAttr1Type] [tinyint] NULL,
	[BaseAttr1Value] [int] NULL,
	[BaseAttr2Type] [tinyint] NULL,
	[BaseAttr2Value] [int] NULL,
	[MP] [int] NULL,
	[Attack] [int] NULL,
	[HP] [int] NULL,
	[Defense] [int] NULL,
	[Res] [int] NULL,
	[Hit] [int] NULL,
	[Dodge] [int] NULL,
	[Cri] [int] NULL,
 CONSTRAINT [PK_ROLEEQUIP] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleEquipGem]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleEquipGem](
	[Id] [int] NOT NULL,
	[Status] [tinyint] NULL,
	[RoleId] [int] NULL,
	[RoleEquipId] [int] NULL,
	[HoleId] [tinyint] NULL,
	[GemId] [int] NULL,
	[GemType] [tinyint] NULL,
	[GemAttrValue] [int] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_ROLEEQUIPGEM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RolePassGameLevelDetail]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RolePassGameLevelDetail](
	[Id] [int] NOT NULL,
	[Status] [tinyint] NULL,
	[ChapterId] [int] NULL,
	[GameLevelId] [int] NULL,
	[Grade] [tinyint] NULL,
	[Star] [tinyint] NULL,
	[IsMopUp] [int] NULL,
	[CreateTime] [datetime] NULL,
 CONSTRAINT [PK_ROLEPASSGAMELEVELDETAIL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RoleSkill]    Script Date: 2019/4/25 22:30:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleSkill](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Status] [tinyint] NULL,
	[RoleId] [int] NULL,
	[SkillId] [int] NULL,
	[SkillLevel] [int] NULL,
	[SlotsNode] [tinyint] NULL,
	[CreateTime] [datetime] NULL,
	[UpdateTime] [datetime] NULL,
 CONSTRAINT [PK_ROLESKILL] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Id], [Status], [AccountId], [JobId], [NickName], [Sex], [Level], [Money], [Gold], [Exp], [MaxHP], [CurrHP], [MaxMP], [CurrMP], [Attack], [AttackAddition], [Defense], [DefenseAddition], [Res], [ResAddition], [Hit], [HitAddition], [Dodge], [DodgeAddition], [Cri], [CriAddition], [Fighting], [FightingAddition], [LastPassGameLevelId], [LastInWorldMapId], [LastInWorldMapPos], [CreateTime], [UpdateTime]) VALUES (17, 1, 1, 1, N'邹飘忌', 0, 1, 0, 0, 0, 450, 450, 32, 32, 19, 0, 18, 0, 9, 0, 19, 0, 12, 0, 6, 0, 582, 0, NULL, 1, NULL, CAST(0x0000A9D100F0680C AS DateTime), CAST(0x0000A9D100F0680C AS DateTime))
INSERT [dbo].[Role] ([Id], [Status], [AccountId], [JobId], [NickName], [Sex], [Level], [Money], [Gold], [Exp], [MaxHP], [CurrHP], [MaxMP], [CurrMP], [Attack], [AttackAddition], [Defense], [DefenseAddition], [Res], [ResAddition], [Hit], [HitAddition], [Dodge], [DodgeAddition], [Cri], [CriAddition], [Fighting], [FightingAddition], [LastPassGameLevelId], [LastInWorldMapId], [LastInWorldMapPos], [CreateTime], [UpdateTime]) VALUES (18, 1, 1, 3, N'夏曼凝', 0, 1, 0, 0, 0, 450, 450, 32, 32, 19, 0, 18, 0, 12, 0, 19, 0, 12, 0, 6, 0, 588, 0, NULL, 1, NULL, CAST(0x0000A9D100F080F3 AS DateTime), CAST(0x0000A9D100F080F3 AS DateTime))
INSERT [dbo].[Role] ([Id], [Status], [AccountId], [JobId], [NickName], [Sex], [Level], [Money], [Gold], [Exp], [MaxHP], [CurrHP], [MaxMP], [CurrMP], [Attack], [AttackAddition], [Defense], [DefenseAddition], [Res], [ResAddition], [Hit], [HitAddition], [Dodge], [DodgeAddition], [Cri], [CriAddition], [Fighting], [FightingAddition], [LastPassGameLevelId], [LastInWorldMapId], [LastInWorldMapPos], [CreateTime], [UpdateTime]) VALUES (19, 1, 1, 1, N'夏侯百仁', 0, 1, 0, 0, 0, 450, 450, 32, 32, 19, 0, 18, 0, 9, 0, 19, 0, 12, 0, 6, 0, 582, 0, NULL, 1, NULL, CAST(0x0000A9D100F28496 AS DateTime), CAST(0x0000A9D100F28496 AS DateTime))
INSERT [dbo].[Role] ([Id], [Status], [AccountId], [JobId], [NickName], [Sex], [Level], [Money], [Gold], [Exp], [MaxHP], [CurrHP], [MaxMP], [CurrMP], [Attack], [AttackAddition], [Defense], [DefenseAddition], [Res], [ResAddition], [Hit], [HitAddition], [Dodge], [DodgeAddition], [Cri], [CriAddition], [Fighting], [FightingAddition], [LastPassGameLevelId], [LastInWorldMapId], [LastInWorldMapPos], [CreateTime], [UpdateTime]) VALUES (20, 1, 1, 2, N'程成斌', 0, 1, 0, 0, 0, 450, 450, 32, 32, 19, 0, 12, 0, 6, 0, 19, 0, 12, 0, 12, 0, 570, 0, NULL, 1, NULL, CAST(0x0000A9D100F28E38 AS DateTime), CAST(0x0000A9D100F28E38 AS DateTime))
INSERT [dbo].[Role] ([Id], [Status], [AccountId], [JobId], [NickName], [Sex], [Level], [Money], [Gold], [Exp], [MaxHP], [CurrHP], [MaxMP], [CurrMP], [Attack], [AttackAddition], [Defense], [DefenseAddition], [Res], [ResAddition], [Hit], [HitAddition], [Dodge], [DodgeAddition], [Cri], [CriAddition], [Fighting], [FightingAddition], [LastPassGameLevelId], [LastInWorldMapId], [LastInWorldMapPos], [CreateTime], [UpdateTime]) VALUES (21, 1, 1, 1, N'道剑剑非道', 0, 1, 0, 0, 0, 5000, 5000, 500, 500, 500, 0, 18, 0, 9, 0, 19, 0, 12, 0, 6, 0, 2500, 0, NULL, 1, NULL, CAST(0x0000A9D100F2BC28 AS DateTime), CAST(0x0000A9D100F2BC28 AS DateTime))
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[RoleSkill] ON 

INSERT [dbo].[RoleSkill] ([Id], [Status], [RoleId], [SkillId], [SkillLevel], [SlotsNode], [CreateTime], [UpdateTime]) VALUES (1, 1, 21, 106, 1, 1, CAST(0x0000A9D100FC905C AS DateTime), CAST(0x0000A9D100FC905C AS DateTime))
INSERT [dbo].[RoleSkill] ([Id], [Status], [RoleId], [SkillId], [SkillLevel], [SlotsNode], [CreateTime], [UpdateTime]) VALUES (2, 1, 21, 107, 1, 2, CAST(0x0000A9D200B0026A AS DateTime), CAST(0x0000A9D200B0026A AS DateTime))
SET IDENTITY_INSERT [dbo].[RoleSkill] OFF
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InitConfig', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InitConfig', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'KEY' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InitConfig', @level2type=N'COLUMN',@level2name=N'Key'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'VALUE' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InitConfig', @level2type=N'COLUMN',@level2name=N'Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'描述' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InitConfig', @level2type=N'COLUMN',@level2name=N'Desc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'InitConfig', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogGameLevel', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogGameLevel', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关卡Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogGameLevel', @level2type=N'COLUMN',@level2name=N'GameLevelId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'难度等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogGameLevel', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=进入关卡
   1=成功
   2=失败' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogGameLevel', @level2type=N'COLUMN',@level2name=N'Action'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogGameLevel', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'攻打游戏关卡记录' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogGameLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=游戏关卡(PVE)
   1=世界地图野外场景(PVP)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'PlayType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩法对应场景' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'PlaySceneId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'难度等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'GoodsType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'精灵Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'SpriteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'精灵数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'SpriteCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色刷怪日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogKillMonster'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0=游戏关卡(PVE)
   1=世界地图野外场景(PVP)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'PlayType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩法对应场景' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'PlaySceneId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'难度等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'GoodsType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'GoodsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'GoodsCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'获得物品日志' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'LogReceiveGoods'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属账号Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'AccountId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'职业编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'JobId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'昵称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'NickName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'性别' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Sex'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Level'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'元宝' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Money'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'金币' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Gold'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经验' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Exp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大气血' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'MaxHP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前血量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'CurrHP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最大法力' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'MaxMP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前法力' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'CurrMP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'攻击' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Attack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'攻击加成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'AttackAddition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物理防御' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Defense'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物理防御加成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'DefenseAddition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'魔法防御' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Res'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'魔法防御加成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'ResAddition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命中' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Hit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命中加成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'HitAddition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'闪避' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Dodge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'闪避加成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'DodgeAddition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'必杀' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Cri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'必杀加成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'CriAddition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'战斗力' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'Fighting'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'战斗力加成' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'FightingAddition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'GoodsType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'GoodsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品数量' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'GoodsCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物品的服务器端Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'GoodsSvrId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色背包' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Role_Backpack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'装备Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'EquipId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'装备种类' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'强化等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'StrengthenLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'强化次数' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'StrengthenCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础属性1类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'BaseAttr1Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础类型1值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'BaseAttr1Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础类型2类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'BaseAttr2Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'基础属性2值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'BaseAttr2Value'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'真气' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'MP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'攻击' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Attack'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'气血' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'HP'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'防御' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Defense'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'抗性' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Res'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'命中' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Hit'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'闪避' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Dodge'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'必杀' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip', @level2type=N'COLUMN',@level2name=N'Cri'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色装备' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquip'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色装备编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'RoleEquipId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'孔编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'HoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宝石Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'GemId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宝石类型' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'GemType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宝石增加属性值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'GemAttrValue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色装备宝石' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleEquipGem'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'章节编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'ChapterId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关卡Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'GameLevelId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'难度等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'Grade'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'星级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'Star'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否扫荡中' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'IsMopUp'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'玩家过关详情' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RolePassGameLevelDetail'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'Status'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'角色编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'RoleId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技能编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'SkillId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技能等级' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'SkillLevel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技能槽编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'SlotsNode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'CreateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'更新时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'RoleSkill', @level2type=N'COLUMN',@level2name=N'UpdateTime'
GO
USE [master]
GO
ALTER DATABASE [DBGameServer] SET  READ_WRITE 
GO
