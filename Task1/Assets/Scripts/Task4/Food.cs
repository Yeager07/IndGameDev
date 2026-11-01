using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private NPC_Health player;
    [SerializeField] bool toxic;
    public DataChangeValue changerValue;

    private void OnTriggerEnter(Collider other)
    {
        if(other.name == player.name)
        ChangeHP(player);
    }

    private void ChangeHP(NPC_Health npc)
    {
        if(!toxic)
        {
            npc.hp = Mathf.Min(npc.hp + changerValue.hungerRecover, changerValue.maxHp);
            npc.hunger = MathF.Max(npc.hunger - changerValue.hungerRecover, 0.0f);
            Destroy(this.gameObject);
        }
        
        else
        {
            StartCoroutine(player.ToxicFoodDamageCoroutine());
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            //Destroy(this.gameObject);
        }
    }
}
