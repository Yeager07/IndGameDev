using UnityEngine;

public class CountScore : MonoBehaviour
{
    [SerializeField] int totalScore = 0;

    public void CountAllScore()
    {
        totalScore = 0;
        
        for(int i = transform.childCount - 1; i >= 0; i--)
        {
            Dice child = transform.GetChild(i).GetComponent<Dice>();
            totalScore += child.currentScore;
        }
    }
}
