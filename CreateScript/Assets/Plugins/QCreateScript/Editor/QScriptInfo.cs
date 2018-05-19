using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CreateScript
{
    public class QScriptInfo : ScriptableObject
    {
        [SerializeField]
        private List<ClassInfo> list = new List<ClassInfo>();

        public void SetClassInfo(string className, FieldInfo[] infos)
        {
            foreach (var key in list)
            {
                if (key.className == className)
                {
                    key.fieldInfos = null;
                    key.fieldInfos = infos;
                    return;
                }
            }
            list.Add(new ClassInfo { className = className, fieldInfos = infos });
        }

        public FieldInfo[] GetFieldInfos(string className)
        {
            foreach (var key in list)
            {
                if (key.className == className)
                {
                    return key.fieldInfos;
                }
            }
            return null;
        }

        [Serializable]
        private class ClassInfo
        {
            public string className;
            public FieldInfo[] fieldInfos;
        }

        [Serializable]
        public class FieldInfo
        {
            public string name;
            public string type;
            public string path;
        }
    }
}