using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class CardC
{
    public string CardCode;//몬스터의 코드
    public int MonsterHp;//몬스터의 체력 HpSystem
    public int MonsterPower;//몬스터의 공격력
    public int MonsterCost;
    public int MonsterDamageType; //몬스터의 대미지 타입 1:물리2:마법3:고정4:혼합
}


public class CardScripts : MonoBehaviour
{
    public CardC[] CardState = new CardC[100];

    //MonsterScripts와 HpSystem과 상호작용 할것
    void Awake()
    {
        CardState[(int)Code.Spider].CardCode = "0";
        CardState[(int)Code.Spider].MonsterHp = 2;
        CardState[(int)Code.Spider].MonsterPower = 1;
        CardState[(int)Code.Spider].MonsterDamageType = 1;
        CardState[(int)Code.Spider].MonsterCost = 1;

        CardState[(int)Code.StrSpider].CardCode = "1";
        CardState[(int)Code.StrSpider].MonsterHp = 3;
        CardState[(int)Code.StrSpider].MonsterPower = 2;
        CardState[(int)Code.StrSpider].MonsterDamageType = 1;
        CardState[(int)Code.StrSpider].MonsterCost = 2;

        CardState[(int)Code.PowSpider].CardCode = "2";
        CardState[(int)Code.PowSpider].MonsterHp = 3;
        CardState[(int)Code.PowSpider].MonsterPower = 2;
        CardState[(int)Code.PowSpider].MonsterDamageType = 1;
        CardState[(int)Code.PowSpider].MonsterCost = 2;

    }

    void Update()
    {


    }

    enum Code
    {
        Spider, StrSpider, PowSpider
    };
}
