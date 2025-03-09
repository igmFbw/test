using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class hitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem effect;
    public void setActive(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
    public bool getActive()
    {
        return gameObject.activeSelf;
    }
    public void startEffect()
    {
        StartCoroutine(closeEffect());
    }
    private IEnumerator closeEffect()
    {
        effect.Play();
        yield return new WaitForSeconds(0.14f);
        effect.Stop();
        gameObject.SetActive(false);
    }
}
