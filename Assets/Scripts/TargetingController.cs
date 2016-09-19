using System.Collections.Generic;
using UnityEngine;

public class TargetingController
{
    private readonly List<BaseCharacter> m_targetCache = new List<BaseCharacter>();

    #region target cache manipulation

    /// <summary>
    /// Registers the in target cache.
    /// </summary>
    /// <param name="baseCharacter">The base character.</param>
    public void RegisterInTargetCache(BaseCharacter baseCharacter)
    {
        m_targetCache.Add(baseCharacter);   
    }

    /// <summary>
    /// Gets the character by identifier.
    /// </summary>
    /// <param name="characterId">The character identifier.</param>
    /// <returns></returns>
    public BaseCharacter GetCharacterById(string characterId)
    {
        return m_targetCache.Find(character => character.m_CharacterId == characterId);
    }

    #endregion

    /// <summary>
    /// Gets the closest character.
    /// </summary>
    /// <param name="askingPosition">The asking position.</param>
    /// <param name="interactionTargetsToIgnore">The interaction targets to ignore.</param>
    /// <param name="includeDead">if set to <c>true</c> [include dead].</param>
    /// <returns></returns>
    public BaseCharacter GetClosestCharacter(Vector3 askingPosition, HashSet<InteractionTarget> interactionTargetsToIgnore = null, bool includeDead = false)
    {
        BaseCharacter closestCharacter = null;

        for (int targetIndex = 0; targetIndex < m_targetCache.Count; targetIndex++)
        {
            var target = m_targetCache[targetIndex];

            if (!includeDead && target.m_StatManagement.IsDead)
            {
                continue;
            }

            if (interactionTargetsToIgnore != null && (interactionTargetsToIgnore.Contains(target.InteractionTarget)))
            {
                continue;
            }
                
            if (closestCharacter == null || 
                Vector3.Distance(askingPosition, target.transform.position) <
                Vector3.Distance(askingPosition, closestCharacter.transform.position))
            {
                closestCharacter = target;
            }
        }

        return closestCharacter;
    }

    /// <summary>
    /// Gets all characters in a circle area.
    /// </summary>
    /// <param name="basePosition">The base position.</param>
    /// <param name="circleRange">The circle range.</param>
    /// <param name="validInteractionTargets">The valid interaction targets.</param>
    /// <returns></returns>
    public List<BaseCharacter> GetAllCharactersInCircleArea(Vector3 basePosition, float circleRange, HashSet<InteractionTarget> validInteractionTargets)
    {
        List<BaseCharacter> validTargets = GetCharactersWithInteractionTarget(validInteractionTargets);
        List<BaseCharacter> targetsInRange = new List<BaseCharacter>();

        for (int characterIndex = 0; characterIndex < validTargets.Count; characterIndex++)
        {
            if (Vector3.Distance(basePosition, validTargets[characterIndex].transform.position) <= circleRange)
            {
                targetsInRange.Add(validTargets[characterIndex]);
            }
        }

        return targetsInRange;
    }

    /// <summary>
    /// Gets the closest character.
    /// </summary>
    /// <param name="askingCharacter">The asking character.</param>
    /// <param name="includeDead">if set to <c>true</c> [include dead].</param>
    /// <returns></returns>
    public BaseCharacter GetClosestCharacter(BaseCharacter askingCharacter, bool includeDead = false)
    {
        return GetClosestCharacter(askingCharacter.transform.position, new HashSet<InteractionTarget>{askingCharacter.InteractionTarget}, includeDead);
    }

    /// <summary>
    /// Gets the characters with interaction target.
    /// </summary>
    /// <param name="interactionTargetsToReturn">The interaction targets to return.</param>
    /// <param name="includeDead">if set to <c>true</c> [include dead].</param>
    /// <returns></returns>
    public List<BaseCharacter> GetCharactersWithInteractionTarget(HashSet<InteractionTarget> interactionTargetsToReturn, bool includeDead = false)
    {
        List<BaseCharacter> charactersWithInteractionTarget = new List<BaseCharacter>();

        for (int targetIndex = 0; targetIndex < m_targetCache.Count; targetIndex++)
        {
            var target = m_targetCache[targetIndex];

            if (!includeDead && target.m_StatManagement.IsDead)
            {
                continue;
            }

            if (interactionTargetsToReturn.Contains(target.InteractionTarget))
            {
                charactersWithInteractionTarget.Add(target);
            }
        }

        return charactersWithInteractionTarget;
    }
}
