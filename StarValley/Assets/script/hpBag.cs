using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpBag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            soundManager.instance.playEatHp();
            Destroy(gameObject);
            global.hp++;
            global.HpChangeEvent();
        }
    }
}
