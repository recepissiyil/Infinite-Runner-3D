using UnityEngine;
using UnityEngine.UI;


/*Attached HealthBar object in Canvas at hierarchy(At Only First Scene) */
public class HealthBar : MonoBehaviour
{
    #region Variables
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    #endregion
    #region Arrange values
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = 0;
        fill.color = gradient.Evaluate(1f);
    }
    public void SetHealth(int health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    } 
    #endregion
}
