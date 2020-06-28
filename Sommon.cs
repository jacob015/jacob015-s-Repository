using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public struct Buff
{
    public string BuffCode1;
    public int BuffPower1;
    public string BuffCode2;
    public int BuffPower2;
    public string BuffCode3;
    public int BuffPower3;
}
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
public struct Status
{
    public string MobCode;
    public int MobCost;
    public int MobOGPower;
    public int MobRPower;
    public int MobOGHP;
    public int MobMaxHP;
    public int MobRHP;
    public int MobPowerType;
    public int MobArmor;
    public bool isAttacked;
}

public class Sommon : MonoBehaviour
{
    public GameObject[] SommonMonster = new GameObject[3];
    public GameObject[] StatusNBuff = new GameObject[3];

    public GameObject[] HPBars = new GameObject[3];
    public Image[] HPimage = new Image[3];
    public Text[] RealHP = new Text[3];
    public Text[] MaxHP = new Text[3];

    public Image[] Attackimage = new Image[3];
    public Text[] PowerTxt = new Text[3];
    public GameObject[] Armorgo = new GameObject[3];
    public Text[] ArmorTxt = new Text[3];

    public Buff[] MobBuff = new Buff[10];
    public GameObject[] MobBuffgo = new GameObject[30];
    public Text[] MobBufftxt = new Text[30];

    bool ons;
    bool isOn;
    int MobNum, EnyNum;
    public int FirstNum, SecondNum;
    float time;
    public GameObject LookCard;
    Image Lookimg;
    public Text Cost;
    public GameObject Powergo;
    Text Power;
    public GameObject HPgo;
    Text HP;
    public Text LExp;
    
    BattleSystem batsys;
    public Monster monster;
    public bool TurnStart = false;
    private int CardNum;
    public  bool SommonSuccess = false;

    public bool[] Sommoned = new bool[3];
    public Status[] State = new Status[3];

    public bool Seleting;
    bool AllySelect;
    bool EnemSelect;
    bool AllyBossSelect;
    bool EnemBossSelect;
    public bool isAllyAttack = false;
    public int ClickCount = 0;
    public GameObject Arrows;
    RectTransform rts;
    AnemyBattle ab;
    Cardsc csc;
    MobAnime ma;
    DamageSystem Dm;

    public GameObject BuffExp;
    public Text Buffexptxt;
    bool isExplain;

    public int DmgPower;
    public int typed;

    float xpos;

    private void Start()
    {
        batsys = gameObject.GetComponent<BattleSystem>();
        Lookimg = LookCard.GetComponent<Image>();
        rts = Arrows.GetComponent<RectTransform>();
        ab = gameObject.GetComponent<AnemyBattle>();
        ma = gameObject.GetComponent<MobAnime>();
        csc = gameObject.GetComponent<Cardsc>();
        Dm = gameObject.GetComponent<DamageSystem>();
        Power = Powergo.GetComponent<Text>();
        HP = HPgo.GetComponent<Text>();
        SommonMonster[0].SetActive(false);
        SommonMonster[1].SetActive(false);
        SommonMonster[2].SetActive(false);
        Sommoned[0] = false;
        Sommoned[1] = false;
        Sommoned[2] = false;
        StatusNBuff[0].SetActive(false);
        StatusNBuff[1].SetActive(false);
        StatusNBuff[2].SetActive(false);
        BuffExp.SetActive(false);
        ons = false;
        isOn = false;
        Seleting = false;
        isExplain = false;
        xpos = 0f;
        Updating();
    }

    //public void CardClick(GameObject go)
    //{
    //    bool ClickCard = false;
    //    bool ClickEnem = false;
    //
    //    if ()
    //}

