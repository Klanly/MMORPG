using System.Collections;

/// <summary>
/// Test实体
/// </summary>
public partial class TestEntity : AbstractEntity
{
    /// <summary>
    /// 职业名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 旋转
    /// </summary>
    public float Rotate { get; set; }

    /// <summary>
    /// 职业名称
    /// </summary>
    public string[] NameList { get; set; }

    /// <summary>
    /// 职业名称
    /// </summary>
    public int[] NumList { get; set; }

    /// <summary>
    /// 职业名称
    /// </summary>
    public string[][] DoubleNameList { get; set; }

}
