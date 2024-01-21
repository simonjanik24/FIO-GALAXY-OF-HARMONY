using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsToBeat : MonoBehaviour
{
    [SerializeField]
    private bool active;
    [SerializeField]
    private float pulseSize = 1.15f;
    [SerializeField]
    private float returnSpeed = 5f;
    [SerializeField]
    private Vector3 startSize;


    // Start is called before the first frame update
    void Start()
    {
        startSize = transform.localScale;
        if (active)
        {
            StartCoroutine(Beat());
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, startSize, Time.deltaTime * returnSpeed);
    }

    public void Pulse()
    {
        transform.localScale = startSize * pulseSize;
    }

    private IEnumerator Beat()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Pulse();
        }
    }
}
