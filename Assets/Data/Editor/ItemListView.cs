using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ACA.Editor
{
    public partial class ItemDatabaseType<D, T> where D : ScriptableObjectDatabase<T> where T : Item, new()
    {
        int _selectedIndex = -1;
        T tItem;
        bool showDetails = false;
        Vector2 _scrollPos = Vector2.zero;
        public void ListView(Vector2 buttonSize, int _listViewWidth)
        {
            _scrollPos = GUILayout.BeginScrollView(_scrollPos, "Box", GUILayout.ExpandHeight(true), GUILayout.Width(_listViewWidth));

            for (int ix = 0; ix < database.Count; ix++)
            {
                if (GUILayout.Button(database.Get(ix).Name, "Box", GUILayout.Width(buttonSize.x), GUILayout.Height(buttonSize.y)))
                {
                    _selectedIndex = ix;
                    tItem = new T();
                    tItem.Clone(database.Get(ix));
                    showDetails = true;
                }
            }
            GUILayout.EndScrollView();
        }
    }
}