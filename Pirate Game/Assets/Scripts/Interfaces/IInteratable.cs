using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteratable
{
    void Interact(Transform playerTransfrom);
    void EnableUI();
    void DisableUI();
}
