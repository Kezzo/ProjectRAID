using System.Collections.Generic;
using UnityEngine;

public class TargetingController
{
    private readonly Dictionary<string, BaseCharacter> m_targetCache = new Dictionary<string, BaseCharacter>();

    #region target cache manipulation

    /// <summary>
    /// Registers the in target cache.
    /// </summary>
    /// <param name="characterId">The mob identifier.</param>
    /// <param name="baseCharacter">The base character.</param>
    public void RegisterInTargetCache(string characterId, BaseCharacter baseCharacter)
    {
        if (m_targetCache.ContainsKey(characterId))
        {
            m_targetCache[characterId] = baseCharacter;
        }
        else
        {
            m_targetCache.Add(characterId, baseCharacter);
        }
    }

    /// <summary>
    /// Gets the character by identifier.
    /// </summary>
    /// <param name="characterId">The character identifier.</param>
    /// <returns></returns>
    public BaseCharacter GetCharacterById(string characterId)
    {
        BaseCharacter character = null;

        m_targetCache.TryGetValue(characterId, out character);

        return character;
    }

    #endregion

    /// <summary>
    /// Gets the closest character.
    /// </summary>
    /// <param name="askingPostion">The asking postion.</param>
    /// <param name="ignoreCharacterId">The ignore character identifier.</param>
    /// <returns></returns>
    public BaseCharacter GetClosestCharacter(Vector3 askingPostion, string ignoreCharacterId = null)
    {
        BaseCharacter closestCharacter = null;

        foreach (var target in m_targetCache)
        {
            if ((!string.IsNullOrEmpty(ignoreCharacterId) && !string.Equals(target.Key, ignoreCharacterId) &&
                (closestCharacter == null || Vector3.Distance(askingPostion, target.Value.transform.position) < Vector3.Distance(askingPostion, closestCharacter.transform.position))))
            {
                closestCharacter = target.Value;
            }
        }

        return closestCharacter;
    }

    /// <summary>
    /// Gets the closest character.
    /// </summary>
    /// <param name="askingCharacter">The asking character.</param>
    /// <returns></returns>
    public BaseCharacter GetClosestCharacter(BaseCharacter askingCharacter)
    {
        return GetClosestCharacter(askingCharacter.transform.position, askingCharacter.m_CharacterId);
    }
}
