using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
  //  [SerializeField]
   // private DamageScreen damageScreen;

    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float healAmount;

    // Speed of the health change
    [SerializeField]
    private float changeSpeedHealing;
    [SerializeField]
    private float changeSpeedDamaging;



    [Header("Pulse Effect")]
    public float pulseSpeed = 5f; // Speed at which the pulse effect occurs
    public Color healthyColor;
    public Color unhealthyColor;

    [SerializeField]
    private float pulseThreshold = 40f;
    [SerializeField]
    private Image image;
    [SerializeField]
    private bool isPulsing = false;

    private float targetAlpha = 1f;


    private float changeSpeed;

    private float targetHealth;

    private bool isUpdatingHealth = false;

    private bool isDead = false;

    private RespawnController respawnController;

    private void Start()
    {
        image.color = healthyColor;
        respawnController = GameObject.Find("RespawnSpots").GetComponent<RespawnController>();
    }

    void Update()
    {

        if (isUpdatingHealth)
        {
            if (!Mathf.Approximately(currentHealth, targetHealth))
            {
                currentHealth = Mathf.MoveTowards(currentHealth, targetHealth, changeSpeed * Time.deltaTime);
                healthBar.fillAmount = currentHealth / 100f;

             //   damageScreen.UpdateScreen(currentHealth);

                if(currentHealth <= 0)
                {
                    //Player is dead
                    isDead = true;

                    if (isDead)
                    {
                     //   damageScreen.ResetScreen();
                        respawnController.Respawn();
                        HealCompletely();
                        isDead = false;
                    }
                    

                }
            }
            else
            {
                isUpdatingHealth = false;
            }
        }


        if (currentHealth < pulseThreshold)
        {
            // Toggle between pulsing colors
            isPulsing = true;
            PulseColor();
        }
        else
        {
            // Reset to healthy color if health is above 40
            isPulsing = false;
            image.color = healthyColor;
        }

    }


    private void PulseColor()
    {
        // Calculate the color to pulse towards
        Color targetColor = isPulsing ? unhealthyColor : healthyColor;

        // Lerp the color towards the target color
        image.color = Color.Lerp(image.color, targetColor, Time.deltaTime * pulseSpeed);

        // If the color is close enough to the target color, switch direction
        if (ColorCloseEnough(image.color, targetColor))
        {
            isPulsing = !isPulsing;
        }
    }

    private bool ColorCloseEnough(Color a, Color b)
    {
        // Check if the color components are close enough
        return Mathf.Abs(a.r - b.r) < 0.01f && Mathf.Abs(a.g - b.g) < 0.01f && Mathf.Abs(a.b - b.b) < 0.01f;
    }

    public void Damage(float damage)
    {
        changeSpeed = changeSpeedDamaging;
        targetHealth = Mathf.Clamp(currentHealth - damage, 0, 100);
        isUpdatingHealth = true;

        if (currentHealth <= 0)
        {
            isUpdatingHealth = false;
            currentHealth = 0;
            healthBar.fillAmount = 0f;

        }
    }

    public void Heal()
    {
        changeSpeed = changeSpeedHealing;
        targetHealth = Mathf.Clamp(currentHealth + healAmount, 0, 100);
        isUpdatingHealth = true;

        if(currentHealth >= maxHealth)
        {
            isUpdatingHealth=false;
            currentHealth = maxHealth;
            healthBar.fillAmount = 1f;

        }
    }


    public void HealCompletely()
    {
        healthBar.fillAmount = 1f;
        currentHealth = maxHealth;
        isUpdatingHealth = false;
    }



    public void Level1Setup()
    {
        HealCompletely();
        Damage(70);
    }
}