    public void BuffOn(GameObject go) //적의 것도 관리함.
    {
        isExplain = true;
        string code = "";
        int power = 0;
        xpos = go.transform.position.x;
        if (int.Parse(go.name) <= 9)
        {
            code = MobBuff[int.Parse(go.name)].BuffCode1;
            power = MobBuff[int.Parse(go.name)].BuffPower1;
        }
        else if (int.Parse(go.name) >= 10 && int.Parse(go.name) <= 19)
        {
            code = MobBuff[int.Parse(go.name)-10].BuffCode2;
            power = MobBuff[int.Parse(go.name) - 10].BuffPower2;
        }
        else if (int.Parse(go.name) >= 20 && int.Parse(go.name) <= 29)
        {
            code = MobBuff[int.Parse(go.name) - 20].BuffCode3;
            power = MobBuff[int.Parse(go.name) - 20].BuffPower3;
        }
        else if (int.Parse(go.name) >= 30 && int.Parse(go.name) <= 39)
        {
            code = ab.MobBuff[int.Parse(go.name)-30].BuffCode1;
            power = ab.MobBuff[int.Parse(go.name) - 30].BuffPower1;
        }
        else if (int.Parse(go.name) >= 40 && int.Parse(go.name) <= 49)
        {
            code = ab.MobBuff[int.Parse(go.name) - 40].BuffCode2;
            power = ab.MobBuff[int.Parse(go.name) - 40].BuffPower2;
        }
        else if (int.Parse(go.name) >= 50 && int.Parse(go.name) <= 59)
        {
            code = ab.MobBuff[int.Parse(go.name) - 50].BuffCode3;
            power = ab.MobBuff[int.Parse(go.name) - 50].BuffPower3;
        }
        Buffexptxt.text = Buffexping(code, power);
    }

    public void AttackTypeOn(GameObject go)
    {
        isExplain = true;
        int a = int.Parse(go.name);
        xpos = go.transform.position.x;
        if (a <= 3)
        {
            Buffexptxt.text = Buffexping(State[a].MobPowerType+"", int.Parse(PowerTxt[a].text));
        }
        else
        {
            Buffexptxt.text = Buffexping(ab.State[a-4].EnemyType + "", int.Parse(ab.PowerTxt[a-4].text));
        }
    }
    public void ArmorOn(GameObject go)
    {
        isExplain = true;
        int a = int.Parse(go.name);
        xpos = go.transform.position.x;
        if (a <= 3)
        {
            Buffexptxt.text = Buffexping("0", State[a].MobArmor);
        }
        else
        {
            Buffexptxt.text = Buffexping("0", ab.State[a - 4].EnemyArmor);
        }
    }

    string Buffexping(string code, int power)
    {
        if (code == "0")
        {
            return "* 이 유닛은 <b>" + power + " </b> 만큼의 방어도를\n가지고 있습니다.";
        }
        else if (code == "1")
        {
            return "* 이 유닛은 <b>" + power + " </b> 만큼의 물리 공격을\n할 수 있습니다.";
        }
        else if (code == "2")
        {
            return "* 이 유닛은 <b>" + power + " </b> 만큼의 마법 공격을\n할 수 있습니다.";
        }
        else if (code == "3")
        {
            return "* 이 유닛은 <b>" + power + " </b> 만큼의 복합 공격을\n할 수 있습니다. 복합 공격은 대상의\n방어를 고려해, 물리와 마법 중 더 큰\n피해를 줄 수 있는 쪽으로 공격합니다.";
        }
        else if (code == "4")
        {
            return "* 이 유닛은 <b>" + power + " </b> 만큼의 관통 공격을\n할 수 있습니다. 방어로 이 피해를\n감소시킬 수 없습니다.";
        }
        else if (code == "5")
        {
            return "이 유닛은 <b>" + power + " </b> 만큼 대상을\n회복시킬 수 있습니다.";
        }
        else if (code == "100")
        {
            return "* 힘\n공격 파워가 <b>" + power + "</b> 만큼 증가합니다."; 
        }
        else if (code == "101")
        {
            return "* 강인함\n최대 체력이 <b>" + power + "</b> 만큼 증가합니다.";
        }
        else if (code == "102")
        {
            return "* 물리 방어\n물리 공격으로 입는 피해가 <b>" + power + "</b> 만큼\n감소합니다.";
        }
        else if (code == "103")
        {
            return "* 마법 방어\n마법 공격으로 입는 피해가 <b>" + power + "</b> 만큼\n감소합니다.";
        }
        else if (code == "104")
        {
            return "* 무뎌짐\n공격 파워가 25% 감소합니다.\n<b>" + power + "</b> 턴 동안 지속됩니다.";
        }
        else if (code == "105")
        {
            return "* 둔감\n받는 피해가 25% 증가합니다.\n<b>" + power + "</b> 턴 동안 지속됩니다.";
        }
        else
        {
            return "* 알 수 없음\n이 효과는 아직 추가되지 않았습니다.";
        }
    }

