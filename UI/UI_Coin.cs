using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class UI_Coin : MonoBehaviour
{
    Text stageCount;
    StringBuilder stringValue;
    private void UpdateValue(long value)
    {
        stageCount.text = NumToString.GetNumberString(ref stringValue, value, 1);
    }

    private void Start()
    {
        ResourceManager.instance.updateCoin += UpdateValue;
        stringValue = new StringBuilder(NumToString.showNumberMax , NumToString.showNumberMax);

        Debug.Log(stringValue.Length);
    }

    private void Awake()
    {
        stageCount = transform.GetChild(1).GetComponent<Text>();
    }
}
