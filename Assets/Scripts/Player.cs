using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Skin playerSkin;
    [SerializeField] private LevelManager levelManager;

    [Header("Score Settings")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int score = 0;
    [Header("Health Settings")]
    [SerializeField] private float maxHealth = 4f;
    [SerializeField] private float currentHealth;
    [SerializeField] private Image[] heartUIs;

    [Header("Movement Settings")]
    [SerializeField] private float normalMoveSpeed = 5f;
    [SerializeField] private float sprintMoveSpeed = 12f;
    [SerializeField] private Vector2 minPosition = new Vector2(-5.4111f, -2.387f);
    [SerializeField] private Vector2 maxPosition = new Vector2(5.426f, 2.89f);

    [Header("Stamina Settings")]
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaRegenRate = 1.5f;
    [SerializeField] private float staminaDepleteRate = 2.5f;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private Color normalStaminaColor = Color.cyan;
    [SerializeField] private Color exhaustedStaminaColor = Color.gray;

    private bool isExhausted;
    private float currentStamina;

    private void Start()
    {
        currentStamina = maxStamina;
        currentHealth = maxHealth;
        UpdateStaminaUI();
        UpdateScoreUI();
    }


    void Update()
    {
        Move();
        HandleStamina();
        HandleColorChange();
    }

    private void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        Vector2 moveInput = new Vector2(moveX, moveY).normalized;


        Vector3 nextPosition = transform.position;
        float speed = IsSprinting() ? sprintMoveSpeed : normalMoveSpeed;

        nextPosition += (Vector3)(moveInput * Time.deltaTime * speed);

        nextPosition.x = Mathf.Clamp(nextPosition.x, minPosition.x, maxPosition.x);
        nextPosition.y = Mathf.Clamp(nextPosition.y, minPosition.y, maxPosition.y);

        transform.position = nextPosition;
    }

    private void HandleStamina()
    {
        if (IsSprinting())
        {
            currentStamina -= staminaDepleteRate * Time.deltaTime;
        }
        else if (currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
        }

        currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);
        isExhausted = currentStamina == 0f || (isExhausted && currentStamina < maxStamina);

        UpdateStaminaUI();
    }

    private void UpdateStaminaUI()
    {
        staminaSlider.value = currentStamina / maxStamina;
        staminaSlider.fillRect.GetComponent<Image>().color = isExhausted ? exhaustedStaminaColor : normalStaminaColor;
    }

    private bool IsMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0f || Input.GetAxisRaw("Vertical") != 0f;
    }
    private bool IsSprinting()
    {
        return Input.GetKey(KeyCode.LeftShift) && currentStamina > 0f && IsMoving() && !isExhausted;
    }
    public void Hurt()
    {
        SoundManager.PlaySound(SoundType.Collision, 0.5f);
        currentHealth--;
        if (currentHealth == 0)
        {
            Destroy(gameObject);
            levelManager.GameOver();
        }
        UpdateHealthUI();
    }
    private void UpdateHealthUI()
    {

        for (int i = 0; i < heartUIs.Length; i++)
        {
            heartUIs[i].enabled = i < currentHealth;
        }
    }

    private void HandleColorChange()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerSkin.SetSkin(Skin.SkinColor.Red);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerSkin.SetSkin(Skin.SkinColor.Blue);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerSkin.SetSkin(Skin.SkinColor.Green);
        }
    }
    public void IncreaseScore()
    {
        SoundManager.PlaySound(SoundType.GetScore, 0.5f);
        score++;
        UpdateScoreUI();
    }
    private void UpdateScoreUI()
    {
        scoreText.text = "SCORE:  " + score.ToString();
    }

    public int GetScore()
    {
        return score;
    }

}

