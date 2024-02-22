using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class TMProFade : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    public float fadeTime = 2f;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }
 

    public void FadeIn() => StartCoroutine(Fade(1));
    public void FadeOut() => StartCoroutine(Fade(0));

    IEnumerator Fade(float alphaTarget)
    {
        float currentAlpha = textMeshPro.color.a;
        float step = Mathf.Abs(alphaTarget - currentAlpha) / fadeTime;
        while(currentAlpha != alphaTarget)
        {
            currentAlpha = Mathf.MoveTowards(currentAlpha, alphaTarget, step * Time.deltaTime);
            Color newColor = textMeshPro.color;
            newColor.a = currentAlpha;
            textMeshPro.color = newColor;
            yield return null;
        }
    }
}
