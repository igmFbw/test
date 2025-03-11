using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class soundManager : MonoBehaviour
{
    public static soundManager _instance;
    public AudioSource audioPlayer;
    public AudioClip exchangeBullet;
    public AudioClip chestOpen;
    public AudioClip eatHp;
    public static soundManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<soundManager>();
                if (_instance == null)
                {
                    GameObject go = new GameObject("soundManage");
                    go.AddComponent<soundManager>();
                }
            }
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(gameObject);
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void playExchangeBullet()
    {
        audioPlayer.clip = exchangeBullet;
        audioPlayer.Play();
    }
    public void playEatHp()
    {
        audioPlayer.clip = eatHp;
        audioPlayer.Play();
    }
    public void playChestOpen()
    {
        audioPlayer.clip = chestOpen;
        audioPlayer.Play();
    }
}
