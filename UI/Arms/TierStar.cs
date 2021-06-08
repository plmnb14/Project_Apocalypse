using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TierStar : MonoBehaviour
{
    #region Private Fields
    private const int maxStarCount = 5;
    private Image[] stars;
    #endregion

    #region Events
    public void ChangeStarState(int showStarCount)
    {
        for(int i = 0; i < showStarCount; i++)
        {
            stars[i].gameObject.SetActive(true);
        }

        for (int i = showStarCount; i < maxStarCount; i++)
        {
            stars[i].gameObject.SetActive(false);
        }
    }
    #endregion

    #region Awake Events
    private void LoadChilds()
    {
        for(int i = 0; i < maxStarCount; i++)
        {
            stars[i] = transform.GetComponent<Image>();
        }
    }
    private void Awake()
    {
        LoadChilds();
    }
    #endregion
}
