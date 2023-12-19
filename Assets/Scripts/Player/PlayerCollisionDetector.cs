using UnityEngine;

[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(WeaponController))]
public class PlayerCollisionDetector : MonoBehaviour
{
    private PlayerController playerController;
    private WeaponController weaponController;
    private HealthManager healthManager;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        weaponController = GetComponent<WeaponController>();
        healthManager = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();
    }


    private void OnParticleCollision(GameObject other)
    {

        ExcecuteCollisionBehavior(other);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ExcecuteCollisionBehavior(collision.gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ExcecuteCollisionBehavior(collision.gameObject);
    }


    private void ExcecuteCollisionBehavior(GameObject gameObject)
    {
        switch (gameObject.tag)
        {
            case "EvilScream":
                if (weaponController.Shield.IsActive == false)
                {
                    healthManager.Damage(10);
                }
                break;
        }
        switch (gameObject.layer)
        {
            case 6: //Dead Layer
                if (weaponController.Shield.IsActive == false)
                {
                    Debug.Log("Hit Dead Layer");

                    healthManager.Damage(10);
                }
                break;

        }
    }


}
