using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class roomMapItem : MonoBehaviour
{
    public Text roomType;
    public Button transmit;
    [HideInInspector] public int n;
    [SerializeField] private GameObject upDoor;
    [SerializeField] private GameObject downDoor;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    private void Awake()
    {
        transmit.onClick.AddListener(() => global.player.transform.position = levelControll.instance.roomItemPos[n]);
    }
    private void Start()
    {
        closeDoor();
    }
    private void closeDoor()
    {
        if(n >= levelControll.instance.pathDir.Count)
        {
            if (levelControll.instance.pathDir[n-1] == direction.up)
            {
                upDoor.SetActive(false);
                rightDoor.SetActive(false);
                leftDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.down)
            {
                downDoor.SetActive(false);
                leftDoor.SetActive(false);
                rightDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.left)
            {
                leftDoor.SetActive(false);
                upDoor.SetActive(false);
                downDoor.SetActive(false);
            }
            else
            {
                rightDoor.SetActive(false);
                upDoor.SetActive(false);
                downDoor.SetActive(false);
            }
        }
        else if (levelControll.instance.pathDir[n] == direction.up)
        {
            if (n == 0)
            {
                downDoor.SetActive(false);
                leftDoor.SetActive(false);
                rightDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.up)
            {
                leftDoor.SetActive(false);
                rightDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.right)
            {
                rightDoor.SetActive(false);
                leftDoor.SetActive(false);
            }
            else
            {
                leftDoor.SetActive(false);
                downDoor.SetActive(false);
            }
        }
        else if (levelControll.instance.pathDir[n] == direction.down)
        {
            if (n == 0)
            {
                leftDoor.SetActive(false);
                rightDoor.SetActive(false);
                upDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.left)
            {
                leftDoor.SetActive(false);
                upDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.right)
            {
                rightDoor.SetActive(false);
                upDoor.SetActive(false);
            }
            else
            {
                rightDoor.SetActive(false);
                leftDoor.SetActive(false);
            }
        }
        else if (levelControll.instance.pathDir[n] == direction.right)
        {
            if (n == 0)
            {
                upDoor.SetActive(false);
                downDoor.SetActive(false);
                leftDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.up)
            {
                leftDoor.SetActive(false);
                upDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.right)
            {
                upDoor.SetActive(false);
                downDoor.SetActive(false);
            }
            else
            {
                downDoor.SetActive(false);
                leftDoor.SetActive(false);
            }
        }
        else
        {
            if (n == 0)
            {
                upDoor.SetActive(false);
                downDoor.SetActive(false);
                rightDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.up)
            {
                rightDoor.SetActive(false);
                upDoor.SetActive(false);
            }
            else if (levelControll.instance.pathDir[n - 1] == direction.left)
            {
                upDoor.SetActive(false);
                downDoor.SetActive(false);
            }
            else
            {
                downDoor.SetActive(false);
                rightDoor.SetActive(false);
            }
        }
    }
}