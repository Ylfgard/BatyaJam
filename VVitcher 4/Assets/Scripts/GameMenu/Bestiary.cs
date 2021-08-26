using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    [SerializeField] private GameObject bestiaryFone;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.V))
        {
            if(bestiaryFone.activeSelf)
                CloseBestiary();
            else
                OpenBestiary();
        }
    }

    public void OpenBestiary()
    {
        GamePauser.GamePause();
        bestiaryFone.SetActive(true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/open_book");
    }

    public void CloseBestiary()
    {
        GamePauser.GameContinue();
        bestiaryFone.SetActive(false);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/close_book");
    }
}
