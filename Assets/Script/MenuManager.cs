using UnityEngine;
using UnityEngine.SceneManagement; // Wajib untuk Scene Flow

public class MenuLoader : MonoBehaviour
{
    // Panggil fungsi ini melalui OnClick() tombol Start di Main Menu
    public void StartGame()
    {
        // Berpindah ke scene gameplay
        SceneManager.LoadScene("FlappyBird");
    }

    // Bisa ditambahkan di GameManager untuk tombol "Back to Menu" saat Game Over
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Keluar dari Game");
        Application.Quit();
    }
}