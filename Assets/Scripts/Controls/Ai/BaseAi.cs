using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseAi : MonoBehaviour
{
    [SerializeField]
    private BaseCharacter m_characterToControl;

    [SerializeField]
    private List<ThreatElement> m_currentThreatList = new List<ThreatElement>();

    [Serializable]
    private class ThreatElement
    {
        public int m_CurrentThreatWithThisAi;
        public BaseCharacter m_BaseCharacter;

        public ThreatElement(BaseCharacter baseCharacter, int currentThreatWithThisAi)
        {
            m_CurrentThreatWithThisAi = currentThreatWithThisAi;
            m_BaseCharacter = baseCharacter;
        }
    }

    private ThreatElement m_currentTarget;

    void Start()
    {
        AttackClosestTarget();
    }

    /// <summary>
    /// Bases the ai loop.
    /// </summary>
    /// <returns></returns>
    private void AttackClosestTarget()
    {
        BaseCharacter targetToAttack = ControllerContainer.TargetingController.GetClosestCharacter(m_characterToControl);

        if (targetToAttack != null)
        {
            ChangeThreat(targetToAttack, 1);
        }
    }

    /// <summary>
    /// Resorts the threat list.
    /// </summary>
    private void ResortThreatList()
    {
        m_currentThreatList.Sort((threatElement1, threatElement2) =>
                threatElement2.m_CurrentThreatWithThisAi.CompareTo(threatElement1.m_CurrentThreatWithThisAi));
    }

    /// <summary>
    /// Called when the threat from someone changed for this ai.
    /// </summary>
    public void ChangeThreat(BaseCharacter causer, int threatChange)
    {
        ThreatElement existingThreatElement = m_currentThreatList.Find(threatElement => 
            threatElement.m_BaseCharacter.m_CharacterId == causer.m_CharacterId);

        if (existingThreatElement != null)
        {
            existingThreatElement.m_CurrentThreatWithThisAi += threatChange;
        }
        else
        {
            // One cannot decrease threat, with no threat registered. No preemptive threat reduction.
            m_currentThreatList.Add(new ThreatElement(causer, Math.Max(0, threatChange)));
        }

        if (m_currentThreatList.Count > 1)
        {
            ResortThreatList();

            ThreatElement newTarget = null;

            if (TargetChangeNecessary(out newTarget))
            {
                m_currentTarget = newTarget;
                m_characterToControl.OnInteraction(m_currentTarget.m_BaseCharacter);
            }
        }
        else
        {
            m_currentTarget = m_currentThreatList[0];
            m_characterToControl.OnInteraction(m_currentTarget.m_BaseCharacter);
        }
    }

    /// <summary>
    /// Determines whether the current target has highest threat.
    /// </summary>
    /// <param name="newTarget">The new target.</param>
    /// <returns></returns>
    private bool TargetChangeNecessary(out ThreatElement newTarget)
    {
        if (m_currentThreatList.Count > 1 && !string.Equals(m_currentThreatList[0].m_BaseCharacter.m_CharacterId, m_currentTarget.m_BaseCharacter.m_CharacterId) &&
            m_currentThreatList[0].m_CurrentThreatWithThisAi > (m_currentTarget.m_CurrentThreatWithThisAi * BaseBalancing.m_TargetChangeByThreatDifference))
        {
            newTarget = m_currentThreatList[0];
            return true;
        }

        newTarget = null;
        return false;
    }
}