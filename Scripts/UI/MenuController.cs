using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject menu;
    
    //menu items 
    List<Text> menuItems;
    

    int selectedItem = 0;

    private void Awake() {
        menuItems =- menu.GetComponentInChildren<Text>().ToList();
    }
    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ++selectedItem;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            --selectedItem;
        }

        selectedItem = MathF.Clamp(selectedItem, 0, menuItems.Count - 1);

    }

    void UpdateItemSelection()
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i == selectedItem)
            {
                menuItems[i].color = ?;
            }
            else 
            {
                menuItems[i].color = Color.white;
            }
        }
    }
}
