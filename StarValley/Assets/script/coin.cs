using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class coin : MonoBehaviour
{
    public Rigidbody2D rb;
    public float detectDistance;
    public LayerMask playerLayer;
    public AudioSource audioPlayer;
    private void Update()
    {

        if (detectPlayer())
        {
            Vector3 dirctionToPlayer = (global.player.transform.position - transform.position).normalized;
            rb.velocity = dirctionToPlayer * 6;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, detectDistance);
    }
    public bool detectPlayer()
    {
        return Physics2D.OverlapCircle(transform.position, detectDistance, playerLayer);
    }
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
            audioPlayer.Play();
            StartCoroutine(wait());
            //gameObject.SetActive(false);
            global.coinNum++;
            global.coinChangeEvent();
        }
    }
    private IEnumerator wait()
    {
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    public void setVelocity(float x,float y)
    {
        rb.velocity = new Vector2(x, y);
    }
}