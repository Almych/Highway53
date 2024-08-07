using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SliderController : MonoBehaviour
{
    [SerializeField] Slider slider;
    public event Action<float> OnSliderChanged;

    void Start()
    {
        if (PlayerPrefs.HasKey("Save"))
            slider.value = PlayerPrefs.GetFloat("Save");
    }

    // Update is called once per frame
    public void SliderSave()
    {
        PlayerPrefs.SetFloat("Save", slider.value);
        OnSliderChanged?.Invoke(slider.value);
    }
}
