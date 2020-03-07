using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeotagMover : MonoBehaviour
{
    private new Camera camera;
    
    void Start()
    {
        camera = Camera.main;
    }

    void FixedUpdate()
    {
        transform.rotation = camera.transform.rotation;
    }
}
