using UnityEngine;

[CreateAssetMenu(fileName = "New ValueChangerData", menuName = "Task4/ValueChangerData")]
public class DataChangeValue : ScriptableObject
{
    [SerializeField] public float valueChange = 1.0f;
    [SerializeField] public float maxHp = 100.0f;
}
