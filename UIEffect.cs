using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffect : MonoBehaviour
{

    public RectTransform DeckImage;
    float deckf;

    void Start()
    {
        
    }

    void Update()
    {

    }

    IEnumerator DeckSmall()
    {
        if (deckf < 0.85f)
        {
            DeckImage.localScale = new Vector3(deckf, deckf, 1f);
            deckf += 0.005f;
            yield return new WaitForSeconds(0.02f);
            StartCoroutine("DeckSmall");
        }
        else
        {
            deckf = 0.85f;
            DeckImage.localScale = new Vector3(deckf, deckf, 1f);
        }
    }

    public void CardDrowEffect(int cardnum)
    {
        deckf = 0.7f;
        StartCoroutine("DeckSmall");
        GameObject go = Instantiate(Resources.Load("Prefabs/Effects/CardDrowEff"), gameObject.transform) as GameObject;
        Transform tr = go.GetComponent<Transform>();
        tr.parent = null;
        tr.localPosition = new Vector3(-7.87f, -3.96f, 80);
        Rigidbody2D rgd = go.GetComponent<Rigidbody2D>();
        Vector2 vec;
        CardEffect ce = go.GetComponent<CardEffect>();
        ce.cardnum = cardnum;
        ce.type = 1;
        if (cardnum == 1)
        {
            vec = new Vector2(8f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 2)
        {
            vec = new Vector2(7f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 3)
        {
            vec = new Vector2(6.5f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 4)
        {
            vec = new Vector2(6f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 5)
        {
            vec = new Vector2(5.5f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 6)
        {
            vec = new Vector2(5f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 7)
        {
            vec = new Vector2(4.7f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 8)
        {
            vec = new Vector2(4.5f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 9)
        {
            vec = new Vector2(4.3f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
        if (cardnum == 10)
        {
            vec = new Vector2(4.1f, 5f);
            rgd.AddForce(vec, ForceMode2D.Impulse);
        }
    }
}
