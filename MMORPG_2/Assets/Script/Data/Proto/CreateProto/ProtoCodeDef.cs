//===================================================
//作    者：xxx 
//创建时间：2019-05-18 15:13:21
//备    注：
//===================================================
using System.Collections;

/// <summary>
/// 协议编号定义
/// </summary>
public class ProtoCodeDef
{
    /// <summary>
    /// 测试协议
    /// </summary>
    public const ushort Test = 10001;

    /// <summary>
    /// 邮件(请求)
    /// </summary>
    public const ushort MailRequest = 10002;

    /// <summary>
    /// 邮件(返回)
    /// </summary>
    public const ushort MailResponse = 50002;

    /// <summary>
    /// 背包列表(请求)
    /// </summary>
    public const ushort BagListRequest = 10301;

    /// <summary>
    /// 背包列表(返回)
    /// </summary>
    public const ushort BagListResponse = 50301;

    /// <summary>
    /// 账号注册(请求)
    /// </summary>
    public const ushort AccountRegisterRequest = 10101;

    /// <summary>
    /// 账号注册(返回)
    /// </summary>
    public const ushort AccountRegisterResponse = 50101;

    /// <summary>
    /// 账号登录(请求)
    /// </summary>
    public const ushort AccountLogOnRequest = 10102;

    /// <summary>
    /// 账号登录(返回)
    /// </summary>
    public const ushort AccountLogOnResponse = 50102;

    /// <summary>
    /// 请求服务器组列表(请求)
    /// </summary>
    public const ushort GameServerPageRequest = 10111;

    /// <summary>
    /// 请求服务器组列表(返回)
    /// </summary>
    public const ushort GameServerPageResponse = 50111;

    /// <summary>
    /// 请求服务器单组列表(请求)
    /// </summary>
    public const ushort GameServerOnePageRequest = 10112;

    /// <summary>
    /// 请求服务器单组列表(返回)
    /// </summary>
    public const ushort GameServerOnePageResponse = 50112;

    /// <summary>
    /// 登录区服(请求)
    /// </summary>
    public const ushort RoleLogOnGameServerRequest = 10201;

    /// <summary>
    /// 登录区服(返回)
    /// </summary>
    public const ushort RoleLogOnGameServerResponse = 50201;

    /// <summary>
    /// 创建角色(请求)
    /// </summary>
    public const ushort RoleCreateRequest = 10202;

    /// <summary>
    /// 创建角色(返回)
    /// </summary>
    public const ushort RoleCreateResponse = 50202;

    /// <summary>
    /// 角色信息(请求)
    /// </summary>
    public const ushort RoleInfoRequest = 10203;

    /// <summary>
    /// 角色信息(返回)
    /// </summary>
    public const ushort RoleInfoResponse = 50203;

    /// <summary>
    /// 角色技能主动通知协议
    /// </summary>
    public const ushort RoleSkillDataResponse = 31001;

    /// <summary>
    /// 客户端发送进去游戏关卡消息
    /// </summary>
    public const ushort GameLevelEnterRequest = 10401;

    /// <summary>
    /// 服务器返回关卡消息
    /// </summary>
    public const ushort GameLevelEnterResponse = 50401;

    /// <summary>
    /// 客户端发送战斗胜利协议
    /// </summary>
    public const ushort GameLevelSuccessRequest = 10402;

    /// <summary>
    /// 服务器返回关卡胜利协议
    /// </summary>
    public const ushort GameLevelSuccessResponse = 50402;

}
