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

    // Update is called once per frame
    void Update()
    {
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