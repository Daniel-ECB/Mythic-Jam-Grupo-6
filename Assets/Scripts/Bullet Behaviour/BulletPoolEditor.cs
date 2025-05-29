using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomEditor(typeof(BulletPool))]
public class BulletPoolEditor : Editor
{
    public override void OnInspectorGUI()
    {
        BulletPool pool = (BulletPool)target;

        EditorGUILayout.LabelField("Bullet Prefabs por Tipo", EditorStyles.boldLabel);

        // Asegurarse de que hay una entrada por cada tipo
        if (pool.bulletPrefabs == null)
        {
            pool.bulletPrefabs = new List<BulletPool.BulletPrefabByType>();
        }

        foreach (BulletType type in System.Enum.GetValues(typeof(BulletType)))
        {
            int index = pool.bulletPrefabs.FindIndex(b => b.bulletType == type);

            BulletPool.BulletPrefabByType entry;
            if (index >= 0)
            {
                entry = pool.bulletPrefabs[index];
            }
            else
            {
                entry = new BulletPool.BulletPrefabByType { bulletType = type };
            }

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(type.ToString(), GUILayout.Width(100));
            entry.bulletPrefab = (GameObject)EditorGUILayout.ObjectField(entry.bulletPrefab, typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();

            if (index >= 0)
            {
                pool.bulletPrefabs[index] = entry;
            }
            else
            {
                pool.bulletPrefabs.Add(entry);
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(pool);
        }
    }
}
