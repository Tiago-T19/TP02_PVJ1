using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image rellenoBarraVida;
    [SerializeField] private TMP_Text textoVida;

    private HealthSystem healthSystem;
   

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = player.GetComponent<HealthSystem>();
    }

    void Update()
    {
        if (healthSystem == null)
            return;

        rellenoBarraVida.fillAmount =
            healthSystem.CurrentHealth / healthSystem.MaxHealth;

        textoVida.text =
            $"{healthSystem.CurrentHealth:0}/{healthSystem.MaxHealth:0}";
    }
}