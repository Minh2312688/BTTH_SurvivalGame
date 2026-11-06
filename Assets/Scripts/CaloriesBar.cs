using UnityEngine;
using UnityEngine.UI;

public class CaloriesBar : MonoBehaviour
{
    private Slider slider;
    public Text caloriesCounter;
    public GameObject   playerState;
    private float currentCalories, maxCalories;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCalories = playerState.GetComponent<PlayerState>().currentCalories;
        maxCalories = playerState.GetComponent<PlayerState>().maxCalories;

        float fillValue = currentCalories / maxCalories;
        slider.value = fillValue;
        caloriesCounter.text = currentCalories.ToString() + "/" + maxCalories.ToString();
    }
}
