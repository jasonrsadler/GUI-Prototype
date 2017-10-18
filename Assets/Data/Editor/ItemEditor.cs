using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ACA.Editor
{
    public class ItemEditor : EditorWindow
    {

        ItemDatabaseType<ItemDatabase, Item> itemDB =
            new ItemDatabaseType<ItemDatabase, Item>
            ("Item.asset");

        int _listViewWidth = 200;
        Vector2 buttonSize = new Vector2(190, 25);

        [MenuItem("ACA/Database/Item Editor %#i")]
        public static void Init()
        {
            ItemEditor window = EditorWindow.GetWindow<ItemEditor>();
            window.minSize = new Vector2(800, 600);
            window.titleContent = new GUIContent("Item Editor");
            window.Show();
        }

        void OnEnable()
        {
            itemDB.OnEnable("Item");
        }

        public void OnGUI()
        {
            itemDB.OnGUI(buttonSize, _listViewWidth);
        }
    }
}