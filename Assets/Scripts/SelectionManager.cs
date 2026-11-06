using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    public static SelectionManager instance{get;set;}
    public bool target;
    public GameObject selectedObject;
      public GameObject interaction_Info_UI;
      public Terrain terrain;
    Text interaction_text;
    public Image centerDotIcon;
    public Image handIcon;
    public bool handIsVisible=true;
    private void Start()
    {
        interaction_text = interaction_Info_UI.GetComponent<Text>();
    }
 
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            var selectionTransform = hit.transform;
 
            InteractableObject interactable = selectionTransform.GetComponent<InteractableObject>();
            if (interactable != null && interactable.playerInRange)
            {
                target = true;
                selectedObject=interactable.gameObject;
                interaction_text.text = interactable.GetItemName();
                interaction_Info_UI.SetActive(true);
                if(interactable.CompareTag("pickable"))
                {
                    centerDotIcon.gameObject.SetActive(false);
                    handIcon.gameObject.SetActive(true);
                    handIsVisible=true;
                }
                else
                {
                    centerDotIcon.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);
                    handIsVisible=false;
                }
               
            }
            else // nếu
            { 
                target = false;
                interaction_Info_UI.SetActive(false);
                 centerDotIcon.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);
                    handIsVisible=false;
            }
 
        }
        else 
            { 
                target = false;
                interaction_Info_UI.SetActive(false);
                 centerDotIcon.gameObject.SetActive(true);
                    handIcon.gameObject.SetActive(false);
                    handIsVisible=false;
            }
    }
    public void DisableSelection()
    {
        //target = false;
        interaction_Info_UI.SetActive(false);
        centerDotIcon.enabled = false;
        handIcon.enabled = false;
        selectedObject = null;
    }
    public void EnableSelection()
    {
        //target = true;
        interaction_Info_UI.SetActive(true);
        centerDotIcon.enabled = true;
        handIcon.enabled = true;
    }
}
