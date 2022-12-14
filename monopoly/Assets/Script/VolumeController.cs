using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public AudioSource source;
    public Slider volumeSlider;
    // Update is called once per frame
    void Start()
    {
        source.volume = 0.5f;
        volumeSlider.value = source.volume;
    }
    void Update()
    {
        source.volume = volumeSlider.value;
    }
}
