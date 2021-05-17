using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public bool scrolled { get; set; }

    public float scrollSpeed;
    public float scrollWidth;
    public GameObject[] scrollObject;
    private Vector3[] originPosition;
    private int objectCount;

    private WaitForEndOfFrame waitForFrame = new WaitForEndOfFrame();
    public IEnumerator ScrollBackground()
    {
        if (!scrolled) scrolled = true;

        while(scrolled)
        {
            for (int i = 0; i < objectCount; i++)
            {
                if (scrollObject[i].transform.position.x < scrollWidth)
                {
                    scrollObject[i].transform.position = originPosition[1];
                }

                scrollObject[i].transform.Translate(Vector3.left * Time.deltaTime * scrollSpeed);
            }

            yield return waitForFrame;
        }

        yield break;
    }

    private void SetUp()
    {
        for (int i = 0; i < objectCount; i++)
        {
            originPosition[i] = scrollObject[i].transform.position;
        }
    }

    private void AwakeSetUp()
    {
        scrolled = false;
        objectCount = scrollObject.Length;
        originPosition = new Vector3[objectCount];
    }

    private void Start()
    {
        SetUp();
    }

    private void Awake()
    {
        AwakeSetUp();
    }
}
