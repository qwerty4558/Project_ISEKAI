using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class FrameWork : MonoBehaviour
{
    [Header("Framework SysUI")]
    [SerializeField] Canvas _frameworkCanvas;
    [SerializeField] QuickToast _quickToastRef;
    [SerializeField] InputBlockToast _inputBlockToastRef;


    public void QuickToast(string msg)
    {
        QuickToast quick = Instantiate(_quickToastRef, _frameworkCanvas.transform);
        quick.Load(msg);

        quick.gameObject.SetActive(true);
    }

    public void InputBlockToast(string msg)
    {
        InputBlockToast input =  Instantiate(_inputBlockToastRef, _frameworkCanvas.transform);
        input.Load(msg);

        input.gameObject.SetActive(true);
    }

}


