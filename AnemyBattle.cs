using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct EBuff
{
    public string BuffCode1;
    public int BuffPower1;
    public string BuffCode2;
    public int BuffPower2;
    public string BuffCode3;
    public int BuffPower3;
}

public struct Enemy
{
    public string EnemyCode;
    public int EnemyCost;
    public int EnemyOGPower;
    public int EnemyRPower;
    public int EnemyOGHp;
    public int EnemyRHp;
    public int EnemyArmor;
    public int EnemyType;
}

public struct EnemyStatus
{
    public string EnemyCode;
    public int EnemyCost;
    public int EnemyOGPower;
    public int EnemyRPower;
    public int EnemyOGHp;
    public int EnemyRHp;
    public int EnemyMaxHP;
    public int EnemyArmor;
    public int EnemyType;
    public bool isAttacked;
}


public class AnemyBattle : MonoBehaviour
{
    public bool EnemyTurn = false;
    public int Turn = 0;
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

    public EBuff[] MobBuff = new EBuff[10];
    public GameObject[] MobBuffgo = new GameObject[30];
    public Text[] MobBufftxt = new Text[30];

    public Text Cost;
    public GameObject LookCard;
    Image Lookimg;
    public GameObject Powergo;
    Text Power;
    public GameObject HPgo;
    Text HP;
    public Text LExp;

    BattleSystem batsys;
    Cardsc csc;
    Sommon sm;
    MobAnime ma;
    DamageSystem Dm;

    bool ons;
    bool isOn;
    int MobNum;
    float time;
    EnemyScripts Enemystat;
    public Enemy Enemy;
    public bool[] Sommoned = new bool[3];
    public EnemyStatus[] State = new EnemyStatus[3];

    public int mobAttackturn;

    void Start()
    {
        Enemystat = gameObject.GetComponent<EnemyScripts>();
        csc = gameObject.GetComponent<Cardsc>();
        batsys = gameObject.GetComponent<BattleSystem>();
        sm = gameObject.GetComponent<Sommon>();
        ma = gameObject.GetComponent<MobAnime>();
        Lookimg = LookCard.GetComponent<Image>();
        Power = Powergo.GetComponent<Text>();
        HP = HPgo.GetComponent<Text>();
        SommonMonster[0].SetActive(false);
        SommonMonster[1].SetActive(false);
        SommonMonster[2].SetActive(false);
        StatusNBuff[0].SetActive(false);
        StatusNBuff[1].SetActive(false);
        StatusNBuff[2].SetActive(false);
        Sommoned[0] = false;
        Sommoned[1] = false;
        Sommoned[2] = false;
        ons = false;
        isOn = false;
        mobAttackturn = 0;
        Updating();
        StartSommonEnemy();
    }

    void StartSommonEnemy()
    {
        EnemySys("00000", 1, Random.Range(7, 10), 2, 10, 4);
        EnemySys("00000", 1, Random.Range(7, 10), 2, 10, 3);
        EnemySys("00000", 1, Random.Range(7, 10), 2, 10, 5);
    }

    public void MobViewOn(GameObject go)
    {
        ons = true;
        if (go.name == "Enemy1")
        {
            MobNum = 0;
        }
        else if (go.name == "Enemy2")
        {
            MobNum = 1;
        }
        else if (go.name == "Enemy3")
        {
            MobNum = 2;
        }
        Image img = go.GetComponent<Image>();
        img.color = new Color(0.7f, 0.7f, 0.7f, 1f);
    }
    public void MobViewOut(GameObject go)
    {
        time = 0f;
        ons = false;
        isOn = false;
        Image img = go.GetComponent<Image>();
        img.color = new Color(1f, 1f, 1f, 1f);
        LookCard.SetActive(false);
    }

