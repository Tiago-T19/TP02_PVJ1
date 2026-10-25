using System;
using Unity.VisualScripting;
using UnityEngine;

public enum GameState
{
    Start,
    Playing,
    Paused,
    Victory,
    GameOver,
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    //static

    //settings
    [Header("State Settings")]
    [SerializeField] private GameState currentState;

    [Header("Victory Settings")]
    [SerializeField] private EnemyAI[] enemies;
    [SerializeField] private int totalEnemies;
    [SerializeField] private int enemiesRemaining;
    //Puntaje
    [Header("Score")]
    [SerializeField] private int score;

    //singleton
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);//UI y escenas
    }

        //estados iniciales
        //EnemyAI[] enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);
     void Start()
    {
        // Busca automáticamente todos los enemigos de la escena
        enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);

        totalEnemies = enemies.Length;
        enemiesRemaining = totalEnemies;

        score = 0;

        Debug.Log($"Enemigos encontrados: {totalEnemies}");
        Debug.Log($"PUNTAJE: {score}");

        ChangeState(GameState.Playing);
    }
    void Update()
    {
        //pausa inputs
        if (Input.GetKeyDown(KeyCode.P))
        {
            Paused();
        }
    }


    //switch estados
    public void ChangeState(GameState newState)
    {
        currentState = newState;
        switch (currentState)
        {
            case GameState.Start:
                Time.timeScale = 1f;//0f - 1f
                Debug.Log("Inicio el juego");
                break;
            case GameState.Playing:
                Time.timeScale = 1f;
                Debug.Log("<color=green>ESTADO: jugando</color>");
                break;
            case GameState.Paused:
                Time.timeScale = 0f;
                Debug.Log("<color=yellow>ESTADO: pausa</color>");
                break;
            case GameState.Victory:
                Time.timeScale = 0f;
                //corrutina para detener el tiempo
                Debug.Log("<color=cyan>ESTADO: Victoria</color>");
                break;
            case GameState.GameOver:
                Time.timeScale = 0f;
                Debug.Log("<color=red>ESTADO: Game Over</color>");
                break;
        }
    }

    //pausa
    public void Paused()
    {
        if (currentState == GameState.Playing)
        {
            ChangeState(GameState.Paused);
        }
        else if (currentState == GameState.Paused)
        {
            ChangeState(GameState.Playing);
        }
    }

    public void AddScore(int points)
    {
        score += points;

        Debug.Log($"<color=yellow>PUNTAJE: {score}</color>");
    }

    //notificaciones
    public void NotifyPlayerDead()
    {
        if (currentState != GameState.Playing) return;

        ChangeState(GameState.GameOver);
    }

    public void NotifyEnemyDefeat()
    {
        if (currentState != GameState.Playing) return;

        enemiesRemaining--;

        score += 100;

        Debug.Log($"PUNTAJE ACTUAL: {score}");
        Debug.Log($"Enemigos restantes: {enemiesRemaining}");
    }

    public void NotifyGoalZone()
    {
        if (currentState != GameState.Playing) return;

        ChangeState(GameState.Victory);
    }

}