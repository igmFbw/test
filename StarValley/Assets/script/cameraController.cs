using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraController : MonoBehaviour
{
    void Update()
    {
        if (global.player)
        {
            var targetPos = global.player.transform.position;
            Vector2 currentPos = Vector2.Lerp(transform.position, targetPos, (1.0f - Mathf.Exp(-Time.deltaTime * 5)));
            transform.position = new Vector3(currentPos.x, currentPos.y, -1);
        }
    }
}