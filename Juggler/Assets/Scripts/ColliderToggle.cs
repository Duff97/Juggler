using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderToggle : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private KeyCode activateKey;
    [SerializeField] private float cooldown;
    [SerializeField] private float activeTime;

    [Header("Reference")]
    [SerializeField] private Collider hitbox;
    
    private bool isActivatable;


    private void Start()
    {
        isActivatable = true;

    }  

    // Update is called once per frame
    void Update()
    {
        if (!isActivatable || !Input.GetKeyDown(activateKey)) { return; }

        isActivatable = false;
        Invoke(nameof(SetActivatable), cooldown);

        hitbox.enabled = true;

        Invoke(nameof(DisableHitbox), activeTime);

        
    }

    private void SetActivatable() { isActivatable = true; }

    private void DisableHitbox() { hitbox.enabled = false; }

    public void HandleBallThrown()
    {
        SetActivatable();
        CancelInvoke(nameof(SetActivatable));
    }
}
