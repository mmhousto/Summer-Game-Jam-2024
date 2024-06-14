using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region Fields

    private StarterAssetsInputs inputs;
    public Transform playerCamRoot;

    private bool isInteracting;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if(inputs != null && inputs.interacting && isInteracting == false)
        {
            isInteracting = true;
            CheckIfInteracting();
        }
    }

    #endregion

    #region Methods

    void CheckIfInteracting()
    {
        Ray ray = new Ray(playerCamRoot.position, playerCamRoot.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                interactable.Interact(gameObject.transform);
            }
        }

        isInteracting = false;
    }

    #endregion

}
