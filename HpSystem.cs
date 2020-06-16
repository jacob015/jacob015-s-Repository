using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpSystem : MonoBehaviour
{

    public GameObject SommonMonster;
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

    private void Start()
    {
        batsys = gameObject.GetComponent<BattleSystem>();
    }

    public void MonSys(int cardnum)
    {
        monster.Cards = batsys.Hand[cardnum].Cards;
        monster.CardsOGHP = batsys.Hand[cardnum].CardsOGHP;
        monster.CardsRHP = batsys.Hand[cardnum].CardsRHP;
        monster.CardsOGCost = batsys.Hand[cardnum].CardsOGCost;
        monster.CardsRCost = batsys.Hand[cardnum].CardsRCost;
        monster.CardsOGPower = batsys.Hand[cardnum].CardsOGPower;
        monster.CardsRPower = batsys.Hand[cardnum].CardsRPower;
    }

    public void BattleField(int cardnum)
    {
        MonSys(cardnum);
        batsys.FieldCount++;
        if (batsys.FieldCount == 1)
        {
            Instantiate(SommonMonster, transform.position = new Vector3(-324, 115, 0), transform.rotation);
            batsys.FieldCount++;
        }
        else if (batsys.FieldCount == 2)
        {
            Instantiate(SommonMonster, transform.position = new Vector3(-238, -13, 0), transform.rotation);
            batsys.FieldCount++;
        }
        else if (batsys.FieldCount == 3)
        {
            Instantiate(SommonMonster, transform.position = new Vector3(-324, -160, 0), transform.rotation);
            batsys.FieldCount = 0;
        }
    }
}
