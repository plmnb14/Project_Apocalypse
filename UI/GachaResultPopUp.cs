using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GachaResultPopUp : MonoBehaviour
{
    public GameObject gachaGrid;
    public GameObject okButton;
    public GameObject skipButton;

    public int gachaCount { get; set; }

    private const int maxGachaCount = 30;
    private int currentGachaCount;
    private GameObject[] gachaResultItems;
    private bool isSkipped;

    private WaitForSeconds waitSecond = new WaitForSeconds(0.1f);
    public IEnumerator ShowGachaResult()
    {
        currentGachaCount = 0;
        while(gachaCount > currentGachaCount)
        {
            gachaResultItems[currentGachaCount].gameObject.SetActive(true);

            if (isSkipped)
                yield break;

            else if (gachaCount == currentGachaCount + 1) break;

            yield return waitSecond;

            currentGachaCount++;
        }

        skipButton.gameObject.SetActive(false);
        okButton.gameObject.SetActive(true);
        yield break;
    }

    public void SkipGachaResult()
    {
        isSkipped = true;
        while (gachaCount > currentGachaCount)
        {
            gachaResultItems[currentGachaCount].gameObject.SetActive(true);
            currentGachaCount++;
        }
        okButton.gameObject.SetActive(true);
    }

    private void CloseGachaResult()
    {
        currentGachaCount = 0;
        while (gachaCount > currentGachaCount)
        {
            gachaResultItems[currentGachaCount].gameObject.SetActive(false);
            currentGachaCount++;
        }
    }

    private void ResetStatus()
    {
        currentGachaCount = 0;
        isSkipped = false;
    }

    private void OnDisable()
    {
        CloseGachaResult();
        ResetStatus();
        okButton.SetActive(false);
        skipButton.SetActive(false);
    }

    private void OnEnable()
    {
        skipButton.SetActive(true);
    }

    private void AwakeSetUp()
    {
        isSkipped = false;
        currentGachaCount = 0;
        gachaResultItems = new GameObject[maxGachaCount];
        for (int i = 0; i < maxGachaCount; i++)
        {
            gachaResultItems[i] = gachaGrid.transform.GetChild(i).gameObject;
        }
    }

    private void Awake()
    {
        AwakeSetUp();
    }
}
