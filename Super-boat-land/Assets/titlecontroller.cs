using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlecontroller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Vector3 original = this.transform.localScale;
        this.transform.localScale = new Vector3(original.x, 0.0f);
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1.0f)
        {
            this.transform.localScale = new Vector3(original.x, Mathf.Lerp(0.0f, original.y, t));
            yield return null;
        }
    }
}
