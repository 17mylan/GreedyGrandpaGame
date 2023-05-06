using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowGO : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] public Transform lookAt;
    [SerializeField] public Vector3 offset;

    [Header("Logic")] private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }
    
    private void Update()
    {
        var pos = cam.WorldToScreenPoint(lookAt.position + offset);

        if (transform.position != pos)
            transform.position = pos;
    }
}
