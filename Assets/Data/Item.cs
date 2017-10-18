using System;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ACA
{
    public enum Category
    {
        Challenge = 0, Continuous = 1
    }

    [Serializable]
    public class Item
    {
        public static List<Item> itemList = new List<Item>();

        public static Item current;
        [SerializeField]
        string _name;

        [SerializeField]
        string _id;

        [SerializeField]
        GameObject _prefab;

        public Item()
        {
            
        }

        public Item(string id, string name, GameObject prefab)
        {
            _id = id;
            _name = name;
            _prefab = prefab;
        }

        public void Clone(Item itemToClone)
        {
            _id = itemToClone.ID;
            _name = itemToClone.Name;
            _prefab = itemToClone.Prefab;
        }



        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public GameObject Prefab
        {
            get { return _prefab; }
            set { _prefab = value; }
        }
        
        public ItemDatabase ItemDatabase
        {
            get { return _itemDatabase; }
            set { _itemDatabase = value; }
        }
        ItemDatabase _itemDatabase;


#if UNITY_EDITOR
        public virtual void OnGUI()
        {
            EditorGUILayout.BeginVertical();
            ID = EditorGUILayout.TextField("UID: ", _id);
            Name = EditorGUILayout.TextField("Item Name: ", _name);
            Prefab = (GameObject)EditorGUILayout.ObjectField(_prefab, typeof(GameObject), true);
            EditorGUILayout.EndVertical();
        }

        public void LoadItemDatabase()
        {
            string DATABASE_NAME = @"Data\ItemData.asset";
            string DATABASE_PATH = @"Data";

            _itemDatabase = ItemDatabase.GetDatabase<ItemDatabase>(DATABASE_PATH, DATABASE_NAME);

            option = new string[_itemDatabase.Count];
            for (int ix = 0; ix < _itemDatabase.Count; ix++)
            {
                option[ix] = _itemDatabase.Get(ix).Name;
            }
        }
        string[] option;
#endif
    }

}
