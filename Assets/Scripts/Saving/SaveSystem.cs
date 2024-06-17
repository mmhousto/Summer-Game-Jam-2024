using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem
{

    /// <summary>
    /// Saves the player's data to a file in binary by serialization.
    /// </summary>
    /// <param name="player"></param>
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/playerData.dat";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerSaveData data = new PlayerSaveData(player);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Loads the player's data after deserializing it and returns it.
    /// </summary>
    /// <returns></returns>
    public static PlayerSaveData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/playerData.dat";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerSaveData data = formatter.Deserialize(stream) as PlayerSaveData;
            stream.Close();

            return data;
        }
        else
        {
            
            return null;
        }
    }
}
