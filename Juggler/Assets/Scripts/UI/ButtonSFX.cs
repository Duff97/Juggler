using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSFX : MonoBehaviour
{
    private AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GetComponent<AudioSource>();
        ButtonEvent.OnClick += HandleButtonClick;
    }
    private void OnDestroy()
    {
        ButtonEvent.OnClick -= HandleButtonClick;
    }

    private void HandleButtonClick()
    {
        sfx.Play();
    }
}
