using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class global : MonoBehaviour
{
    public static Player player;
    public static int hp = 5;
    public static Action HpChangeEvent;
    public static Action coinChangeEvent;
    public static int coinNum;
    public static void resetData()
    {
        hp = 5;
        coinNum = 0;
    }
}