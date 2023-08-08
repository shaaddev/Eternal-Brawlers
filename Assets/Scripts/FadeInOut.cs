using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public float fadeInDuration = 0.5f; // Duration of the fade-in animation
    public float fadeOutDuration = 0.5f; // duration of the fade-out animation
    public float pauseDuration = 0.2f; // duration between the pause of fade in and out

    private CanvasGroup canvasgroup;
    // Start is called before the first frame update
    void Start()
    {
        // get cg comp
        canvasgroup = GetComponent<CanvasGroup>();
        // start the sequence
        StartCoroutine(FadeInOutLoop());
    }

    // Update is called once per frame
    private System.Collections.IEnumerator FadeInOutLoop()
    {
        while (true)
        {
            // fade-in
            yield return FadeIn(fadeInDuration);

            // pause between fade-in and fade-out
            yield return new WaitForSeconds(pauseDuration);

            // fade-out
            yield return FadeOut(fadeOutDuration);
        }
    }

    private System.Collections.IEnumerator FadeIn(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = time / duration;
            canvasgroup.alpha = alpha;
            yield return null;
        }
        canvasgroup.alpha = 1f;
    }

    private System.Collections.IEnumerator FadeOut(float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = 1f - (time / duration);
            canvasgroup.alpha = alpha;
            yield return null;
        }
        canvasgroup.alpha = 0f;
    }
}
