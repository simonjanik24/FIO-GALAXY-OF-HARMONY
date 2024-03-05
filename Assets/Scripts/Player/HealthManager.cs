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

    private float changeSpeed;

    private float targetHealth;

    private bool isUpdatingHealth = false;

    private bool isDead = false;

    private RespawnController respawnController;

    private void Start()
    {
        respawnController = GameObject.Find("RespawnSpots").GetComponent<RespawnController>();
        Damage(30);
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
}
