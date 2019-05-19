using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;

public class GameUtil
{
    #region 获取随机名字
    //姓
    static string[] surnameArray = {"司马", "欧阳", "端木", "上官", "独孤", "夏侯", "尉迟", "赫连", "皇甫", "公孙", "慕容", "长孙", "宇文", "司徒", "轩辕", "百里", "呼延", "令狐",
            "诸葛", "南宫", "东方", "西门", "李", "王", "张", "刘", "陈", "杨", "赵", "黄", "周", "胡", "林", "梁", "宋", "郑", "唐", "冯", "董", "程", "曹", "袁", "许", "沈",
            "曾", "彭", "吕", "蒋", "蔡", "魏", "叶", "杜", "夏", "汪", "田", "方", "石", "熊", "白", "秦", "江", "孟", "龙", "万", "段", "雷", "武", "乔", "洪", "鲁", "葛", "柳",
            "岳", "梅", "辛", "耿", "关", "苗", "童", "项", "裴", "鲍", "霍", "甘", "景", "包", "柯", "阮", "华", "滕", "穆", "燕", "敖", "冷", "卓", "花", "蓝", "楚", "荆", "官",
            "尉", "施", "姜", "戚", "邹", "严", "顾", "贺", "陆", "骆", "戴", "贾"};
    //男1名 
    static string[] male1Array = {"峰", "不", "近", "小", "千", "万", "百", "一", "求", "笑", "双", "凌", "伯", "仲", "叔", "飞", "晓", "昌", "霸", "冲", "留", "九", "子", "立", "小", "博",
            "才", "光", "弘", "华", "清", "灿", "俊", "凯", "乐", "良", "明", "健", "辉", "天", "星", "永", "玉", "英", "修", "义", "雪", "嘉", "成", "傲", "欣", "逸", "飘", "凌",
            "威", "火", "森", "杰", "思", "智", "辰", "元", "夕", "苍", "劲", "巨", "潇", "紫", "邪", "尘"};
    //男2名        
    static string[] male2Array = {"败", "悔", "南", "宝", "仞", "刀", "斐", "德", "云", "天", "仁", "岳", "宵", "忌", "爵", "权", "敏", "阳", "狂", "冠", "康", "平", "香", "刚", "强",
            "凡", "邦", "福", "歌", "国", "和", "康", "澜", "民", "宁", "然", "顺", "翔", "晏", "宜", "怡", "易", "志", "雄", "佑", "斌", "河", "元", "墨", "松", "林", "之",
            "翔", "竹", "宇", "轩", "荣", "哲", "风", "霜", "山", "炎", "罡", "盛", "睿", "达", "洪", "武", "耀", "磊", "寒", "冰", "潇", "痕", "岚", "空"};
    //女1名            
    static string[] female1Array = {"思", "冰", "夜", "依", "小", "香", "绿", "向", "映", "含", "曼", "春", "醉", "之", "新", "雨", "天", "如", "若", "涵", "亦", "采", "冬", "芷",
            "绮", "雅", "飞", "又", "寒", "忆", "晓", "乐", "笑", "妙", "元", "碧", "翠", "初", "怀", "幻", "慕", "秋", "语", "觅", "幼", "灵", "傲", "冷", "沛", "念", "寻",
            "水", "紫", "易", "惜", "诗", "妃", "雁", "盼", "尔", "以", "雪", "夏", "凝", "迎", "问", "宛", "梦", "怜", "听", "巧", "凡", "静"};
    //女2名
    static string[] female2Array = {"烟", "琴", "蓝", "梦", "丹", "柳", "冬", "萍", "菱", "寒", "阳", "霜", "白", "丝", "南", "真", "露", "云", "芙", "筠", "容", "香", "荷", "风", "儿",
            "雪", "巧", "蕾", "芹", "柔", "灵", "卉", "夏", "岚", "蓉", "萱", "珍", "彤", "蕊", "曼", "凡", "兰", "晴", "珊", "易", "妃", "春", "玉", "瑶", "文", "双", "竹",
            "凝", "桃", "菡", "绿", "枫", "梅", "旋", "山", "松", "之", "亦", "蝶", "莲", "柏", "波", "安", "天", "薇", "海", "翠", "槐", "秋", "雁", "夜"};

