using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
public enum direction
{
    up, down, left, right
}
public class levelControll : MonoBehaviour
{
    public static levelControll instance;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject roomTriggerParent;
    public TileBase ground0;
    public TileBase ground1;
    public TileBase ground2;
    public TileBase ground3;
    public room roomBuild;
    public TileBase groundTile
    {
        get
        {
            int index = UnityEngine.Random.Range(0, 4);
            if (index == 0)
                return ground0;
            else if (index == 1)
                return ground1;
            else if (index == 2)
                return ground2;
            else
                return ground3;
        }
    }
    public Tilemap groundTileMap;
    public TileBase tile0;
    public TileBase tile1;
    public TileBase tile2;
    public TileBase tile3;
    public TileBase tileTile
    {
        get
        {
            int index = UnityEngine.Random.Range(0, 4);
            if (index == 0)
                return tile0;
            else if (index == 1)
                return tile1;
            else if (index == 2)
                return tile2;
            else
                return tile3;
        }
    }
    public Tilemap tileTileMap;
    public Player player;
    private List<roomType> roType = new List<roomType>()
    {
        roomType.init,
        roomType.normal,
        roomType.normal,
        roomType.chest,
        roomType.normal,
        roomType.final,
    };
    public final finalDoor;
    private List<string> initRoom = new List<string>()
    {
        "1111111   1111111",
        "1               1",
        "1               1",
        "1               1",
        "1  @            1",
        "                 ",
        "                 ",
        "                 ",
        "1               1",
        "1               1",
        "1               1",
        "1               1",
        "1111111   1111111",
    };
    private List<string> normalRoom1 = new List<string>()
    {
        "1111111ddd1111111",
        "1  e            1",
        "1 11       e 11 1",
        "1 1           1 1",
        "1      e        1",
        "d  e            d",
        "d       1       d",
        "d          e    d",
        "1   e           1",
        "1 1    e      1 1",
        "1 11         11 1",
        "1       e       1",
        "1111111ddd1111111",
    };
    private List<string> normalRoom2 = new List<string>()
    {
        "1111111ddd1111111",
        "1  e            1",
        "1          e    1",
        "1               1",
        "1       1       1",
        "d     e 1     e d",
        "d     11111e    d",
        "d       1       d",
        "1   e   1       1",
        "1      e        1",
        "1               1",
        "1           e   1",
        "1111111ddd1111111",
    };
    private List<string> normalRoom3 = new List<string>()
    {
        "1111111ddd1111111",
        "1  e            1",
        "1       11   e  1",
        "1    1      1   1",
        "1               1",
        "d   e           d",
        "d               d",
        "d   e      e    d",
        "1               1",
        "1    1      1   1",
        "1       11      1",
        "1  e            1",
        "1111111ddd1111111",
    };
    private List<string> normalRoom4 = new List<string>()
    {
        "1111111ddd1111111",
        "1               1",
        "1   e   1  e    1",
        "1               1",
        "1      e        1",
        "d  e            d",
        "d  1         1  d",
        "d    e     e    d",
        "1               1",
        "1   e           1",
        "1      e1       1",
        "1               1",
        "1111111ddd1111111",
    };
    private List<string> normalRoom
    {
        get
        {
            int index = UnityEngine.Random.Range(0, 5);
            if (index == 0)
                return normalRoom1;
            else if (index == 1)
                return normalRoom2;
            else if (index == 2)
                return normalRoom3;
            else
                return normalRoom4;
        }
    }
    private List<string> finalRoom = new List<string>()
    {
        "1111111   1111111",
        "1               1",
        "1               1",
        "1               1",
        "1               1",
        "                 ",
        "        #        ",
        "                 ",
        "1               1",
        "1               1",
        "1               1",
        "1               1",
        "1111111   1111111",
    };
    private List<string> ChestRoom = new List<string>()
    {
        "1111111   1111111",
        "1               1",
        "1               1",
        "1               1",
        "1               1",
        "                 ",
        "        c        ",
        "                 ",
        "1               1",
        "1               1",
        "1               1",
        "1               1",
        "1111111   1111111",
    };
    [HideInInspector] public List<direction> pathDir = new List<direction>();
    [HideInInspector] public List<roomNode> roomList;
    public List<Vector2> roomItemPos = new List<Vector2>();
    private void Awake()
    {
        instance = this;
        Application.targetFrameRate = 60;
    }
    private void Start()
    {   //虽然内存仍可优化，但是这一大段算法误动，误动，误动！！！！！
        roomList = new List<roomNode>(); //BFS生成房间
        Queue<roomNode> qu = new Queue<roomNode>();
        List<Vector2> locate = new List<Vector2>();
        qu.Enqueue(new roomNode()//房间节点
        {
            x = 0,
            y = 0,
            roType = roomType.init
        });
        roomNode generateNode = null;
        List<direction> availableDirect = new List<direction>();
        for (int i = 0; i <= 5&&qu.Count>0; i++)
        {
            generateNode = qu.Dequeue();
            locate.Add(new Vector2(generateNode.x, generateNode.y));//flag标记
            roomList.Add(generateNode);//生成列表
            if (i==5)
                break;
            availableDirect = new List<direction>();
            if (!locate.Contains(new Vector2(generateNode.x+1,generateNode.y)))
            {
                availableDirect.Add(direction.right);
            }
            if (!locate.Contains(new Vector2(generateNode.x-1, generateNode.y)))
            {
                availableDirect.Add(direction.left);
            }
            if (!locate.Contains(new Vector2(generateNode.x, generateNode.y+1)))
            {
                availableDirect.Add(direction.up);
            }
            if (!locate.Contains(new Vector2(generateNode.x, generateNode.y-1)))
            {
                availableDirect.Add(direction.down);
            }
            var nextDir = availableDirect[UnityEngine.Random.Range(0, availableDirect.Count)];
            pathDir.Add(nextDir);
            roomType item = roType[i+1];
            if (nextDir == direction.right)
            {
                qu.Enqueue(new roomNode()
                {
                    x = generateNode.x + 1,
                    y = generateNode.y,
                    roType = item
                });
            }
            else if (nextDir == direction.left)
            {
                qu.Enqueue(new roomNode()
                {
                    x = generateNode.x - 1,
                    y = generateNode.y,
                    roType = item
                });
            }
            else if (nextDir == direction.up)
            {
                qu.Enqueue(new roomNode()
                {
                    x = generateNode.x,
                    y = generateNode.y + 1,
                    roType = item
                });
            }
            else
            {
                qu.Enqueue(new roomNode()
                {
                    x = generateNode.x,
                    y = generateNode.y - 1,
                    roType = item
                });
            }
        }
        int n = 0;//传入生成房间个数，方便根据门列表凿通路
        foreach (var item in roomList)
        {
            generateByType(item.x, item.y,item,n);
            n++;
        }
    }
    private void generateByType(int x,int y,roomNode item,int num)
    {
        var roomPosX = (normalRoom.First().Length + 2) * x;
        var roomPosY = (normalRoom.Count() + 2) * y;
        if (item.roType == roomType.normal)
        {
            generateRoom(roomPosX,roomPosY, normalRoom,num);
        }
        else if (item.roType == roomType.init)
        {
            generateRoom(roomPosX,roomPosY, initRoom,num);
        }
        else if (item.roType == roomType.chest)
        {
            generateRoom(roomPosX,roomPosY, ChestRoom, num);
        }
        else if (item.roType == roomType.final)
        {
            generateRoom(roomPosX,roomPosY, finalRoom, num);
        }
        generatePath(roomPosX, roomPosY, num);
    }//万恶的计算瓦片坐标
    private void generatePath(int startPosX, int startPosY,int num)
    {
        int roomWidth = initRoom[0].Length;
        int roomHeight = initRoom.Count;
        if (num == 0)//第一个房间特判
        {
            if (pathDir[num] == direction.up)
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
                pathUpRoad(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num] == direction.down)
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
                pathDownRoad(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num] == direction.left)
            {
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
                pathLeftRoad(startPosX, startPosY, roomWidth, roomHeight);
            }
            else
            {
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathRightRoad(startPosX, startPosY, roomWidth, roomHeight);
            }
            return;
        }
        if (num >= pathDir.Count)//最后一个房间特判
        {
            if (pathDir[num-1] == direction.up)
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num-1] == direction.down)
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num-1] == direction.left)
            {
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
            }
            else
            {
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
            }
            return;
        }
        else if (pathDir[num] == direction.up)
        {
            if (pathDir[num - 1] == direction.up)
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num -1] == direction.right)
            {
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
            }
            else
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
            }
            pathUpRoad(startPosX, startPosY, roomWidth, roomHeight);
        }
        else if (pathDir[num] == direction.down)
        {
            if (pathDir[num - 1] == direction.down)
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num - 1] == direction.right)
            {
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
            }
            else
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
            }
            pathDownRoad(startPosX, startPosY, roomWidth, roomHeight);
        }
        else if (pathDir[num] == direction.left)
        {
            if (pathDir[num - 1] == direction.down)
            {
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num - 1] == direction.left)
            {
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
            }
            else
            {
                pathRight(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
            }
            pathLeftRoad(startPosX, startPosY, roomWidth, roomHeight);
        }
        else
        {
            if (pathDir[num - 1] == direction.down)
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
            }
            else if (pathDir[num - 1] == direction.right)
            {
                pathDown(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
            }
            else
            {
                pathLeft(startPosX, startPosY, roomWidth, roomHeight);
                pathUp(startPosX, startPosY, roomWidth, roomHeight);
            }
            pathRightRoad(startPosX, startPosY, roomWidth, roomHeight);
        }
    }
    private void pathUpRoad(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int x = startPosX + roomWidth / 2;
        int y = startPosY + roomHeight + 1;
        tileTileMap.SetTile(new Vector3Int(x, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x + 1, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x - 1, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x, y + 1, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x + 1, y + 1, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x - 1, y + 1, 0), tileTile);
        groundTileMap.SetTile(new Vector3Int(x + 2, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x + 2, y + 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x - 2, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x - 2, y + 1, 0), groundTile);
    }
    private void pathDownRoad(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int x = startPosX + roomWidth / 2;
        int y = startPosY - 1;
        tileTileMap.SetTile(new Vector3Int(x, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x + 1, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x - 1, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x, startPosY, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x + 1, startPosY, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x - 1, startPosY, 0), tileTile);
        groundTileMap.SetTile(new Vector3Int(x + 2, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x + 2, startPosY, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x - 2, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x - 2, startPosY, 0), groundTile);
    }
    private void pathLeftRoad(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int x1 = startPosX - 2;int x2 = startPosX - 1;
        int y = roomHeight / 2;
        tileTileMap.SetTile(new Vector3Int(x1, startPosY + y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x1, startPosY + y + 1, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x1, startPosY + y + 2, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x2, startPosY + y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x2, startPosY + y + 1, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x2, startPosY + y + 2, 0), tileTile);
        groundTileMap.SetTile(new Vector3Int(x1, startPosY + y - 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x1, startPosY + y + 3, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x2, startPosY + y - 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x2, startPosY + y + 3, 0), groundTile);
    }
    private void pathRightRoad(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int x1 = startPosX + 1 + roomWidth;int x2 = startPosX + roomWidth;
        int y = startPosY + roomHeight / 2;
        tileTileMap.SetTile(new Vector3Int(x1, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x1, y + 1, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x1, y + 2, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x2, y, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x2, y + 1, 0), tileTile);
        tileTileMap.SetTile(new Vector3Int(x2, y + 2, 0), tileTile);
        groundTileMap.SetTile(new Vector3Int(x1, y - 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x1, y + 3, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x2, y - 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x2, y + 3, 0), groundTile);
    }
    private void pathLeft(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int y = startPosY + roomHeight / 2;
        groundTileMap.SetTile(new Vector3Int(startPosX, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(startPosX, y + 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(startPosX, y + 2, 0), groundTile);
      }
    private void pathDown(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int y = startPosX + roomWidth / 2;
        groundTileMap.SetTile(new Vector3Int(y, startPosY + 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(y + 1, startPosY + 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(y - 1, startPosY + 1, 0), groundTile);
    }
    private void pathRight(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int x = startPosX + roomWidth - 1;
        int y = startPosY + roomHeight / 2;
        groundTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x, y + 1, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x, y + 2, 0), groundTile);
    }
    private void pathUp(int startPosX, int startPosY, int roomWidth, int roomHeight)
    {
        int x = startPosX + roomWidth / 2;
        int y = startPosY + roomHeight;
        groundTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x + 1, y, 0), groundTile);
        groundTileMap.SetTile(new Vector3Int(x - 1, y, 0), groundTile);
    }
    private void generateRoom(int startPosX,int startPosY,List<string> roomCode,int num)
    {
        int roomWidth = roomCode[0].Length;
        int roomHeight = roomCode.Count;
        float roomX = startPosX + roomWidth * .5f;
        float roomY = startPosY + roomHeight * .5f+1;
        room newRoom = Instantiate(roomBuild, new UnityEngine.Vector3(roomX, roomY, 0), UnityEngine.Quaternion.identity);
        newRoom.transform.SetParent(roomTriggerParent.transform, true);
        newRoom.roomTriggerSize = new Vector2(roomWidth-2, roomHeight-2);
        newRoom.roomCount = num;
        newRoom.gameObject.SetActive(true);
        roomItemPos.Add(newRoom.transform.position);
        newRoom.bc.size = new UnityEngine.Vector2(roomWidth - 2, roomHeight - 2);
        for (int i = 0; i < roomHeight; i++)
        {
            string rowcode = roomCode[i];
            int n = rowcode.Length;
            for (int j = 0; j < n; j++)
            {
                char code = rowcode[j];
                int x = j + startPosX;
                int y = roomHeight - i+startPosY;
                if (code == ' ')
                {
                    tileTileMap.SetTile(new Vector3Int(x, y, 0), tileTile);
                }
                else if (code == '1')
                {
                    groundTileMap.SetTile(new Vector3Int(x, y, 0), groundTile);
                }
                else if (code == 'e')
                {
                    newRoom.enemyPos.Add(new UnityEngine.Vector3(x + 0.5f, y + 0.5f, 0));
                    tileTileMap.SetTile(new Vector3Int(x, y, 0), tileTile);
                }
                else if (code == '@')
                {
                    var play = Instantiate(player, new UnityEngine.Vector3(x + 0.5f, y + 0.5f, 0), UnityEngine.Quaternion.identity);
                    play.gameObject.SetActive(true);
                    global.player = play;
                    tileTileMap.SetTile(new Vector3Int(x, y, 0), tileTile);
                }
                else if (code == '#')
                {
                    var final = Instantiate(finalDoor, new UnityEngine.Vector3(x + 0.5f, y + 0.5f, 0), UnityEngine.Quaternion.identity);
                    final.gameObject.SetActive(true);
                    tileTileMap.SetTile(new Vector3Int(x, y, 0), tileTile);
                }
                else if(code == 'd')
                {
                    newRoom.doorPos.Add(new UnityEngine.Vector3(x + 0.5f, y + 0.5f, 0));
                    tileTileMap.SetTile(new UnityEngine.Vector3Int(x, y, 0), tileTile);
                }
                else if(code == 'c')
                {
                    var che = Instantiate(chest, new UnityEngine.Vector3(x + 0.5f, y + 0.5f, 0), UnityEngine.Quaternion.identity);
                    tileTileMap.SetTile(new Vector3Int(x, y, 0), tileTile);  
;                }
            }
        }
    }
}