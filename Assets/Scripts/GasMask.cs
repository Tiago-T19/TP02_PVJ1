using System.Collections;
using UnityEngine;

public class PowerUpMascara : MonoBehaviour
{
    [Header("Mask Settings")]
    [SerializeField] private float extraHealth = 200f;
    [SerializeField] private float duration = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem health =
                other.GetComponent<HealthSystem>();

            if (health != null)
            {
                StartCoroutine(ApplyMaskEffect(health));
            }

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

    private IEnumerator ApplyMaskEffect(HealthSystem health)
    {
        float originalMaxHealth = health.MaxHealth;

        health.IncreaseMaxHealth(extraHealth);

        Debug.Log("Mascara activada");

        yield return new WaitForSeconds(duration);

        health.RestoreMaxHealth(originalMaxHealth);

        Debug.Log("Mascara terminada");

        Destroy(gameObject);
    }
}
