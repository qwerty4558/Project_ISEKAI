using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UISpriteAnimation : MonoBehaviour
{
    public Image m_Image;
    public Sprite empty_spite;

    public Sprite[] m_SpriteArray;
    public float m_Speed = .02f;

    private int m_IndexSprite;
    [SerializeField]Coroutine m_CoroutineAnim;


    public void StartEffact()
    {
        if (m_CoroutineAnim == null)
        {
            m_CoroutineAnim = StartCoroutine(Func_PlayAnimUI());
        }
    }

    IEnumerator Func_PlayAnimUI()
    {
        m_IndexSprite = 0;
        while(m_IndexSprite < m_SpriteArray.Length)
        {
            m_Image.sprite = m_SpriteArray[m_IndexSprite];
            m_IndexSprite += 1;

            yield return new WaitForSeconds(m_Speed);
        }
        m_Image.sprite = empty_spite;

        m_CoroutineAnim = null;
    }
}
