using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sommon : MonoBehaviour {

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
        SommonMonster.SetActive(false);
        batsys = gameObject.GetComponent<BattleSystem>();
    }

    public void MonSys(int cardnum)
    {
        monster.Car
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

    }

}
