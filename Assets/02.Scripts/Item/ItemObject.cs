﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour , IInteractable
{
    public ItemData itemData;

    public Transform handHoldPoint;
    bool isGrabbed = false;

    private void Start()
    {
        handHoldPoint = PlayerManager.Instance.Player.controller.handHoldPoint;
    }
    public string GetInteractPrompt()
    {
        string str = $"{itemData.displayName}\n{itemData.description}";
        return str;
    }

    public void OnInteract()
    {
        PlayerManager.Instance.Player.itemData = itemData;
        PlayerManager.Instance.Player.addItem?.Invoke();
        Destroy(gameObject);
    }
    public void OnPick()
    {
        isGrabbed = true;

        // 물리 해제
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = true;
            rb.detectCollisions = false;
        }
        if (TryGetComponent<Collider>(out var col))
            col.enabled = false;

        // 손 위치에 붙이기
        transform.SetParent(handHoldPoint);
    }

    public void OnDrop()
    {
        isGrabbed = true;
        // 물리 해제
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.isKinematic = false;
            rb.detectCollisions = true;
        }
        if (TryGetComponent<Collider>(out var col))
            col.enabled = true;

        transform.SetParent(null , worldPositionStays : true);
    }
}
