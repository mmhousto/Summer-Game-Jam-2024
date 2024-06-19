using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerExample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManagerScript.Instance.setGameState(GameManagerScript.GameState.StealthGame);
        Debug.Log("play stealth game BGM");

        StartCoroutine(switchBGM());
    }

    private IEnumerator switchBGM()
    {
        yield return new WaitForSeconds(3.0f);
        GameManagerScript.Instance.setGameState(GameManagerScript.GameState.CollectionGame);
        Debug.Log("play collection game BGM");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
