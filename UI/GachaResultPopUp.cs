using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GachaResultPopUp : MonoBehaviour
{
    public GameObject gachaGrid;
    public GameObject okButton;
    public GameObject skipButton;

    public Queue<int> gachaItemCode { get; set; }
    public int gachaCount { get; set; }

    private const int maxGachaCount = 30;
    private int currentGachaCount;
    private GameObject[] gachaResultItems;
    private bool isSkipped;

    private Sprite[] gachaFrameSprite;
    private Sprite gachaIconSprite;
    private Sprite[] gachaGradeSprite;

    private WaitForSeconds waitSecond = new WaitForSeconds(0.1f);
    public IEnumerator ShowGachaResult()
    {
        currentGachaCount = 0;
        while(gachaCount > currentGachaCount)
        {
            gachaResultItems[currentGachaCount].gameObject.SetActive(true);
            ResultIconChange();

            currentGachaCount++;

            if (isSkipped)
                yield break;

            else if (gachaCount == currentGachaCount) break;

            yield return waitSecond;
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
            ResultIconChange();
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
    private void ResultIconChange()
    {
        int itemCode = gachaItemCode.Dequeue();
        DataManger dataManager = DataManger.instance;
        var itemData = dataManager.weaponStatForDataDictionary[itemCode];

        gachaResultItems[currentGachaCount].GetComponent<Image>().sprite = gachaFrameSprite[itemData.grade];
        gachaResultItems[currentGachaCount].transform.GetChild(0).GetComponent<Image>().sprite = gachaGradeSprite[itemData.grade];
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
        gachaItemCode = new Queue<int>();

        isSkipped = false;
        currentGachaCount = 0;
        gachaResultItems = new GameObject[maxGachaCount];
        for (int i = 0; i < maxGachaCount; i++)
        {
            gachaResultItems[i] = gachaGrid.transform.GetChild(i).gameObject;
        }

        gachaFrameSprite = Resources.LoadAll<Sprite>("Sprite/UI/EquipIconBackground");
        gachaGradeSprite = Resources.LoadAll<Sprite>("Sprite/UI/RarityIcon2");
        gachaIconSprite = Resources.Load<Sprite>("Sprite/UI/EquipIcon");
    }

    private void Awake()
    {
        AwakeSetUp();
    }
}
