using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UImap : MonoBehaviour
{
    public static UImap instance;
    [SerializeField] private roomMapItem roomBlock;
    [SerializeField] private RectTransform roomBlockParent;
    private List<roomMapItem> roomMapItems = new List<roomMapItem>();
    private roomMapItem item;
    private int buildCount;
    private bool isBuid  = false;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        if (!isBuid)
        {
            buildRoomNode();
            isBuid = true;
        }
        Time.timeScale = 0;
        int n = levelControll.instance.roomList.Count;
        for (int i = 0; i < n; i++)
        {
            if (!levelControll.instance.roomList[i].isLock)
            {
                roomMapItems[i].gameObject.SetActive(true);
            }
        }
    }
    private void buildRoomNode()
    {
        int length = levelControll.instance.roomList.Count;
        for (int i = 0; i < length; i++)
        {
            roomMapItem newRoom = Instantiate(roomBlock);
            newRoom.n = i;
            newRoom.GetComponent<RectTransform>().SetParent(roomBlockParent,false);
            newRoom.GetComponent<RectTransform>().localPosition = new Vector2(levelControll.instance.roomList[i].x * 80, levelControll.instance.roomList[i].y * 80);
            if (newRoom.GetComponent<RectTransform>().localPosition.x > 320)
            {
                roomBlockParent.localPosition = new Vector2(roomBlockParent.localPosition.x - 80, roomBlockParent.localPosition.y);
            }
            else if (newRoom.GetComponent<RectTransform>().localPosition.x < -320)
            {
                roomBlockParent.localPosition = new Vector2(roomBlockParent.localPosition.x + 80, roomBlockParent.localPosition.y);
            }
            if (newRoom.GetComponent<RectTransform>().localPosition.y > 160)
            {
                roomBlockParent.localPosition = new Vector2(roomBlockParent.localPosition.x, roomBlockParent.localPosition.y - 80);
            }
            else if (newRoom.GetComponent<RectTransform>().localPosition.x < -160)
            {
                roomBlockParent.localPosition = new Vector2(roomBlockParent.localPosition.x, roomBlockParent.localPosition.y + 80);
            }
            if (levelControll.instance.roomList[i].roType == roomType.init)
            {
                newRoom.roomType.text = "³õÊ¼";
            }
            else if (levelControll.instance.roomList[i].roType == roomType.normal)
            {
                newRoom.roomType.text = "Õ½¶·";
            }
            else if (levelControll.instance.roomList[i].roType == roomType.chest)
            {
                newRoom.roomType.text = "½±Àø";
            }
            else
            {
                newRoom.roomType.text = "½áÊø";
            }
            roomMapItems.Add(newRoom);
            newRoom.gameObject.SetActive(false);
        }
    }
}