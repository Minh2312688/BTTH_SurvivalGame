using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class EquipSystem : MonoBehaviour
{
    public static EquipSystem Instance { get; set; }
 
    // -- UI -- //
    public GameObject quickSlotsPanel;
 
    public List<GameObject> quickSlotsList = new List<GameObject>();
    //public List<string> itemList = new List<string>();
    public GameObject numbersHolder;
    public int selectedNumber;
    public GameObject selectedItem;
    public GameObject toolHolder;
    public GameObject selectedItemModel;
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
 
 
    private void Start()
    {
        PopulateSlotList();
    }
    void Update()
    {
        //alpha 1-7 for quick slots là hàng số 2 trên bàn phim
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectQuickSlot(1);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            SelectQuickSlot(2);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            SelectQuickSlot(3);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            SelectQuickSlot(4);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            SelectQuickSlot(5);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6))
        {
            SelectQuickSlot(6);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha7))
        {
            SelectQuickSlot(7);
        }
    }
    void SelectQuickSlot(int number)
    {
        if(checkIfSlotIsFull(number)==true)
        {
            Debug.Log("Slot "+number+" is full");
            if(selectedNumber!=number)
            {
                Debug.Log("Selected slot is "+number);
                selectedNumber=number;
                //Unselect previously selected item
                if(selectedItem!=null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected=false;
                }
                selectedItem=getSelectedItem(number);
                selectedItem.gameObject.GetComponent<InventoryItem>().isSelected=true;
                SetEquippedModel(selectedItem);
                //Changing the color
                foreach(Transform child in numbersHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color=Color.gray;
                }
                Text toBeChange=numbersHolder.transform.Find("number"+number).transform.Find("Text").GetComponent<Text>();
                toBeChange.color=Color.white;
            }
            else //We are trying to select the same item again, so we will unselect it
            {
                selectedNumber=1;//nul
                //Unselect previously selected item
                if(selectedItem!=null)
                {
                    selectedItem.gameObject.GetComponent<InventoryItem>().isSelected=false;
                    selectedItem=null;
                }
                if(selectedItemModel!=null)
                {
                    DestroyImmediate(selectedItemModel.gameObject);
                    selectedItemModel=null;
                }
                selectedItem=getSelectedItem(number);
                selectedItem.gameObject.GetComponent<InventoryItem>().isSelected=true;
                //Changing the color
                foreach(Transform child in numbersHolder.transform)
                {
                    child.transform.Find("Text").GetComponent<Text>().color=Color.gray;
                }
                Text toBeChange=numbersHolder.transform.Find("number"+number).transform.Find("Text").GetComponent<Text>();
                toBeChange.color=Color.white;
            }
            
        }
    }

        private void SetEquippedModel(GameObject selectedItem)
        {
            if(selectedItemModel!=null)
            {
                DestroyImmediate(selectedItemModel.gameObject);
                selectedItemModel=null;
            }
            string selectedItemName=selectedItem.name.Replace("(Clone)","");
             selectedItemModel=Instantiate(Resources.Load<GameObject>(selectedItemName+"_Model"),
            new Vector3(0.49f, 1.46f, 0.81f),               // Vị trí mới bạn vừa chỉnh
Quaternion.Euler(0f, -110f, 0f));                // Góc xoay mới bạn vừa chỉnh
            selectedItemModel.transform.SetParent(toolHolder.transform,false);

           
        }
        
    
    GameObject getSelectedItem(int slotnumber)
    {
        return quickSlotsList[slotnumber-1].transform.GetChild(0).gameObject;
    }
    bool checkIfSlotIsFull(int slotNumber)
    {
        if(quickSlotsList[slotNumber-1].transform.childCount>0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private void PopulateSlotList()
    {
        foreach (Transform child in quickSlotsPanel.transform)
        {
            if (child.CompareTag("QuickSlot"))
            {
                quickSlotsList.Add(child.gameObject);
            }
        }
    }
 
    public void AddToQuickSlots(GameObject itemToEquip)
    {
        // Find next free slot
        GameObject availableSlot = FindNextEmptySlot();
        // Set transform of our object
        itemToEquip.transform.SetParent(availableSlot.transform, false);
        
 
        InventorySystem.Instance.ReCalculateList();
 
    }
 
 
    private GameObject FindNextEmptySlot()
    {
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount == 0)
            {
                return slot;
            }
        }
        return new GameObject();
    }
 
    public bool CheckIfFull()
    {
 
        int counter = 0;
 
        foreach (GameObject slot in quickSlotsList)
        {
            if (slot.transform.childCount > 0)
            {
                counter += 1;
            }
        }
 
        if (counter == 7)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}