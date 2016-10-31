using System.Collections.Generic;
using UnityEngine;

public class BaseAreaEffect : MonoBehaviour
{
    protected float m_EffectRange = 0f;
    protected float m_EffectActivationDelay = 0f;
    protected int m_EffectDamage = 0;

    protected HashSet<InteractionTarget> m_PossibleTargets;

    /// <summary>
    /// Initializes the parameters.
    /// </summary>
    public virtual void InitializeBalancingParameter()
    {
        //Initialize character specific parameters in the child.
    }

    /// <summary>
    /// Starts the area effect.
    /// </summary>
    public void StartAreaEffect()
    {
        Root.Instance.CoroutineHelper.CallDelayed(this, m_EffectActivationDelay, () =>
        {
            //TODO: Improve effect range based on visuals
            var enemiesToDamage = ControllerContainer.TargetingController.GetAllCharactersInCircleArea(this.transform.position, m_EffectRange,
                m_PossibleTargets);

            OnEffectTrigger(enemiesToDamage);
        });
    }

    /// <summary>
    /// Damages all given characters.
    /// </summary>
    /// <param name="enemiesToApplyEffectOn">The enemies to damage.</param>
    protected virtual void OnEffectTrigger(List<BaseCharacter> enemiesToApplyEffectOn)
    {
        
    }
}
