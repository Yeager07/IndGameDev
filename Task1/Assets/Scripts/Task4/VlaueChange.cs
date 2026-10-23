using System.Runtime.CompilerServices;
using UnityEngine;

public class VlaueChange : MonoBehaviour
{
    public DataChangeValue changerValue;
    
    private void ChangeHP(NPC_Health npc)
    {
        if(transform.name == "SphereHeal")
        npc.hp = Mathf.Min(npc.hp+changerValue.valueChange, changerValue.maxHp);
        
        else
        {
            if(npc.hp == 0)
            Destroy(npc.gameObject);

            npc.hp -= changerValue.valueChange;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        ChangeHP(other.gameObject.GetComponent<NPC_Health>());
        UnityEngine.Debug.Log("Пересечение");
    }
}
