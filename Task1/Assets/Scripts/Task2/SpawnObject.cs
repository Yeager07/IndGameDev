using UnityEngine;
using System.Collections;
using static System.Math;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabs = new GameObject[5];
    [SerializeField] private int countObjects;
    [SerializeField] private int radiusSpawn = 5;
    private int currentRadius;
    private int currentCountObjects;
 
    void Awake()
    {
        currentRadius = radiusSpawn;
        currentCountObjects = countObjects;

        int generatedCount = 0;

        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform child = transform.GetChild(i);
            Destroy(child.gameObject);
        }
        
        while(generatedCount != countObjects)
        {
            GameObject spawnedObject = Instantiate(prefabs[generatedCount % prefabs.Length]);
            spawnedObject.transform.parent = transform;
            spawnedObject.transform.position = new Vector3(radiusSpawn, 0.0f, 0.0f);
            transform.Rotate(new Vector3(0.0f, 360/countObjects, 0.0f));
            generatedCount += 1;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentRadius != radiusSpawn || currentCountObjects != countObjects)
        Awake();
    }
}
