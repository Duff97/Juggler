using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    [SerializeField] float moveVelocity;
    [SerializeField] float openedDistance;

    private Rigidbody rb;

    private bool isOpening;
    private bool isClosing;

    public static event Action OnOpened;
    public static event Action OnClosed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Open()
    {
        isOpening = true;
        isClosing = false;

        rb.velocity = new Vector3(moveVelocity * -1, 0, 0);
    }

    public void Close()
    {
        isClosing = true;
        isOpening = false;

        rb.velocity = new Vector3(moveVelocity, 0, 0);
    }

    private void FixedUpdate()
    {
        if (!isClosing && !isOpening) return;

        if (isClosing ? IsClosed() : IsOpened())
        {
            isClosing = false;
            isOpening = false;
            rb.velocity = Vector3.zero;
        }
    }

    private bool IsClosed() 
    {
        if (transform.position.x < 0) return false;

        OnClosed?.Invoke();
        return true;
    }

    private bool IsOpened()
    {
        if (transform.position.x > openedDistance) return false;

        OnOpened?.Invoke();
        return true;
    }
}
