using UnityEngine;
using UnityEngine.InputSystem; // Import the new Input System namespace
using System.Collections;

public class Player : MonoBehaviour
{
    private SpriteRenderer SpriteRenderer;

    public Sprite[] sprites;//array sprite

    private int spriteIndex;//untuk melacak index saat ini pada array

    private Vector3 direction;
        
    public float gravity = -9.8f;

    public float strength = 5.0f;

    private InputAction upBirdAction;

    // Hit Audio
    public AudioSource hitSound;


    private void Awake()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void Start()
    {
        // Mencari action bernama "UPbird" dari Input Action Asset
        upBirdAction = InputSystem.actions.FindAction("UPbird");

        if (upBirdAction != null)
        {
            upBirdAction.Enable();
        }

        InvokeRepeating(nameof(AnimateSprite), 0.15f, 0.15f);
    }

    //reset posisi burung
    private void OnEnable()
    {
        Vector2 position = transform.position;
        position.y = 0f;
        transform.position = position;
    }

    // Update is called once per frame
    void Update()
    {
        if (upBirdAction != null && upBirdAction.WasPressedThisFrame())
        {
            direction = Vector3.up * strength;
        }

        // Terapkan gravitasi (menambah nilai negatif membuat burung turun)
        direction.y += gravity * Time.deltaTime;

        // Update posisi burung
        transform.position += direction * Time.deltaTime;
    }

    private void AnimateSprite()
    {
        spriteIndex++;

        if (spriteIndex >= sprites.Length)
        {
            spriteIndex = 0;
        }
        SpriteRenderer.sprite = sprites[spriteIndex];
    }

    //untuk deteksi tabrakan
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            if (hitSound) hitSound.Play(); // Mainkan suara tabrakan
            StartCoroutine(FlashEffect());// efek pas nabrak
            FindAnyObjectByType<GameManager>().GameOver();
        } else if (other.gameObject.tag == "Scoring")
        {
            FindAnyObjectByType<GameManager>().IncreaseScore();
        }   
    }

    // StartCoroutine(FlashEffect());
    private IEnumerator FlashEffect()
    {
        // Mengubah warna sprite menjadi merah sesaat
        SpriteRenderer.color = Color.red;
        yield return new WaitForSecondsRealtime(0.3f);
        SpriteRenderer.color = Color.white;
    }

    public void ResetDirection()
    {
        direction = Vector3.zero;
    }
}
