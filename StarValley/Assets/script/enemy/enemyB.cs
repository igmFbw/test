using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class enemyB : enemy
{
    protected override void Update()
    {
        base.Update();
    }
    protected override void shoot()
    {
        int count = 3;
        float angle = 15;
        Vector3 directionToPlayer = (global.player.transform.position - transform.position).normalized;
        float mainAnle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x)*Mathf.Rad2Deg;
        for (int i = 0; i < count; i++)
        {
            float bulletAngle = mainAnle + i * angle - count * .5f * angle;
            Vector2 dir = new Vector2(Mathf.Cos(bulletAngle*Mathf.Deg2Rad), Mathf.Sin(bulletAngle*Mathf.Deg2Rad)).normalized;
            Vector2 pos = new Vector2(transform.position.x, transform.position.y) + .5f * dir;
            enemBullet bullet = bornBullet();//Instantiate(enemyBullet, pos, Quaternion.identity);
            bullet.direction = dir;
            bullet.gameObject.SetActive(true);
            int index = Random.Range(0, shootSound.Count);
            shootSoundPlayer.clip = shootSound[index];
            shootSoundPlayer.Play();
        }
    }
}