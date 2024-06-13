using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
    }

    public override void Interact()
    {
        if (canInteract)
        {
            canInteract = false;
            Debug.Log("Collected: " + gameObject.name, gameObject);
            Destroy(gameObject);
            // Add one to collected info ui
        }

    }
}
