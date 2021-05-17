using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StageCounting : MonoBehaviour
{
    Text stageCount;
    private void UpdateValue(int index)
    {
        stageCount.text = "stage : " + index;
    }

    private void Start()
    {
        StageManager.instance.stageindex += UpdateValue;
    }

    private void Awake()
    {
        stageCount = transform.GetChild(0).GetComponent<Text>();
    }
}
