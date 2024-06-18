using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionStart : Interactable
{
    IllusionGameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("Illusion Manager").GetComponent<IllusionGameManager>();
        canInteract = gameManager.IsGameActive() ? false : true;
    }

    private void Update()
    {
        if (canInteract != !gameManager.IsGameActive()) canInteract = gameManager.IsGameActive() ? false : true;
    }

    public override void Interact(Transform interactedTarget)
    {
        if (!canInteract) return;
        gameManager.StartGame();
    }
}
