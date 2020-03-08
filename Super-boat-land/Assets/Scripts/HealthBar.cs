using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public GameObject hpBar;

    // percentageHealth should be between [0, 1]
    public void SetHealth(float percentageHealth)
    {
        float health = Mathf.Clamp(percentageHealth, 0, 1);
        hpBar.transform.localScale = new Vector3(health, 1, 1);
    }
}
