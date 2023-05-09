//
// Created on Sun Mar 07 2023
//
// Copyright (c) 2023 JONGCHAN PARK
//

using System;
using System.Collections.Generic;

public class UIAppraise : UIBase
{
    public enum AppraisePhaseState
    {
        AppraisingStart,
        Appraising,
        AppraisingEnd,
    }

    private AppraisePhaseState m_CurrentAppraisingState = AppraisePhaseState.Appraising;

    private List<UIAppraiseBaseTool> m_ListOfAppraiseTool;

    private ToolData.kTOOL_TYPE m_CurrentTool;

    #region Override

    public override void Initialize()
    {
        base.Initialize();
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

    public void UpdateAppraiseTool(ToolData.kTOOL_TYPE toolType)
    {
        m_CurrentTool = toolType;
    }

    #endregion

    #region Func

    public ToolData.kTOOL_TYPE GetCurrentTool()
    { 
        return m_CurrentTool; 
    }

    #endregion
}