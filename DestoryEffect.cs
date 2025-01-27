using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestoryEffect : MonoBehaviour
{
    // Start is called before the first frame up
    public float timeToDestroy=1f;
    void Start()
    {
        StartCoroutine(WaitToDestory());
    }

    // niszczy obiekt po okreslonym czasie
    IEnumerator WaitToDestory()
    {
        yield return new WaitForSeconds(timeToDestroy); Destroy(gameObject);
    }
}
