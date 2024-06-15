// Morgan Houston
using System.Collections;
using UnityEngine;

public class Interactable : MonoBehaviour, IInteractable
{
    #region Fields

    protected bool canInteract;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
    }

    #endregion

    #region Methods

    public virtual void Interact()
    {
        if (!canInteract) return;
        canInteract = false;
        Debug.Log("Interacted with: " + gameObject.name, gameObject);
        StartCoroutine(EndInteract());

    }

    public virtual void Interact(Transform interactedTarget)
    {
        if (!canInteract) return;
        canInteract = false;
        Debug.Log("Interacted with: " + gameObject.name, gameObject);
        StartCoroutine(EndInteract());

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

    #endregion
}
