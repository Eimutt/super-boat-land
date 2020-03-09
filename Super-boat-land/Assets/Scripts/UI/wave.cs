using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour
{

    public float Interval = 2f;
    public float FadeTime = 1f;
    public float TimeLeft;

    public bool Shown = true;

    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = Random.Range(0.8f, 2f);
        FadeTime = Random.Range(0.1f, 0.8f);
    }

    // Update is called once per frame
    void Update()
    {
        
         TimeLeft -= Time.deltaTime;
         if(TimeLeft < 0)
         {
             if(Shown){
                //Fade out
                StartCoroutine(FadeTo(0.0f, FadeTime));
                Shown = false;
             }else{
                //Fade in
                StartCoroutine(FadeTo(1.0f, FadeTime));
                Shown = true;
            }
            TimeLeft = Random.Range(0, Interval);
            FadeTime = Random.Range(0.4f, 0.8f);
        }
    }

    IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = this.GetComponent<SpriteRenderer>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha,aValue,t));
            this.GetComponent<SpriteRenderer>().color = newColor;
            yield return null;
        }
    }

}
