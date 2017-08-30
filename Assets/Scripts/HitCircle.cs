using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HitCircle : NoteLogic
{

    public override void OnJudgetOperation()
    {
        //这里简单的通过时间的差值来判断得分
        float dValue = Mathf.Abs(curTime  - startTime);

        if (dValue < startTime * 0.35f)
        {
            curScore = eScore.Perfect;
        }
        else if (dValue < startTime * 0.7f)
        {
            curScore = eScore.Good;
        }
        else
        {
            curScore = eScore.Fail;
        }

        SetCurState(eState.Over);
    }
 
}
