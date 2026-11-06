using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EquipableItem : MonoBehaviour
{
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetMouseButtonDown(0)&&InventorySystem.Instance.isOpen==false
       &&CraftingSystem.Instance.isOpen==false
       &&SelectionManager.instance.handIsVisible==false)//left mouse button
        {
            animator.SetTrigger("hit");
        }
    }
}
