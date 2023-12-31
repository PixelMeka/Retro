using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;
    public float cameraDistance;
    public float cameraHeight;
    public float cameraX;

    public float camera2D_Distance;
    public float camera2D_Height;
    public float camera2D_X;

    public bool area2D = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        area2D = player.GetComponentInChildren<PlayerCollider>().area2D;

        Vector3 pos = player.transform.position;

        if (!area2D)
        {
            pos.z += cameraDistance;
            pos.y += cameraHeight;
            pos.x += cameraX;
        }
        
        if(area2D)
        {
            pos.z += camera2D_Distance;
            pos.y += camera2D_Height;
            pos.x += camera2D_X;
        }
        
        transform.position = pos;
    }
}
