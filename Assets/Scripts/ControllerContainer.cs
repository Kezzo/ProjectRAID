public static class ControllerContainer
{
    private static TargetingController m_targetingController;
    public static TargetingController TargetingController
    {
        get { return m_targetingController ?? (m_targetingController = new TargetingController()); }
    }
}
