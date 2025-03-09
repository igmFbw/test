using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemBullet : MonoBehaviour
{
    public Vector2 direction;
    [HideInInspector] public int bulletSpeed;
    private void Start()
    {
        objectPool.instance.releaseEnemyBullet(this);
        //Destroy(gameObject,5);
    }
    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * 5);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "player")
        {
            collision.gameObject.GetComponent<Player>().hurt(1);
        }
        objectPool.instance.releaseEnemyBullet(this);
        //Destroy(gameObject);
    }
    public bool getActive()
    {
        return gameObject.activeSelf;
    }
    public void setActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}