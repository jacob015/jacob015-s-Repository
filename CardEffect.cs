using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffect : MonoBehaviour
{
    public int type; 
    //1: 드로우 / 2: 덱에 섞어넣기(미완) / 3: 드로우 시 후광 / 4: 카드 드래그 시 후광
    public int cardnum;

    Transform tr;

    BattleSystem bs;

    float scal;
    float a;
    SpriteRenderer spr;

    void Start()
    {
        tr = gameObject.GetComponent<Transform>();
        bs = GameObject.Find("Systems").GetComponent<BattleSystem>();
        spr = gameObject.GetComponent<SpriteRenderer>();
        if (type == 3)
        {
            a = 0.8f;
            Color color = spr.color;
            color.a = a;
            spr.color = color;
            tr.localScale = new Vector3(0.8f, 0.8f, 1f);
            StartCoroutine("Invisible");
        }
        if (type == 4)
        {
            a = 1f;
            tr.localRotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
            StartCoroutine("Invisible");
        }
    }

    void Update()
    {
        if (type == 1)
        {
            tr.Rotate(0f, 0f, 180f * Time.deltaTime);
            GameObject go = Instantiate(Resources.Load("Prefabs/Effects/YBall"), tr) as GameObject;
            go.transform.parent = null;
            go.transform.position = new Vector3(tr.position.x, tr.position.y, 81f);
            CardEffect cee = go.GetComponent<CardEffect>();
            cee.type = 3;
            if (tr.position.y < -4)
            {
                bs.CardImage[cardnum-1].SetActive(true);
                Destroy(gameObject);
            }
        }
        if (type == 4)
        {
            tr.Translate(5f * Time.deltaTime, 0f, 0f);
        }
    }
    
    IEnumerator Invisible()
    {
        if (a > 0)
        {
            a -= 0.02f;
            Color color = spr.color;
            color.a = a;
            spr.color = color;
            tr.localScale = new Vector3(a, a, 1f);
            yield return new WaitForSeconds(0.008f);
            StartCoroutine("Invisible");
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
