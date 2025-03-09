using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class blood : MonoBehaviour
{
    public Vector2 targetSize;
    private float duration = .1f;
    private float currentTimer;
    private Vector3 targetRotation;
    /*private void Start()
    {
        initialize();
    }*/
    private void Update()
    {
        if (currentTimer < duration)
        {
            float t = (currentTimer * 1.0f) / duration;
            Vector2 newScale = Vector2.Lerp(new Vector2(1,1), targetSize, t);
            transform.localScale = newScale;
            currentTimer += Time.deltaTime;
        }
    }
    public void initialize()
    {
        transform.localScale = new Vector2(1, 1);
        targetSize = new Vector2(Random.Range(1, 2.0f), Random.Range(1, 2.0f));
        currentTimer = 0;
        targetRotation = new Vector3(0, 0, Random.Range(0, 180));
        transform.eulerAngles = targetRotation;
    }
    public bool isActive()
    {
        return gameObject.activeSelf;
    }
    public void playEffect()
    {
        gameObject.SetActive(true);
        StartCoroutine(stopEffect());
    }
    private IEnumerator stopEffect()
    {
        initialize();
        yield return new WaitForSeconds(3);
        gameObject.SetActive(false);
    }
    public void closeActive()
    {
        gameObject.SetActive(false);
    }
    public bool getActive()
    {
        return gameObject.activeSelf;
    }
}
