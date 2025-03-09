using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MP5 : basicGun
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
        //base.shootUp(targetDir);
    }*/
    public override void shooting(Vector2 targetDir)
    {
        if (!canShoot)
            return;
        if (currentBulletCount > 0)
        {
            shootSoundPlayer.clip = shootSound[0];
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