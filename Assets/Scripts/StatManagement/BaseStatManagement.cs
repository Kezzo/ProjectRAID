using System;
using UnityEngine;

public class BaseStatManagement : MonoBehaviour
{
    [SerializeField]
    private int m_health;

    private int m_maxHealth = 0;

    public Action<int, int> m_OnHealthChange;

    /// <summary>
    /// Sets the maximum health.
    /// </summary>
    /// <param name="maxHealth">The maximum health.</param>
    public void Initialize(int maxHealth)
    {
        m_maxHealth = maxHealth;
        m_health = maxHealth;
    }

    /// <summary>
    /// Changes the health.
    /// </summary>
    /// <param name="value">The value.</param>
    public virtual void ChangeHealth(int value)
    {
        m_health += value;

        m_health = Mathf.Clamp(m_health, 0, m_maxHealth);

        if (m_health <= 0)
        {
            //TODO: Implement death
        }

        if (m_OnHealthChange != null)
        {
            m_OnHealthChange(m_health, m_maxHealth);
        }
    }

    /// <summary>
    /// Gets the current health.
    /// </summary>
    /// <returns></returns>
    public int GetCurrentHealth()
    {
        return m_health;
    }
}
