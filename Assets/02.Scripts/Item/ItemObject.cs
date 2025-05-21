using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour , IInteractable
{
    public ItemData itemData;

    public Transform handHoldPoint;
    bool isGrabbed = false;

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
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }
}
