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
    }

    public void tmep()
    {
        //물체의 화면상 크기
        //내가 정면을 향해 ray를 날려서
        //최대 ray는 정해져있고
        //ray를통해 거리룰 구하고
        //구한값을 통해 오브젝트 크기 조절을 한다.
    }
    public void OnPickUp(InputAction.CallbackContext context)
    {
      
        if (context.phase == InputActionPhase.Started)
        {
            if (grabObject != null && isGrabbed)
            {
                grabObject.GetComponent<IInteractable>().OnDrop();
                grabObject = null;
                isGrabbed = false;
            }
            else if(grabObject == null && !isGrabbed && curInteractable != null)
            {
                curInteractable.OnPick();
                grabObject = curInteractGameObject;
                isGrabbed = true;
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
}
