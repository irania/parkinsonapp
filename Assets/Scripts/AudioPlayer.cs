using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioClip AudioClip;
    private AudioSource AudioSource;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource = GameObject.FindObjectOfType<AudioSource>();
    }

    // Update is called once per frame
    public void OnClick()
    {
        AudioSource.clip = AudioClip;
        AudioSource.Play();
    }
}
