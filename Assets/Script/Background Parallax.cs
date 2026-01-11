using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{

    private MeshRenderer meshRenderer;

    private float animationSpeed = 1f;

    // Tambahkan variabel ini
    public Texture2D backgroundSiang;
    public Texture2D backgroundMalam;
    private bool isMalam = false;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();

        // Set awal ke siang
        if (backgroundSiang != null)
            meshRenderer.material.mainTexture = backgroundSiang;
    }

    private void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(animationSpeed * Time.deltaTime, 0);
    }

    // Fungsi untuk mengganti gambar yang akan dipanggil dari GameManager
    public void ChangeBackground()
    {
        if (!isMalam && backgroundMalam != null)
        {
            meshRenderer.material.mainTexture = backgroundMalam;
            isMalam = true;
            Debug.Log("Background berubah ke Malam!");
        }
    }

    // Fungsi Reset saat Play lagi
    public void ResetBackground()
    {
        if (backgroundSiang != null)
        {
            meshRenderer.material.mainTexture = backgroundSiang;
            isMalam = false; // Reset flag agar bisa berubah lagi saat poin 10
        }
    }
}
