using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Slider : NoteLogic
{
    [Header("移动挂点")]
    public GameObject movePoint;
    [Header("移动开始挂点")]
    public GameObject moveStartPoint;
    [Header("移动目标挂点")]
    public GameObject moveTargetPoint;
    [Header("滚动球物体")]
    public GameObject rollBar;
    [Header("移动提示圈")]
    public RectTransform moveTipObj;

    public override void OnJudgetOperation()
    {
        movePoint.transform.position = moveStartPoint.transform.position;
        rollBar.gameObject.SetActive(true);

        Tweener tween = null;
        //Dotween可以很方便各种链接Update功能和Complete功能
        tween = movePoint.transform.DOMove(moveTargetPoint.transform.position, judgeTime).OnComplete(() =>
        {
            //当球移动到目标位置后调用

            float dValue = Mathf.Abs(startTime+judgeTime -curTime);

            //和点击圆圈的判定一样，其实也可以做不同的处理
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

            rollBar.gameObject.SetActive(false);
            SetCurState(eState.Over);

        }).OnUpdate(() =>
        {
            //在每一帧移动中判断鼠标是否超出跟随小球移动的黄色圆圈
            if (Vector3.Distance(Input.mousePosition, moveTipObj.position) > moveTipObj.sizeDelta.x * 0.5f)
            {
                if (tween != null)
                {
                    tween.Kill();
                }
                curScore = eScore.Fail;
                rollBar.gameObject.SetActive(false);
                SetCurState(eState.Over);
            }
        });
    }
 
}
