using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Element : MonoBehaviour
{
    Text stageCount;
    private void UpdateValue(long value)
    {
        stageCount.text = value.ToString();
    }

    private void Start()
    {
        ResourceManager.instance.updateElement += UpdateValue;
    }

    private void Awake()
    {
        stageCount = transform.GetChild(1).GetComponent<Text>();
    }
}
