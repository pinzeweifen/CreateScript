using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CreateScript
{
    public class QDeleteNothingSelected : Editor
    {
        [MenuItem("Assets/NothingSelected")]
        static void Init()
        {
            var array = Selection.gameObjects;
            if (array == null)
            {
                EditorUtility.DisplayDialog(QConfigure.msgTitle, QConfigure.noSelect, QConfigure.ok);
                return;
            }
            
            foreach(var item in array)
            {
                DeleteComponent(item.transform);
                WhileTr(item.transform);
            }
        }
        
        private static void WhileTr(Transform target)
        {
            foreach (Transform tr in target)
            {
                DeleteComponent(tr);
                if (tr.childCount > 0)
                {
                    WhileTr(tr);
                }
            }
        }

        private static void DeleteComponent(Transform tr)
        {
            var coms = tr.GetComponents<Component>();
            var count = coms.Length;
            SerializedObject so = new SerializedObject(tr.gameObject);
            var soProperties = so.FindProperty("m_Component");

            for (int i = count-1; i >=0; i--)
            {
                if (coms[i] == null)
                {
                    soProperties.DeleteArrayElementAtIndex(i);
                }
            }
            so.ApplyModifiedProperties();
        }
    }
}
