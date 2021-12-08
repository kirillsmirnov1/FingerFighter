using FingerFighter.Model.Combat;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Data/EnemyFormationPack", fileName = "EnemyFormationPack", order = 0)]
    public class EnemyFormationPack : ScriptableObject
    {
        [SerializeField] private string id;
        [SerializeField] private ulong cost;
        [SerializeField] private EnemyStats boss;
        [SerializeField] private EnemyFormation[] formations;

        public string Id => id;
        public EnemyFormation[] Formations => formations;
        public EnemyStats Boss => boss;

        public void Overwrite(string newId, ulong newCost, EnemyFormation[] newFormations, EnemyStats newBoss)
        {
            (id, cost, formations, boss) = (newId, newCost, newFormations, newBoss);
            SetPackDirty();
        }

        private void SetPackDirty()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}