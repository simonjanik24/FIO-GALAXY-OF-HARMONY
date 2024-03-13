using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(BoxCollider2D))]
public class WeaponCollectable : MonoBehaviour
{
    [SerializeField]
    private bool isCollectable;
    [SerializeField]
    private WeaponsEnum weapon;
    [SerializeField]
    private GameObject onDestroyObject;

    [SerializeField]
    private AudioSource audioClip;

    private NotificationController notificationController;
    private GoalController goalController;

    public WeaponsEnum Weapon { get => weapon; set => weapon = value; }

    private void Awake()
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

            AddWeaponToInventar(weapon);
            notificationController.Show("FluteWeaponWheel");
            Destroy(onDestroyObject);
        }
    }


    private void AddWeaponToInventar(WeaponsEnum _weapon)
    {
        if (goalController.Goals != null)
        {
            if(goalController.Goals.Weapons != null)
            {
                goalController.Goals.Weapons.Add(_weapon);
            }
            else
            {
                goalController.Goals.Weapons = new List<WeaponsEnum>();
                goalController.Goals.Weapons.Add(_weapon);
            }
        }
        else
        {
            goalController.Goals = new Goals();
            goalController.Goals.Weapons = new List<WeaponsEnum>();
            goalController.Goals.Weapons.Add(_weapon);
        }
    }
}
