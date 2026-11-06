using System.Collections.Generic;
using System.Collections;

using UnityEngine;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{
    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;
    public List<string> inventoryItemList = new List<string>();
    //Category Buttons
    Button toolsBtn;
    // Craft buttons
    Button crafAxeBtn;
    //Requirement Text
    Text axeReq1,axeReq2;
    public bool isOpen;
    //All Blueprints
    public Blueprint AxeBLP = new Blueprint("Axe", "Stone", "Stick", 3, 3, 2);
    public Blueprint allBLP=new Blueprint("","Stone","Stick",2,2,2);

    public static CraftingSystem Instance { get;  set; }

    void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);//gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isOpen = false;
        toolsBtn = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBtn.onClick.AddListener(delegate { OpenToolsCategory(); });
        //Axe
        axeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<Text>();
        axeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<Text>();
        crafAxeBtn = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        crafAxeBtn.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });
    }

    void  CraftAnyItem(Blueprint blueprintToCraft)
    {
        Debug.Log("Attempting to craft: " + blueprintToCraft.name);
        InventorySystem.Instance.AddItemToInventory(blueprintToCraft.name);
        if(blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);

        }
        else if(blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }
        StartCoroutine(calculate());
        RefreshNeededItems();
    }
    public IEnumerator calculate()
    {
        yield return 0;
        InventorySystem.Instance.ReCalculateList();
    }

    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && !isOpen)
        {
 
		      	Debug.Log("c is pressed");
            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SelectionManager.instance.DisableSelection();
            SelectionManager.instance.GetComponent<SelectionManager>().enabled = false;
            isOpen = true;
 
        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            if(!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                SelectionManager.instance.EnableSelection();
                SelectionManager.instance.GetComponent<SelectionManager>().enabled = true;
            isOpen = true;
            }

            isOpen = false;
        }
    }
    
    public void RefreshNeededItems()
    {
        int stoneCount=0;
        int stickCount=0;
        inventoryItemList = InventorySystem.Instance.itemList;
        foreach (string itemName in inventoryItemList)
        {
            switch (itemName)
            {
                case "Stone":
                    stoneCount++;
                    break;
                case "Stick":
                    stickCount++;
                    break;
            }
            
        }
        //Axe//
        axeReq1.text = "3 Stone[" + stoneCount + "]";
        axeReq2.text = "3 Stick[" + stickCount + "]";
        if(stoneCount >= 3 && stickCount >= 3)
        {
            crafAxeBtn.gameObject.SetActive(true);
        }
        else
        {
            crafAxeBtn.gameObject.SetActive(false);
        }
    }
}
