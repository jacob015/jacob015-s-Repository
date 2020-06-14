using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public Text Decktxt; //덱 UI 옆의 텍스트
    public GameObject[] CardImage = new GameObject[10]; //손패의 카드 오브젝트

    public string[] Decks = new string[100]; //게임이 진행되는 동안의 내 덱
    public int[] DecksCost = new int[100];
    public int[] DecksPower = new int[100];
    public int[] DecksHP = new int[100];

    public string[] BDecks = new string[100]; //전투 속에만 유효한 배틀 덱
    public int[] BDecksOGCost = new int[100]; //OG: 덱에서 가져온 카드의 본래 값
    public int[] BDecksRCost = new int[100]; //R: 전투에서 변경될 수 있는 값
    public int[] BDecksOGPower = new int[100]; //이 값의 비교에 따라 텍스트 색이 변함
    public int[] BDecksRPower = new int[100];
    public int[] BDecksOGHP = new int[100];
    public int[] BDecksRHP = new int[100];

    public string[] Cards = new string[10];
    public int[] CardsOGCost = new int[10];
    public int[] CardsRCost = new int[10];
    public int[] CardsOGPower = new int[10];
    public int[] CardsRPower = new int[10];
    public int[] CardsOGHP = new int[10];
    public int[] CardsRHP = new int[10];

    public int Energy;
    public Text EnergyTxt;

    public Text Announce;

    public int CardPosition = 0;
    public int HandCount = 0;
    public int deckcards;
    public int Turn = 0;
    public bool PlayerTurn = true; //첫턴은 항상 플레이어의 우선 나중에 수정 가능
    float AATC; //경고문 알파 값 변경

    UIEffect ue;
    Cardsc csc;

    void Start()
    {
        Color color = Announce.color;
        color.a = 0;
        Announce.color = color;
        AATC = 0;
        DeckSame();
        AnemyBattle anemyBattle = GameObject.Find("Systems").GetComponent<AnemyBattle>();
        ue = gameObject.GetComponent<UIEffect>();
        csc = gameObject.GetComponent<Cardsc>();
        for (int i = 0; i < 10; i++)
        {
            CardImage[i].SetActive(false);
        }
    }

    public void DeckSame()
    {
        for (int i = 0; i < Decks.Length; i++)
        {
            if (Decks[i] == "")
            {
                deckcards = i;
                break;
            }
        }
        for (int i = 0; i < deckcards; i++)
        {
            BDecks[i] = Decks[i];
            BDecksOGCost[i] = DecksCost[i];
            BDecksRCost[i] = BDecksOGCost[i];
            BDecksOGPower[i] = DecksPower[i];
            BDecksRPower[i] = BDecksOGPower[i];
            BDecksOGHP[i] = DecksHP[i];
            BDecksRHP[i] = BDecksOGHP[i];
        }
    }
    public void CardUse(int cardnum)
    {
        if (CardsRCost[cardnum] > Energy)
        {
            Announce.text = "에너지가 부족하여 카드를 사용할 수 없습니다!";
            StopCoroutine("AnnounceAppear");
            StopCoroutine("AnnounceDisappear");
            AATC = 0;
            StartCoroutine("AnnounceAppear");
        }
        else
        {
            Energy -= CardsRCost[cardnum];
            if (Cards[cardnum] == "10001")
            {
                Energy += CardsRPower[cardnum];
            }
            Cards[cardnum] = "";
            CardsOGCost[cardnum] = 0;
            CardsRCost[cardnum] = 0;
            CardsOGPower[cardnum] = 0;
            CardsRPower[cardnum] = 0;
            CardsOGHP[cardnum] = 0;
            CardsRHP[cardnum] = 0;
            HandCount--;
            CardImage[cardnum].SetActive(false);
            CardReload(cardnum);
            HandCard();
            for(int i = 0; i < HandCount; i++)
            {
                CardImage[i].SetActive(true);
            }
            for (int i = HandCount; i < 10; i++)
            {
                CardImage[i].SetActive(false);
            }
        }
        csc.CardReloading();
    }
    public void CardDraw()
    {
        csc.CardReloading();
        for (int i = 0; i < 10; i++)
        {
            int b = Random.Range(0, deckcards);
            if (BDecks[b] != "")
            {
                for (int j = 0; j < 10; j++)
                {
                    if (Cards[j] == "")
                    {
                        Cardsc csc = gameObject.GetComponent<Cardsc>();
                        Cards[j] = BDecks[b];
                        CardsOGCost[j] = BDecksOGCost[b];
                        CardsRCost[j] = BDecksRCost[b];
                        CardsOGPower[j] = BDecksOGPower[b];
                        CardsRPower[j] = BDecksRPower[b];
                        CardsOGHP[j] = BDecksOGHP[b];
                        CardsRHP[j] = BDecksRHP[b];
                        csc.Costs[j].text = "" + CardsRCost[j];
                        csc.Power[j].text = "" + CardsRPower[j];
                        Text tx = csc.HPgo[j].GetComponent<Text>();
                        tx.text = "" + CardsRHP[j];
                        BDecks[b] = "";
                        BDecksOGCost[b] = 0;
                        BDecksRCost[b] = 0;
                        BDecksOGPower[b] = 0;
                        BDecksRPower[b] = 0;
                        BDecksOGHP[b] = 0;
                        BDecksRHP[b] = 0;
                        CardReload(b);
                        HandCount++;
                        ue.CardDrowEffect(j+1);
                        HandCard();
                        deckcards--;
                        break;
                    }
                    else if (Cards[9] != "")
                    {
                        BDecks[b] = "";
                        BDecksOGCost[b] = 0;
                        BDecksRCost[b] = 0;
                        BDecksOGPower[b] = 0;
                        BDecksRPower[b] = 0;
                        BDecksOGHP[b] = 0;
                        BDecksRHP[b] = 0;
                        Announce.text = "카드를 더 이상 뽑을 수 없어 카드가 소멸됩니다.";
                        StopCoroutine("AnnounceAppear");
                        StopCoroutine("AnnounceDisappear");
                        AATC = 0;
                        StartCoroutine("AnnounceAppear");
                        CardReload(b);
                        deckcards--;
                        break;
                    }
                }
            }
            else if (BDecks[0] == "")
            {
                Announce.text = "덱에 카드가 없어 피해를 받습니다!";
                StopCoroutine("AnnounceAppear");
                StopCoroutine("AnnounceDisappear");
                AATC = 0;
                StartCoroutine("AnnounceAppear");
            }
            break;
        }
    }

    IEnumerator AnnounceAppear()
    {
        if (AATC < 1)
        {
            AATC += 0.02f;
            Color color = Announce.color;
            color.a = AATC;
            Announce.color = color;
            yield return new WaitForSeconds(0.02f);
            StartCoroutine("AnnounceAppear");
        }
        else
        {
            StartCoroutine("AnnounceDisappear");
        }
    }
    IEnumerator AnnounceDisappear()
    {
        if (AATC > 0)
        {
            AATC -= 0.02f;
            Color color = Announce.color;
            color.a = AATC;
            Announce.color = color;
            yield return new WaitForSeconds(0.02f);
            StartCoroutine("AnnounceDisappear");
        }
        else
        {
            AATC = 0;
        }
    }

    public void CardReload(int Startnum)
    {
        csc.CardReloading();
        for (int i = Startnum; i >= 0; i++)
        {
            if (BDecks[i] == "" && BDecks[i + 1] != "")
            {
                BDecks[i] = BDecks[i + 1];
                BDecksOGCost[i] = BDecksOGCost[i + 1];
                BDecksRCost[i] = BDecksRCost[i + 1];
                BDecksOGPower[i] = BDecksOGPower[i + 1];
                BDecksRPower[i] = BDecksRPower[i + 1];
                BDecksOGHP[i] = BDecksOGHP[i + 1];
                BDecksRHP[i] = BDecksRHP[i + 1];
                BDecks[i + 1] = "";
                BDecksOGCost[i + 1] = 0;
                BDecksRCost[i + 1] = 0;
                BDecksOGPower[i + 1] = 0;
                BDecksRPower[i + 1] = 0;
                BDecksOGHP[i + 1] = 0;
                BDecksRHP[i + 1] = 0;
            }
            else if (BDecks[i] == "" && BDecks[i + 1] == "")
            {
                break;
            }
        }
        for (int i = 0; i < 8; i++)
        {
            if (Cards[i] == "" && Cards[i + 1] != "")
            {
                Cards[i] = Cards[i + 1];
                CardsOGCost[i] = CardsOGCost[i + 1];
                CardsRCost[i] = CardsRCost[i + 1];
                CardsOGPower[i] = CardsOGPower[i + 1];
                CardsRPower[i] = CardsRPower[i + 1];
                CardsOGHP[i] = CardsOGHP[i + 1];
                CardsRHP[i] = CardsRHP[i + 1];
                Cards[i + 1] = "";
                CardsOGCost[i + 1] = 0;
                CardsRCost[i + 1] = 0;
                CardsOGPower[i + 1] = 0;
                CardsRPower[i + 1] = 0;
                CardsOGHP[i + 1] = 0;
                CardsRHP[i + 1] = 0;
            }
        }
    }
    public void CardGain(string Card, int Cost, int Power, int HP)
    {
        csc.CardReloading();
        BDecks[deckcards] = Card;
        BDecksOGCost[deckcards] = Cost;
        BDecksRCost[deckcards] = Cost;
        BDecksOGPower[deckcards] = Power;
        BDecksRPower[deckcards] = Power;
        BDecksOGHP[deckcards] = HP;
        BDecksRHP[deckcards] = HP;
        deckcards++;
    }

    void TurnOver()
    {
        PlayerTurn = false;
        AnemyBattle anemyBattle = GameObject.Find("Systems").GetComponent<AnemyBattle>();
        anemyBattle.EnemyTurn = true;
    }

    public void HandCard()
    {
        csc.CardReloading();
        RectTransform[] rt = new RectTransform[10];
        for (int i = 0; i < rt.Length; i++)
        {
            rt[i] = CardImage[i].GetComponent<RectTransform>();
        }
        if (HandCount == 1)
        {
            rt[0].localPosition = new Vector3(CardPosition, -350, 0);
        }
        else if (HandCount == 2)
        {
            rt[0].localPosition = new Vector3(CardPosition + 50, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition - 50, -350, 0);
        }
        else if (HandCount == 3)
        {
            rt[0].localPosition = new Vector3(CardPosition + 95, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition - 95, -350, 0);
        }
        else if (HandCount == 4)
        {
            rt[0].localPosition = new Vector3(CardPosition + 135, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition + 45, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition - 45, -350, 0);
            rt[3].localPosition = new Vector3(CardPosition - 135, -350, 0);
        }
        else if (HandCount == 5)
        {
            rt[0].localPosition = new Vector3(CardPosition + 170, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition + 85, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition, -350, 0);
            rt[3].localPosition = new Vector3(CardPosition - 85, -350, 0);
            rt[4].localPosition = new Vector3(CardPosition - 170, -350, 0);
        }
        else if (HandCount == 6)
        {
            rt[0].localPosition = new Vector3(CardPosition + 200, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition + 120, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition + 40, -350, 0);
            rt[3].localPosition = new Vector3(CardPosition - 40, -350, 0);
            rt[4].localPosition = new Vector3(CardPosition - 120, -350, 0);
            rt[5].localPosition = new Vector3(CardPosition - 200, -350, 0);
        }
        else if (HandCount == 7)
        {
            rt[0].localPosition = new Vector3(CardPosition + 225, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition + 150, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition + 75, -350, 0);
            rt[3].localPosition = new Vector3(CardPosition, -350, 0);
            rt[4].localPosition = new Vector3(CardPosition - 75, -350, 0);
            rt[5].localPosition = new Vector3(CardPosition - 150, -350, 0);
            rt[6].localPosition = new Vector3(CardPosition - 225, -350, 0);
        }
        else if (HandCount == 8)
        {
            rt[0].localPosition = new Vector3(CardPosition + 245, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition + 175, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition + 105, -350, 0);
            rt[3].localPosition = new Vector3(CardPosition + 35, -350, 0);
            rt[4].localPosition = new Vector3(CardPosition - 35, -350, 0);
            rt[5].localPosition = new Vector3(CardPosition - 105, -350, 0);
            rt[6].localPosition = new Vector3(CardPosition - 175, -350, 0);
            rt[7].localPosition = new Vector3(CardPosition - 245, -350, 0);
        }
        else if (HandCount == 9)
        {
            rt[0].localPosition = new Vector3(CardPosition + 260, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition + 195, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition + 130, -350, 0);
            rt[3].localPosition = new Vector3(CardPosition + 65, -350, 0);
            rt[4].localPosition = new Vector3(CardPosition, -350, 0);
            rt[5].localPosition = new Vector3(CardPosition - 65, -350, 0);
            rt[6].localPosition = new Vector3(CardPosition - 130, -350, 0);
            rt[7].localPosition = new Vector3(CardPosition - 195, -350, 0);
            rt[8].localPosition = new Vector3(CardPosition - 260, -350, 0);
        }
        else if (HandCount == 10)
        {
            rt[0].localPosition = new Vector3(CardPosition + 270, -350, 0);
            rt[1].localPosition = new Vector3(CardPosition + 210, -350, 0);
            rt[2].localPosition = new Vector3(CardPosition + 150, -350, 0);
            rt[3].localPosition = new Vector3(CardPosition + 90, -350, 0);
            rt[4].localPosition = new Vector3(CardPosition + 30, -350, 0);
            rt[5].localPosition = new Vector3(CardPosition - 30, -350, 0);
            rt[6].localPosition = new Vector3(CardPosition - 90, -350, 0);
            rt[7].localPosition = new Vector3(CardPosition - 150, -350, 0);
            rt[8].localPosition = new Vector3(CardPosition - 210, -350, 0);
            rt[9].localPosition = new Vector3(CardPosition - 270, -350, 0);
        }
    }


    void Update()
    {
        Decktxt.text = "" + deckcards;
        EnergyTxt.text = "" + Energy;
        if (deckcards == 0)
        {
            Decktxt.color = Color.red;
        }
        else
        {
            Decktxt.color = Color.white;
        }

        if (PlayerTurn == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CardDraw();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                CardGain("10001", 1, 5, 0);
            }
            //TurnOver();
        }
        else if (PlayerTurn == false)
        {
            //플레이어 턴이 아니더라도 카드에 대응 할 수 있게 만들것
        }
    }
}
