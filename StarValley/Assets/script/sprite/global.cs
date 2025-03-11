using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class global : MonoBehaviour
{
    public static Player player;
    public static int hp = 100;
    public static Action HpChangeEvent;
    public static Action coinChangeEvent;
    public static int coinNum;
    public static void resetData()
    {
        hp = 3;
    }
}