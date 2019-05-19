
//===================================================
//作    者：
//创建时间：2019-02-07 10:26:42
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;

/// <summary>
/// WorldMap实体
/// </summary>
public partial class WorldMapEntity : AbstractEntity
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 场景名称
    /// </summary>
    public string SceneName { get; set; }

    /// <summary>
    /// 场景类型
    /// </summary>
    public string SceneType { get; set; }

    /// <summary>
    /// 主角出生点坐标
    /// </summary>
    public float[] RoleBirthPos { get; set; }

    /// <summary>
    /// 世界地图所对应NPC表格
    /// </summary>
    public string NPCExcel { get; set; }

    /// <summary>
    /// NPC预设所在文件夹
    /// </summary>
    public string NPCFloader { get; set; }

    /// <summary>
    /// 摄像机角度
    /// </summary>
    public float CameraRotation { get; set; }

    /// <summary>
    /// 关联场景Id列表
    /// </summary>
    public int[] NearSceneIdArray { get; set; }

}
