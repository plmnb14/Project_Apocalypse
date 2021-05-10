using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DamageFont : UI_Object
{
    public enum fontUsedType { Default, Critical, Dot, Heal, Reduce }

    const int maxSize = 10;
    const float gap = 0.14f;

    private float moveSpeed;
    //private float scaleSpeed;
    private float fontLifeTime;
    private SpriteRenderer[] fontChild = new SpriteRenderer[maxSize];
    private Sprite[] fontSprite = new Sprite[maxSize];

    public void ChangeColor(fontUsedType types = fontUsedType.Default)
    {
        Color changeColor = (types == fontUsedType.Default ? Color.white : Color.red);
        for (int i = 0; i < maxSize; i++)
        {
            fontChild[i].color = changeColor;
        }
    }

    public void SetNumber(float number, Vector3 position)
    {
        transform.position = position + Vector3.up * 0.2f;

        int integerNum = (int)number;
        int[] num = new int[maxSize];

        int integerMax = 1000000000;
        for(int i = 0; i < maxSize-1; i++)
        {
            num[i] = integerNum / integerMax;
            integerNum -= num[i] * integerMax;
            integerMax /= 10;
        }
        num[9] = integerNum;

        bool checkZero = false;
        int j = 0;
        float count = maxSize;
        for (int i = 0; i < maxSize; i++)
        {
            if (!checkZero)
            {
                while (i < maxSize)
                {
                    if (num[i] <= 0) i++;

                    else
                    {
                        i--;
                        break;
                    }
                }

                count = (((maxSize-1) - i) / 2) * gap;
                count -= i % 2 == 0 ? 0.0f : gap * 0.5f;

                checkZero = true;
            }

            else
            {
                fontChild[i].transform.position += new Vector3(gap * j - count, 0.0f, 0.0f);
                fontChild[i].gameObject.SetActive(true);
                fontChild[i].sprite = fontSprite[num[i]];
                j++;
            }
        }

        StartCoroutine(LifeCycle());
    }

    WaitForEndOfFrame waitFrame = new WaitForEndOfFrame();
    private IEnumerator LifeCycle()
    {
        while (fontLifeTime > 0)
        {
            transform.position += Vector3.up * Time.deltaTime * moveSpeed;
            moveSpeed -= Time.deltaTime * 1.5f;

            foreach (var font in fontChild)
            {
                font.color = new Color(font.color.r, font.color.g, font.color.b, font.color.a - Time.deltaTime * 1.5f);
            }

            fontLifeTime -= Time.deltaTime;
            yield return waitFrame;
        }

        foreach (var font in fontChild)
        {
            font.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            font.gameObject.SetActive(false);
        }

        fontLifeTime = 0.0f;
       // payback(this);
    }

    private void SetUp()
    {
        moveSpeed = 3.0f;
        //scaleSpeed = 8.0f;
        fontLifeTime = 1.5f;

        fontSprite = Resources.LoadAll<Sprite>("Sprite/UI/Num");
        fontChild = gameObject.GetComponentsInChildren<SpriteRenderer>();

        foreach (var child in fontChild)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        SetUp();   
    }
}
