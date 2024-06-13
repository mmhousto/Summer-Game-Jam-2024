using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{

    protected bool canInteract;

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Interact()
    {
        if(canInteract)
        {
            canInteract = false;
            Debug.Log("Interacted with: " + gameObject.name, gameObject);
            StartCoroutine(EndInteract());
        }
        
    }

    public bool CanInteract()
    {
        return canInteract;
    }

    protected IEnumerator EndInteract()
    {
        yield return new WaitForSeconds(1);

        canInteract = true;
    }

}
