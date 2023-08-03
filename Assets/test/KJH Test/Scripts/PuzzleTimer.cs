using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleTimer : MonoBehaviour
{
    [SerializeField] Image timer;

    [SerializeField] float puzzleTime = 30f;
    [SerializeField] float currentTime;
    [SerializeField] float player_Damage = 10f;

    [SerializeField] PlayerController _player;


    void Start()
    {
        _player = PlayerController.Instance;

    }

    public void OnEnable()
    {
        currentTime = puzzleTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeSelf)
        {
            currentTime -= Time.deltaTime;
            timer.fillAmount = currentTime / puzzleTime;  
            if(currentTime <= 0)
            {
                _player.GetDamage(10f);
                currentTime = puzzleTime;
            }
        }
    }
}