    public void EnemySys(string Code, int Cost, int HP, int Power, int Armor, int Type)
    {
        Enemy.EnemyCode = Code;
        Enemy.EnemyCost = Cost;
        Enemy.EnemyOGHp = HP;
        Enemy.EnemyRHp = Enemy.EnemyOGHp;
        Enemy.EnemyOGPower = Power;
        Enemy.EnemyRPower = Enemy.EnemyOGPower;
        Enemy.EnemyArmor = Armor;
        Enemy.EnemyType = Type;

        for (int i = 0; i < Sommoned.Length; i++)
        {
            if (!Sommoned[i])
            {
                Sommoned[i] = true;
                SommonMonster[i].SetActive(true);
                StatusNBuff[i].SetActive(true);
                State[i].isAttacked = true;
                State[i].EnemyCode = Enemy.EnemyCode;
                State[i].EnemyCost = Enemy.EnemyCost;
                State[i].EnemyOGPower = Enemy.EnemyOGPower;
                State[i].EnemyRPower = Enemy.EnemyRPower;
                State[i].EnemyOGHp = Enemy.EnemyOGHp;
                State[i].EnemyRHp = Enemy.EnemyRHp;
                State[i].EnemyMaxHP = State[i].EnemyRHp;
                State[i].EnemyArmor = Enemy.EnemyArmor;
                State[i].EnemyType = Enemy.EnemyType;
                Image img = SommonMonster[i].GetComponent<Image>();
                //몬스터 코드 조건
                if (State[i].EnemyCode == "000")
                {
                    img.sprite = Resources.Load<Sprite>("Sprites/Larva1");
                    State[i].EnemyType = 1;
                }
                RectTransform rt = SommonMonster[i].GetComponent<RectTransform>();
                if (i == 0)
                {
                    rt.localPosition = new Vector3(410f, 80f, 0);
                }
                if (i == 1)
                {
                    rt.localPosition = new Vector3(200f, -60f, 0);
                }
                if (i == 2)
                {
                    rt.localPosition = new Vector3(410f, -200f, 0);
                }
                break;
            }
        }
    }

    public void BuffGain(int mob, string buffcode, int buffpower)
    {
        Dm.EnBuffGain(mob, buffcode, buffpower);
    }

