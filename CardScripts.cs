using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardScripts : MonoBehaviour
{
    typedef struct CCode
    {
    public int CardCode; //몬스터의 코드
    public int MonsterHp; //몬스터의 체력 HpSystem
    public int MonsterPower; //몬스터의 공격력
    public int MonsterDamageType; //몬스터의 대미지 타입 1:물리2:마법3:고정4:혼합
    }
    
    CCode spider;

    void awake()
    {
    spider.CardCode = 0;
    spider.MonsterHp = 2;
    spider.MonstrrPower = 1;
    spider.MonsterDamageType = 1;
    }
    
    enum Code
    {
        Example
    };
    
}
