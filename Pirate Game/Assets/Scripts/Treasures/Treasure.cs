using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour, IInteratable
{
    public void OpenChest()
    {
        Debug.Log("Opening chest");
    }

    public void Interact()
    {
        // Create a UI that shows when you are in range to handle input

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }
}
