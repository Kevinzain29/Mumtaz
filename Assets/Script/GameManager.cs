using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections; // Wajib untuk Coroutine

public class GameManager : MonoBehaviour
{
    public Player player;

    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI highScoreText; // UI untuk High Score

    public TextMeshProUGUI countdownText; //hitung mundur

    public GameObject playButton;

    public GameObject gameOver;

    private int score;

    public BackgroundParallax backgroundScript;

    private void Awake()
    {

        Pause();

        UpdateHighScoreUI(); // Menampilkan skor tertinggi saat startup

    }

    public void Play()
    {
        // Memulai Coroutine hitung mundur
        StartCoroutine(StartCountdown());
    }

    // Coroutine
    private IEnumerator StartCountdown()
    {
        score = 0;
        scoreText.text = score.ToString();

        //Untuk Mengembalikan background ke siang
        if (backgroundScript != null)
        {
            backgroundScript.ResetBackground();
        }

        playButton.SetActive(false);
        gameOver.SetActive(false);

        Time.timeScale = 0f;
        player.enabled = false; // Player diam saat countdown

        // Menghapus pipa-pipa yang masih ada
        Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        // Proses Hitung Mundur (Kriteria g: Coroutine)
        int count = 3;
        while (count > 0)
        {
            countdownText.text = count.ToString();
            yield return new WaitForSecondsRealtime(1f);
            count--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSecondsRealtime(0.5f);
        countdownText.text = "";

        // Mulai Game
        Time.timeScale = 1f;
        player.enabled = true;
    }

    //public void Play()
    //{
    //    score = 0;
    //    scoreText.text = score.ToString();

    //    playButton.SetActive(false);
    //    gameOver.SetActive(false);

    //    Time.timeScale = 1f;
    //    player.enabled = true;

    //    Pipes[] pipes = FindObjectsByType<Pipes>(FindObjectsSortMode.None);

    //    for (int i = 0; i < pipes.Length; i++)
    //    {
    //        Destroy(pipes[i].gameObject);
    //    }
    //}

    public void Pause()
    {
        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();

        // Ganti background jadi malam saat poin 10
        if (score == 10)
        {
            backgroundScript.ChangeBackground();
        }
    }

    //update highscore
    private void UpdateHighScoreUI()
    {
        if (highScoreText != null)
            highScoreText.text = "Best: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    public void GameOver()
    {
        //Debug.Log("Game Over");
        gameOver.SetActive(true);
        playButton.SetActive(true);

        Pause();

        // Data Persistence (Menyimpan High Score)
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }

        UpdateHighScoreUI();
    }
    //pause game
    public void TogglePause()
    {
        // Jika waktu sedang berjalan, maka hentikan (Pause)
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 0f;
            player.enabled = false;
        }
        else if (Time.timeScale == 0f && !playButton.activeSelf)
        {
            // Reset direction burung agar tidak menyimpan tenaga lompatan terakhir
            player.ResetDirection();
            // Jika sedang berhenti (dan bukan di layar menu), maka jalankan lagi
            Time.timeScale = 1f;
            player.enabled = true;
        }
    }
}
