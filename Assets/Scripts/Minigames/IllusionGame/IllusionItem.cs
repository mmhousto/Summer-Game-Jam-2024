using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllusionItem : Interactable
{
    private IllusionGameManager illusionGameManager;
    private HealthManager healthManager;
    public bool correctItem = false;

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        canInteract = true;
        illusionGameManager = GameObject.Find("Illusion Manager").GetComponent<IllusionGameManager>();
        healthManager = GameObject.Find("HealthManager").GetComponent <HealthManager>();
        gameObject.SetActive(false);
    }

    #endregion

    #region Methods

    public override void Interact(Transform transform)
    {
        if (canInteract)
        {
            canInteract = false;
            if (correctItem)
            {
                SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.illusionWin);
                illusionGameManager.WonGame();
            }
            else
            {
                SoundManagerScript.Instance.PlaySFXSound(SoundManagerScript.Instance.illusionDeath);
                illusionGameManager.SetObjects();
                healthManager.TakeDamage();
            }
                
        }

    }

    public void EnableInteract()
    {
        canInteract = true;
    }

    #endregion
}
