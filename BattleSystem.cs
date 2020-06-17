using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : MonoBehaviour
{
    public Text Decktxt;
    public GameObject[] CardImage = new GameObject[10];

    public string[] Decks = new string[100];
    public int[] DecksCost = new int[100];
    public int[] DecksPower = new int[100];
    public int[] DecksHP = new int[100];

    public struct Card
    {
        public string Cards;
        public int CardsOGCost;
        public int CardsRCost;
        public int CardsOGPower;
        public int CardsRPower;
        public int CardsOGHP;
        public int CardsRHP;
    }

    public struct BCard
    {
        public string CardCode;//몬스터의 코드
        public int MonsterHp;//몬스터의 체력 HpSystem
        public int MonsterPower;//몬스터의 공격력
        public int MonsterCost;
        public int MonsterDamageType; //몬스터의 대미지 타입 1:물리2:마법3:고정4:혼합
    }

    public int Energy;
    public int maxEnergy;
    public Text EnergyTxt;

    public Text Announce;

    public int CardPosition = 0;
    public int HandCount = 0;
    int deckcards;
    public int Turn = 0;
    public bool PlayerTurn; //첫턴은 항상 플레이어의 우선 나중에 수정 가능
    float AATC; //경고문 알파 값 변경
    public int FieldCount = 0;

    public Image TurnButton;
    bool CarduseCant;

    UIEffect ue;
    Cardsc csc;
    CardScripts CCode;
    HpSystem sommon;

    public BCard[] InfCard = new BCard[100];
    public Card[] Hand = new Card[10];
    public Card[] BDeck = new Card[100];

    void Start()
    {
        Energy = 3;
        maxEnergy = Energy;
        Color color = Announce.color;
        color.a = 0;
        Announce.color = color;
        AATC = 0;
        CopyState();
        DeckSame();
        AnemyBattle anemyBattle = GameObject.Find("Systems").GetComponent<AnemyBattle>();
        ue = gameObject.GetComponent<UIEffect>();
        csc = gameObject.GetComponent<Cardsc>();
        CCode = gameObject.GetComponent<CardScripts>();
        sommon = gameObject.GetComponent<HpSystem>();
        for (int i = 0; i < 10; i++)
        {
            CardImage[i].SetActive(false);
        }
        StartCoroutine("FirstTurnDrow");
    }
    public void CopyState()
    {
        for (int i = 0; i < 100; i++)
        {
            InfCard[i].CardCode = CCode.CardState[i].CardCode;
            InfCard[i].MonsterCost = CCode.CardState[i].MonsterCost;
            InfCard[i].MonsterDamageType = CCode.CardState[i].MonsterDamageType;
            InfCard[i].MonsterHp = CCode.CardState[i].MonsterHp;
            InfCard[i].MonsterPower = CCode.CardState[i].MonsterPower;
        }
        Decks[0] = InfCard[0].CardCode;
        DecksCost[0] = InfCard[0].MonsterCost;
        DecksPower[0] = InfCard[0].MonsterPower;
        DecksHP[0] = InfCard[0].MonsterHp;
    }

    IEnumerator FirstTurnDrow()
    {
        yield return new WaitForSeconds(0.75f);
        PlayerTurn = false;
        TurnStart();
        yield return new WaitForSeconds(0.75f);
        CardDraw();
        yield return new WaitForSeconds(0.75f);
        CardDraw();
        yield return new WaitForSeconds(0.75f);
        CardDraw();
    }

    public void DeckSame()
    {
        for (int i = 0; i < 100; i++)
        {
            if (Decks[i] == "")
            {
                deckcards = i;
                break;
            }
        }
        
        for (int i = 0; i < deckcards; i++)
        {
            BDeck[i].Cards = Decks[i];
            BDeck[i].CardsOGCost = DecksCost[i];
            BDeck[i].CardsRCost = BDeck[i].CardsOGCost;
            BDeck[i].CardsOGPower = DecksPower[i];
            BDeck[i].CardsRPower = BDeck[i].CardsOGPower;
            BDeck[i].CardsOGHP = DecksHP[i];
            BDeck[i].CardsRHP = BDeck[i].CardsOGHP;
        }
    }
    public void CardUse(int cardnum)
    {
        if (Hand[cardnum].CardsRCost > Energy)
        {
            Announce.text = "에너지가 부족하여 카드를 사용할 수 없습니다!";
            StopCoroutine("AnnounceAppear");
            StopCoroutine("AnnounceDisappear");
            AATC = 0;
            StartCoroutine("AnnounceAppear");
        }
        else
        {
            Energy -= Hand[cardnum].CardsRCost;
            if (Hand[cardnum].Cards == "10001")
            {
                Energy += Hand[cardnum].CardsRPower;
            }
            sommon.BattleField(cardnum);
            Hand[cardnum].Cards = null;
            Hand[cardnum].CardsOGCost = 0;
            Hand[cardnum].CardsRCost = 0;
            Hand[cardnum].CardsOGPower = 0;
            Hand[cardnum].CardsRPower = 0;
            Hand[cardnum].CardsOGHP = 0;
            Hand[cardnum].CardsRHP = 0;
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
        for (int i = 0; i < 10; i++)
        {
            if (Hand[i].Cards != null)
            {
                if (Hand[i].CardsRCost <= Energy)
                {
                    CarduseCant = false;
                    break;
                }
            }
            else
            {
                CarduseCant = true;
                break;
            }
        }
        csc.CardReloading();
    }
    public void CardDraw()
    {
        csc.CardReloading();
        
        int b = Random.Range(0, deckcards - 1);
        if (BDeck[b].Cards != null)
        {
            for (int j = 0; j < 10; j++)
            {
                if (Hand[j].Cards == null)
                {
                    Cardsc csc = gameObject.GetComponent<Cardsc>();
                    Hand[j] = BDeck[b];
                    csc.Costs[j].text = "" + Hand[j].CardsRCost;
                    csc.Power[j].text = "" + Hand[j].CardsRPower;
                    Text tx = csc.HPgo[j].GetComponent<Text>();
                    tx.text = "" + Hand[j].CardsRHP;
                    BDeck[b].Cards = null;
                    BDeck[b].CardsOGCost = 0;
                    BDeck[b].CardsRCost = 0;
                    BDeck[b].CardsOGPower = 0;
                    BDeck[b].CardsRPower = 0;
                    BDeck[b].CardsOGHP = 0;
                    BDeck[b].CardsRHP = 0;
                    CardReload(b);
                    HandCount++;
                    ue.CardDrowEffect(j + 1);
                    HandCard();
                    deckcards--;
                    break;
                }
                else if (Hand[9].Cards != null)
                {
                    BDeck[b].Cards = null;
                    BDeck[b].CardsOGCost = 0;
                    BDeck[b].CardsRCost = 0;
                    BDeck[b].CardsOGPower = 0;
                    BDeck[b].CardsRPower = 0;
                    BDeck[b].CardsOGHP = 0;
                    BDeck[b].CardsRHP = 0;
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
        else if (BDeck[0].Cards == null)
        {
            Announce.text = "덱에 카드가 없어 피해를 받습니다!";
            StopCoroutine("AnnounceAppear");
            StopCoroutine("AnnounceDisappear");
            AATC = 0;
            StartCoroutine("AnnounceAppear");
        }
            
        
        for (int i = 0; i < 10; i++)
        {
            if (Hand[i].Cards != null)
            {
                if (Hand[i].CardsRCost <= Energy)
                {
                    CarduseCant = false;
                    break;
                }
            }
            else
            {
                CarduseCant = true;
                break;
            }
        }
    }
    public void TurnEnd()
    {
        if (PlayerTurn)
        {
            PlayerTurn = false;
            Announce.text = "상대 턴";
            StopCoroutine("AnnounceAppear");
            StopCoroutine("AnnounceDisappear");
            AATC = 0;
            StartCoroutine("AnnounceAppear");
            AnemyBattle anemyBattle = GameObject.Find("Systems").GetComponent<AnemyBattle>();
            anemyBattle.EnemyTurn = true;
        }
    }
    public void TurnStart()
    {
        if (!PlayerTurn)
        {
            TurnButton.color = new Color(1f, 1f, 1f, 1f);
            PlayerTurn = true;
            Announce.text = "내 턴";
            StopCoroutine("AnnounceAppear");
            StopCoroutine("AnnounceDisappear");
            AATC = 0;
            StartCoroutine("AnnounceAppear");
            AnemyBattle anemyBattle = GameObject.Find("Systems").GetComponent<AnemyBattle>();
            anemyBattle.EnemyTurn = false;
            anemyBattle.Turn++;
            Energy = maxEnergy;
            CardDraw();
        }
    }

    public void ButtonOn()
    {
        if (PlayerTurn)
        {
            TurnButton.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        }
    }
    public void ButtonOff()
    {
        if (PlayerTurn)
        {
            TurnButton.color = new Color(1f, 1f, 1f, 1f);
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
            if (BDeck[i].Cards == null && BDeck[i + 1].Cards != null)
            {
                BDeck[i].Cards = BDeck[i + 1].Cards;
                BDeck[i].CardsOGCost = BDeck[i + 1].CardsOGCost;
                BDeck[i].CardsRCost = BDeck[i + 1].CardsRCost;
                BDeck[i].CardsOGPower = BDeck[i + 1].CardsOGPower;
                BDeck[i].CardsRPower = BDeck[i + 1].CardsRPower;
                BDeck[i].CardsOGHP = BDeck[i + 1].CardsOGHP;
                BDeck[i].CardsRHP = BDeck[i + 1].CardsRHP;
                BDeck[i + 1].Cards = null;
                BDeck[i + 1].CardsOGCost = 0;
                BDeck[i + 1].CardsRCost = 0;
                BDeck[i + 1].CardsOGPower = 0;
                BDeck[i + 1].CardsRPower = 0;
                BDeck[i + 1].CardsOGHP = 0;
                BDeck[i + 1].CardsRHP = 0;
            }
            else if (BDeck[i].Cards == null && BDeck[i + 1].Cards == null)
            {
                break;
            }
        }
        for (int i = Startnum; i < 9; i++)
        {
            if (Hand[i].Cards == null && Hand[i + 1].Cards != null)
            {
                Hand[i].Cards = Hand[i + 1].Cards;
                Hand[i].CardsOGCost = Hand[i + 1].CardsOGCost;
                Hand[i].CardsRCost = Hand[i + 1].CardsRCost;
                Hand[i].CardsOGPower = Hand[i + 1].CardsOGPower;
                Hand[i].CardsRPower = Hand[i + 1].CardsRPower;
                Hand[i].CardsOGHP = Hand[i + 1].CardsOGHP;
                Hand[i].CardsRHP = Hand[i + 1].CardsRHP;
                Hand[i + 1].Cards = null;
                Hand[i + 1].CardsOGCost = 0;
                Hand[i + 1].CardsRCost = 0;
                Hand[i + 1].CardsOGPower = 0;
                Hand[i + 1].CardsRPower = 0;
                Hand[i + 1].CardsOGHP = 0;
                Hand[i + 1].CardsRHP = 0;
            }
        }
    }
    public void CardGain(string Card, int Cost, int Power, int HP)
    {
        csc.CardReloading();
        BDeck[deckcards].Cards = Card;
        BDeck[deckcards].CardsOGCost = Cost;
        BDeck[deckcards].CardsRCost = Cost;
        BDeck[deckcards].CardsOGPower = Power;
        BDeck[deckcards].CardsRPower = Power;
        BDeck[deckcards].CardsOGHP = HP;
        BDeck[deckcards].CardsRHP = HP;
        deckcards++;
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

        if (PlayerTurn)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                CardDraw();
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                CardGain("10001", 1, 5, 0);
            }
            
            if (CarduseCant)
            {
                TurnButton.sprite = Resources.Load<Sprite>("Sprites/UIs/TurnEnd_On");
            }
            else
            {
                TurnButton.sprite = Resources.Load<Sprite>("Sprites/UIs/TurnEnd_Off");
            }
        }
        else
        {
            TurnButton.sprite = Resources.Load<Sprite>("Sprites/UIs/TurnEnemy");
            //플레이어 턴이 아니더라도 카드에 대응 할 수 있게 만들것
            if (Input.GetKeyDown(KeyCode.Q))
            {
                TurnStart();
            }
        }
    }
}
