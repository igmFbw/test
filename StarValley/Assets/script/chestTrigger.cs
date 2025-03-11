using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class chestTrigger : MonoBehaviour
{
    [SerializeField] private GameObject hp;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            soundManager.instance.playChestOpen();
            Instantiate(hp,new Vector2(transform.position.x,transform.position.y+.5f), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
