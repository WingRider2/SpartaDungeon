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

    void DrawDebugSphere(Vector3 center, float r, Color col, float duration)
    {
        const int segments = 12;
        float deltaTheta = 360f / segments;
        Vector3 prevPoint = center + (Vector3.up * r);
        for (int i = 1; i <= segments; i++)
        {
            float theta = deltaTheta * i * Mathf.Deg2Rad;
            Vector3 nextPoint = center + new Vector3(Mathf.Sin(theta) * r, Mathf.Cos(theta) * r, 0);
            Debug.DrawLine(prevPoint, nextPoint, col, duration);
            prevPoint = nextPoint;
        }
        // XZ 평면에도 하나 더 그리려면 반복문 복사 후 Y↔Z 교체
    }
    public void ObjSize()
    {

        Ray ray = camera.ScreenPointToRay(
            (grabObject.transform.position - this.transform.position).normalized
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
            // 충돌 발생 시: origin→충돌지점 라인 빨강
            Debug.DrawLine(ray.origin, hit.point, Color.red, 0.1f);
            // 충돌 지점에 녹색 구 그리기
            DrawDebugSphere(hit.point, radius, Color.green, 0.1f);

            // hit.distance 는 “ray 시작점에서
            // sphere가 닿기까지” 거리
            backgroundDistance = hit.distance;
        }
        else backgroundDistance = maxCheckDistance;

        //initialDistance = Mathf.Min(initialDistance , backgroundDistance);

        float scaleFactor = backgroundDistance / initialDistance;
        grabObject.transform.localScale = Vector3.one * (initialScale * scaleFactor);


        Vector3 dir = (grabObject.transform.position - camera.transform.position).normalized;
        grabObject.transform.position
            = camera.transform.position
            + dir * backgroundDistance;
    }

}
