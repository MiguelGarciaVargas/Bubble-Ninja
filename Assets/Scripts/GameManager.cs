using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TMP_Text scoreText;
    public Image fadeImage;
    private Blade blade;
    private Spawner spawner;

    private int score;

    public HealthManager healthManager;

    public AudioClip explosionSound; 
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spawner = FindFirstObjectByType <Spawner>();
        blade = FindFirstObjectByType <Blade>();
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        //Score
        score = 0;
        scoreText.text = score.ToString();
        //Fade
        Time.timeScale = 1f;
        //Objects
        blade.enabled = true;
        spawner.enabled = true;
        //Health
        healthManager.ResetHealth();

        ClearScene();
    }

    private void ClearScene()
    {
        Fruit[] fruits = FindObjectsByType<Fruit>(FindObjectsSortMode.None);

        foreach (Fruit f in fruits)
        {
            Destroy(f.gameObject);
        }

        Bomb[] bombs = FindObjectsByType<Bomb>(FindObjectsSortMode.None);

        foreach (Bomb b in bombs)
        {
            Destroy(b.gameObject);
        }
    }
    public void IncreaseScore(int points)
    {
        score += points;
        scoreText.text = score.ToString();
    }

    //EndGame
    public void LoseLife()
    {

        if (healthManager.LoseLife() <= 0)
        {
            Explode();
        }
    }

    public void Explode() 
    {
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }

        blade.enabled = false;
        spawner.enabled = false;

        StartCoroutine(ExplodeSequence());  
    }

    private IEnumerator ExplodeSequence()
    {
        float elapsed = 0f;
        float duration = 1f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);   
            fadeImage.color = Color.Lerp(Color.clear, Color.black, t);
            Time.timeScale = 1f - t;
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }

        yield return new WaitForSecondsRealtime(1f);

        NewGame();

        elapsed = 0f;

        while (elapsed < duration)
        {
            float t = Mathf.Clamp01(elapsed / duration);
            fadeImage.color = Color.Lerp(Color.black, Color.clear, t);
            elapsed += Time.unscaledDeltaTime;

            yield return null;
        }
    }
}