    public void BuffOff()
    {
        isExplain = false;
        Buffexptxt.text = "";
    }

    public void MobClick(GameObject go)
    {
        if (batsys.isCard == false)
        {
            bool ClickAlly = false;
            bool ClickEnem = false;
            bool ClickABoss = false;
            bool ClickEBoss = false;
            if (go.name == "Mob1")
            {
                if (Seleting)
                    SecondNum = 0;
                else
                    FirstNum = 0;
                ClickAlly = true;
            }
            else if (go.name == "Mob2")
            {
                if (Seleting)
                    SecondNum = 1;
                else
                    FirstNum = 1;
                ClickAlly = true;
            }
            else if (go.name == "Mob3")
            {
                if (Seleting)
                    SecondNum = 2;
                else
                    FirstNum = 2;
                ClickAlly = true;
            }
            else if (go.name == "MobBoss")
            {
                if (Seleting)
                    SecondNum = 3;
                else
                    FirstNum = 3;
                ClickABoss = true;
            }
            else if (go.name == "Enemy1")
            {
                if (Seleting)
                    SecondNum = 4;
                else
                    FirstNum = 4;
                ClickEnem = true;
            }
            else if (go.name == "Enemy2")
            {
                if (Seleting)
                    SecondNum = 5;
                else
                    FirstNum = 5;
                ClickEnem = true;
            }
            else if (go.name == "Enemy3")
            {
                if (Seleting)
                    SecondNum = 6;
                else
                    FirstNum = 6;
                ClickEnem = true;
            }
            else if (go.name == "EnemyBoss")
            {
                if (Seleting)
                    SecondNum = 7;
                else
                    FirstNum = 7;
                ClickEBoss = true;
            }

            if (!Seleting && ClickAlly)
            {
                if (!State[FirstNum].isAttacked)
                {
                    ClickCount++;
                    time = 0f;
                    ons = false;
                    isOn = false;
                    Image img = go.GetComponent<Image>();
                    img.color = new Color(1f, 1f, 1f, 1f);
                    LookCard.SetActive(false);
                    Seleting = true;
                    int power = State[FirstNum].MobRPower;
                    DmgPower = power;
                    typed = State[FirstNum].MobPowerType;
                    ma.SpriteChanging(FirstNum, State[FirstNum].MobCode, 2);
                    for (int i = 0; i < 10; i++)
                    {
                        if (MobBuff[i].BuffCode1 == "100" && FirstNum == 0)
                        {
                            power = State[FirstNum].MobRPower + MobBuff[i].BuffPower1;
                            break;
                        }
                        else if (MobBuff[i].BuffCode2 == "100" && FirstNum == 1)
                        {
                            power = State[FirstNum].MobRPower + MobBuff[i].BuffPower2;
                            break;
                        }
                        else if (MobBuff[i].BuffCode3 == "100" && FirstNum == 2)
                        {
                            power = State[FirstNum].MobRPower + MobBuff[i].BuffPower3;
                            break;
                        }
                    }
                    if (power < 0)
                        power = 0;
                    batsys.Select_Attack(State[FirstNum].MobPowerType, power);
                    if (State[FirstNum].MobPowerType == 5)
                    {
                        AllySelect = true;
                        AllyBossSelect = true;
                        EnemSelect = true;
                        EnemBossSelect = true;
                    }
                    else
                    {
                        AllySelect = false;
                        AllyBossSelect = false;
                        EnemSelect = true;
                        EnemBossSelect = true;
                    }
                    MobDamaging(int.Parse(go.name));
                }
                else
                {
                    batsys.Announcing("이 탐험대원은 아직 공격할 수 없습니다!");
                }
            }
            else if (Seleting && ClickEnem)
            {
                SelectEnd();
                if (SecondNum >= 4 && SecondNum <= 6) //대상이 '무리' 일 경우
                {
                    if (State[FirstNum].MobPowerType == 5)
                    {
                        ab.State[SecondNum - 4].EnemyRHp += DmgPower;
                        if (ab.State[SecondNum-4].EnemyRHp > ab.State[SecondNum - 4].EnemyMaxHP)
                            ab.State[SecondNum - 4].EnemyRHp = ab.State[SecondNum - 4].EnemyMaxHP;
                        GameObject tx = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DmgText"), transform) as GameObject;
                        tx.transform.parent = GameObject.Find("Canvas").transform;
                        tx.transform.localScale = new Vector3(1f, 1f, 1f);
                        tx.GetComponent<RectTransform>().localPosition = go.GetComponent<RectTransform>().localPosition;
                        tx.GetComponent<DmgText>().Texting(2, DmgPower);
                    }
                    else
                    {
                        if (ab.State[SecondNum - 4].EnemyArmor >= DmgPower)
                        {
                            ab.State[SecondNum - 4].EnemyArmor -= DmgPower;
                            GameObject tx = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DmgText"), transform) as GameObject;
                            tx.transform.parent = GameObject.Find("Canvas").transform;
                            tx.transform.localScale = new Vector3(1f, 1f, 1f);
                            tx.GetComponent<RectTransform>().localPosition = go.GetComponent<RectTransform>().localPosition;
                            tx.GetComponent<DmgText>().Texting(1, DmgPower);
                        }
                        else
                        {
                            int dmg = DmgPower - ab.State[SecondNum - 4].EnemyArmor;
                            ab.State[SecondNum - 4].EnemyArmor = 0;
                            ab.State[SecondNum - 4].EnemyRHp -= dmg;
                            GameObject tx = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DmgText"), transform) as GameObject;
                            tx.transform.parent = GameObject.Find("Canvas").transform;
                            tx.transform.localScale = new Vector3(1f, 1f, 1f);
                            tx.GetComponent<RectTransform>().localPosition = go.GetComponent<RectTransform>().localPosition;
                            tx.GetComponent<DmgText>().Texting(1, DmgPower);
                        }
                    }
                }
                State[FirstNum].isAttacked = true;
            }
            else if (Seleting && ClickAlly)
            {
                SelectEnd();
                if (SecondNum <= 2) //대상이 '탐험대원' 일 경우
                {
                    if (State[FirstNum].MobPowerType == 5) //회복
                    {
                        State[SecondNum].MobRHP += DmgPower;
                        if (State[SecondNum].MobRHP > State[SecondNum].MobMaxHP)
                            State[SecondNum].MobRHP = State[SecondNum].MobMaxHP;
                        GameObject tx = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DmgText"), transform) as GameObject;
                        tx.transform.parent = GameObject.Find("Canvas").transform;
                        tx.transform.localScale = new Vector3(1f, 1f, 1f);
                        tx.GetComponent<RectTransform>().localPosition = go.GetComponent<RectTransform>().localPosition;
                        tx.GetComponent<DmgText>().Texting(2, DmgPower);
                        State[FirstNum].isAttacked = true;
                    }
                    else //딜링
                    {
                        batsys.Announcing("아군을 공격할 수는 없습니다!");
                    }
                }
            }
        }
    }

