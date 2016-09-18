using UnityEngine;

public class BaseMeeleCharacter : BaseCharacter
{
    //[Header("BaseMeele")]

    protected override void OnAutoInteractionTriggered(BaseCharacter targetToInteractWith)
    {
        base.OnAutoInteractionTriggered(targetToInteractWith);

        m_animator.SetTrigger("Attack");
    }
}
