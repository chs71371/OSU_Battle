using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Page_GamePlay))]
public class LevelEditor :  Editor
{
    Page_GamePlay mScript;

    void OnEnable()
    {
        mScript = target as Page_GamePlay;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("添加Note"))
        {
            mScript.levelNoteList.Add(new LevelNoteData());
        }

        for (int i = 0; i < mScript.levelNoteList.Count; i++)
        {
            var note = mScript.levelNoteList[i];

            note.showIndex = EditorGUILayout.IntField("顺序编号", note.showIndex);
            note.curType = (LevelNoteData.eNoteType)EditorGUILayout.EnumPopup("类型", note.curType);
            note.position = EditorGUILayout.Vector3Field("位置", note.position);
            note.eulerAngles = EditorGUILayout.Vector3Field("角度", note.eulerAngles);
            note.creatTime = EditorGUILayout.FloatField("创建时间", note.creatTime);
            note.delayTime = EditorGUILayout.FloatField("延迟开始时间", note.delayTime);
            note.startTime = EditorGUILayout.FloatField("响应时间", note.startTime);
            note.operationTime = EditorGUILayout.FloatField("操作时间", note.operationTime);
            note.desTime = EditorGUILayout.FloatField("删除时间", note.desTime);
            note.targetValue = EditorGUILayout.FloatField("目标值", note.targetValue);
            if (GUILayout.Button("删除该节点"))
            {
                mScript.levelNoteList.RemoveAt(i);
                break;
            }

        }



        if (GUI.changed)
        {
            GUI.changed = false;
            EditorUtility.SetDirty(mScript);
            Undo.RecordObjects(new UnityEngine.Object[] { mScript }, mScript.name);
        }
    }

   
}
