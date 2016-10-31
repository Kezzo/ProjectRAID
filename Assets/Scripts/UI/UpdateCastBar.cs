using UnityEngine;
using UnityEngine.UI;

public class UpdateCastBar : MonoBehaviour
{
    [SerializeField]
    private BaseCasterCharacter m_caster;

    [SerializeField]
    private Image m_castBar;

    /// <summary>
    /// Updates this instance.
    /// </summary>
    private void Update()
    {
        if (m_caster.CurrentCasting != null && m_caster.CurrentCasting.m_IsCasting)
        {
            float timeSinceCastStart = Time.time - m_caster.CurrentCasting.m_CastStartTime;

            m_castBar.fillAmount = (float)timeSinceCastStart / (float)m_caster.CurrentCasting.m_CastTime;
        }
        else
        {
            m_castBar.fillAmount = 0f;
        }
    }
}
