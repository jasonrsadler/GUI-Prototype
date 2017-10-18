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
        [SerializeField]
        D database;
        [SerializeField]
        string dbName;
        [SerializeField]
        string dbPath = @"Data";
        public string strItemType = "Item";

        public ItemDatabaseType(string n)
        {
            dbName = n;
        }

        public void OnEnable(string itemType)
        {
            strItemType = itemType;
            if (database == null)
            {
                LoadDatabase();
            }
        }

        public void OnGUI(Vector2 buttonSize, int _listViewWidth)
        {
            ListView(buttonSize, _listViewWidth);
            ItemDetails();


        }
    }
}