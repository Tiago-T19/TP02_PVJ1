using UnityEngine;

public class PowerUpInvisibilidad : MonoBehaviour
{
    internal bool IsInvisible;
    [SerializeField] private float duracion = 15f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement player =
                other.GetComponent<PlayerMovement>();

            if (player != null)
            {
                player.ActivarInvisibilidad(duracion);
            }

            Destroy(gameObject);
        }
    }
}