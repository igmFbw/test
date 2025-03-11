using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Player : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sprite;
    public Transform weapon;
    public basicGun currentGun;
    public room currentRoom;
    public Vector2 roomTriggerSize;
    private enemy enemyTarget;
    public AudioSource audioPlayer;
    public AudioClip hurtSound;
    public AudioClip dieSound;
    [SerializeField] private hitEffect playerHitEffect;
    [SerializeField] private blood playerBloodEffect;
    [SerializeField] private LayerMask enemyLay;
    private void Update()
    {
        if (Time.timeScale == 0)
        {
            currentGun.shootSoundPlayer.Stop();
            return;
        }
        Vector3 mouseScreenPosition = Input.mousePosition;
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        Vector3 shootDir = (mouseWorldPosition - transform.position).normalized;
        enemyTarget = findEnemy();
        if (enemyTarget != null)
            shootDir = (enemyTarget.transform.position - transform.position).normalized;
        float radius = Mathf.Atan2(shootDir.y, shootDir.x);
        float eulerAngle = radius * Mathf.Rad2Deg;
        weapon.localRotation = Quaternion.Euler(0, 0, eulerAngle);
        if (shootDir.x > 0)
        {
            weapon.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            weapon.transform.localScale = new Vector3(1, -1, 1);
        }
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        rb.velocity = new Vector2(horizontal, vertical).normalized * 5;
        if (horizontal < 0)
        {
            sprite.flipX = true;
        }
        else if (horizontal > 0)
        {
            sprite.flipX = false;
        }
        if (Input.GetMouseButton(0))
        {
            currentGun.shooting(shootDir);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentGun.shootDown(shootDir);
        }
    }
    private enemy findEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(currentRoom.transform.position, roomTriggerSize, 0);
        var sorted = colliders.OrderBy(collider=>Vector2.Distance(collider.transform.position,transform.position)).ToArray();
        foreach (var collider in sorted)
        {
            if (collider.tag == "enemy" && Physics2D.Raycast(transform.position, (collider.transform.position - transform.position).normalized, Mathf.Infinity, enemyLay))
            {
                return collider.GetComponent<enemy>();
            }
        }
        return null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(currentRoom.transform.position, roomTriggerSize);
        if(enemyTarget != null)
        {
            Gizmos.DrawLine(transform.position,enemyTarget.transform.position);
        }
    }
    public void hurt(int damage)
    {
        hurtEffect();
        global.hp -= damage;
        audioPlayer.clip = hurtSound;
        audioPlayer.Play();
        if (global.hp < 0)
        {
            global.hp = 0;
            audioPlayer.clip = dieSound;
            audioPlayer.Play();
            GameUI.Instance.gameLose.SetActive(true);
            Time.timeScale = 0;
        }
        global.HpChangeEvent();
    }
    private void hurtEffect()
    {
        hitEffect hitEffect = objectPool.instance.getPlayerHitEffect("playerHitEffect", playerHitEffect);
        hitEffect.setActive(true);
        hitEffect.transform.position = transform.position;
        hitEffect.startEffect();
        blood newBlood = objectPool.instance.getPlayerBloodEffect("playerBlood", playerBloodEffect);
        newBlood.transform.position = transform.position;
        newBlood.playEffect();
    }
}