using UnityEngine;

public class CountScore : MonoBehaviour
{
    [SerializeField] int totalScore = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        totalScore = 0;
        
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Dice child = transform.GetChild(i).GetComponent<Dice>();
            totalScore += child.currentScore;
        }
    }
}
