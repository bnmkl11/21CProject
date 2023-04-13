// Create By 23.04.13 JONGCHAN PARK

using System;

public class UIAppraise : UIBase
{
    public enum AppraisePhaseState
    {
        AppraisingStart,
        Appraising,
        AppraisingEnd,
    }

    private AppraisePhaseState m_CurrentAppraisingState = AppraisePhaseState.Appraising;

    #region Override

    public override void Initialize()
    {
        base.Initialize();
        
        // PoolingManager.Instance.CreatePool<UIAppraiseBaseTool>();
    }

    #endregion
    
    #region Update

    public void UpdateState(AppraisePhaseState state)
    {
        m_CurrentAppraisingState = state;
    
        switch (state)
        {
            case AppraisePhaseState.AppraisingStart:
                break;
            case AppraisePhaseState.Appraising:
                break;
            case AppraisePhaseState.AppraisingEnd:
                break;
        }
    }

    #endregion
}