using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersHealthScript : MonoBehaviour
{
    public int playersHealthScript = 100;

    void Start()
    {
        
    }

    public void DamagePlayer(int damageToPlayer)
    {
        playersHealthScript -= damageToPlayer;
    }
}