    public void SelectEnd()
    {
        if (!batsys.isCard)
            ma.AttackAfter(FirstNum);
        Seleting = false;
        batsys.StartCoroutine("AnnounceDisappear");
        Image img;
        img = SommonMonster[0].GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        img = SommonMonster[1].GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        img = SommonMonster[2].GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        img = ab.SommonMonster[0].GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        img = ab.SommonMonster[1].GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        img = ab.SommonMonster[2].GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
    }

    public void MobViewOn(GameObject go)
    {
        int a = 0;
        if (go.name == "Mob1")
        {
            MobNum = 0;
            a = 0;
            ons = true;
        }
        else if (go.name == "Mob2")
        {
            MobNum = 1;
            a = 1;
            ons = true;
        }
        else if (go.name == "Mob3")
        {
            MobNum = 2;
            a = 2;
            ons = true;
        }
        else if (go.name == "Enemy1")
            a = 4;
        else if (go.name == "Enemy2")
            a = 5;
        else if (go.name == "Enemy3")
            a = 6;
        Image img = go.GetComponent<Image>();
        img.color = new Color(0.7f, 0.7f, 0.7f, 1f);
        if (Seleting && !batsys.isCard)
        {
            batsys.typed = State[FirstNum].MobPowerType;
            MobDamaging(a);
        }
        else if (Seleting && batsys.isCard)
        {
            DmgPower = batsys.powered;
            AnnoTexting(DmgPower);
        }
            
    }
    public void MobViewOut(GameObject go)
    {
        time = 0f;
        ons = false;
        isOn = false;
        Image img = go.GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        LookCard.SetActive(false);
        if (Seleting && !batsys.isCard)
            MobAnnoOriginal();
        else if (Seleting && batsys.isCard)
            AnnoTexting(batsys.powered);
    }

