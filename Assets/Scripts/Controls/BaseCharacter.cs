using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionTarget
{
    Tank,
    Rogue,
    Heal,
    Mage,
    Boss,
    Add
}

public class BaseCharacter : MonoBehaviour
{
    private Coroutine m_currentMovementCoroutine;
    protected Coroutine m_CurrentInteractionCoroutine;

    protected InteractionTarget m_InteractionTarget;
    protected HashSet<InteractionTarget> m_PossibleInteractionTargets;

    protected float m_TimeSinceLastAutoInteraction;
    protected float m_AutoInteractionCD;

    void Awake()
    {
        InitializeBalancingParameter();
    }

    void Update()
    {
        m_TimeSinceLastAutoInteraction += Time.deltaTime;
    }

    /// <summary>
    /// Initializes the balancing parameter.
    /// </summary>
    public virtual void InitializeBalancingParameter()
    {
        //Initialize character specific balancing parameters in the child.
    }

    /// <summary>
    /// Called when the character was selected.
    /// </summary>
    public virtual void OnSelected()
    {
        //Debug.Log("Selected Character: "+this.name);
    }

    /// <summary>
    /// Called when an interaction with a target happened.
    /// </summary>
    /// <param name="interactionTarget">The interaction target.</param>
    public virtual void OnInteraction(BaseCharacter interactionTarget)
    {
        Debug.Log(string.Format("'{0}' is interacting with '{1}'", name, interactionTarget.name));

        StopMovement();
        LookAt(interactionTarget.transform.position);

        StartAutoInteraction(interactionTarget);
    }

    /// <summary>
    /// Starts the automatic heal.
    /// </summary>
    /// <param name="characterToInteractWith">The character to heal.</param>
    private void StartAutoInteraction(BaseCharacter characterToInteractWith)
    {
        if (m_CurrentInteractionCoroutine != null)
        {
            StopCoroutine(m_CurrentInteractionCoroutine);
        }

        m_CurrentInteractionCoroutine = StartCoroutine(AutoInteractionCoroutine(characterToInteractWith, m_AutoInteractionCD));
    }

    /// <summary>
    /// Moves to.
    /// </summary>
    /// <param name="worldPositionToMoveTo">The world position to move to.</param>
    public void MoveTo(Vector3 worldPositionToMoveTo)
    {
        //Debug.Log("Character: "+this.name+" moves to "+ worldPositionToMoveTo);

        //Stop previous movement.
        StopMovement();

        //Stop current interaction.
        StopInteraction();

        m_currentMovementCoroutine = StartCoroutine(MovementCoroutine(worldPositionToMoveTo));
    }

    /// <summary>
    /// Looks at.
    /// </summary>
    /// <param name="postitionToLookAt">The postition to look at.</param>
    private void LookAt(Vector3 postitionToLookAt)
    {
        transform.rotation = Quaternion.LookRotation((new Vector3(postitionToLookAt.x, 0f, postitionToLookAt.z) - 
            (new Vector3(transform.position.x, 0f, transform.position.z))).normalized);
    }

    /// <summary>
    /// Stops the movement.
    /// </summary>
    private void StopMovement()
    {
        if (m_currentMovementCoroutine != null)
        {
            StopCoroutine(m_currentMovementCoroutine);
        }
    }

    /// <summary>
    /// Stops the interaction.
    /// </summary>
    private void StopInteraction()
    {
        if (m_CurrentInteractionCoroutine != null)
        {
            StopCoroutine(m_CurrentInteractionCoroutine);
        }
    }

    /// <summary>
    /// Called when the automatic interaction happened.
    /// </summary>
    /// <param name="targetToInteractWith">The target to interact with.</param>
    protected virtual void OnAutoInteraction(BaseCharacter targetToInteractWith) {}

    /// <summary>
    /// The continuous heal coroutine.
    /// </summary>
    /// <param name="targetToInteractWith">The target to heal.</param>
    /// <param name="autoInteractionCd">The automatic interaction cd.</param>
    /// <returns></returns>
    private IEnumerator AutoInteractionCoroutine(BaseCharacter targetToInteractWith, float autoInteractionCd)
    {
        while (true)
        {
            if (m_TimeSinceLastAutoInteraction >= autoInteractionCd && 
                m_PossibleInteractionTargets.Contains(targetToInteractWith.m_InteractionTarget))
            {
                m_TimeSinceLastAutoInteraction = 0f;

                OnAutoInteraction(targetToInteractWith);
            }

            yield return null;
        }
    }

    /// <summary>
    /// Movements the numerator.
    /// </summary>
    /// <param name="movementTarget">The movement target.</param>
    /// <returns></returns>
    private IEnumerator MovementCoroutine(Vector3 movementTarget)
    {
        LookAt(movementTarget);

        while (true)
        {
            float currentDistance = Vector3.Distance(transform.position, movementTarget);
            float step = BaseBalancing.m_CharacterSpeed * Time.deltaTime;

            if (currentDistance < 1f)
            {
                yield break;
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, movementTarget, step);
            }

            yield return null;
        }
    }
}
