using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Crystal : MonoBehaviour
{
    Text stageCount;
    private void UpdateValue(long value)
    {
        stageCount.text = value.ToString();
    }

    private void Start()
    {
        ResourceManager.instance.updateCrystal += UpdateValue;
    }

    private void Awake()
    {
        stageCount = transform.GetChild(1).GetComponent<Text>();
    }
}
