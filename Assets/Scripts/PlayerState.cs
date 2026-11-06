using UnityEngine;
using System.Collections;

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; set; }
    //Player Health
    public float currentHealth;
    public float maxHealth;
    //Player Calories
    public float currentCalories;
    public float maxCalories;
    float distannceTravelled=0;
    Vector3 lastPosition;
    public GameObject playerBody;
    //Player Hydration
    public float currentHydrationPercent;
    public float maxHydrationPercent;
    public bool isHydrationActive;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        currentHealth = maxHealth;
        currentCalories = maxCalories;
        currentHydrationPercent = maxHydrationPercent;
        lastPosition = playerBody.transform.position;
        StartCoroutine(decreaseHydration());
    }
    IEnumerator decreaseHydration()
    {
        while (isHydrationActive)
        {
            currentHydrationPercent -= 1f;
            yield return new WaitForSeconds(2f);
        }
    }
    // Update is called once per frame
    void Update()
    {
        distannceTravelled += Vector3.Distance(playerBody.transform.position, lastPosition);
        lastPosition = playerBody.transform.position;
        if(distannceTravelled >= 5f)
        {
            currentCalories -= 1;
            distannceTravelled = 0;
        }
        if(Input.GetKeyDown(KeyCode.N))
        {
            currentHealth -= 10;
        }
    }
    public void setHealth(float health)
    {
        currentHealth = health;
    }
    public void setCalories(float calories)
    {
        currentCalories = calories;
    }
    public void setHydration(float hydration)
    {
        currentHydrationPercent = hydration;
    }
}
