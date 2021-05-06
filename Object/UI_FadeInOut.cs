using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_FadeInOut : MonoBehaviour
{
    public float fadeSpeed = 1.0f;

    private WaitForEndOfFrame waitFrame;
    private float ratio;

    public virtual IEnumerator FadeOut(CanvasGroup canvasGroup, System.Action nextFirstEvent = null)
    {
        ratio = 1.0f;
        while (ratio > 0.0f)
        {
            ratio -= fadeSpeed * Time.deltaTime;

            if (ratio <= 0.0f) ratio = 0.0f;

            canvasGroup.alpha = ratio;

            yield return waitFrame;
        }

        if (null != nextFirstEvent) nextFirstEvent();
    }

    public virtual IEnumerator FadeIn(CanvasGroup canvasGroup, System.Action nextFirstEvent = null)
    {
        ratio = 0.0f;
        while (ratio < 1.0f)
        {
            ratio += fadeSpeed * Time.deltaTime;

            if (ratio >= 1.0f) ratio = 1.0f;

            canvasGroup.alpha = ratio;

            yield return waitFrame;
        }

        if (null != nextFirstEvent) nextFirstEvent();
    }

    private void Awake()
    {
        waitFrame = new WaitForEndOfFrame();
        ratio = 0.0f;
    }
}
