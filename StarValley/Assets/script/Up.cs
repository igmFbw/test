using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using UnityEngine;
using UnityEngine.UI;
public class Up : MonoBehaviour
{
    public Button First;
    public Button Second;
    public Button Third;
    public GameObject Panel;

    private void Start()
    {
        //Panel.SetActive(false);
    }

    private void Update()
    {
        First.onClick.AddListener(HpUp);
        Second.onClick.AddListener(BulletUp);
        Third.onClick.AddListener(SuperUp);
    }

    private void HpUp()
    {
        global.hp += 3;
    }

    private void BulletUp()
    {

    }

    private void SuperUp()
    {

    }
}
