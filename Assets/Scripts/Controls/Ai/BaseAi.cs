using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAi : MonoBehaviour
{
    [SerializeField]
    private BaseCharacter m_characterToControl;

    private readonly Dictionary<string, double> m_currentThreat = new Dictionary<string, double>();
    private BaseCharacter m_currentTarget;
    private TargetingController m_targeting;

    void Start()
    {
        StartAiLoop();
    }

    /// <summary>
    /// Starts the ai loop.
    /// </summary>
    public void StartAiLoop()
    {
        m_targeting = ControllerContainer.TargetingController;

        StartCoroutine(BaseAiLoop());
    }

    /// <summary>
    /// Bases the ai loop.
    /// </summary>
    /// <returns></returns>
    private IEnumerator BaseAiLoop()
    {
        if (m_currentThreat.Count < 1)
        {
            BaseCharacter targetToAttack = m_targeting.GetClosestCharacter(m_characterToControl);

            if (targetToAttack != null)
            {
                m_currentTarget = targetToAttack;
                m_currentThreat.Add(targetToAttack.m_CharacterId, 1.0);

                m_characterToControl.OnInteraction(targetToAttack);
            }
        }
        else
        {
            var targetWithHighestThreat = GetCurrentTargetWithHighestThreat();

            if (!string.Equals(targetWithHighestThreat, m_currentTarget.m_CharacterId))
            {
                BaseCharacter characterToSwitchTo = m_targeting.GetCharacterById(targetWithHighestThreat);

                if (characterToSwitchTo != null)
                {
                    m_currentTarget = characterToSwitchTo;
                    m_characterToControl.OnInteraction(characterToSwitchTo);
                }
            }
        }

        yield return null;
    }

    /// <summary>
    /// Determines whether the current target has highest threat.
    /// </summary>
    /// <returns></returns>
    private string GetCurrentTargetWithHighestThreat()
    {
        double currentThreat = 0.0;

        m_currentThreat.TryGetValue(m_currentTarget.m_CharacterId, out currentThreat);

        foreach (var threatTarget in m_currentThreat)
        {
            if (!string.Equals(threatTarget.Key, m_currentTarget.m_CharacterId) && threatTarget.Value > currentThreat * BaseBalancing.m_TargetChangeByThreatDifference)
            {
                return threatTarget.Key;
            }
        }

        return m_currentTarget.m_CharacterId;
    }
}
