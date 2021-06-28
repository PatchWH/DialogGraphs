using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using PWH.DialogSystem;
using XNodeEditor;

[CustomNodeEditor(typeof(DialogNode))]
public class DialogNodeEditor : NodeEditor
{
    bool expanded = false;

    public override void OnBodyGUI()
    {
        var node = (DialogNode)target;
        if (node == null)
            return;

        if (GUILayout.Button(expanded ? "Collapse" : "Expand"))
        {
            expanded = !expanded;
            EditorPrefs.SetBool(target.GetInstanceID().ToString(), expanded);
        }

        expanded = EditorPrefs.GetBool(target.GetInstanceID().ToString());

        if (expanded)
            base.OnBodyGUI();
        else
        {
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("entry"));
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("exit"));

            if(target is MessageNode)
            {
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("text"));
            }

            if(target is OpinionCheckNode)
            {
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("else"));
            }
        }
    }
}
