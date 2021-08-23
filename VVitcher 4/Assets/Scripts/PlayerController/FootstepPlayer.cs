using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    private FMOD.Studio.EventInstance footstep;
    public int surface = 0;
    
    public void PlayFootstep()
    {
        footstep = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Footsteps");
        footstep.setParameterByName("Surface", surface);
        footstep.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        footstep.start();
        footstep.release();
    }
}
