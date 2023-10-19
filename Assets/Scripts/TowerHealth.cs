using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TowerHealth : MonoBehaviour
{
    int maxHealth = 3;
    AudioSource audioSource;
    [SerializeField] AudioClip hoverClip;
    [SerializeField] AudioClip clickClip;
    [SerializeField] AudioClip denyClip;
    [SerializeField] int currentHealth;
    [SerializeField] float loadDelay = 0.5f;
    public int CurrentHealth { get { return currentHealth; } }

    private string currentSceneName;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        currentSceneName = SceneManager.GetActiveScene().name;
    }

    private void Update()
    {
        if (currentSceneName == "Game" && Input.GetKeyDown(KeyCode.Escape))
        {
            StartCoroutine(Delay("GameOver", .5f));
        }
    }

    public void Quit()
    {
        Debug.Log("Nooooo T_T");
        Application.Quit();
    }

    private void Start()
    {
        ResetToMaxHealth();
    }

    public void ResetToMaxHealth()
    {
        currentHealth = maxHealth;
    }

    public void LoadGame()
    {
        FindObjectOfType<ScoreKeeper>().ResetScore();
        ResetToMaxHealth();
        StartCoroutine(Delay("Game", loadDelay));
    }

    IEnumerator Delay(string name, float delay)
    {
        yield return new WaitForSecondsRealtime(delay);
        SceneManager.LoadScene(name);
    }

    public void LoadMainMenu()
    {
        StartCoroutine(Delay("MainMenu", loadDelay));
    }

    public void LoadGameOver()
    {
        StartCoroutine(Delay("GameOver", 2f));
    }

    public void Hurt()
    {
        currentHealth--;
        FindObjectOfType<DisplayUI>().UpdateUI();
        ProcessGameOver();
    }

    public void ProcessGameOver()
    {
        if (currentHealth <= 0)
        {
            LoadGameOver();
        }
    }

    public void OnMouseHover()
    {
        audioSource.PlayOneShot(hoverClip, 0.5f);
    }

    public void OnMouseClickMenu()
    {
        audioSource.PlayOneShot(clickClip, 0.5f);
    }
}
