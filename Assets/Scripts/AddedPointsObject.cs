using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AddedPointsObject : MonoBehaviour
{
    void Start()
    {
        transform.position = transform.parent.position - new Vector3(0, 25f, 0);
        StartCoroutine(JustDoIt());
    }

    private IEnumerator JustDoIt()
    {
        TMP_Text tmp = transform.GetComponent<TMP_Text>();
        while (true)
        {
            Color color = tmp.color;
            if (color.a < 0.01f) break;
            transform.localPosition = (transform.localPosition) + new Vector3(0, 1, 0);
            color.a -= 0.05f;
            tmp.color = color;
            yield return new WaitForSeconds(0.05f);
        }
        Destroy(gameObject);
    }
    
}
