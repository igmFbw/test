using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class objectPool : MonoBehaviour
{
    [SerializeField] private Transform playerBullet;
    [SerializeField] private Transform enemyBullet;
    [SerializeField] private Transform playerHitEffect;
    [SerializeField] private Transform enemyHitEffect;
    [SerializeField] private Transform playerBlood;
    [SerializeField] private Transform enemyBlood;
    [SerializeField] private Transform coinParent;
    public static objectPool instance;
    private Dictionary<string, List<playBullet>> playerBulletPool;
    private Dictionary<string, List<enemBullet>> enemyBulletPool;
    private Dictionary<string, List<hitEffect>> playerHitEffectPool;
    private Dictionary<string, List<hitEffect>> enemyHitEffectPool;
    private Dictionary<string, List<blood>> playerBloodPool;
    private Dictionary<string, List<blood>> enemyBloodPool;
    private Dictionary<string, List<coin>> coinPool;
    private void Awake()
    {
        instance = this;
        playerBulletPool = new Dictionary<string, List<playBullet>>();
        enemyBulletPool = new Dictionary<string, List<enemBullet>>();
        playerHitEffectPool = new Dictionary<string, List<hitEffect>>();
        enemyHitEffectPool = new Dictionary<string, List<hitEffect>>();
        playerBloodPool = new Dictionary<string, List<blood>>();
        enemyBloodPool = new Dictionary<string, List<blood>>();
        coinPool = new Dictionary<string, List<coin>>();
    }
    public playBullet getPlayerBullet(string name,playBullet bullet)
    {
        if (!playerBulletPool.ContainsKey(name))
        {
            playerBulletPool.Add(name, new List<playBullet>());
            playBullet bu = Instantiate(bullet);
            bu.setActive(false);
            bu.transform.parent = playerBullet;
            playerBulletPool[name].Add(bu);
        }
        foreach(var item in playerBulletPool[name])
        {
            if(item.getActive() == false)
            {
                return item;
            }
        }
        playBullet bull = Instantiate(bullet);
        bull.setActive(false);
        bull.transform.parent = playerBullet;
        playerBulletPool[name].Add(bull);
        return bull;
    }
    public void releasePlayerBullet(playBullet obj)
    {
        obj.setActive(false);
    }
    public enemBullet getEnemyBullet(string name, enemBullet bullet)
    {
        if (!enemyBulletPool.ContainsKey(name))
        {
            enemyBulletPool.Add(name,new List<enemBullet>());
            enemBullet newBullet = Instantiate(bullet);
            newBullet.setActive(false);
            newBullet.transform.parent = enemyBullet;
            enemyBulletPool[name].Add(newBullet);
        }
        foreach(var item in enemyBulletPool[name])
        {
            if(item.getActive() == false)
            {
                return item;
            }
        }
        enemBullet newEnemyBullet = Instantiate(bullet);
        newEnemyBullet.setActive(false);
        newEnemyBullet.transform.parent = enemyBullet;
        enemyBulletPool[name].Add(newEnemyBullet);
        return newEnemyBullet;
    }
    public void releaseEnemyBullet(enemBullet obj)
    {
        obj.setActive(false);
    }
    public hitEffect getPlayerHitEffect(string name,hitEffect effect)
    {
        if(!playerHitEffectPool.ContainsKey(name))
        {
            playerHitEffectPool.Add(name,new List<hitEffect>());
            hitEffect playerEffect = Instantiate(effect);
            playerEffect.setActive(false);
            playerEffect.transform.parent = playerHitEffect;
            playerHitEffectPool[name].Add(playerEffect);
        }
        foreach(var item in playerHitEffectPool[name])
        {
            if(item.getActive() == false)
            {
                return item;
            }
        }
        hitEffect newEffect = Instantiate(effect);
        newEffect.setActive(false);
        newEffect.transform.parent = playerHitEffect;
        playerHitEffectPool[name].Add(newEffect);
        return newEffect;
    }
    public hitEffect getEnemyHitEffect(string name, hitEffect effect)
    {
        if (!enemyHitEffectPool.ContainsKey(name))
        {
            enemyHitEffectPool.Add(name, new List<hitEffect>());
            hitEffect enemyEffect = Instantiate(effect);
            enemyEffect.setActive(false);
            enemyEffect.transform.parent = enemyHitEffect;
            enemyHitEffectPool[name].Add(enemyEffect);
        }
        foreach (var item in enemyHitEffectPool[name])
        {
            if (item.getActive() == false)
            {
                return item;
            }
        }
        hitEffect newEffect = Instantiate(effect);
        newEffect.setActive(false);
        newEffect.transform.parent = enemyHitEffect;
        enemyHitEffectPool[name].Add(newEffect);
        return newEffect;
    }
    public blood getPlayerBloodEffect(string name, blood value)
    {
        if (!playerBloodPool.ContainsKey(name))
        {
            playerBloodPool.Add(name, new List<blood>());
            blood playerBloodEffect = Instantiate(value);
            playerBloodEffect.closeActive();
            playerBloodEffect.transform.parent = playerBlood;
            playerBloodPool[name].Add(playerBloodEffect);
        }
        foreach (var item in playerBloodPool[name])
        {
            if (item.getActive() == false)
            {
                return item;
            }
        }
        blood newEffect = Instantiate(value);
        newEffect.closeActive();
        newEffect.transform.parent = playerBlood;
        playerBloodPool[name].Add(newEffect);
        return newEffect;
    }
    public blood getEnemyBloodEffect(string name, blood value)
    {
        if (!enemyBloodPool.ContainsKey(name))
        {
            enemyBloodPool.Add(name, new List<blood>());
            blood enemyBloodEffect = Instantiate(value);
            enemyBloodEffect.closeActive();
            enemyBloodEffect.transform.parent = enemyBlood;
            enemyBloodPool[name].Add(enemyBloodEffect);
        }
        foreach (var item in enemyBloodPool[name])
        {
            if (item.getActive() == false)
            {
                return item;
            }
        }
        blood newEffect = Instantiate(value);
        newEffect.closeActive();
        newEffect.transform.parent = enemyBlood;
        enemyBloodPool[name].Add(newEffect);
        return newEffect;
    }
    public coin getCoin(string name, coin value)
    {
        if (!coinPool.ContainsKey(name))
        {
            coinPool.Add(name, new List<coin>());
            coin coinEffect = Instantiate(value);
            coinEffect.setActive(false);
            coinEffect.transform.parent = coinParent;
            coinPool[name].Add(coinEffect);
        }
        foreach (var item in coinPool[name])
        {
            if (item.getActive() == false)
            {
                return item;
            }
        }
        coin newCoin = Instantiate(value);
        newCoin.setActive(false);
        newCoin.transform.parent = coinParent;
        coinPool[name].Add(newCoin);
        return newCoin;
    }
}