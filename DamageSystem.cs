using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSystem : MonoBehaviour
{
    BattleSystem battle;
    Sommon sommon;
    AnemyBattle enemy;

    private void Start()
    {
        battle = gameObject.GetComponent<BattleSystem>();
        sommon = gameObject.GetComponent<Sommon>();
        enemy = gameObject.GetComponent<AnemyBattle>();
    }

    public int EnMobDamaging(int Spider, int MobNum, int Type)
    {
        int power = enemy.State[MobNum].EnemyRPower;
        for (int i = 0; i < 10; i++)
        {
            if (enemy.MobBuff[i].BuffCode1 == "100" && MobNum == 0)
            {
                power = enemy.State[MobNum].EnemyRPower + enemy.MobBuff[i].BuffPower1;
                break;
            }
            else if (enemy.MobBuff[i].BuffCode2 == "100" && MobNum == 1)
            {
                power = enemy.State[MobNum].EnemyRPower + enemy.MobBuff[i].BuffPower2;
                break;
            }
            else if (enemy.MobBuff[i].BuffCode3 == "100" && MobNum == 2)
            {
                power = enemy.State[MobNum].EnemyRPower + enemy.MobBuff[i].BuffPower3;
                break;
            }
        }
        for (int i = 0; i < 10; i++)
        {
            if (enemy.MobBuff[i].BuffCode1 == "104" && MobNum == 0)
            {
                float pp = power * 0.75f;
                power = Mathf.FloorToInt(pp);
                break;
            }
            else if (enemy.MobBuff[i].BuffCode2 == "104" && MobNum == 1)
            {
                float pp = power * 0.75f;
                power = Mathf.FloorToInt(pp);
                break;
            }
            else if (enemy.MobBuff[i].BuffCode3 == "104" && MobNum == 2)
            {
                float pp = power * 0.75f;
                power = Mathf.FloorToInt(pp);
                break;
            }
        }
        if (power < 0)
            power = 0;
        if (Type == 5)
            return power;
        else
        {
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode1 == "105" && Spider == 0)
                {
                    float pp = power * 1.25f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode2 == "105" && Spider == 1)
                {
                    float pp = power * 1.25f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode3 == "105" && Spider == 2)
                {
                    float pp = power * 1.25f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
            }
            if (Type == 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (sommon.MobBuff[i].BuffCode1 == "102" && Spider == 0)
                    {
                        power -= sommon.MobBuff[i].BuffPower1;
                        if (power < 0)
                            power = 0;
                        break;
                    }
                    else if (sommon.MobBuff[i].BuffCode2 == "102" && Spider == 1)
                    {
                        power -= sommon.MobBuff[i].BuffPower2;
                        if (power < 0)
                            power = 0;
                        break;
                    }
                    else if (sommon.MobBuff[i].BuffCode3 == "102" && Spider == 2)
                    {
                        power -= sommon.MobBuff[i].BuffPower3;
                        if (power < 0)
                            power = 0;
                        break;
                    }
                }
            }
            else if (battle.typed == 2)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (sommon.MobBuff[i].BuffCode1 == "103" && Spider == 0)
                    {
                        power -= sommon.MobBuff[i].BuffPower1;
                        if (power < 0)
                            power = 0;
                    }
                    else if (sommon.MobBuff[i].BuffCode2 == "103" && Spider == 1)
                    {
                        power -= sommon.MobBuff[i].BuffPower2;
                        if (power < 0)
                            power = 0;
                    }
                    else if (sommon.MobBuff[i].BuffCode3 == "103" && Spider == 2)
                    {
                        power -= sommon.MobBuff[i].BuffPower3;
                        if (power < 0)
                            power = 0;
                    }
                }
            }
            else if (battle.typed == 3)
            {
                int melee = 0;
                int magic = 0;
                for (int i = 0; i < 10; i++)
                {
                    if (sommon.MobBuff[i].BuffCode1 == "102" && Spider == 0)
                    {
                        melee = sommon.MobBuff[i].BuffPower1;
                        break;
                    }
                    else if (sommon.MobBuff[i].BuffCode2 == "102" && Spider == 1)
                    {
                        melee = sommon.MobBuff[i].BuffPower2;
                        break;
                    }
                    else if (sommon.MobBuff[i].BuffCode3 == "102" && Spider == 2)
                    {
                        melee = sommon.MobBuff[i].BuffPower3;
                        break;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    if (sommon.MobBuff[i].BuffCode1 == "103" && Spider == 0)
                    {
                        magic = sommon.MobBuff[i].BuffPower1;
                        break;
                    }
                    else if (sommon.MobBuff[i].BuffCode2 == "103" && Spider == 1)
                    {
                        magic = sommon.MobBuff[i].BuffPower2;
                        break;
                    }
                    else if (sommon.MobBuff[i].BuffCode3 == "103" && Spider == 2)
                    {
                        magic = sommon.MobBuff[i].BuffPower3;
                        break;
                    }
                }
                if (melee < magic)
                    power -= melee;
                else if (melee > magic)
                    power -= magic;
                else
                    power -= melee;
            }
        }
        return power;
    }

    public void MobDamaging(int mob)
    {
        Text annotxt = battle.Announce.GetComponent<Text>();
        battle.StopCoroutine("AnnounceAppear");
        battle.StopCoroutine("AnnounceDisappear");
        battle.AATC = 0;
        int FirstNum = sommon.FirstNum;
        if (mob >= 4 && mob <= 6)
        {
            int power = sommon.State[FirstNum].MobRPower;
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode1 == "100" && FirstNum == 0)
                {
                    power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower1;
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode2 == "100" && FirstNum == 1)
                {
                    power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower2;
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode2 == "100" && FirstNum == 1)
                {
                    power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower3;
                    break;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode1 == "104" && FirstNum == 0)
                {
                    float pp = power * 0.75f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode1 == "104" && FirstNum == 1)
                {
                    float pp = power * 0.75f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode3 == "104" && FirstNum == 2)
                {
                    float pp = power * 0.75f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
            }
            if (power < 0)
                power = 0;
            if (battle.typed == 5)
            {
                sommon.DmgPower = power;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    if (enemy.MobBuff[i].BuffCode1 == "105" && mob == 4)
                    {
                        float pp = power * 1.25f;
                        power = Mathf.FloorToInt(pp);
                        break;
                    }
                    else if (enemy.MobBuff[i].BuffCode2 == "105" && mob == 5)
                    {
                        float pp = power * 1.25f;
                        power = Mathf.FloorToInt(pp);
                        break;
                    }
                    else if (enemy.MobBuff[i].BuffCode3 == "105" && mob == 6)
                    {
                        float pp = power * 1.25f;
                        power = Mathf.FloorToInt(pp);
                        break;
                    }
                }
                sommon.DmgPower = power;
                if (battle.typed == 1)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (enemy.MobBuff[i].BuffCode1 == "102" && mob == 4)
                        {
                            sommon.DmgPower -= enemy.MobBuff[i].BuffPower1;
                            if (sommon.DmgPower < 0)
                                sommon.DmgPower = 0;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode2 == "102" && mob == 5)
                        {
                            sommon.DmgPower -= enemy.MobBuff[i].BuffPower2;
                            if (sommon.DmgPower < 0)
                                sommon.DmgPower = 0;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode3 == "102" && mob == 6)
                        {
                            sommon.DmgPower -= enemy.MobBuff[i].BuffPower3;
                            if (sommon.DmgPower < 0)
                                sommon.DmgPower = 0;
                            break;
                        }
                    }
                }
                else if (battle.typed == 2)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        if (enemy.MobBuff[i].BuffCode1 == "103" && mob == 4)
                        {
                            sommon.DmgPower -= enemy.MobBuff[i].BuffPower1;
                            if (sommon.DmgPower < 0)
                                sommon.DmgPower = 0;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode2 == "103" && mob == 5)
                        {
                            sommon.DmgPower -= enemy.MobBuff[i].BuffPower2;
                            if (sommon.DmgPower < 0)
                                sommon.DmgPower = 0;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode3 == "103" && mob == 6)
                        {
                            sommon.DmgPower -= enemy.MobBuff[i].BuffPower3;
                            if (sommon.DmgPower < 0)
                                sommon.DmgPower = 0;
                            break;
                        }
                    }
                }
                else if (battle.typed == 3)
                {
                    int melee = 0;
                    int magic = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        if (enemy.MobBuff[i].BuffCode1 == "102" && mob == 4)
                        {
                            melee = enemy.MobBuff[i].BuffPower1;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode2 == "102" && mob == 5)
                        {
                            melee = enemy.MobBuff[i].BuffPower2;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode3 == "102" && mob == 6)
                        {
                            melee = enemy.MobBuff[i].BuffPower3;
                            break;
                        }
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        if (enemy.MobBuff[i].BuffCode1 == "103" && mob == 4)
                        {
                            magic = enemy.MobBuff[i].BuffPower1;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode2 == "103" && mob == 5)
                        {
                            magic = enemy.MobBuff[i].BuffPower2;
                            break;
                        }
                        else if (enemy.MobBuff[i].BuffCode3 == "103" && mob == 6)
                        {
                            magic = enemy.MobBuff[i].BuffPower3;
                            break;
                        }
                    }
                    if (melee < magic)
                        sommon.DmgPower -= melee;

                    else if (melee > magic)
                        sommon.DmgPower -= magic;
                    else
                        sommon.DmgPower -= melee;
                }
                else if (battle.typed == 4)
                {
                    //같은 변수에 할당?
                }
            }
        }
        else
        {
            if (battle.typed != 5)
            {
                annotxt.text = "아군을 공격할 수는 없습니다!";
                return;
            }
            else
            {
                int power = sommon.State[FirstNum].MobRPower;
                for (int i = 0; i < 10; i++)
                {
                    if (sommon.MobBuff[i].BuffCode1 == "100" && FirstNum == 0)
                    {
                        power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower1;
                    }
                    else if (sommon.MobBuff[i].BuffCode2 == "100" && FirstNum == 1)
                    {
                        power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower2;
                    }
                    else if (sommon.MobBuff[i].BuffCode3 == "100" && FirstNum == 2)
                    {
                        power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower3;
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    if (sommon.MobBuff[i].BuffCode1 == "104" && FirstNum == 0)
                    {
                        float pp = power * 0.75f;
                        power = Mathf.FloorToInt(pp);
                        break;
                    }
                    if (sommon.MobBuff[i].BuffCode2 == "104" && FirstNum == 1)
                    {
                        float pp = power * 0.75f;
                        power = Mathf.FloorToInt(pp);
                        break;
                    }
                    if (sommon.MobBuff[i].BuffCode3 == "104" && FirstNum == 2)
                    {
                        float pp = power * 0.75f;
                        power = Mathf.FloorToInt(pp);
                        break;
                    }
                }
                if (power < 0)
                    power = 0;
                sommon.DmgPower = power;
            }
        }
        AnnoTexting(sommon.DmgPower);
        battle.Announce.SetActive(true);
        battle.BlackBack.SetActive(true);
        annotxt.color = new Color(1f, 1f, 1f, 1f);
        battle.BlackBack.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.7f);
    }

    public void MobAnnoOriginal()
    {
        Text annotxt = battle.Announce.GetComponent<Text>();
        battle.StopCoroutine("AnnounceAppear");
        battle.StopCoroutine("AnnounceDisappear");
        battle.AATC = 0;
        int FirstNum = sommon.FirstNum;
        if (!battle.isCard)
        {
            int power = sommon.State[FirstNum].MobRPower;
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode1 == "100" && FirstNum == 0)
                {
                    power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower1;
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode2 == "100" && FirstNum == 1)
                {
                    power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower2;
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode3 == "100" && FirstNum == 2)
                {
                    power = sommon.State[FirstNum].MobRPower + sommon.MobBuff[i].BuffPower3;
                    break;
                }
            }
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode1 == "104" && FirstNum == 0)
                {
                    float pp = power * 0.75f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode2 == "104" && FirstNum == 1)
                {
                    float pp = power * 0.75f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode3 == "104" && FirstNum == 2)
                {
                    float pp = power * 0.75f;
                    power = Mathf.FloorToInt(pp);
                    break;
                }
            }
            if (power < 0)
                power = 0;
            AnnoTexting(power);
        }
        battle.Announce.SetActive(true);
        battle.BlackBack.SetActive(true);
        annotxt.color = new Color(1f, 1f, 1f, 1f);
        battle.BlackBack.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0.7f);
    }

    public void AnnoTexting(int p)
    {
        Text annotxt = battle.Announce.GetComponent<Text>();
        if (battle.typed == 0)
        {
            annotxt.text = "아군을 공격할 수는 없습니다!";
        }
        else if (battle.typed == 1)
        {
            annotxt.text = "적을 <b>" + p + "</b> 의 물리 피해로 공격합니다.";
        }
        else if (battle.typed == 2)
        {
            annotxt.text = "적을 <b>" + p + "</b> 의 마법 피해로 공격합니다.";
        }
        else if (battle.typed == 3)
        {
            annotxt.text = "적을 <b>" + p + "</b> 의 복합 피해로 공격합니다.";
        }
        else if (battle.typed == 4)
        {
            annotxt.text = "적을 <b>" + p + "</b> 의 관통 피해로 공격합니다.";
        }
        else if (battle.typed == 5)
        {
            annotxt.text = "대상을 <b>" + p + "</b> 만큼 회복시킵니다.";
        }
        else if (battle.typed == 10001)
        {
            annotxt.text = "대상에게 <b>" + p + "</b> 만큼 피해를 주고, <b>힘 4</b> 을 부여합니다.";
        }
        else if (battle.typed == 10002)
        {
            annotxt.text = "대상에게 <b>물리 방어 3</b>, <b>무뎌짐 2</b> 을 부여합니다.";
        }
        else if (battle.typed == 10003)
        {
            annotxt.text = "대상에게 <b>둔감 99</b> 을 부여합니다.";
        }
    }
    public void BuffGain(int Mob, string BuffCode, int BuffPower)
    {
        if (Mob == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode1 == null)
                {
                    sommon.MobBuff[i].BuffCode1 = BuffCode;
                    sommon.MobBuff[i].BuffPower1 = BuffPower;
                    break;
                }
                else if (sommon.MobBuff[i].BuffCode1 == BuffCode)
                {
                    sommon.MobBuff[i].BuffPower1 += BuffPower;
                    break;
                }
                else if (sommon.MobBuff[9].BuffCode1 != null)
                {
                    sommon.MobBuff[0].BuffCode1 = null;
                    sommon.MobBuff[0].BuffPower1 = 0;
                    BuffReload();
                }
            }
        }
        else if (Mob == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode2 == null)
                {
                    sommon.MobBuff[i].BuffCode2 = BuffCode;
                    sommon.MobBuff[i].BuffPower2 = BuffPower;
                }
                else if (sommon.MobBuff[i].BuffCode2 == BuffCode)
                {
                    sommon.MobBuff[i].BuffPower2 += BuffPower;
                    break;
                }
                else if (sommon.MobBuff[9].BuffCode2 != null)
                {
                    sommon.MobBuff[0].BuffCode2 = null;
                    sommon.MobBuff[0].BuffPower2 = 0;
                    BuffReload();
                }
            }
        }
        else if (Mob == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                if (sommon.MobBuff[i].BuffCode3 == null)
                {
                    sommon.MobBuff[i].BuffCode3 = BuffCode;
                    sommon.MobBuff[i].BuffPower3 = BuffPower;
                }
                else if (sommon.MobBuff[i].BuffCode3 == BuffCode)
                {
                    sommon.MobBuff[i].BuffPower3 += BuffPower;
                    break;
                }
                else if (sommon.MobBuff[9].BuffCode3 != null)
                {
                    sommon.MobBuff[0].BuffCode3 = null;
                    sommon.MobBuff[0].BuffPower3 = 0;
                    BuffReload();
                }
            }
        }
    }

    public void EnBuffGain(int Mob, string BuffCode, int BuffPower)
    {
        if (Mob == 0)
        {
            for (int i = 0; i < 10; i++)
            {
                if (enemy.MobBuff[i].BuffCode1 == null)
                {
                    enemy.MobBuff[i].BuffCode1 = BuffCode;
                    enemy.MobBuff[i].BuffPower1 = BuffPower;
                    break;
                }
                else if (enemy.MobBuff[i].BuffCode1 == BuffCode)
                {
                    enemy.MobBuff[i].BuffPower1 += BuffPower;
                    break;
                }
                else if (enemy.MobBuff[9].BuffCode1 != null)
                {
                    enemy.MobBuff[0].BuffCode1 = null;
                    enemy.MobBuff[0].BuffPower1 = 0;
                    //enemy.BuffReload;
                }
            }
        }
        else if (Mob == 1)
        {
            for (int i = 0; i < 10; i++)
            {
                if (enemy.MobBuff[i].BuffCode2 == null)
                {
                    enemy.MobBuff[i].BuffCode2 = BuffCode;
                    enemy.MobBuff[i].BuffPower2 = BuffPower;
                    break;
                }
                else if (enemy.MobBuff[i].BuffCode2 == BuffCode)
                {
                    enemy.MobBuff[i].BuffPower2 += BuffPower;
                    break;
                }
                else if (enemy.MobBuff[9].BuffCode2 != null)
                {
                    enemy.MobBuff[0].BuffCode2 = null;
                    enemy.MobBuff[0].BuffPower2 = 0;
                    //enemy.BuffReload;
                }
            }
        }
        else if (Mob == 2)
        {
            for (int i = 0; i < 10; i++)
            {
                if (enemy.MobBuff[i].BuffCode3 == null)
                {
                    enemy.MobBuff[i].BuffCode3 = BuffCode;
                    enemy.MobBuff[i].BuffPower3 = BuffPower;
                    break;
                }
                else if (enemy.MobBuff[i].BuffCode3 == BuffCode)
                {
                    enemy.MobBuff[i].BuffPower3 += BuffPower;
                    break;
                }
                else if (enemy.MobBuff[9].BuffCode3 != null)
                {
                    enemy.MobBuff[0].BuffCode3 = null;
                    enemy.MobBuff[0].BuffPower3 = 0;
                    //enemy.BuffReload;
                }
            }
        }
    }

    public void EnBuffReload()
    {
        for (int i = 0; i < 9; i++)
        {
            if (enemy.MobBuff[i].BuffPower1 == 0)
                enemy.MobBuff[i].BuffCode1 = null;
            if (enemy.MobBuff[i].BuffPower2 == 0)
                enemy.MobBuff[i].BuffCode2 = null;
            if (enemy.MobBuff[i].BuffPower3 == 0)
                enemy.MobBuff[i].BuffCode3 = null;
            if (enemy.MobBuff[i].BuffCode1 == null)
            {
                enemy.MobBuff[i].BuffCode1 = enemy.MobBuff[i + 1].BuffCode1;
                enemy.MobBuff[i].BuffPower1 = enemy.MobBuff[i + 1].BuffPower1;
                enemy.MobBuff[i + 1].BuffCode1 = null;
                enemy.MobBuff[i + 1].BuffPower1 = 0;
            }
            if (enemy.MobBuff[i].BuffCode2 == null)
            {
                enemy.MobBuff[i].BuffCode2 = enemy.MobBuff[i + 1].BuffCode2;
                enemy.MobBuff[i].BuffPower2 = enemy.MobBuff[i + 1].BuffPower2;
                enemy.MobBuff[i + 1].BuffCode2 = null;
                enemy.MobBuff[i + 1].BuffPower2 = 0;
            }
            if (enemy.MobBuff[i].BuffCode3 == null)
            {
                enemy.MobBuff[i].BuffCode3 = enemy.MobBuff[i + 1].BuffCode3;
                enemy.MobBuff[i].BuffPower3 = enemy.MobBuff[i + 1].BuffPower3;
                enemy.MobBuff[i + 1].BuffCode3 = null;
                enemy.MobBuff[i + 1].BuffPower3 = 0;
            }
        }
    }

    public void BuffReload()
    {
        for (int i = 0; i < 9; i++)
        {
            if (sommon.MobBuff[i].BuffPower1 == 0)
                sommon.MobBuff[i].BuffCode1 = null;
            if (sommon.MobBuff[i].BuffPower2 == 0)
                sommon.MobBuff[i].BuffCode2 = null;
            if (sommon.MobBuff[i].BuffPower3 == 0)
                sommon.MobBuff[i].BuffCode3 = null;
            if (sommon.MobBuff[i].BuffCode1 == null)
            {
                sommon.MobBuff[i].BuffCode1 = sommon.MobBuff[i + 1].BuffCode1;
                sommon.MobBuff[i].BuffPower1 = sommon.MobBuff[i + 1].BuffPower1;
                sommon.MobBuff[i + 1].BuffCode1 = null;
                sommon.MobBuff[i + 1].BuffPower1 = 0;
            }
            if (sommon.MobBuff[i].BuffCode2 == null)
            {
                sommon.MobBuff[i].BuffCode2 = sommon.MobBuff[i + 1].BuffCode2;
                sommon.MobBuff[i].BuffPower2 = sommon.MobBuff[i + 1].BuffPower2;
                sommon.MobBuff[i + 1].BuffCode2 = null;
                sommon.MobBuff[i + 1].BuffPower2 = 0;
            }
            if (sommon.MobBuff[i].BuffCode3 == null)
            {
                sommon.MobBuff[i].BuffCode3 = sommon.MobBuff[i + 1].BuffCode3;
                sommon.MobBuff[i].BuffPower3 = sommon.MobBuff[i + 1].BuffPower3;
                sommon.MobBuff[i + 1].BuffCode3 = null;
                sommon.MobBuff[i + 1].BuffPower3 = 0;
            }
        }
    }

}
