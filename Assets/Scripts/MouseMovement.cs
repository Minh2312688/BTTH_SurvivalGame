using UnityEngine;

public class Mousemovement : MonoBehaviour
{
   public float mouseSensitivity = 100f;//Độ nhạy của chuột, càng cao thì chuột càng nhạy
 
    float xRotation = 0f;// góc quay x, dùng để xoay camera lên xuống
    float YRotation = 0f;//góc quay y, dùng để xoay camera trái phải
 
    void Start()
    {
      //Locking the cursor to the middle of the screen and making it invisible

      Cursor.lockState = CursorLockMode.Locked;
      
    }
 
    void Update()
    {
      
      // Kiểm tra an toàn trước khi sử dụng Instance
      bool inventoryOpen = InventorySystem.Instance != null && InventorySystem.Instance.isOpen;
      bool craftingOpen = CraftingSystem.Instance != null && CraftingSystem.Instance.isOpen;
      // Khóa cursor khi không mở bất kỳ UI nào
      if (!inventoryOpen && !craftingOpen)
      {
        Cursor.lockState = CursorLockMode.Locked;
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
 
       //control rotation around x axis (Look up and down)
       xRotation -= mouseY;
 
       //we clamp the rotation so we cant Over-rotate (like in real life)
       xRotation = Mathf.Clamp(xRotation, -90f, 90f);
       // để không bị xoay quá 90 độ lên hoặc xuống, chúng ta giới hạn góc quay x từ -90 đến 90 độ
       // xem sau khi giới hạn, nếu chuột di chuyển quá nhanh thì camera sẽ không bị xoay quá 90 độ lên hoặc xuống
 
       //control rotation around y axis (Look up and down)
       YRotation += mouseX;
 
       //applying both rotations
       transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);
 
    }
    }
}