    /// <summary> 创建随机名 </summary>
    /// <param name="gender">1=男 2=女</param>
    /// <returns></returns>
    public static string RandomName(int gender)
    {
        string CurName = "";  //当前的名字

        string[] CopyArray1;
        string[] CopyArray2;
        

        //判断角色是男是女
        //if(角色是男) 将男名数组复制到CopyArray中
        if (gender==1)
        {
            CopyArray1 = new string[male1Array.Length];
            CopyArray2 = new string[male2Array.Length];
            male1Array.CopyTo(CopyArray1, 0);
            male2Array.CopyTo(CopyArray2, 0);
        }
        else
        {
            CopyArray1 = new string[female1Array.Length];
            CopyArray2 = new string[female2Array.Length];
            female1Array.CopyTo(CopyArray1, 0);
            female2Array.CopyTo(CopyArray2, 0);
        }

        int LastNameNum = 0;  //名的字数
        int TempRan = UnityEngine.Random.Range(1, 11);
        if (TempRan % 3 == 0)
        {
            LastNameNum = 1;
        }
        else
        {
            LastNameNum = 2;
        }

        //随机姓名+随机名字(名是一个字或者两个字)
        if (LastNameNum == 1)
        {
            int FirstNameIndex = UnityEngine.Random.Range(0, surnameArray.Length);
            int LastName1 = UnityEngine.Random.Range(0, CopyArray1.Length);
            CurName = surnameArray[FirstNameIndex] + CopyArray1[LastName1];
        }
        else if (LastNameNum == 2)
        {
            int FirstNameIndex = UnityEngine.Random.Range(0, surnameArray.Length);
            int LastName1 = UnityEngine.Random.Range(0, CopyArray1.Length);
            int LastName2 = UnityEngine.Random.Range(0, CopyArray2.Length);
            CurName = surnameArray[FirstNameIndex] + CopyArray1[LastName1] + CopyArray2[LastName2];
        }

        return CurName;
    }

    /// <summary>
    /// 获取剧情关卡背景图
    /// </summary>
    /// <param name="picName"></param>
    /// <returns></returns>
    public static Texture LoadGameLevelMapPic(string picName)
    {
        return Resources.Load(string.Format("UI/GameLevel/GameLevelMap/{0}", picName), typeof(Texture)) as Texture;
    }


    /// <summary>
    /// 获取图片资源
    /// </summary>
    /// <param name="type"></param>
    /// <param name="picName"></param>
    /// <returns></returns>
    //public static Sprite LoadSprite(E_SpriteSourceType type, string picName)
    //{
    //    string path = string.Empty;
    //    switch (type)
    //    {
    //        case E_SpriteSourceType.GameLevelIco:
    //            path = "UI/GameLevel/GameLevelIco";
    //            break;
    //        case E_SpriteSourceType.GameLevelDetail:
    //            path = "UI/GameLevel/GameLevelDetail";
    //            break;
    //        case E_SpriteSourceType.WorldMapIco:
    //            path = "UI/WorldMap";
    //            break;
    //        case E_SpriteSourceType.WorldMapSmall:
    //            path = "UI/SmallMap";
    //            break;
    //    }

    //    return Resources.Load(string.Format("{0}/{1}", path, picName), typeof(Sprite)) as Sprite;
    //}

    /// <summary>
    /// 获取道具图片
    /// </summary>
    /// <param name="goodsId"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    //public static Sprite LoadGoodsImg(int goodsId, E_GoodsType type)
    //{
    //    string pathName = string.Empty;
    //    switch (type)
    //    {
    //        case E_GoodsType.Equip:
    //            pathName = "EquipIco";
    //            break;
    //        case E_GoodsType.Item:
    //            pathName = "ItemIco";
    //            break;
    //        case E_GoodsType.Material:
    //            pathName = "MaterialIco";
    //            break;
    //    }

    //    return Resources.Load(string.Format("UI/{0}/{1}", pathName, goodsId), typeof(Sprite)) as Sprite;
    //}
    #endregion

    #region 获取角色动画状态
    //private static Dictionary<string, E_RoleAnimatorState> dic;

