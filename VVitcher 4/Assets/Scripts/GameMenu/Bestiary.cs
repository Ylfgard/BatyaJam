using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bestiary : MonoBehaviour
{
    [SerializeField] private GameObject bestiaryFone;

    private void Start()
    {
        CloseBestiary();   
    }
    
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
    }

    public void CloseBestiary()
    {
        GamePauser.GameContinue();
        bestiaryFone.SetActive(false);
    }
}
