using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {

    //Custom
    [HideInInspector] public AudioSource audioTakeItem;
    public AudioClip clipTakeItem;

    [HideInInspector] public AudioSource audioSuccess;
    public AudioClip clipSuccess;

    [HideInInspector] public AudioSource audioWalking;
    public AudioClip clipWalking;

    [HideInInspector] public AudioSource audioLockedDoor;
    public AudioClip clipLockedDoor;

    // Use this for initialization
    void Awake ()
    {
        //Custom
        audioTakeItem = AddAudio(clipTakeItem, false, false);
        audioSuccess = AddAudio(clipSuccess, false, false);
        audioWalking = AddAudio(clipWalking, false, false);
        audioLockedDoor = AddAudio(clipLockedDoor, false, false);
    }

	void Start()
	{

	}

    public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>() as AudioSource;
        newAudio.clip = clip;
        newAudio.loop = loop;
        newAudio.playOnAwake = playAwake;
        return newAudio;
    }
}
