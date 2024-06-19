using System;

[Serializable]
public class PlayerSaveData
{
    public bool scavengerRespect;
    public bool magiciansRespect;
    public bool thievesRespect;

    public int lastLocation;

    public PlayerSaveData(Player player)
    {
        scavengerRespect = player.scavengerRespect;
        magiciansRespect = player.magiciansRespect;
        thievesRespect = player.thievesRespect;
        lastLocation = player.lastLocation;

    }
}
