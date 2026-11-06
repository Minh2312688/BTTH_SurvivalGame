using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class InventorySystem : MonoBehaviour
{
     public static InventorySystem Instance { get; set; }
     public List<GameObject> slotList = new List<GameObject>();
     public List<string> itemList = new List<string>();
     GameObject itemToAdd;
     GameObject whatSlotToEquip;
     public GameObject ItemInfoUI;

    public GameObject inventoryScreenUI;
    public bool isOpen;
    public GameObject pickupAlert;
    public TextMeshProUGUI pickupName;
    public Image pickupImage;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
 
 
    void Start()
    {
        isOpen = false;
        PopulateSlotList();
        Cursor.visible = false;
    }
 
 
    void Update()
    {
 
        if (Input.GetKeyDown(KeyCode.I) && !isOpen)
        {
 
		      	Debug.Log("i is pressed");
            inventoryScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.I) && isOpen)
        {
            inventoryScreenUI.SetActive(false);
            if(!CraftingSystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SelectionManager.instance.EnableSelection();
                SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;

            }

            isOpen = false;
        }
    }
    void PopulateSlotList()
    {
        foreach (Transform child in inventoryScreenUI.transform)
        {
            if (child.CompareTag("Slot"))
            {
                slotList.Add(child.gameObject);
            }
            
        }
        
    }
    public void AddItemToInventory(string itemName)
    {
        whatSlotToEquip = FindNextEmptySlot();
        //itemToAdd = Instantiate(Resources.Load<GameObject>(itemName),whatSlotToEquip.transform.position,whatSlotToEquip.transform.rotation);
        GameObject itemPrefab = Resources.Load<GameObject>(itemName);
         if(itemPrefab == null)
         {
             Debug.LogError("Item prefab not found in Resources folder: " + itemName);
         }
        else
        {
            for (int i = 0; i < slotList.Count; i++)
        {
            if (slotList[i].transform.childCount == 0)
            {
                Instantiate(itemPrefab, slotList[i].transform);
                itemList.Add(itemName);
                TriggerPickupPopup(itemName, itemPrefab.GetComponent<Image>().sprite);
                ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
                break;
            }
        }
        }
        
    }
    void TriggerPickupPopup(string itemName,Sprite itemSprite)
    {
        pickupAlert.SetActive(true);
        pickupName.text = itemName;
        pickupImage.sprite = itemSprite;
        Invoke("ClosePickupPopup", 2f);

    }
    void ClosePickupPopup()
    {
        pickupAlert.SetActive(false);

    }
    public bool CheckIfFull()
    {
        int count = 0;
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount != 0)
            {
                count++;
            }
        }
        if(count == 21)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }
    public void RemoveItem(string nameToRemove,int amountToRemove)
    {
        int counter=amountToRemove;
        for(var i=slotList.Count-1;i>=0;i--)
        {
            if(slotList[i].transform.childCount >0)
            {
                if(slotList[i].transform.GetChild(0).name==nameToRemove+"(Clone)"&&counter!=0)
                {
                    Destroy(slotList[i].transform.GetChild(0).gameObject);
                    itemList.Remove(nameToRemove);
                    counter--;
                    ReCalculateList();
                CraftingSystem.Instance.RefreshNeededItems();
                }
            }
           
        }
    }
    public void ReCalculateList()
    {
        itemList.Clear();
        foreach (GameObject slot in slotList)
        {
            if (slot.transform.childCount > 0)
            {
                string name=slot.transform.GetChild(0).name.Replace("(Clone)", "");
                itemList.Add(name);
            }
        }
    }
 
}