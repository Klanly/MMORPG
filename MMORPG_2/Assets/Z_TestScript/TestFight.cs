/// <summary>
/// 功能描述    ：
/// 创 建 者    ：
/// 创建日期    ：2019-01-05 14:44:43 
/// 最后修改者  ：
/// 最后内容描述：
/// 最后修改日期：2019-01-05 14:44:43 
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>  </summary>
public class TestFight : MonoBehaviour {

    public RoleCtrl TestRoleCtrl;
	// Use this for initialization
	void Start () {
        EffectManager.Instance.Init();
        JobEntity entity = JobDBModel.Instance.Get(1);
        for (int j = 0; j < entity.WeaponPath.Length; j++)
        {
            GameObject weaponObj = AssetBundleManager.Instance.Load(string.Format("Download/Model/{0}/{1}.assetbundle", entity.WeaponFloader, entity.WeaponPath[j]), entity.WeaponPath[j]);
            Instantiate(weaponObj, TestRoleCtrl.transform.GetChild(0).Find(entity.WeaponParent[j]));
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        if (TestRoleCtrl == null) return;
        int posY = 0;
        if (GUI.Button(new Rect(1,posY,80,30),"普通待机"))
        {
            TestRoleCtrl.ToIdle();
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "战斗待机"))
        {
            TestRoleCtrl.ToIdle(RoleIdleState.IdleFight);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "跑"))
        {
            TestRoleCtrl.ToRun();
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "受伤"))
        {
            //TestRoleCtrl.ToHurt(0, 0);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "死亡"))
        {
            TestRoleCtrl.ToDie();
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "战斗胜利"))
        {
            TestRoleCtrl.ToSelect();
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击1"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.PhyAttack, 101);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击2"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.PhyAttack, 102);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击3"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.PhyAttack, 103);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "物理攻击4"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.PhyAttack, 104);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击1"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 105);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击2"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 106);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击3"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 107);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击4"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 108);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击5"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 109);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击6"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 110);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击7"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 111);
        }
        posY += 30;
        if (GUI.Button(new Rect(1, posY, 80, 30), "技能攻击8"))
        {
            TestRoleCtrl.ToAttack(RoleAttackType.SkillAttack, 112);
        }
    }
}
