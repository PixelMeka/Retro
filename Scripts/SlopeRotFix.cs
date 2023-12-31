using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeRotFix : MonoBehaviour
{
    public GameObject pl;

    // Start is called before the first frame update
    void Start()
    {
        pl.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        float rot = pl.transform.rotation.eulerAngles.x;

        gameObject.transform.localRotation = Quaternion.Euler(0, 90, -rot);
    }
}
