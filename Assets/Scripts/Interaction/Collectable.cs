// Morgan Houston
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Interactable
{
    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
    }

    #endregion

    #region Methods

    public override void Interact(Transform transform)
    {
        if (canInteract)
        {
            canInteract = false;
            Debug.Log("Collected: " + gameObject.name, gameObject);
            Destroy(gameObject);
            // Add one to collected info ui
        }

    }

    #endregion
}
