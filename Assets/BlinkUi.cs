using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkUi : MonoBehaviour
{
    [SerializeField] Image _image;

    private void Start()
    {
        BlinkStart();
    }

    public void BlinkStart()
    {
        StartCoroutine(BlinkMode());
    }

    IEnumerator BlinkMode()
    {
        while (true)
        {
            Color color = _image.color;
            color.a = 0f;
            for (float t = 0; t < 1; t+= Time.deltaTime)
            {
                _image.color = Color.Lerp(_image.color, color, Time.deltaTime);
                yield return null;
            }

            for (float t = 1; t > 0; t -= Time.deltaTime)
            {
                color.a = 1;
                _image.color = Color.Lerp(_image.color, color, Time.deltaTime);
                yield return null;
            }
        }
    }
}
