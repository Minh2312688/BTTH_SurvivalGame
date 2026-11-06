using UnityEngine;
using UnityEngine.UI;

public class HydrationBar : MonoBehaviour
{
    private Slider slider;
    public Text hydrationCounter;
    public GameObject playerState;
    private float currentHydrationPercent, maxHydrationPercent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        currentHydrationPercent = playerState.GetComponent<PlayerState>().currentHydrationPercent;
        maxHydrationPercent = playerState.GetComponent<PlayerState>().maxHydrationPercent;

        float fillValue = currentHydrationPercent / maxHydrationPercent;
        slider.value = fillValue;
        hydrationCounter.text = currentHydrationPercent.ToString() + "/" + maxHydrationPercent.ToString();
    }
}