    //public static E_RoleAnimatorState GetRoleAnimatorState(E_RoleAttackType type, int index)
    //{
    //    if (dic == null)
    //    {
    //        dic = new Dictionary<string, E_RoleAnimatorState>();
    //        dic["PhyAttack1"] = E_RoleAnimatorState.PhyAttack1;
    //        dic["PhyAttack2"] = E_RoleAnimatorState.PhyAttack2;
    //        dic["PhyAttack3"] = E_RoleAnimatorState.PhyAttack3;
    //        dic["Skill1"] = E_RoleAnimatorState.Skill1;
    //        dic["Skill2"] = E_RoleAnimatorState.Skill2;
    //        dic["Skill3"] = E_RoleAnimatorState.Skill3;
    //        dic["Skill4"] = E_RoleAnimatorState.Skill4;
    //        dic["Skill5"] = E_RoleAnimatorState.Skill5;
    //        dic["Skill6"] = E_RoleAnimatorState.Skill6;
    //    }

    //    string key = string.Format("{0}{1}", type == E_RoleAttackType.PhyAttack ? "PhyAttack" : "Skill", index);

    //    if (dic.ContainsKey(key))
    //    {
    //        return dic[key];
    //    }
    //    return E_RoleAnimatorState.Skill1;
    //}
    #endregion

