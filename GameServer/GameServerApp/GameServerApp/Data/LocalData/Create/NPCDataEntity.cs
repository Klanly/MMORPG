
//===================================================
//作    者：
//创建时间：2019-01-10 09:41:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// NPCData实体
/// </summary>
public partial class NPCDataEntity : AbstractEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 场景名称
    /// </summary>
    public string PrefabName { get; set; }

    /// <summary>
    /// NPC坐标
    /// </summary>
    public float[] RoleBirthPos { get; set; }

    /// <summary>
    /// 头像所在图集
    /// </summary>
    public string HeadPicAtlas { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string HeadPic { get; set; }

    /// <summary>
    /// 半身像所在路径
    /// </summary>
    public string HalfBodyPath { get; set; }

    /// <summary>
    /// 自言自语
    /// </summary>
    public string Talk { get; set; }

}
