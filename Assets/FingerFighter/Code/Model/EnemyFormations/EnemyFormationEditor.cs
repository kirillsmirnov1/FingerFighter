﻿using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    public class EnemyFormationEditor : MonoBehaviour
    {
        [SerializeField] private EnemyFormationPackEditor packEditor;
        [SerializeField] public EnemyFormation formation;
        
        private void OnValidate()
        {
            packEditor = GetComponentInParent<EnemyFormationPackEditor>();
            UpdatePack();
        }

        public void UpdatePack() => packEditor?.UpdatePack();

        private void OnDrawGizmosSelected()
        {
            DrawBorderRect();
            DrawFormationEntries();
        }

        private void DrawFormationEntries()
        {
            for (int i = 0; i < formation.entries.Length; i++)
            {
                DrawFormationEntry(i);
            }
        }

        private void DrawFormationEntry(int index)
        {
            var formationEntry = formation.entries[index];
            
            if (formationEntry.enemy == null)
            {
                Debug.LogWarning($"No stats for {gameObject.name}:{index}");
                return;
            }
            
            Gizmos.DrawIcon(formationEntry.pos, AssetDatabase.GetAssetPath(formationEntry.enemy.gizmoIcon));
        }

        private static void DrawBorderRect()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(Vector3.zero, 10 * Vector3.one);
        }
    }
}