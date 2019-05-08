using UnityEngine;

public class IOS : MonoBehaviour //IOS - Interactable Objects Scanning 
{
    [SerializeField] Camera cam;

    private bool look = false;

    private Collider currentObject;

    void Update()
    { 
        RaycastHit hit;

        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), out hit, 5, 9))
        {
            if (!look)
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                currentObject = hit.collider;

                if (interactable != null)
                {
                    interactable.OnFocus();

                    look = true;
                }
            }
            else
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();

                if (Input.GetMouseButtonDown(0))
                {
                    look = false;

                    interactable.OnClick(transform);
                }
                
                if (interactable == null || hit.collider.name != currentObject.name)
                {
                    currentObject.GetComponent<Interactable>().OnOut();

                    look = false;
                }
            }
        }
    }
}
