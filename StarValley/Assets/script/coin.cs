using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coin : MonoBehaviour
{
    public Rigidbody2D rb;
    public bool getActive()
    {
        return gameObject.activeSelf;
    }
    public void setActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "player")
        {
            gameObject.SetActive(false);
            global.coinNum++;
            global.coinChangeEvent();
        }
    }
    public void setVelocity(float x,float y)
    {
        rb.velocity = new Vector2(x, y);
    }
}