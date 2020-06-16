using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnemyBattle : MonoBehaviour
{
    public int Turn;
    public bool EnemyTurn = false;
    private void Start()
    {
        BattleSystem battleSystem = GameObject.Find("Systems").GetComponent<BattleSystem>();
        Turn = battleSystem.Turn; 
    }
    // Update is called once per frame
    void Update()
    {
        if(EnemyTurn == true)
        {
            //TurnOver();
        }
        else if(EnemyTurn == false)
        {
            //플레이어의 턴이라도 적은 패턴을 통해 플레이어의 공격에 대응할 것
        }
    }

    void TurnOver()
    {
        EnemyTurn = false;
        BattleSystem battleSystem = GameObject.Find("Systems").GetComponent<BattleSystem>();
        battleSystem.PlayerTurn = true;
    }
}
