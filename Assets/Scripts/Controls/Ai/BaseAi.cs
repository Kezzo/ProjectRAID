using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAi : MonoBehaviour
{
    [SerializeField]
    protected BaseCharacter m_characterToControl;

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
    protected float m_CastingBlockedUntil = 0f;

    protected void Start()
    {
        AttackClosestTarget();

        StartCoroutine(AiLoop());
    }

    /// <summary>
    /// This gets called everytime the ai can decides to something.
    /// Normally scripts override this method.
    /// </summary>
    protected virtual void AiStep()
    {
        CheckForDeadCharactersInThreatList();
    }

    /// <summary>
    /// The ai loop.
    /// </summary>
    /// <returns></returns>
    private IEnumerator AiLoop()
    {
        while (true)
        {
            if (m_characterToControl.m_StatManagement.IsDead)
            {
                yield break;
            }

            AiStep();

            yield return null;
        }
    }

    /// <summary>
    /// Checks for dead characters in threat list.
    /// </summary>
    private void CheckForDeadCharactersInThreatList()
    {
        if (m_currentThreatList.Count > 0)
        {
            bool currentTargetIsDead = false;
            if (m_currentTarget != null && m_currentTarget.m_BaseCharacter.m_StatManagement.IsDead)
            {
                m_currentTarget = null;
                currentTargetIsDead = true;
            }

            int removedThreatElements = m_currentThreatList.RemoveAll(
                threatElement => threatElement.m_BaseCharacter.m_StatManagement.IsDead);

            if (currentTargetIsDead || removedThreatElements > 0)
            {
                UpdateThreatList();
            }
        }
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
        else
        {
            m_characterToControl.StopInteraction();
        }
    }

    /// <summary>
    /// Resorts the threat list.
    /// </summary>
    private void ReSortThreatList()
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
            // One cannot decrease threat, with no threat registered. No preemptive threat reduction!
            m_currentThreatList.Add(new ThreatElement(causer, Math.Max(0, threatChange)));
        }

        UpdateThreatList();
    }

    /// <summary>
    /// Updates the threat list.
    /// </summary>
    private void UpdateThreatList()
    {
        if (m_currentThreatList.Count > 0)
        {
            ReSortThreatList();

            ThreatElement newTarget = null;

            if (TargetChangeNecessary(out newTarget))
            {
                m_currentTarget = newTarget;
                m_characterToControl.OnInteraction(m_currentTarget.m_BaseCharacter);
            }
        }
        else
        {
            AttackClosestTarget();
        }
    }

    /// <summary>
    /// Determines whether the Ai should switch to a new target, based on the current threatlist.
    /// </summary>
    /// <param name="newTarget">The new target.</param>
    /// <returns></returns>
    private bool TargetChangeNecessary(out ThreatElement newTarget)
    {
        if (m_currentThreatList.Count > 0)
        {
            //TODO: Use InteractionTarget here?
            bool currentTargetFirstInThreatList = m_currentTarget != null && 
                string.Equals(m_currentThreatList[0].m_BaseCharacter.m_CharacterId, m_currentTarget.m_BaseCharacter.m_CharacterId);

            bool highestElementInThreatListReachedAggroLimit = m_currentTarget == null || 
                m_currentThreatList[0].m_CurrentThreatWithThisAi > (m_currentTarget.m_CurrentThreatWithThisAi *
                                                                BaseBalancing.m_TargetChangeByThreatDifference);

            if (!currentTargetFirstInThreatList && highestElementInThreatListReachedAggroLimit)
            {
                newTarget = m_currentThreatList[0];
                return true;
            }
        }

        newTarget = null;
        return false;
    }
}