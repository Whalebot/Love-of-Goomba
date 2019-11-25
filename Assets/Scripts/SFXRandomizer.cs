using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXRandomizer : MonoBehaviour
{
    [SerializeField]
    AudioClip[] audioClips;
    [SerializeField] float pitchRange = 0.1F;
    [SerializeField] [Range(0, 1)] float minVol = 0.8F;
    [SerializeField] [Range(0, 1)] float maxVol = 1F;

    AudioSource AS;

    // Start is called before the first frame update
    void Start()
    {
        AS = GetComponent<AudioSource>();

        if(audioClips.Length > 0)
        AS.clip = audioClips[Random.Range(0, audioClips.Length)];

        AS.pitch = Random.Range(1 - pitchRange, 1 + pitchRange);
        AS.volume = Random.Range(minVol, maxVol);
        AS.Play();
    }

}
