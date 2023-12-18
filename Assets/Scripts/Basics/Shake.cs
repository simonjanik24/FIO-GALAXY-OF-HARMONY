using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shake : MonoBehaviour
{
    [SerializeField]
    private float duration = 1f;

    [SerializeField]
    private float strength = 1f;

    private void Start()
    {
        ShakeMe();
    }


    public void ShakeMe()
    {
        StartCoroutine(Shaking());
    }
    private IEnumerator Shaking()
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = startPosition + new Vector3(Random.RandomRange(0,strength), Random.RandomRange(0, strength), Random.RandomRange(0, strength));
            yield return null;
        }
        transform.position = startPosition;
    }
}
