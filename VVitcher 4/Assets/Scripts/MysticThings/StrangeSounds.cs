using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeSounds : MysticThings
{
    private FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef] [SerializeField] private string soundPath;
    [SerializeField] private bool autoPlay;
    [SerializeField] private float minSoundDelay, maxSoundDelay;

    private void Start()
    {
        if(autoPlay) StartCoroutine(SoundDelay());
    }

    public override void StartMystic()
    {
        if(autoPlay)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(soundPath, this.gameObject);
        }
        else
        {
            instance = FMODUnity.RuntimeManager.CreateInstance(soundPath);
            instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
            instance.start();
        }
    }

    public override void EndMystic()
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
    }

    IEnumerator SoundDelay()
    {
        yield return new WaitForSeconds(Random.Range(minSoundDelay, maxSoundDelay));
        StartMystic();
        StartCoroutine(SoundDelay());
    }
}
