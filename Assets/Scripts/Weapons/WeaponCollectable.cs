using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(Collider2D))]
public class WeaponCollectable : MonoBehaviour
{
    [SerializeField]
    private bool isCollectable;
    [SerializeField]
    private string notificationTarget;
    [SerializeField]
    private WeaponsEnum weapon;
    [SerializeField]
    private GameObject onDestroyObject;

    [SerializeField]
    private AudioSource audioSource;

    private NotificationController notificationController;
    private GoalController goalController;

    public WeaponsEnum Weapon { get => weapon; set => weapon = value; }



    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        notificationController = GameObject.FindGameObjectWithTag("NotificationController").
            gameObject.GetComponent<NotificationController>();
        goalController = GameObject.FindGameObjectWithTag("GameController").
            gameObject.GetComponent<GoalController>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isCollectable && collision.gameObject.tag == "Player")
        {
            if (notificationTarget != "")
            {
                notificationController.Show(notificationTarget);
            }

            AddWeaponToInventar(weapon);
            Destroy(onDestroyObject);
        }
    }


    private void AddWeaponToInventar(WeaponsEnum _weapon)
    {
        audioSource.Play();
        if (goalController.Weapons != null)
        {
            goalController.Weapons.Add(_weapon);
         
        }
        else
        {
            goalController.Weapons = new List<WeaponsEnum>();
            goalController.Weapons.Add(_weapon);
        }
    }
}
