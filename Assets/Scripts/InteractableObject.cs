using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public bool playerInRange = false;
    public string ItemName;
 
    public string GetItemName()
    {
        return ItemName;
    }
    
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Mouse0)&&SelectionManager.instance.target&&SelectionManager.instance.selectedObject==gameObject)
        {
            if(!InventorySystem.Instance.CheckIfFull())
            {
                InventorySystem.Instance.AddItemToInventory(ItemName);
                Destroy(gameObject);

               
            }
            else
            {
                Debug.Log("Inventory is full");
                 
            }
           
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}