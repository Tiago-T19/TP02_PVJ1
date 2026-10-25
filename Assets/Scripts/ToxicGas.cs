using UnityEngine;

public class ToxicGas : MonoBehaviour
{
    [Header("GasToxico")]
    [SerializeField] private float damage = 10f; // Daño
    [SerializeField] private float damageInterval; // Intervalo de daño
    [SerializeField] private ParticleSystem[] gas; // Gas
    private float timer;
    private HealthSystem health;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        health = other.gameObject.GetComponent<HealthSystem>();
        timer = 0f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        health = other.gameObject.GetComponent<HealthSystem>();
        if (health == null) return;

        timer += Time.deltaTime;
        if (timer >= damageInterval)
        {
            health.TakeDamage(damage);
            timer = 0f;
        }
        foreach (var g in gas)
        {
            g.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Player")) return;
        health = null;
        timer = 0f;
        foreach (var g in gas)
        {
            g.Stop();
        }
    }
}
