using System.Runtime.CompilerServices;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ValueChange : MonoBehaviour
{
    [SerializeField] private NPC_Health player;
    public DataChangeValue changerValue;

    private void ChangeHP(NPC_Health npc)
    {
        if(transform.name == "SphereHeal")
        npc.hp = Mathf.Min(npc.hp + (changerValue.valueChange * Time.deltaTime), changerValue.maxHp);
        
        else
        {
            if(npc.hp == 0)
            Destroy(npc.gameObject);

            npc.hp -= changerValue.valueChange * Time.deltaTime;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.name == player.name)
        ChangeHP(player);
    }
}
