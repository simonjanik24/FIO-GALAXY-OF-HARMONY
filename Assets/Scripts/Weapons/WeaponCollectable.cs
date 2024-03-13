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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision on Collectable");
        if (isCollectable && collision.gameObject.tag == "Player")
        {
            goalController.Goals.Weapons.Add(weapon);
            notificationController.Show("FluteWeaponWheel");
            Destroy(onDestroyObject);
        }
    }
}
