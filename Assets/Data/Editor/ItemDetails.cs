#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ACA.Editor
{
    public partial class ItemDatabaseType<D, T> where D : ScriptableObjectDatabase<T> where T : Item, new()
    {
        public void ItemDetails()
        {
            GUILayout.BeginVertical("Box", GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));
            GUILayout.BeginVertical(GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(true));

            if (showDetails)
                tItem.OnGUI();


            GUILayout.EndVertical();
            GUILayout.Space(50f);
            GUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));

            DisplayButtons();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();

        }

        void DisplayButtons()
        {
            if (showDetails)
            {
                ButtonSave();
                if (_selectedIndex > -1)
                    ButtonDelete();
                ButtonCancel();
            }
            else
            {
                ButtonCreate();
            }
        }
        
        void ButtonCreate()
        {

            if (GUILayout.Button("Create " + strItemType))
            {
                tItem = new T();
                showDetails = true;
            }
        }

        void ButtonSave()
        {
            GUI.SetNextControlName("SaveButton");
            if (GUILayout.Button("Save"))
            {
                if (_selectedIndex == -1)
                {                    
                    Add(tItem);
                }
                else
                {
                    Replace(_selectedIndex, tItem);
                }
                showDetails = false;
                _selectedIndex = -1;

                tItem = null;

                GUI.FocusControl("SaveButton");

            }
        }

        void ButtonCancel()
        {
            if (GUILayout.Button("Cancel"))
            {
                tItem = null;
                showDetails = false;
                _selectedIndex = -1;
                GUI.FocusControl("SaveButton");
            }
        }

        void ButtonDelete()
        {
            if (GUILayout.Button("Delete"))
            {
                if (EditorUtility.DisplayDialog("Delete " + tItem.Name, "Are you sure that you want to delete " + tItem.Name + " from the database?", "Delete", "Cancel"))
                {
                    RemoveAt(_selectedIndex);
                    showDetails = false;
                    tItem = null;
                    _selectedIndex = -1;
                    GUI.FocusControl("SaveButton");
                }
            }
        }
    }
}