    #region GetRandomPos 获取目标点周围的随机点
    /// <summary>
    /// 获取目标点周围的随机点
    /// </summary>
    /// <param name="targerPos"></param>
    /// <param name="distance"></param>
    /// <returns></returns>
    public static Vector3 GetRandomPos(Vector3 targerPos, float distance)
    {
        //1.定义一个向量
        Vector3 v = new Vector3(0, 0, 1); //z轴超前的

        //2.让向量旋转
        v = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360f), 0) * v;

        //3.向量 * 距离(半径) = 坐标点
        Vector3 pos = v * distance * UnityEngine.Random.Range(0.8f, 1f);

        //4.计算出来的 围绕主角的 随机坐标点
        return targerPos + pos;
    }

    public static Vector3 GetRandomPos(Vector3 currPos, Vector3 targerPos, float distance)
    {
        //1.定义一个向量
        Vector3 v = (currPos - targerPos).normalized;

        //2.让向量旋转
        v = Quaternion.Euler(0, UnityEngine.Random.Range(-90f, 90f), 0) * v;

        //3.向量 * 距离(半径) = 坐标点
        Vector3 pos = v * distance * UnityEngine.Random.Range(0.8f, 1f);

        //4.计算出来的 围绕主角的 随机坐标点
        return targerPos + pos;
    }
    #endregion


    #region ------ GetTargetRandomPos 获取目标位置周围随机点 ------
    /// <summary> 获取目标位置周围随机点 </summary>
    /// <param name="targetPos">目标位置</param>
    /// <param name="redius">半径</param>
    /// <returns></returns>
    public static Vector3 GetTargetRandomPos(Vector3 targetPos, float redius)
    {
        //1.定义一个向量
        Vector3 v3 = new Vector3(0, 0, 1);
        //2.让向量随机旋转
        v3 = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0) * v3;
        //3.向量 * 半径 = 坐标点
        Vector3 pos = v3 * redius * UnityEngine.Random.Range(0.8f, 1f);
        //4.得到怪物的坐标
        return targetPos + pos;
    }

    // Update is called once per frame
    void Update()
    {

    }
    #endregion

    #region GetPathLen 计算路径的长度
    /// <summary>
    /// 计算路径的长度
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static float GetPathLen(List<Vector3> path)
    {
        float pathLen = 0f; //路径的总长度 计算出路径

        for (int i = 0; i < path.Count; i++)
        {
            if (i == path.Count - 1) continue;

            float dis = Vector3.Distance(path[i], path[i + 1]);
            pathLen += dis;
        }

        return pathLen;
    }
    #endregion

    #region GetFileName 获取文件名
    /// <summary>
    /// 获取文件名
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static string GetFileName(string path)
    {
        string fileName = path;
        int lastIndex = path.LastIndexOf('/');
        if (lastIndex > -1)
        {
            fileName = fileName.Substring(lastIndex + 1);
        }

        lastIndex = fileName.LastIndexOf('.');
        if (lastIndex > -1)
        {
            fileName = fileName.Substring(0, lastIndex);
        }

        return fileName;
    }
    #endregion


    #region ------ 获取角色攻击状态 ------

    /// <summary> 角色战斗状态字典 </summary>
    private static Dictionary<string, RoleAnimatorState> m_AttackStateDic;

    /// <summary> 获取角色攻击状态 </summary>
    /// <param name="type">攻击类型</param>
    /// <param name="index">索引</param>
    /// <returns></returns>
    public static RoleAnimatorState GetRoleAnimatorState(RoleAttackType type, int index)
    {
        if (m_AttackStateDic == null)
        {
            m_AttackStateDic = new Dictionary<string, RoleAnimatorState>();
            m_AttackStateDic["PhyAttack1"] = RoleAnimatorState.PhyAttack1;
            m_AttackStateDic["PhyAttack2"] = RoleAnimatorState.PhyAttack2;
            m_AttackStateDic["PhyAttack3"] = RoleAnimatorState.PhyAttack3;
            m_AttackStateDic["PhyAttack4"] = RoleAnimatorState.PhyAttack4;
            m_AttackStateDic["Skill1"] = RoleAnimatorState.Skill1;
            m_AttackStateDic["Skill2"] = RoleAnimatorState.Skill2;
            m_AttackStateDic["Skill3"] = RoleAnimatorState.Skill3;
            m_AttackStateDic["Skill4"] = RoleAnimatorState.Skill4;
            m_AttackStateDic["Skill5"] = RoleAnimatorState.Skill5;
            m_AttackStateDic["Skill6"] = RoleAnimatorState.Skill6;
            m_AttackStateDic["Skill7"] = RoleAnimatorState.Skill7;
            m_AttackStateDic["Skill8"] = RoleAnimatorState.Skill8;
        }
        string key = string.Format("{0}{1}", type == RoleAttackType.PhyAttack ? "PhyAttack" : "Skill", index);
        if (m_AttackStateDic.ContainsKey(key))
        {
            return m_AttackStateDic[key];
        }
        return RoleAnimatorState.PhyAttack1;
    }
    #endregion

    public static void LoadStar(Transform parent, int starCount = 3, int totalCount = 3, int row = 3, float showStarDelay = 0f)
    {
        GameObject obj = null;
        if (parent.transform.childCount == 0)
        {
            obj = ResourcesManager.Instance.LoadOther("StarContainer");
            obj.SetParent(parent);
            obj.GetComponent<StarContainer>().Init(starCount, totalCount, row, showStarDelay);
        }
        else
        {
            obj = parent.transform.GetChild(0).gameObject;
            obj.GetComponent<StarContainer>().SetStarCount(starCount);
        }
    }

    /// <summary> 关卡角色复活 </summary>
    public static void OnGameLevelRoleResurgence()
    {
        if (GlobalInit.Instance.CurrPlayer != null && GlobalInit.Instance.CurrPlayer.CurrRoleInfo != null)
        {
            GlobalInit.Instance.CurrPlayer.CurrRoleInfo.CurrHP = GlobalInit.Instance.CurrPlayer.CurrRoleInfo.MaxHP;
            GlobalInit.Instance.CurrPlayer.CurrRoleInfo.CurrMP = GlobalInit.Instance.CurrPlayer.CurrRoleInfo.MaxMP;
            GlobalInit.Instance.CurrPlayer.GetComponent<CharacterController>().enabled = true;
            GlobalInit.Instance.CurrPlayer.LockEnemy = null;
            GlobalInit.Instance.CurrPlayer.ToIdle(RoleIdleState.IdleFight);
        }
    }

    /// <summary> 角色回到主城 </summary>
    public static void EnterToCity()
    {
        if (GlobalInit.Instance.CurrPlayer != null && GlobalInit.Instance.CurrPlayer.CurrRoleInfo != null)
        {
            GlobalInit.Instance.CurrPlayer.CurrRoleInfo.CurrHP = GlobalInit.Instance.CurrPlayer.CurrRoleInfo.MaxHP;
            GlobalInit.Instance.CurrPlayer.CurrRoleInfo.CurrMP = GlobalInit.Instance.CurrPlayer.CurrRoleInfo.MaxMP;
            GlobalInit.Instance.CurrPlayer.GetComponent<CharacterController>().enabled = true;
            GlobalInit.Instance.CurrPlayer.LockEnemy = null;
            GlobalInit.Instance.CurrPlayer.ToIdle();
        }
    }
}