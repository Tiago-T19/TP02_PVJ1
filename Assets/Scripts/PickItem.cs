using UnityEngine;

public class PickItem : MonoBehaviour
{
    [Header("Referencia a la mano del jugador")]
    [SerializeField] private Transform hand;

    private GameObject currentItem = null;

    private void OnTriggerStay(Collider other)
    {
        // Solo recoge objetos con Tag Item
        if (other.CompareTag("Item") && currentItem == null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pick(other.gameObject);
            }
        }
    }

    private void Pick(GameObject item)
    {
        currentItem = item;

        // Equipar objeto en la mano
        item.transform.SetParent(hand);
        item.transform.localPosition = Vector3.zero;
        item.transform.localRotation = Quaternion.identity;

        // Desactivar físicas
        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.linearVelocity = Vector3.zero;
        }

        // Desactivar collider para evitar conflictos
        Collider col = item.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        // Si es un arma, activar el disparo
        GunController gun = item.GetComponent<GunController>();
        if (gun != null)
        {
            gun.enabled = true;
        }

        Debug.Log("Arma recogida: " + item.name);
    }


   /* public bool HasWeapon()
    {
        if (currentItem == null)
            return false;

        return currentItem.GetComponent<GunController>() != null;
    } 
    */

    public GameObject DropItem()
    {
        if (currentItem == null)
            return null;

        GameObject temp = currentItem;

        temp.transform.SetParent(null);

        Rigidbody rb = temp.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        Collider col = temp.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

        currentItem = null;

        return temp;
    }

    public bool HasItem()
    {
        return currentItem != null;
    }
}