using GogoGaga.OptimizedRopesAndCables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockingSystem : MonoBehaviour, IInteratable
{
    [SerializeField] private Merchant merchant;

    [SerializeField] private bool isFree = true;
    [SerializeField] private Transform dockTransform;
    [SerializeField] private Transform ropeTransform;
    [SerializeField] private float lerpSpeed = 2f; // Speed at which the player moves towards the dock
    private bool isDocking = false;
    [SerializeField] private bool docked = false;
    private GameObject player;

    [SerializeField] private GameObject interactCanvas;
    [SerializeField] private GameObject ropePrefab;
    private Rope rope;
    private void Awake()
    {
        DisableUI();
    }
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    public void Interact(Transform playerTransform)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isDocking = true;

            //GameObject ropeObject = Instantiate(ropePrefab, playerTransform.position, Quaternion.identity);
            //rope = ropeObject.GetComponent<Rope>();
            //rope.SetStartPoint(playerTransform);
            //rope.SetEndPoint(ropeTransform);
        }
    }

    public void EnableUI()
    {
        interactCanvas.SetActive(true);
    }

    public void DisableUI()
    {
        interactCanvas.SetActive(false);
    }

    private void Update()
    {
        if (isDocking && Input.GetKey(KeyCode.Space))
        {
            // Lerp player's position towards the dock position
            player.transform.position = Vector3.Lerp(player.transform.position, dockTransform.position, Time.deltaTime * lerpSpeed);

            // Optionally, also lerp rotation if needed
            player.transform.rotation = Quaternion.Lerp(player.transform.rotation, dockTransform.rotation, Time.deltaTime * lerpSpeed);

            // Stop docking once close enough
            if (Vector3.Distance(player.transform.position, dockTransform.position) < 0.1f)
            {
                isDocking = false;
                docked = true;
                if (docked && merchant != null)
                {
                    Debug.Log("Dock Contains merchant");
                    // Dock contains merchant, here comes the merchant scene or teh merchant UI
                }
                else
                {
                    Debug.Log("Ship Docked");
                    return;
                }
            } 
        }
        // Stop docking when space is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isDocking = false;
        }

        if (Vector3.Distance(player.transform.position, dockTransform.position) > 0.1f)
        {
            docked = false;
        }
    }
}
