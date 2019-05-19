using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorUtil {

    public static Color GetColor(int colorIndex)
    {
        switch (colorIndex)
        {
            case 1:
                return Color.white;
            case 2:
                return Color.green;
            case 3:
                return Color.blue;
            case 4:
                return new Color(128,0,128);
            case 5:
                return Color.red;
            default:
                return Color.white;
        }
    }

    /// <summary> 获取服务器状态颜色 </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    public static Color GetServerColor(int colorIndex)
    {
        switch (colorIndex)
        {
            case 1:
                return Color.green;
            case 2:
                return Color.yellow;
            case 3:
                return Color.red;
            default:
                return Color.gray;
        }
    }
}
