using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Switch;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class MenuController : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField]
    private Transform menus;
    [SerializeField]
    private EventSystem eventSystem;


    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
    }


    public void OpenMenu(MenuState state)
    {
        OpenAndCloseOthersBy(state.ToString());
        ChangeFirstSelectedButtonInEventSystem();
        animator.SetBool("OpenMainMenu", true);

       

    }

    public void CloseMenu()
    {
        animator.SetBool("OpenMainMenu", false);
    }



    private void ChangeFirstSelectedButtonInEventSystem()
    {
        foreach (Transform child in menus)
        {
            if (child.gameObject.activeSelf)
            {
                foreach (Transform childOfChild in child)
                {
                    if (childOfChild.gameObject.GetComponent<Button>())
                    {
                        eventSystem.SetSelectedGameObject(childOfChild.gameObject);
                        Debug.Log("UI First Selected: " + eventSystem.currentSelectedGameObject.name);
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
                break;
            }
            else
            {
                continue;
            }
            
            
        }
    }

    private void OpenAndCloseOthersBy(string name)
    {
       foreach(Transform child in menus)
        {
            if(child.gameObject.name == name)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
