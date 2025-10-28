using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footStep : MonoBehaviour
{
    public void FootStepSound()
    {
       SoundManager.instance.sfxSource.PlayOneShot(SoundManager.instance.walk, SoundManager.instance.sfxVolume);
    }
}