    public void BuffReload()
    {
        Dm.EnBuffReload();
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
                tr.localPosition = new Vector3(-580f, 0f, 0f);
            tr.localScale = new Vector3(0f, 0f, 1f);
            scal = 0f;

            Powergo.SetActive(true);
            HPgo.SetActive(true);

            Cost.text = "" + State[MobNum].EnemyCost;
            Power.text = "" + State[MobNum].EnemyOGPower;
            HP.text = "" + State[MobNum].EnemyOGHp;

            RectTransform rtt = csc.LBuffExp[0].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(350, 130, 0);
            rtt = csc.LBuffExp[1].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(350, -40, 0);
            rtt = csc.LBuffExp[2].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(725, 130, 0);
            rtt = csc.LBuffExp[3].GetComponent<RectTransform>();
            rtt.localPosition = new Vector3(725, -40, 0);

            if (State[MobNum].EnemyCode == "00000")
            {
                LExp.text = "다른 곳에서부터 내려온\n아이입니다. 어쩌다가?";
                Lookimg.sprite = Resources.Load<Sprite>("Sprites/Cards/LarvaCard");
            }

            StartCoroutine("LookAppear");
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

    public void EnemyAttackStart()
    {
        StartCoroutine("EnemyAttacks");
    }

    IEnumerator EnemyAttacks()
    {
        yield return new WaitForSeconds(1f);
        if (mobAttackturn <= 2)
        {
            if (Sommoned[mobAttackturn] && !State[mobAttackturn].isAttacked)
            {
                if (State[mobAttackturn].EnemyType != 5)
                {
                    for (int i = 0; i >= 0; i++)
                    {
                        int a = Random.Range(0, 2);
                        if (sm.Sommoned[a] && sm.State[a].MobRHP > 0)
                        {
                            sm.Arrows.SetActive(true);
                            sm.Arrows.GetComponent<RectTransform>().localPosition = sm.SommonMonster[a].GetComponent<RectTransform>().localPosition;
                            ma.SpriteChanging(mobAttackturn + 4, State[0].EnemyCode, 2);
                            int dmg = MobDamaging(a, mobAttackturn, State[mobAttackturn].EnemyType);
                            yield return new WaitForSeconds(0.75f);
                            ma.AttackAfter(mobAttackturn + 4);
                            sm.Arrows.SetActive(false);
                            if (sm.State[a].MobArmor >= dmg)
                            {
                                sm.State[a].MobArmor -= dmg;
                                GameObject tx = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DmgText"), transform) as GameObject;
                                tx.transform.parent = GameObject.Find("Canvas").transform;
                                tx.transform.localScale = new Vector3(1f, 1f, 1f);
                                tx.GetComponent<RectTransform>().localPosition = sm.SommonMonster[a].GetComponent<RectTransform>().localPosition;
                                tx.GetComponent<DmgText>().Texting(1, dmg);
                            }
                            else
                            {
                                int dmg2 = dmg - sm.State[a].MobArmor;
                                sm.State[a].MobArmor = 0;
                                sm.State[a].MobRHP -= dmg2;
                                GameObject tx = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DmgText"), transform) as GameObject;
                                tx.transform.parent = GameObject.Find("Canvas").transform;
                                tx.transform.localScale = new Vector3(1f, 1f, 1f);
                                tx.GetComponent<RectTransform>().localPosition = sm.SommonMonster[a].GetComponent<RectTransform>().localPosition;
                                tx.GetComponent<DmgText>().Texting(1, dmg);
                            }
                            State[mobAttackturn].isAttacked = true;
                            break;
                        }
                        else if (!sm.Sommoned[0] && !sm.Sommoned[1] && !sm.Sommoned[2])
                            break;
                    }
                }
                else
                {
                    int a = 0;
                    float b = 1f;
                    for (int i = 0; i < 3; i++)
                    {
                        if (Sommoned[i])
                        {
                            float x = State[i].EnemyRHp;
                            float y = State[i].EnemyMaxHP;
                            if (b > x / y)
                            {
                                a = i;
                                b = x / y;
                            }
                            Debug.Log(x + " - " + y);
                        }
                    }
                    sm.Arrows.SetActive(true);
                    sm.Arrows.GetComponent<RectTransform>().localPosition = SommonMonster[a].GetComponent<RectTransform>().localPosition;
                    ma.SpriteChanging(mobAttackturn + 4, State[mobAttackturn].EnemyCode, 2);
                    int dmg = MobDamaging(a, mobAttackturn, State[mobAttackturn].EnemyType);
                    yield return new WaitForSeconds(0.75f);
                    ma.AttackAfter(mobAttackturn + 4);
                    sm.Arrows.SetActive(false);
                    State[a].EnemyRHp += dmg;
                    if (State[a].EnemyRHp > State[a].EnemyMaxHP)
                        State[a].EnemyRHp = State[a].EnemyMaxHP;
                    GameObject tx = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/DmgText"), transform) as GameObject;
                    tx.transform.parent = GameObject.Find("Canvas").transform;
                    tx.transform.localScale = new Vector3(1f, 1f, 1f);
                    tx.GetComponent<RectTransform>().localPosition = SommonMonster[a].GetComponent<RectTransform>().localPosition;
                    tx.GetComponent<DmgText>().Texting(2, dmg);
                    State[mobAttackturn].isAttacked = true;
                }
            }
            mobAttackturn++;
            StartCoroutine("EnemyAttacks");
        }  
        else
        {
            yield return new WaitForSeconds(0.5f);
            batsys.TurnStart();
        }
    }

    public int MobDamaging(int spider, int mobnum, int type)
    {
        Dm.EnMobDamaging(spider, mobnum, type);
        return 0;
    }

    void Updating()
    {
        for (int i = 0; i < SommonMonster.Length; i++)
        {
            if (Sommoned[i])
            {
                float a = State[i].EnemyRHp;
                float b = State[i].EnemyMaxHP;
                HPimage[i].fillAmount = a / b;
                RealHP[i].text = "" + State[i].EnemyRHp;
                MaxHP[i].text = "" + State[i].EnemyMaxHP;
                if (State[i].EnemyRHp > State[i].EnemyMaxHP)
                {
                    State[i].EnemyRHp = State[i].EnemyMaxHP;
                }
                if (State[i].EnemyMaxHP > State[i].EnemyOGHp)
                {
                    MaxHP[i].color = Color.green;
                }
                else if (State[i].EnemyMaxHP < State[i].EnemyOGHp)
                {
                    MaxHP[i].color = Color.red;
                }
                else
                {
                    MaxHP[i].color = Color.white;
                }
                if (State[i].EnemyRHp == State[i].EnemyMaxHP)
                {
                    RealHP[i].color = Color.white;
                }
                else
                {
                    RealHP[i].color = Color.red;
                }

                if (State[i].EnemyType == 1)
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Melee");
                else if (State[i].EnemyType == 2)
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Magic");
                else if (State[i].EnemyType == 3)
                    Attackimage[i].sprite = Resources.Load<Sprite>("Sprites/UIs/Attack_Mix");
                else if (State[i].EnemyType == 4)
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

                int d = State[i].EnemyRPower;
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
                if (d > State[i].EnemyOGPower)
                    PowerTxt[i].color = Color.green;
                else if (d < State[i].EnemyOGPower)
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
                            img.sprite = sm.Buffspr(MobBuff[j].BuffCode1);
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
                            img.sprite = sm.Buffspr(MobBuff[j].BuffCode2);
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
                            img.sprite = sm.Buffspr(MobBuff[j].BuffCode3);
                        }
                    }
                }

                if (State[i].EnemyArmor >= 1)
                {
                    HPimage[i].color = new Color(1f, 1f, 1f, 1f);
                    Armorgo[i].SetActive(true);
                    ArmorTxt[i].text = "" + State[i].EnemyArmor;
                }
                else
                {
                    HPimage[i].color = new Color(0.95f, 0.34f, 0.34f, 1f);
                    Armorgo[i].SetActive(false);
                }
            }
        }

        Invoke("Updating", 0.05f);
    }
}
