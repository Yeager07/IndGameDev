using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    private Vector3 rotation = new Vector3(0.0f, 0.0f, 0.0f);
    private Vector3 rotationX = new Vector3(1.0f, 0.0f, 0.0f);
    private Vector3 rotationY = new Vector3(0.0f, 1.0f, 0.0f);
    private Vector3 rotationZ = new Vector3(0.0f, 0.0f, 1.0f);
    
    [SerializeField] private float speedRotation = 1;
    [SerializeField] private bool rotateX = true;
    [SerializeField] private bool rotateY = false;
    [SerializeField] private bool rotateZ = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotateX)
        rotation += rotationX;
        
        if(rotateY)
        rotation += rotationY;

        if(rotateZ)
        rotation += rotationZ;

        transform.localRotation = Quaternion.Euler(rotation * speedRotation);
    }
}
