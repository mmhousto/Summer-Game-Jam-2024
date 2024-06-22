using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThroneManager : MonoBehaviour
{
    public GameObject thiefLord, magicianLord, scavengerLord;

    // Start is called before the first frame update
    void Start()
    {
        if(Player.Instance != null)
        {
            Player player = Player.Instance;
            if (player.scavengerRespect == true) scavengerLord.SetActive(true);
            else scavengerLord.SetActive(false);

            if(player.thievesRespect == true) thiefLord.SetActive(true);
            else thiefLord.SetActive(false);

            if(player.magiciansRespect == true) magicianLord.SetActive(true);
            else magicianLord.SetActive(false);
        }
        
    }

}
