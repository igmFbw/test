using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class pistol : basicGun
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Update()
    {
        base.Update();
    }
    /*public override void shootUp(Vector2 targetDir)
    {
        int index = Random.Range(0, shootSound.Count);
        shootSoundPlayer.clip = shootSound[index];
        base.shootUp(targetDir);
    }*/
    public override void shooting(Vector2 targetDir)
    {
        if (!canShoot)
            return;
        if (currentBulletCount > 0)
        {
            int index = Random.Range(0, shootSound.Count);
            shootSoundPlayer.clip = shootSound[index];
            shoot(targetDir);
        }
        else if (currentBulletCount <= 0)
        {
            shootSoundPlayer.clip = noBullet;
            shootSoundPlayer.Play();
        }
    }
    public override void shootDown(Vector2 targetDir)
    {
        base.shootDown(targetDir);
    }
}