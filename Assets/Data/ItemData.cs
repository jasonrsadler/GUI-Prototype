using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Text;

namespace ACA
{
    public static class ItemData
    {

        public static List<Item> items = new List<Item>();
        public static List<ItemTransform> itemXForms = new List<ItemTransform>();

#if UNITY_EDITOR
        public static List<Item> Load()
        {
            try
            {
                string DATABASE_NAME = @"Data\ItemData.asset";
                string DATABASE_PATH = @"Resources";
                ItemDatabase _itemsDatabase = ItemDatabase.GetDatabase<ItemDatabase>(DATABASE_NAME, DATABASE_PATH);
                for (int ix = 0; ix < _itemsDatabase.Count; ix++)
                {
                    items.Add(new Item
                        (_itemsDatabase.Item[ix].Name, _itemsDatabase.Item[ix].ID, _itemsDatabase.Item[ix].Prefab));
                }
                return items;
            }
            catch (Exception ex)
            {
                Debug.Log(ex.Message);
                return null;
            }
        }
#endif

        public static Item GetItemByID(int id)
        {
            return items[id - 1];
        }

        public static List<Item> LoadData(ItemDatabase itemData)
        {
            items = new List<Item>();
            for (int ix = 0; ix < itemData.Count; ix++)
            {
                items.Add(new Item(itemData.Item[ix].Name, itemData.Item[ix].ID, itemData.Item[ix].Prefab));
            }
            return items;
        }



        public static List<ItemTransform> UpdateItem(ItemTransform iExt)
        {

            for (int ix = 0; ix < itemXForms.Count; ix++)
            {

                if (itemXForms[ix].ID == iExt.ID)
                {
                    itemXForms[ix] = iExt;
                    Debug.Log("in" + itemXForms[ix].Position);
                }
                else
                    Debug.Log(itemXForms[ix].Position);

            }

            UpdateData();
            return itemXForms;
        }

        internal static List<ItemTransform> InitializeTransformData()
        {
            for (int ix = 0; ix < items.Count; ix++)
            {
                itemXForms.Add(new ItemTransform
                {
                    ID = items[ix].ID,
                    Position = items[ix].Prefab.transform.position,
                    Rotation = items[ix].Prefab.transform.rotation,
                    Scale = items[ix].Prefab.transform.localScale
                });

            }
            Debug.Log("Initialized " + itemXForms.Count + " itemXForms");
            UpdateData();
            return itemXForms;
        }

        internal static List<ItemTransform> LoadTransformData(List<ItemTransform> itemTransform)
        {
            if (File.Exists(Application.persistentDataPath + "/iData.dat"))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(Application.persistentDataPath + "/iData.dat", FileMode.Open);
                itemXForms = (List<ItemTransform>)bf.Deserialize(file);
                file.Close();
            }
            for (int ix = 0; ix < itemXForms.Count; ix++)
            {
                Debug.Log("Position: " + itemXForms[ix].Position);
            }
            return itemXForms;
        }

        public static void UpdateData()
        {
            List<ItemTransform> iExt = new List<ItemTransform>();
            Debug.Log("UpdateData " + items.Count.ToString() + " Items and " + itemXForms.Count.ToString() + " ItemXForms");
            for (int ix = 0; ix < items.Count; ix++)
            {
                iExt.Add(itemXForms[ix]);
                Debug.Log("Position before write: " + itemXForms[ix].Position);

            }
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/iData.dat");
            bf.Serialize(file, iExt);
            file.Close();
        }

        // O log(2n) -- refactor later
        //private static List<Item> UpdateItems(List<ItemTransform> itemData)
        //{
        //    for (int ix = 0; ix < items.Count; ix++)
        //    {
        //        for (int jx = 0; jx < itemData.Count; jx++)
        //        {
        //            if (items[ix].ID == itemData[jx].ID)
        //            {
        //                items[ix] = itemData[jx];
        //            }
        //        }
        //    }
        //    return items;
        //}
    }

}