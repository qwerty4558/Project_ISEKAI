using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_View : MonoBehaviour
{    public abstract void Inittialize();
    public virtual void Hide() => gameObject.SetActive(false);

    public virtual void Show() => gameObject.SetActive(true);
}
