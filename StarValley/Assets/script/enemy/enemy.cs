using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemy : MonoBehaviour
{
    public enum States
    {
        FollowPlayer,
        Shoot
    }
    public List<AudioClip> shootSound = new List<AudioClip>();
    public AudioSource shootSoundPlayer;
    public States state = States.FollowPlayer;
    public float FollowPlayerSeconds;
    public float CurrentSeconds = 0;
    public enemBullet enemyBullet;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    [HideInInspector] public room bornRoom;
    [HideInInspector] public float hp = 5;
    [SerializeField] protected hitEffect enemyHitEffect;
    [SerializeField] protected blood enemyBloodEffect;
    [SerializeField] protected coin coinDrop;
    protected virtual void Start()
    {
        FollowPlayerSeconds = Random.Range(2, 4f);
    }
    protected virtual void Update()
    {
        if (Time.timeScale == 0||!global.player)
            return;
        if (state == States.FollowPlayer)
        {
            if(CurrentSeconds > FollowPlayerSeconds)
            {
                state = States.Shoot;
                CurrentSeconds = 0;
            }
            Vector3 dirctionToPlayer = (global.player.transform.position - transform.position).normalized;
            if (dirctionToPlayer.x > 0)
            {
                sprite.flipX = false;
            }
            else
            {
                sprite.flipX = true;
            }
            rb.velocity = dirctionToPlayer * 3;
            CurrentSeconds += Time.deltaTime;
        }
        else if(state == States.Shoot)
        {
            /*CurrentSeconds += Time.deltaTime;
            if(CurrentSeconds > 1.0f)
            {
                state = States.FollowPlayer;
                FollowPlayerSeconds = Random.Range(2, 4f);
            }
            if (Time.frameCount % 20 == 0)
            {
                rb.velocity = Vector3.zero;
                shoot();
            }*/
            shoot();
            FollowPlayerSeconds = Random.Range(2, 4f);
            state = States.FollowPlayer;
        }
    }
    protected virtual void shoot()
    {
        Vector3 directionToPlayer = (global.player.transform.position - transform.position).normalized;
        enemBullet bullet = bornBullet();//Instantiate(enemyBullet, transform.position, Quaternion.identity);
        bullet.direction = directionToPlayer;
        bullet.gameObject.SetActive(true);
        int index = Random.Range(0, shootSound.Count);
        shootSoundPlayer.clip = shootSound[index];
        shootSoundPlayer.Play();
    }
    public void hurt(float damage)
    {
        hurtEffect();
        hp -= damage;
        if (hp <= 0)
        {
            Destroy(gameObject);
            coin newCoin = objectPool.instance.getCoin("coin", coinDrop);
            newCoin.transform.position = transform.position;
            newCoin.setActive(true);
            newCoin.setVelocity(Random.Range(-3f, 3f), Random.Range(-3f, 3f));
            bornRoom.checkWave();
        }
    }
    protected virtual enemBullet bornBullet()
    {
        enemBullet newBullet = objectPool.instance.getEnemyBullet("enemyBullet", enemyBullet);
        newBullet.transform.position = transform.position;
        return newBullet;
    }
    protected void hurtEffect()
    {
        hitEffect effect = objectPool.instance.getEnemyHitEffect("enemyHitEffect", enemyHitEffect);
        effect.setActive(true);
        effect.transform.position = transform.position;
        effect.startEffect();
        blood newBlood = objectPool.instance.getEnemyBloodEffect("enemyblood", enemyBloodEffect);
        newBlood.transform.position = transform.position;
        newBlood.playEffect();
    }
}