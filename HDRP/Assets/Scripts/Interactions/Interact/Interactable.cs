using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] Transform InfoItemTab;
    Transform InfoTab;

    public float radius = 5f;

    Transform playerPosition;

    bool isFocus = false;

    private void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
    }

    public virtual void Interact()
    {
        OnOut();

        Debug.Log("INTERACT with " + transform.name);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public void OnFocus()
    {
        // Funkcja do refaktoryzacji
        InfoTab = Instantiate(InfoItemTab, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity, transform);

        Transform infoTab = InfoTab.transform.GetChild(0).transform.GetChild(0);

        if (transform.GetComponent<ItemPickup>())
        {
            infoTab.GetChild(0).GetComponent<Text>().text = transform.GetComponent<ItemPickup>().item.name;
            infoTab.GetChild(1).GetComponent<Text>().text = transform.GetComponent<ItemPickup>().item.description;
        }
        else
        {
            infoTab.GetChild(1).GetComponent<Text>().text = "Porozmawiaj z " + transform.GetComponent<CharracterConv>().transform.name;
        }
    }

    public void OnLostFocus()
    {
        OnOut();
    }

    public void OnOut()
    {
        Destroy(InfoTab.gameObject);
    }

    public void OnClick(Transform playerTransform)
    {
        float distance = Vector3.Distance(playerPosition.position, transform.position);
        if (distance <= radius)
        {
            Interact();
        }
    }
}
