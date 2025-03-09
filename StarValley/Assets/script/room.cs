using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum roomType
{
    init,
    normal,
    final,
    chest,
}
public class room:MonoBehaviour
{
    public List<Vector3> enemyPos = new List<Vector3>();
    public List<Vector3> doorPos = new List<Vector3>();
    private List<GameObject> lockDoor = new List<GameObject>();
    private bool isBorn = false;
    public BoxCollider2D bc;
    public enemy enemyToBorn;
    public enemy enemyBToBorn;
    public Vector2 roomTriggerSize;
    private int enemyCount;
    private int enemyDie;
    private Player player;
    private List<enemyWave> enemyWaves = new List<enemyWave>();
    [SerializeField] private GameObject door;
    [HideInInspector] public int roomCount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            if (isBorn)
                return;
            levelControll.instance.roomList[roomCount].isLock = false;
            isBorn = true;
            player = collision.transform.GetComponent<Player>();
            player.currentRoom = this;
            player.roomTriggerSize = roomTriggerSize;
            for(int i= 0; i < Random.Range(1, 4); i++)
            {
                enemyWaves.Add(new enemyWave());
            }
            if (enemyPos.Count <= 0)
            {
                return;
            }
            enemyCount = enemyPos.Count;
            enemyDie = 0;
            generateEnemy();
            Invoke("closeDoor", .15f);
        }
    }
    private void closeDoor()
    {
        foreach (var item in doorPos)
        {
            GameObject newDoor = Instantiate(door, item, Quaternion.identity);
            lockDoor.Add(newDoor);
        }
    }

    private void generateEnemy()
    {
        foreach (var item in enemyPos)
        {
            var enem = Instantiate(enemyToBorn, item, Quaternion.identity);
            enem.gameObject.SetActive(true);
            enem.bornRoom = this;
        }
    }
    public void checkWave()
    {
        enemyDie++;
        if(enemyDie >= enemyCount)
        {
            enemyDie = 0;
            enemyWaves.Remove(enemyWaves[0]);
            if (enemyWaves.Count != 0)
                generateEnemy();
            else
            {
                foreach (var item in lockDoor)
                {
                    Destroy(item.gameObject);
                }
            }
        }
    }
}