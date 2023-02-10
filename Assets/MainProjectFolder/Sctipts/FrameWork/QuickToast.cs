using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuickToast : MonoBehaviour
{
    [Header("refs.")]
    public Image img;
    public Text text;

    [Header("Default Setting")]
    public float showSec = 0.2f;
    public float staySec = 0.5f;
    public float fadeoutSec = 0.2f;

    float _accTime = 0f;

    public void Load(string msg)
    {
        text.text = msg;

        this.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
    }

    void Update()
    {
        _accTime += Time.unscaledDeltaTime;

        if (_accTime <= showSec)
        {
            float xRot = Mathf.Lerp(90f, 0f, _accTime / showSec);
            img.transform.localEulerAngles = new Vector3(xRot, 0f, 0f);
        }
        else if (_accTime <= (showSec + staySec))
        {
            img.transform.localRotation = Quaternion.identity;
        }
        else if (_accTime <= (showSec + staySec + fadeoutSec))
        {
            float a = Mathf.Lerp(1f, 0f, (_accTime - showSec - staySec) / fadeoutSec);

            Color cImg = new Color(1f, 1f, 1f, a);
            img.color = cImg;
            Color cText = new Color(a, a, a, a);
            text.color = cText;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}

