using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameUI : MonoBehaviour
{
    public static GameUI Instance;
    public GameObject gamePass;
    public GameObject gameLose;
    public Text hp;
    public Text bulletCount;
    public Text coinCount;
    public UImap Uimap;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        updateHp();
        updateCoin();
        global.HpChangeEvent += updateHp;
        global.coinChangeEvent += updateCoin;
        gamePass.transform.Find("BtnReStart").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            global.resetData();
            Time.timeScale = 1;
        });
        gameLose.transform.Find("BtnReStart").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
            global.resetData();
            Time.timeScale = 1;
        });
    }
    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.M))
        {
            if(Uimap.gameObject.activeSelf)
            {
                Uimap.gameObject.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                Uimap.gameObject.SetActive(true);
            }
        }
    }
    private void updateHp()
    {
        hp.text = "HP:" + global.hp.ToString();
    }
    private void updateCoin()
    {
        coinCount.text = global.coinNum.ToString();
    }
    private void OnDestroy()
    {
        Instance = null;
        global.HpChangeEvent -= updateHp;
        global.coinChangeEvent -= updateCoin;
    }
}