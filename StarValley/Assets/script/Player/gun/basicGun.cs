using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class basicGun : MonoBehaviour
{
    [SerializeField]protected float shootCool;
    protected float shootTimer;
    public playBullet bullet;
    public List<AudioClip> shootSound = new List<AudioClip>();
    [SerializeField] protected AudioClip noBullet;
    public AudioSource shootSoundPlayer;
    public int bulletCount;
    [HideInInspector]public int currentBulletCount;
    protected bool canShoot;
    [SerializeField] private GameObject gunFire;
    [SerializeField] private float closeWaitCool;
    [SerializeField] private Transform firePos;
    protected virtual void Start()
    {
        shootTimer = 0;
        canShoot = true;
        currentBulletCount = bulletCount;
        UpdateBulletUI();
    }
    protected virtual void Update()
    {
        if (Time.timeScale == 0)
            return;
        if(Input.GetKeyDown(KeyCode.R))
        {
            currentBulletCount = bulletCount;
            UpdateBulletUI();
            soundManager.instance.playExchangeBullet();
        }
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootCool)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
    }
    /*public virtual void shootUp(Vector2 targetDir)
    {
        if (canShoot && currentBulletCount > 0)
        {
            shoot(targetDir);
        }
    }*/
    public virtual void shooting(Vector2 targetDir)
    {
        if (!canShoot)
            return;
        if (currentBulletCount > 0)
        {
            shoot(targetDir);
        }
        else if (currentBulletCount <= 0)
        {
            shootSoundPlayer.clip = noBullet;
            shootSoundPlayer.Play();
        }
    }
    public virtual void shootDown(Vector2 targetDir)
    {
        Invoke("stopSound",closeWaitCool);
    }
    private void stopSound()
    {
        shootSoundPlayer.Stop();
    }
    protected virtual void shoot(Vector2 targetDir)
    {
        playBullet playerBullet = objectPool.instance.getPlayerBullet("playerBullet", bullet);
        playerBullet.gameObject.SetActive(true);
        playerBullet.direction = targetDir;
        playerBullet.transform.position = firePos.position;
        shootSoundPlayer.Play();
        shootTimer = 0;
        currentBulletCount--;
        if(currentBulletCount <= 0 )
        {
            shootSoundPlayer.Stop();
        }
        UpdateBulletUI();
        StartCoroutine("gunFireFx");
    }
    protected virtual void UpdateBulletUI()
    {
        GameUI.Instance.bulletCount.text = ":" + currentBulletCount.ToString()+"/"+bulletCount.ToString();
    }
    private IEnumerator gunFireFx()
    {
        gunFire.SetActive(true);
        yield return new WaitForSeconds(.14f);
        gunFire.SetActive(false);
    }
}