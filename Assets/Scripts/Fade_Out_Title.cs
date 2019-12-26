using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Fade_Out_Title : MonoBehaviour
{
    public float fadeOutTime;
    TextMeshProUGUI text;

    void Start()
    {
        text = this.GetComponent<TextMeshProUGUI>();
    }

    void Fade_Out()
    {
        //StartCoroutine(Fade_Out_Routine());
    }

    public IEnumerator Fade_Out_Routine()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        while (text.color.a > 0.0f)
        {
            text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * fadeOutTime));
            yield return null;
        }
    }
}
