using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetGameServerEntity {

    /// <summary> 游戏服Id </summary>
	public int Id { get; set; }
    /// <summary> 游戏服运行状态 </summary>
    public int RunStatus { get; set; }
    /// <summary> 是否推荐 </summary>
    public bool IsCommand { get; set; }
    /// <summary> 是否是新服 </summary>
    public bool IsNew { get; set; }
    /// <summary> 游戏服名称 </summary>
    public string Name { get; set; }
    /// <summary> 游戏服Ip </summary>
    public string Ip { get; set; }
    /// <summary> 游戏服端口号 </summary>
    public int Port { get; set; }
}
