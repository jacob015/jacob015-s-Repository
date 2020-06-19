using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Sommon : MonoBehaviour
{
    public GameObject SommonMonster,SommonMonster2, SommonMonster3;
    public GameObject ClickZone, ClickZone2, ClickZone3, ClickOther;
    private GameObject target;
    
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
    public bool TurnStart = false;
    private int CardNum;
    public  bool SommonSuccess = false;

    private void Start()
    {
        SommonMonster.SetActive(false);
        SommonMonster2.SetActive(false);
        SommonMonster3.SetActive(false);
        ClickZone.SetActive(false);
        ClickZone2.SetActive(false);
        ClickZone3.SetActive(false);
        ClickOther.SetActive(false);
    }

    public void MonSys(int cardnum)
    {
        CardNum = cardnum;
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
        ClickOther.SetActive(true);
        ClickZone.transform.localPosition = new Vector3(-300, 150, 0);
        ClickZone2.transform.localPosition = new Vector3(-100, 0, 0);
        ClickZone3.transform.localPosition = new Vector3(-300, -150, 0);
    }

    public void SMother()
    {
        if (TurnStart == true)
        {
            SommonSuccess = false;
            ClickZone.SetActive(false);
            ClickZone2.SetActive(false);
            ClickZone3.SetActive(false);
            batsys.Energy += monster.CardsRCost;
            batsys.CardImage[CardNum].SetActive(true);
            batsys.HandCount++;
            ClickOther.SetActive(false);
        }
    }

    public void SM1()
    {
        batsys = gameObject.GetComponent<BattleSystem>();
        if (TurnStart == true)
        {
            TurnStart = false;
            if (batsys.FieldCount != 1)
            {
                SommonSuccess = true;
                SommonMonster.SetActive(true);
                SommonMonster.transform.localPosition = new Vector3(-300, 150, 0);
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
                ClickOther.SetActive(false);
                batsys.FieldCount = 1;
                monster.Cards = null;
                monster.CardsOGCost = 0;
                monster.CardsOGHP = 0;
                monster.CardsOGPower = 0;
                monster.CardsRCost = 0;
                monster.CardsRPower = 0;
                monster.CardsRHP = 0;
            }
            else if (batsys.FieldCount == 1)
            {
                ClickZone.SetActive(false);
            }
        }
    }

    public void SM2()
    {
        batsys = gameObject.GetComponent<BattleSystem>();
        if (TurnStart == true)
        {
            TurnStart = false;
            if (batsys.FieldCount != 2)
            {
                SommonSuccess = true;
                SommonMonster2.SetActive(true);
                SommonMonster2.transform.localPosition = new Vector3(-100, 0, 0);
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
                ClickOther.SetActive(false);
                batsys.FieldCount = 2;
                monster.Cards = null;
                monster.CardsOGCost = 0;
                monster.CardsOGHP = 0;
                monster.CardsOGPower = 0;
                monster.CardsRCost = 0;
                monster.CardsRPower = 0;
                monster.CardsRHP = 0;
            }
            else if (batsys.FieldCount == 2)
            {
                ClickZone2.SetActive(false);
            }
        }
    }

    public void SM3()
    {
        batsys = gameObject.GetComponent<BattleSystem>();
        if (TurnStart == true)
        {
            TurnStart = false;
            if (batsys.FieldCount != 3)
            {
                SommonSuccess = true;
                SommonMonster3.SetActive(true);
                SommonMonster3.transform.localPosition = new Vector3(-300, -150, 0);
                ClickZone.SetActive(false);
                ClickZone2.SetActive(false);
                ClickZone3.SetActive(false);
                ClickOther.SetActive(false);
                batsys.FieldCount = 3;
                monster.Cards = null;
                monster.CardsOGCost = 0;
                monster.CardsOGHP = 0;
                monster.CardsOGPower = 0;
                monster.CardsRCost = 0;
                monster.CardsRPower = 0;
                monster.CardsRHP = 0;
            }
            else if (batsys.FieldCount == 3)
            {
                ClickZone3.SetActive(false);
            }
        }
    }
}
