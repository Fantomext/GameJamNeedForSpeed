using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private CarController _car;
    [SerializeField] List<Image> _images = new List<Image>();
    [SerializeField] Image _mainImage;
    [SerializeField] Image _nitroBar;
    [SerializeField] List<Color> _colors = new List<Color>();
    [SerializeField] private TMP_Text _text;
    bool _nitroStart = true;

    private void Start()
    {
        AlphaMax();

    }

    private void Update()
    {
        FillBar();
        ChangeColorSpeedometr();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            FillNitroBar();
        }
        UpdateText();
    }

    public void AlphaMax()
    {
        for (int i = 0; i < _colors.Count; i++)
        {
            _colors[i] = new Color(_colors[i].r, _colors[i].g, _colors[i].b, 1);
        }
    }

    public void UpdateText()
    {
        float value = _car.ReturnMagnitude() * 3.24f;
        _text.text = value.ToString("0");
    }

    public void FillBar()
    {
        float carProgress  = _car.ReturnMagnitude() / 27.2f;
        _mainImage.fillAmount = carProgress;

    }

    public void FillNitroBar()
    {
        StartCoroutine(FillNitroCor());
    }

    IEnumerator FillNitroCor()
    {
        if (_nitroStart)
        {
            _nitroStart = false;
            for (float t = 1; t > 0; t -= Time.deltaTime / 3)
            {
                _nitroBar.fillAmount = t;
                yield return null;
            }

            for (float t = 0; t < 1; t += Time.deltaTime / 3)
            {
                _nitroBar.fillAmount = t;
                yield return null;
            }
            _nitroStart = true;
        }
        
        
    }

    public void ChangeColorSpeedometr()
    {
        Debug.Log(_mainImage.fillAmount);
        for (int i = 0; i < _images.Count; i++)
        {
            if (_mainImage.fillAmount < 0.60)
            {
                _images[i].color = _colors[0];
                Debug.Log("first");
            }
            else if (_mainImage.fillAmount > 0.61 && _mainImage.fillAmount < 0.98)
            {
                _images[i].color = _colors[1];
                Debug.Log("second");
            }
            else if (_mainImage.fillAmount >= 0.99)
            {
                _images[i].color = _colors[2];
                Debug.Log("third");
            }
        }
        
    }
}
