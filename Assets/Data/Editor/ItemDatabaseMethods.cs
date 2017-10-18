#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ACA.Editor
{
    public partial class ItemDatabaseType<D, T> where D : ScriptableObjectDatabase<T> where T : Item, new()
    {
#if UNITY_EDITOR
        public void Replace(int index, T item)
        {
            database.Item[index] = item;
            EditorUtility.SetDirty(database);
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
#else
        public static U GetDatabase<U>(string dbName)
        {
        Debug.Log("Getting Database2");
            
            U db = Resources.Load
                (dbName, typeof(U))
                as U;
            return db;
        }
#endif

        public void Add(T item)
        {
            database.Item.Add(item);
            EditorUtility.SetDirty(database);
        }

        public void Insert(int index, T item)
        {
            database.Item.Insert(index, item);
            EditorUtility.SetDirty(database);
        }

        public void Remove(T item)
        {
            database.Item.Remove(item);
            EditorUtility.SetDirty(database);
        }

        public void RemoveAt(int item)
        {
            database.RemoveAt(item);
            EditorUtility.SetDirty(database);
        }
        void LoadDatabase()
        {
            string dbFullPath = @"Assets/" + dbPath + "/" + dbName;

            database = AssetDatabase.LoadAssetAtPath
                (dbFullPath, typeof(D))
                as D;
            if (database == null)
            {
                CreateDatabase(dbFullPath);
            }
        }

        void CreateDatabase(string dbFullPath)
        {
            if (!AssetDatabase.IsValidFolder(@"Assets/" + dbPath))
            {
                AssetDatabase.CreateFolder(@"Assets", dbPath);
            }
            database = ScriptableObject.CreateInstance<D>() as D;
            AssetDatabase.CreateAsset(database, dbFullPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
    }
}