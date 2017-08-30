using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnAround :NoteLogic
{

    public RectTransform uiDisk;
 
    public EventTriggerListener eventTrigger;

    Vector2 perTouchDir;

    Vector2 uiDiskPos;

    float curAngle;

    public override void OnJudgetOperation()
    {
        eventTrigger.onBeginDrag = BegionTurn;
        eventTrigger.onDrag = Turn;
        uiDiskPos = uiDisk.transform.position;

        StartCoroutine(OnDelayCall(judgeTime));
    }


    IEnumerator OnDelayCall(float rTime)
    {
        yield return new WaitForSeconds(rTime);
        JudgetStore();
    }


    public void BegionTurn(PointerEventData rData)
    {
         
        perTouchDir = rData.position - uiDiskPos;
    }

    public void Turn(PointerEventData rData)
    {
        var curTouchDir = rData.position - uiDiskPos;


        var crossValue = Vector3.Cross(perTouchDir, curTouchDir);

        float angle = -Vector3.Angle(perTouchDir, curTouchDir);


        if (crossValue.z > 0)
        {
            angle = 0;
        }
       

        uiDisk.transform.Rotate(new Vector3(0, 0, angle));

        curAngle -= angle;

        perTouchDir = curTouchDir;
     
        circleTipObj.gameObject.transform.localScale = Vector3.one * Mathf.Lerp(10,0, curAngle / targetValue);

        if (curAngle > targetValue)
        {
            JudgetStore();
        }
    }

    public void JudgetStore()
    {
        if (curState == eState.Over)
        {
            return;
        }

        if (curAngle > targetValue)
        {
            curScore = eScore.Perfect;
        }
        else if (curAngle > targetValue*0.6f)
        {
            curScore = eScore.Good;
        }
        else 
        {
            curScore = eScore.Fail;
        }

        StopCoroutine("OnDelayCall");
        SetCurState(eState.Over);
    }


}
