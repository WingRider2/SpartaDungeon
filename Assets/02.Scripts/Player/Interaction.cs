using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public TextMeshProUGUI promptText;
    private Camera camera;

    private bool isGrabbed;
    public GameObject grabObject;


    float initialDistance;
    float initialScale;
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {

                    curInteractGameObject = hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    SetPromptText();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                promptText.gameObject.SetActive(false);
            }
        }
        if (isGrabbed)
        {
            ObjSize();
        }
    }

    public void OnPickUp(InputAction.CallbackContext context)
    {

        if (context.phase == InputActionPhase.Performed)
        {
            if (grabObject != null && isGrabbed)
            {
                grabObject.GetComponent<IInteractable>().OnDrop();
                grabObject = null;
                isGrabbed = false;
            }
            else if (grabObject == null && !isGrabbed && curInteractable != null)
            {
                curInteractable.OnPick();
                grabObject = curInteractGameObject;
                isGrabbed = true;

                Vector3 camPos = camera.transform.position;
                initialDistance = Vector3.Distance(grabObject.transform.position, camPos);
                initialScale = grabObject.transform.localScale.x;
            }
        }

    }


    private void SetPromptText()
    {
        promptText.gameObject.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptText.gameObject.SetActive(false);
        }
    }

    public void ObjSize()
    {

        Ray ray = camera.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );
        RaycastHit hit;

        int interactableLayer = LayerMask.NameToLayer("Interactable");
        int playerLayer = LayerMask.NameToLayer("Player");
        int mask = Physics.DefaultRaycastLayers
                 & ~((1 << interactableLayer) | (1 << playerLayer));

        Collider col = grabObject.GetComponent<Collider>();
        float radius = col.bounds.extents.magnitude;

        float backgroundDistance;
        if (Physics.SphereCast(ray, radius, out hit, maxCheckDistance, mask))
        {
            backgroundDistance = hit.distance;
        }
        else backgroundDistance = maxCheckDistance;

        //initialDistance = Mathf.Min(initialDistance , backgroundDistance);

        float scaleFactor = backgroundDistance / initialDistance;


        grabObject.transform.localScale = Vector3.one * (initialScale * scaleFactor);


        Vector3 dir = (grabObject.transform.position - camera.transform.position).normalized;
        grabObject.transform.position = camera.transform.position + dir * initialDistance;
    }

}
