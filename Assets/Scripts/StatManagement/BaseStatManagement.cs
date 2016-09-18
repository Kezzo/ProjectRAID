using System;
using UnityEngine;

public class BaseStatManagement : MonoBehaviour
{
    [SerializeField]
    private int m_health;
    public int Health { get { return m_health; } }

    public bool IsDead { get { return m_health <= 0; } }

    private int m_maxHealth = 0;

    public Action<int, int> m_OnHealthChange;

    public Action m_OnDead;

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

        if (m_OnHealthChange != null)
        {
            m_OnHealthChange(m_health, m_maxHealth);
        }

        if (IsDead && m_OnDead != null)
        {
            m_OnDead();
        }
    }
}