    public void MobDamaging(int mob)
    {
        Dm.MobDamaging(mob);
    }

    public void MobAnnoOriginal()
    {
        Dm.MobAnnoOriginal();
    }
    void AnnoTexting(int p)
    {
        Dm.AnnoTexting(p);
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
        
        for (int i = 0; i < Sommoned.Length; i++)
        {
            if (!Sommoned[i])
            {
                Sommoned[i] = true;
                SommonMonster[i].SetActive(true);
                StatusNBuff[i].SetActive(true);
                State[i].isAttacked = true;
                State[i].MobCode = monster.Cards;
                State[i].MobCost = monster.CardsOGCost;
                State[i].MobOGPower = monster.CardsOGPower;
                State[i].MobRPower = monster.CardsRPower;
                State[i].MobMaxHP = monster.CardsRHP;
                State[i].MobOGHP = monster.CardsOGHP;
                State[i].MobRHP = monster.CardsRHP;
                Image img = SommonMonster[i].GetComponent<Image>();
                if (State[i].MobCode == "20001")
                {
                    img.sprite = Resources.Load<Sprite>("Sprites/AttackSpider1");
                    State[i].MobPowerType = 5;
                }
                RectTransform rt = SommonMonster[i].GetComponent<RectTransform>();
                if (i == 0)
                {
                    rt.localPosition = new Vector3(-410f, 80f, 0);
                }
                if (i == 1)
                {
                    rt.localPosition = new Vector3(-200f, -60f, 0);
                }
                if (i == 2)
                {
                    rt.localPosition = new Vector3(-410f, -200f, 0);
                }
                break;
            }
        }
    }

    public void BuffGain(int mob, string buffcode, int buffpower)
    {
        Dm.BuffGain(mob, buffcode, buffpower);
    }

    public void BuffReload()
    {
        Dm.BuffReload();
    }

