using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBlockToast : MonoBehaviour
{
    public enum EState : int
    {
        Opening,
        WaitingForInput,
        Closing
    }

    [Header("refs.")]
    public Button inputBlocker;
    public Image bg;
    public Image img;
    public Text text;

    [Header("Default Setting")]
    public float showSec = 0.2f;
    public float fadeoutSec = 0.2f;

    public EState currentState = EState.Opening;
    float _accTime = 0f;

    public void Load(string msg)
    {
        text.text = msg;

        bg.transform.localScale = new Vector3(0f, 1f, 1f);
        img.transform.localEulerAngles = new Vector3(90f, 0f, 0f);
    }

    void Awake()
    {
        inputBlocker.onClick.AddListener(OnInputBlocker);
    }

    void Update()
    {
        if (currentState == EState.WaitingForInput)
            return;

        _accTime += Time.unscaledDeltaTime;

        switch (currentState)
        {
            case EState.WaitingForInput:
                break;

            case EState.Opening:
                {
                    if (_accTime > showSec)
                    {
                        currentState = EState.WaitingForInput;

                        bg.transform.localScale = new Vector3(1f, 1f, 1f);
                        img.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
                    }
                    else
                    {
                        float t = _accTime / showSec;

                        float xScale = Mathf.Lerp(0f, 1f, t);
                        bg.transform.localScale = new Vector3(xScale, 1f, 1f);

                        float xRot = Mathf.Lerp(90f, 0f, t);
                        img.transform.localEulerAngles = new Vector3(xRot, 0f, 0f);
                    }
                }
                break;
            case EState.Closing:
                {
                    if (_accTime > fadeoutSec)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        float a = Mathf.Lerp(1f, 0f, _accTime / fadeoutSec);

                        Color cImg = new Color(1f, 1f, 1f, a);
                        img.color = cImg;
                        Color cText = new Color(a, a, a, a);
                        text.color = cText;
                    }
                }
                break;
        }
    }

    void OnInputBlocker()
    {
        if (currentState == EState.WaitingForInput)
        {
            _accTime = 0f;
            currentState = EState.Closing;
        }
    }
}
