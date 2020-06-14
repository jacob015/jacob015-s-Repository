using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cardsc : MonoBehaviour
{
    BattleSystem bs;

    public Transform[] Cardstr = new Transform[10];
    public Image[] Cards = new Image[10];
    public Text[] Costs = new Text[10];
    public Text[] Power = new Text[10];
    public GameObject[] HPgo = new GameObject[10]; //스킬 카드는 체력이 없으므로, Setactive false하기 위해 옵젝으로 저장
    Text[] HP = new Text[10];

    public GameObject LookCards;
    bool isOn;
    Image lookimg;
    public Text LCost;
    public Text LPower;
    public GameObject LHPgo;
    Text LHP;

    int b;
    bool ons;
    int CardOnNum;
    float time;

    Vector3 moveBefore;
    int moveCardnum;
    bool cardmoving;
    bool carduseReady;

    void Start()
    {
        bs = GameObject.Find("Systems").GetComponent<BattleSystem>();
        lookimg = LookCards.GetComponent<Image>();
        LHP = LHPgo.GetComponent<Text>();
        LookCards.SetActive(false);
        isOn = false;
        b = 0;
        ons = false;
        CardOnNum = 0;
        for (int i = 0; i < HP.Length; i++)
        {
            HP[i] = HPgo[i].GetComponent<Text>();
        }
    }

    public void CardOn(GameObject go)
    {
        if (!cardmoving)
        {
            ons = true;
            CardOnNum = int.Parse(go.name);
            Cardstr[CardOnNum].localScale = new Vector3(0.7f, 0.7f, 1f);
        }
    }
    public void CardOff()
    {
        time = 0;
        ons = false;
        isOn = false;
        Cardstr[CardOnNum].localScale = new Vector3(0.6f, 0.6f, 1f);
        LookCards.SetActive(false);
    }
    public void DragStart(GameObject go)
    {
        carduseReady = false;
        CardOff();
        moveBefore = go.transform.localPosition;
        cardmoving = true;
        moveCardnum = int.Parse(go.name);
    }
    public void DragEnd(GameObject go)
    {
        cardmoving = false;
        if (!carduseReady) //이 if문 밖에 하나 더 넣어 내 턴일 때만 실행되도록
        {
            go.transform.localPosition = moveBefore;
        }
        else if (carduseReady)
        {
            bs.CardUse(moveCardnum);
        }
    }

    void Update()
    {
        if (cardmoving)
        {
            Cardstr[moveCardnum].localPosition = new Vector3(Input.mousePosition.x - 641.7f, Input.mousePosition.y - 417.8f, 0f);
            if (Cardstr[moveCardnum].localPosition.y >= moveBefore.y + 250f)
            {
                carduseReady = true;
                GameObject go = Instantiate(Resources.Load("Prefabs/Effects/Stars"), Cardstr[moveCardnum]) as GameObject;
                Transform tr = go.GetComponent<Transform>();
                tr.parent = null;
                tr.localPosition = new Vector3(Cardstr[moveCardnum].position.x, Cardstr[moveCardnum].position.y, 95f);
                CardEffect ce = go.GetComponent<CardEffect>();
                ce.type = 4;
            }
            else
            {
                carduseReady = false;
            }
        }

        if (ons)
        {
            time += 1f * Time.deltaTime;
        }
        else
        {
            time = 0;
        }

        if (!isOn && time >= 0.5f)
        {
            isOn = true;
            LookCards.SetActive(true);
            RectTransform tr = LookCards.GetComponent<RectTransform>();
            tr.localScale = new Vector3(0f, 0f, 1f);
            scal = 0f;

            lookimg.sprite = Cards[CardOnNum].sprite;
            LCost.text = Costs[CardOnNum].text;
            LCost.color = Costs[CardOnNum].color;
            LPower.text = Power[CardOnNum].text;
            LPower.color = Power[CardOnNum].color;
            LHP.text = HP[CardOnNum].text;
            LHP.color = HP[CardOnNum].color;
            if (int.Parse(bs.Cards[CardOnNum]) < 20000)
            {
                LHPgo.SetActive(false);
            }
            else
            {
                LHPgo.SetActive(true);
            }

            StartCoroutine("LookAppear");
        }
    }

    float scal;
    IEnumerator LookAppear()
    {
        if (scal < 1f)
        {
            scal += 0.05f;
            RectTransform tr = LookCards.GetComponent<RectTransform>();
            tr.localScale = new Vector3(scal, scal, 1f);
            yield return new WaitForSeconds(0.002f);
            StartCoroutine("LookAppear");
        }
    }

    public void CardReloading()
    {
        for (int b = 0; b < bs.Cards.Length; b++)
        {
            if (bs.Cards[b] != "")
            {
                if (int.Parse(bs.Cards[b]) < 20000)
                {
                    HPgo[b].SetActive(false);
                }
                else
                {
                    HPgo[b].SetActive(true);
                }
            }

            Costs[b].text = "" + bs.CardsRCost[b];
            Power[b].text = "" + bs.CardsRPower[b];
            HP[b].text = "" + bs.CardsRHP[b];

            if (bs.CardsRCost[b] == bs.CardsOGCost[b])
            {
                if (bs.CardsRCost[b] > bs.Energy)
                {
                    Costs[b].color = Color.red;
                }
                else
                {
                    Costs[b].color = Color.white;
                }
            }
            else if (bs.CardsRCost[b] > bs.CardsOGCost[b])
            {
                Costs[b].color = Color.red;
            }
            else if (bs.CardsRCost[b] < bs.CardsOGCost[b])
            {
                if (bs.CardsRCost[b] > bs.Energy)
                {
                    Costs[b].color = Color.red;
                }
                else
                    Costs[b].color = Color.green;
            }

            if (bs.CardsRPower[b] == bs.CardsOGPower[b])
            {
                Power[b].color = Color.white;
            }
            else if (bs.CardsRPower[b] > bs.CardsOGPower[b])
            {
                Power[b].color = Color.green;
            }
            else if (bs.CardsRPower[b] < bs.CardsOGPower[b])
            {
                Power[b].color = Color.red;
            }

            if (bs.CardsRHP[b] == bs.CardsOGHP[b])
            {
                HP[b].color = Color.white;
            }
            else if (bs.CardsRHP[b] > bs.CardsOGHP[b])
            {
                HP[b].color = Color.green;
            }
            else if (bs.CardsRHP[b] < bs.CardsOGHP[b])
            {
                HP[b].color = Color.red;
            }

            //카드 코드 형식 : 1 / 00 / 01
            //첫 번째 숫자 : 카드 종류 - 1 : 스킬 / 2 : 탐험대원 등등...
            //두 번째 숫자 : 탐험대 종류 - 00 : 공용 / 01 : 화염 등등...
            //세 번째 숫자 : 만들어진 순서
            if (bs.Cards[b] == "10001")
            {
                Cards[b].sprite = Resources.Load<Sprite>("Sprites/Cards/JustCard");
            }
            else if (bs.Cards[b] == "20001")
            {
                Cards[b].sprite = Resources.Load<Sprite>("Sprites/Cards/JustUnit");
            }
        }
        
    }
}
