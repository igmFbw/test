using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class playBullet : MonoBehaviour
{
    [HideInInspector] public int bulletSpeed;
    public Vector2 direction;
    private void Start()
    {
        Invoke("destroyBullet", 5);
    }
    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * 15);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "enemy")
        {
            collision.gameObject.GetComponent<enemy>().hurt(1);
        }
        destroyBullet();
    }
    public void setActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public bool getActive()
    {
        return this.gameObject.activeSelf;
    }
    private void destroyBullet()
    {
        objectPool.instance.releasePlayerBullet(this);
    }
}