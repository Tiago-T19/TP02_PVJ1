using UnityEngine;
using static UnityEngine.UI.Image;

public class GunController : MonoBehaviour
{
    [Header("Gun Settings")]
    [SerializeField] private float damage = 50f;
    [SerializeField] private float range = 50f;

    [Header("Ammo")]
    [SerializeField] private int maxAmmo = 20;
    private int currentAmmo;

    [Header("References")]
    [SerializeField] private Camera playerCamera;

    private void Start()
    {
        currentAmmo = maxAmmo;
        enabled = false;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    private void OnDrawGizmos()
    {
        if (playerCamera == null)
            return;

        Gizmos.color = Color.red;

        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f)
        );

        Gizmos.DrawRay(ray.origin, ray.direction * range);
    }

    private void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("Sin munición");
            return;
        }

        currentAmmo--;

        Debug.Log($"Disparo realizado. Balas restantes: {currentAmmo}");

        Ray ray = playerCamera.ScreenPointToRay(
            new Vector3(Screen.width / 2f, Screen.height / 2f)
        );

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("Impactó en: " + hit.collider.name);

            HealthSystem targetHealth =
                hit.collider.GetComponent<HealthSystem>();

            if (targetHealth != null)
            {
                targetHealth.TakeDamage(damage);
            }
        }
    }

    public int GetAmmo()
    {
        return currentAmmo;
    }
    
}