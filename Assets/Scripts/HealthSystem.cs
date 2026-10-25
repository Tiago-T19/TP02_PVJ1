using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{

    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;

    public float MaxHealth => maxHealth;
    public float CurrentHealth => currentHealth;//usamos getter y setter para el gameManager - solo lectura

    [SerializeField] private bool isDead;
    public bool IsDead
    {
        get => isDead;

    }

    // [SerializeField] private GameObject vfx;

    private Rigidbody rb;
    private Collider col;
    private Attack attack;
    private MeshRenderer mesh;
    // Barra de vida
    private Coroutine flashCoroutine;
    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;

        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        attack = GetComponent<Attack>();
        mesh = GetComponentInChildren<MeshRenderer>();
    }
    public void TakeDamage(float amount)
    {
        IEnumerator DamageFlash()
        {
            mesh.enabled = false;

            yield return new WaitForSeconds(0.1f);

            mesh.enabled = true;

            yield return new WaitForSeconds(0.1f);

            mesh.enabled = false;

            yield return new WaitForSeconds(0.1f);

            mesh.enabled = true;
        }


        if (isDead) return;
        currentHealth -= amount;
        //clamp permite normalizar un valor de acuerdo a los limites
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

       

        //Debug.Log($"Salud actual: " + currentHealth);
        Debug.Log($"VIDA: {currentHealth}/{maxHealth}");

        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        flashCoroutine = StartCoroutine(DamageFlash());

        if (currentHealth <= 0f)
        {
            Dead();
        }
    }

    // Power Up de mascara de gas
    public void Heal(float amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log($"Curado. Vida actual: {currentHealth}");
    }

    // Incrementa la vida maxima y cura al personaje con la nueva vida extra    
    public void IncreaseMaxHealth(float extraHealth)
    {
        maxHealth += extraHealth;

        // También cura la nueva vida extra
        currentHealth = maxHealth;

        Debug.Log($"Vida máxima aumentada a {maxHealth}");
    }

    // vuelve a la vida normal
    public void RestoreMaxHealth(float originalMax)
    {
        maxHealth = originalMax;

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);

        Debug.Log($"Vida máxima restaurada a {maxHealth}");
    }

    private void Dead()
    {
        Debug.Log("ENTRO A DEAD");

        isDead = true;

        Destroy(gameObject);
    
        //agregar condicional comparando tags player y character, notificar al gameManager

        if (gameObject.CompareTag("Player"))
        {
            GameManager.Instance.NotifyPlayerDead();
        }
        else if (gameObject.CompareTag("Character"))
        {
            GameManager.Instance.NotifyEnemyDefeat();
        }

       


        Debug.Log("Character eliminado");
        // Notificación al GameManager si existiera 
        //GameManager.Instance.ChangeState(GameState.GameOver);
        //Destroy object
        //efectos
        //animaciones
    }

    IEnumerator DestroyTransition()
    {
        if (col != null)
            col.enabled = false;

        if (rb != null)
            rb.isKinematic = true;

        if (mesh != null)
            mesh.enabled = false;

        if (attack != null)
            attack.enabled = false;

        Debug.Log("Iniciando destruccion");

        yield return new WaitForSeconds(1f);

        Debug.Log("Destruyendo objeto");

        Destroy(gameObject);
    }
    //crear corrutina deshabilitando componentes y destruyendo el objeto

}
