using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sommon : MonoBehaviour
{
    public GameObject SommonMonster,SommonMonster2, SommonMonster3;
    public GameObject ClickZone, ClickZone2, ClickZone3;
    private GameObject target;
    public Transform tr;
    public struct Monster
    {
        public string Cards;
        public int CardsOGCost;
        public int CardsRCost;
        public int CardsOGPower;
        public int CardsRPower;
        public int CardsOGHP;
        public int CardsRHP;
    }

    BattleSystem batsys;
    public Monster monster;
    bool TurnStart = false;

    private void Start()
    {
        SommonMonster.SetActive(false);
        SommonMonster2.SetActive(false);
        SommonMonster3.SetActive(false);
        ClickZone.SetActive(false);
        ClickZone2.SetActive(false);
        ClickZone3.SetActive(false);
    }

    public void MonSys(int cardnum)
    {
        batsys = gameObject.GetComponent<BattleSystem>();
        monster.Cards = batsys.Hand[cardnum].Cards;
        monster.CardsOGHP = batsys.Hand[cardnum].CardsOGHP;
        monster.CardsRHP = batsys.Hand[cardnum].CardsRHP;
        monster.CardsOGCost = batsys.Hand[cardnum].CardsOGCost;
        monster.CardsRCost = batsys.Hand[cardnum].CardsRCost;
        monster.CardsOGPower = batsys.Hand[cardnum].CardsOGPower;
        monster.CardsRPower = batsys.Hand[cardnum].CardsRPower;
    }

    public void ClickZoneOpen()
    {
        ClickZone.SetActive(true);
        ClickZone2.SetActive(true);
        ClickZone3.SetActive(true);
        ClickZone.transform.localPosition = new Vector3(-300, 150, 0);
        ClickZone2.transform.localPosition = new Vector3(-100, 0, 0);
        ClickZone3.transform.localPosition = new Vector3(-300, -150, 0);
    }

    public void SM1()
    {
        if (TurnStart == true)
        {
            TurnStart = false;
            if (batsys.FieldCount != 1)
            {
                SommonMonster.SetActive(true);
                SommonMonster.transform.localPosition = new Vector3(-300, 150, 0);
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
                batsys.FieldCount = 1;
            }
            else
            {
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
            }
        }
    }

    public void SM2()
    {
        if (TurnStart == true)
        {
            TurnStart = false;
            if (batsys.FieldCount != 2)
            {
                SommonMonster2.SetActive(true);
                SommonMonster2.transform.localPosition = new Vector3(-100, 0, 0);
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
                batsys.FieldCount = 2;
            }
            else
            {
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
            }
        }
    }

    public void SM3()
    {
        if (TurnStart == true)
        {
            TurnStart = false;
            if (batsys.FieldCount != 3)
            {
                SommonMonster3.SetActive(true);
                SommonMonster3.transform.localPosition = new Vector3(-300, -150, 0);
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
                batsys.FieldCount = 3;
            }
            else
            {
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
            }
        }
    }
}
