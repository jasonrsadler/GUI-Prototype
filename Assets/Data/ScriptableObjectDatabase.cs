#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace ACA
{

    public class ScriptableObjectDatabase<T> : ScriptableObject where T : class
    {
        [SerializeField]
        protected List<T> _item = new List<T>();

        public List<T> Item
        {
            get
            {
                return _item;
            }

        }

        public int Count
        {
            get
            {
                return Item.Count;
            }
        }

        public T Get(int index)
        {
            return Item.ElementAt(index);
        }


#if UNITY_EDITOR
        public void Add(T i)
        {
            _item.Add(i);
            EditorUtility.SetDirty(this);
        }

        public void Insert(int index, T i)
        {
            _item.Insert(index, i);
            EditorUtility.SetDirty(this);
        }

        public void Remove(T i)
        {
            _item.Remove(i);
            EditorUtility.SetDirty(this);
        }

        public void RemoveAt(int index)
        {
            _item.RemoveAt(index);
            EditorUtility.SetDirty(this);
        }
#endif

#if UNITY_EDITOR
        public void Replace(int index, T i)
        {
            _item[index] = i;
            EditorUtility.SetDirty(this);
        }



        public static U GetDatabase<U>(string dbPath, string dbName) where U : ScriptableObject
        {
            string dbFullPath = @"Assets/" + dbPath + "/" + dbName;

            U db = AssetDatabase.LoadAssetAtPath
                (dbFullPath, typeof(U))
                as U;
            if (db == null)
            {
                if (!AssetDatabase.IsValidFolder(@"Assets/" + dbPath))
                {
                    AssetDatabase.CreateFolder(@"Assets", dbPath);
                }
                db = ScriptableObject.CreateInstance<U>() as U;
                AssetDatabase.CreateAsset(db, dbFullPath);

                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            return db;
        }
#endif
    }
}
