using UnityEngine;

public class WeaponWheelSelector : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "WeaponWheelButton")
        {
            collision.gameObject.GetComponent<WeaponWheelButton>().HoverEnter();
        }
        
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WeaponWheelButton")
        {
            collision.gameObject.GetComponent<WeaponWheelButton>().HoverStay();
        }
        
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "WeaponWheelButton")
        {
            collision.gameObject.GetComponent<WeaponWheelButton>().HoverExit();
        }
        
    }

    
}
