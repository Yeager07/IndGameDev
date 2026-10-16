using UnityEngine;

public class GetCubeSideScore : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.name == "Table")
        transform.parent.GetComponent<Dice>().CheckNormal(transform);
        
        return;
    }
}
