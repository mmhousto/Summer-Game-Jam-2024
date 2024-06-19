// Morgan Houston
using StarterAssets;
using UnityEngine;
using UnityEngine.UI;

public class Interact : MonoBehaviour
{
    #region Fields
    public Transform playerCamRoot;
    public GameObject contextPrompt;
    public Sprite[] contextPromptImages;

    private StarterAssetsInputs inputs;
    private AnimationPlayerManager animManager;
    private Image contextImage;
    private Animator contextAnim;
    private bool isInteracting;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        inputs = GetComponent<StarterAssetsInputs>();
        if(GetComponent<AnimationPlayerManager>() != null)
            animManager = GetComponent<AnimationPlayerManager>();
        contextImage = contextPrompt.GetComponent<Image>();
        contextAnim = contextPrompt.GetComponent<Animator>();
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
                if (interactable.CanInteract() == false)
                {
                    contextPrompt.SetActive(false);
                    return;
                }

                if(contextPrompt != null && !contextPrompt.activeInHierarchy)
                {
                    contextPrompt.SetActive(true);
                    switch (interactable.tag)
                    {
                        case "Grab":
                            contextAnim.SetFloat("State", 0);
                            break;
                        case "Talk":
                            contextAnim.SetFloat("State", 1);
                            break;
                        case "Quest":
                            contextAnim.SetFloat("State", 2);
                            break;
                        default:

                            break;
                    }
                }
                    
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
