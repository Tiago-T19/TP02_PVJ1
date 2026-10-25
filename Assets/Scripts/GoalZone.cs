using UnityEngine;

public class GoalZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Verifica que quien entró sea el jugador
        if (other.CompareTag("Player"))
        {
            Debug.Log("META ALCANZADA");

            GameManager.Instance.NotifyGoalZone();
        }
    }
}
