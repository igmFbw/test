using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class corpse : MonoBehaviour
{
    public SpriteRenderer sr;
    private Vector3 targetRotation;
    private void Start()
    {
        StartCoroutine(stopEffect());
    }
    public void initialize()
    {
        targetRotation = new Vector3(0, 0, Random.Range(0, 180));
        transform.eulerAngles = targetRotation;
    }
    private IEnumerator stopEffect()
    {
        initialize();
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }
    public void setSprite(Sprite sp)
    {
        sr.sprite = sp;
    }
}
