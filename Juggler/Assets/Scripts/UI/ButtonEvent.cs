using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEvent : MonoBehaviour
{

    public static event Action OnClick;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(EmitClickEvent);
    }

    private void EmitClickEvent()
    {
        OnClick?.Invoke();
    }
}
