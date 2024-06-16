// Morgan Houston
using StarterAssets;
using UnityEngine;

public class Interact : MonoBehaviour
{
    #region Fields
    public Transform playerCamRoot;
    public GameObject contextPrompt;

    private StarterAssetsInputs inputs;
    private AnimationPlayerManager animManager;
    private bool isInteracting;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        if(GetComponent<AnimationPlayerManager>() != null)
            animManager = GetComponent<AnimationPlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfInteractable();

        if(inputs != null && inputs.interacting && isInteracting == false)
        {
            isInteracting = true;
            inputs.interacting = false;
        }
        else if (inputs != null && !inputs.interacting && isInteracting == true)
            isInteracting = false;
            
    }

    #endregion

    #region Methods

    void CheckIfInteractable()
    {
        Ray ray = new Ray(playerCamRoot.position, playerCamRoot.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 5))
        {
            if (hit.collider.TryGetComponent(out Interactable interactable))
            {
                if(contextPrompt != null && !contextPrompt.activeInHierarchy)
                    contextPrompt.SetActive(true);
                
                if (isInteracting == true && animManager != null) animManager.PlayInteractAnimation(); // Play Interaction animation

                if(isInteracting == true)
                    interactable.Interact(gameObject.transform);
                
            }
            else if(contextPrompt != null && contextPrompt.activeInHierarchy)
            {
                contextPrompt.SetActive(false);
            }
        }
        else if (contextPrompt != null && contextPrompt.activeInHierarchy)
        {
            contextPrompt.SetActive(false);
        }
    }

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