    private void Update()
    {
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
            LookCard.SetActive(true);
            RectTransform tr = LookCard.GetComponent<RectTransform>();
            if (MobNum <= 2)
                tr.localPosition = new Vector3(580f, 0f, 0f);
            tr.localScale = new Vector3(0f, 0f, 1f);
            scal = 0f;

            Powergo.SetActive(true);
            HPgo.SetActive(true);

            Cost.text = "" + State[MobNum].MobCost;
            Power.text = "" + State[MobNum].MobOGPower;
            HP.text = ""+State[MobNum].MobOGHP;
            
            RectTransform rtt = csc.LBuffExp[0].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(-350, 130, 0);
            rtt = csc.LBuffExp[1].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(-350, -40, 0);
            rtt = csc.LBuffExp[2].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(-725, 130, 0);
            rtt = csc.LBuffExp[3].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(-725, -40, 0);

            if (State[MobNum].MobCode == "20001")
            {
                LExp.text = "";
                Lookimg.sprite = Resources.Load<Sprite>("Sprites/Cards/JustUnit");
            }
            else if (State[MobNum].MobCode == "00000")
            {
                LExp.text = "다른 곳에서부터 내려온\n아이입니다. 어쩌다가?";
                Lookimg.sprite = Resources.Load<Sprite>("Sprites/Cards/LarvaCard");
            }

            StartCoroutine("LookAppear");
        }
        if (Seleting)
        {
            Arrows.SetActive(true);
            rts.localPosition = new Vector3(Input.mousePosition.x - 958f, Input.mousePosition.y - 540f, 0f); 
        }
        else if (!Seleting && !ab.EnemyTurn)
        {
            Arrows.SetActive(false);
        }
        rts.Rotate(0f, 0f, 180f * Time.deltaTime);
        if (isExplain)
        {
            BuffExp.SetActive(true);
            RectTransform rtt = BuffExp.GetComponent<RectTransform>();
            if (xpos < 0f)
                rtt.localPosition = new Vector3(Input.mousePosition.x - 670f, Input.mousePosition.y - 410f, 0f);
            else
                rtt.localPosition = new Vector3(Input.mousePosition.x - 1240f, Input.mousePosition.y - 410f, 0f);
        }
        else
        {
            BuffExp.SetActive(false);
        }
    }

    float scal;
    IEnumerator LookAppear()
    {
        if (scal < 1.5f)
        {
            scal += 0.16f;
            RectTransform tr = LookCard.GetComponent<RectTransform>();
            tr.localScale = new Vector3(scal, scal, 1f);
            yield return new WaitForSeconds(0.002f);
            StartCoroutine("LookAppear");
        }
    }

    public Sprite Buffspr(string code)
    {
        Sprite spr = null;
        if (code == "100")
            spr = Resources.Load<Sprite>("Sprites/Buffs/Buff_Power");

        return spr;
    }

    void Updating()
    {
        for (int i = 0; i < SommonMonster.Length; i++)
        {
            if (Sommoned[i])
            {
                float a = State[i].MobRHP;
                float b = State[i].MobMaxHP;
                HPimage[i].fillAmount = a / b;
                RealHP[i].text = "" + State[i].MobRHP;
                MaxHP[i].text = "" + State[i].MobMaxHP;
                if (State[i].MobRHP > State[i].MobMaxHP)
                {
                    State[i].MobRHP = State[i].MobMaxHP;
                }
                if (State[i].MobMaxHP > State[i].MobOGHP)
                {
                    MaxHP[i].color = Color.green;
                }
                else if (State[i].MobMaxHP < State[i].MobOGHP)
                {
                    MaxHP[i].color = Color.red;
                }
                else
                {
                    MaxHP[i].color = Color.white;
                }
                if (State[i].MobRHP == State[i].MobMaxHP)
                {
                    RealHP[i].color = Color.white;
                }
                else
                {
                    RealHP[i].color = Color.red;
                }

                if (State[i].MobPowerType == 1)
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Melee");
                else if (State[i].MobPowerType == 2)
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Magic");
                else if (State[i].MobPowerType == 3)
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Mix");
                else if (State[i].MobPowerType == 4)
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Pierce");
                else
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Heal");
                if (State[i].isAttacked)
                {
                    Attackimage[i].color = new Color(0.3f, 0.3f, 0.3f, 1f);
                }
                else
                {
                    Attackimage[i].color = Color.white;
                }

                int d = State[i].MobRPower;
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0)
                    {
                        if (MobBuff[j].BuffCode1 == "100")
                        {
                            d += MobBuff[j].BuffPower1;
                            break;
                        }
                    }
                    else if (i == 1)
                    {
                        if (MobBuff[j].BuffCode2 == "100")
                        {
                            d += MobBuff[j].BuffPower2;
                            break;
                        }
                    }
                    else if (i == 2)
                    {
                        if (MobBuff[j].BuffCode3 == "100")
                        {
                            d += MobBuff[j].BuffPower3;
                            break;
                        }
                    }
                }
                for (int j = 0; j < 10; j++)
                {
                    if (i == 0)
                    {
                        if (MobBuff[j].BuffCode1 == "104")
                        {
                            float e = d * 0.75f;
                            d = Mathf.FloorToInt(e);
                            break;
                        }
                    }
                    else if (i == 1)
                    {
                        if (MobBuff[j].BuffCode2 == "104")
                        {
                            float e = d * 0.75f;
                            d = Mathf.FloorToInt(e);
                            break;
                        }
                    }
                    else if (i == 2)
                    {
                        if (MobBuff[j].BuffCode3 == "104")
                        {
                            float e = d * 0.75f;
                            d = Mathf.FloorToInt(e);
                            break;
                        }
                    }
                }
                PowerTxt[i].text = d + "";
                if (d > State[i].MobOGPower)
                    PowerTxt[i].color = Color.green;
                else if (d < State[i].MobOGPower)
                    PowerTxt[i].color = Color.red;
                else
                    PowerTxt[i].color = Color.white;

                for (int j = 0; j < 10; j++)
                {
                    if (i == 0)
                    {
                        if (MobBuff[j].BuffCode1 == null)
                        {
                            MobBuffgo[j].SetActive(false);
                        }
                        else
                        {
                            MobBuffgo[j].SetActive(true);
                            MobBufftxt[j].text = "" + MobBuff[j].BuffPower1;
                            Image img = MobBuffgo[j].GetComponent<Image>();
                            img.sprite = Buffspr(MobBuff[j].BuffCode1);
                        }
                    }
                    if (i == 1)
                    {
                        if (MobBuff[j].BuffCode2 == null)
                        {
                            MobBuffgo[i * 10 + j].SetActive(false);
                        }
                        else
                        {
                            MobBuffgo[i * 10 + j].SetActive(true);
                            MobBufftxt[i * 10 + j].text = "" + MobBuff[j].BuffPower2;
                            Image img = MobBuffgo[i * 10 + j].GetComponent<Image>();
                            img.sprite = Buffspr(MobBuff[j].BuffCode2);
                        }
                    }
                    if (i == 2)
                    {
                        if (MobBuff[j].BuffCode3 == null)
                        {
                            MobBuffgo[i * 10 + j].SetActive(false);
                        }
                        else
                        {
                            MobBuffgo[i * 10 + j].SetActive(true);
                            MobBufftxt[i * 10 + j].text = "" + MobBuff[j].BuffPower3;
                            Image img = MobBuffgo[i * 10 + j].GetComponent<Image>();
                            img.sprite = Buffspr(MobBuff[j].BuffCode3);
                        }
                    }
                }

                if (State[i].MobArmor >= 1)
                {
                    HPimage[i].color = new Color(1f, 1f, 1f, 1f);
                    Armorgo[i].SetActive(true);
                    ArmorTxt[i].text = "" + State[i].MobArmor;
                }
                else
                {
                    HPimage[i].color = new Color(0.95f, 0.34f, 0.34f, 1f);
                    Armorgo[i].SetActive(false);
                }
            }
            if (Seleting)
            {
                for (int j = 0; j < SommonMonster.Length; j++)
                {
                    Image img = SommonMonster[j].GetComponent<Image>();
                    img.color = new Color(0.7f, 0.7f, 0.7f, 1f);
                    AnemyBattle ab = gameObject.GetComponent<AnemyBattle>();
                    img = ab.SommonMonster[j].GetComponent<Image>();
                    img.color = new Color(0.7f, 0.7f, 0.7f, 1f);
                    if (AllySelect)
                    {
                        img = SommonMonster[j].GetComponent<Image>();
                        img.color = new Color(1f, 1f, 1f, 1f);
                    }
                    else if (EnemSelect)
                    {
                        img = ab.SommonMonster[j].GetComponent<Image>();
                        img.color = new Color(1f, 1f, 1f, 1f);
                    }
                }
            }
        }
        
        Invoke("Updating", 0.05f);
    }
}
