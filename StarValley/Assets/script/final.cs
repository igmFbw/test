using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class final : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="player")
        {
            GameUI.Instance.gamePass.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
