using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthBar : MonoBehaviour
{
    [SerializeField]
    private BaseStatManagement m_statManagement;

    [SerializeField]
    private Image m_healthBar;

    void Start()
    {
        m_statManagement.m_OnHealthChange += OnHealthChange;
    }

    /// <summary>
    /// Called when a health change occured.
    /// </summary>
    /// <param name="currentHealth">The current health.</param>
    /// <param name="maxHealth">The maximum health.</param>
    private void OnHealthChange(int currentHealth, int maxHealth)
    {
        m_healthBar.fillAmount = (float)currentHealth / (float)maxHealth;
    }
}
