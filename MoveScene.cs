using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SceneIndex { Logo, Title, Loading, Main, Scene_End }

public class MoveScene : MonoBehaviour, IPointerClickHandler
{
    private string[] sceneName = { "Scene_Logo", "Scene_Title", "Scene_Loading", "Scene_Main", "Scene_Logo" };
    public SceneIndex sceneIndex;
    private string sceneNameCur;

    public void OnPointerClick(PointerEventData eventData)
    {
        LoadingManager.LoadingScene(sceneNameCur);
    }

    private void Awake()
    {
        sceneNameCur = sceneName[(int)sceneIndex];
    }
}
