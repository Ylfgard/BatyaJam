using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrangeSounds : MysticThings
{
    private FMOD.Studio.EventInstance instance;
    [FMODUnity.EventRef] [SerializeField] private string soundPath;
    [SerializeField] private bool autoPlay, autoStop;
    [SerializeField] private float minSoundDelay, maxSoundDelay;

    private void Start()
    {
        if(autoPlay) StartCoroutine(SoundDelay());
    }

    public override void StartMystic()
    {
        instance = FMODUnity.RuntimeManager.CreateInstance(soundPath);
        instance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
        instance.start();
    }

    private void OnDestroy() 
    {
        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
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
        if(autoStop)
        {
            yield return new WaitForSeconds(5);
            EndMystic();
        }
        StartCoroutine(SoundDelay());
    }
}
