using System.Collections;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    private Coroutine m_currentMovementCoroutine;

    protected Coroutine m_CurrentInteractionCoroutine;
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

        m_currentMovementCoroutine = StartCoroutine(MovementNumerator(worldPositionToMoveTo));
    }

    /// <summary>
    /// Looks at.
    /// </summary>
    /// <param name="postitionToLookAt">The postition to look at.</param>
    private void LookAt(Vector3 postitionToLookAt)
    {
        transform.rotation = Quaternion.LookRotation((postitionToLookAt - transform.position).normalized);
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
    /// Movements the numerator.
    /// </summary>
    /// <param name="movementTarget">The movement target.</param>
    /// <returns></returns>
    private IEnumerator MovementNumerator(Vector3 movementTarget)
    {
        LookAt(movementTarget);

        while (true)
        {
            float currentDistance = Vector3.Distance(transform.position, movementTarget);
            float step = BaseBalancing.CharacterSpeed * Time.deltaTime;

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
