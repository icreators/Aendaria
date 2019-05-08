using UnityEngine;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] Transform InfoItemTab;
    Transform InfoTab;

    public float radius = 3f;

    bool isFocus = false;

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
        InfoTab = Instantiate(InfoItemTab, new Vector3(transform.position.x, transform.position.y + 0.2f, transform.position.z), Quaternion.identity, transform);

        if (transform.GetComponent<ItemPickup>()) InfoTab.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Podnieś               " + transform.GetComponent<ItemPickup>().item.name;
        if (transform.GetComponent<CharracterConv>()) InfoTab.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = "Porozmawiaj z               " + transform.GetComponent<CharracterConv>().transform.name;
    }

    public void OnOut()
    {
        Destroy(InfoTab.gameObject);
    }

    public void OnClick(Transform playerTransform)
    {
        float distance = Vector3.Distance(playerTransform.position, transform.position);
        if (distance <= radius)
        {
            Interact();
        }
    }
}
