using FingerFighter.Model.Combat;
using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Data/EnemyFormationPack", fileName = "EnemyFormationPack", order = 0)]
    public class EnemyFormationPack : ScriptableObject
    {
        [SerializeField] private EnemyStats boss;
        [SerializeField] private EnemyFormation[] formations;

        public EnemyFormation[] Formations
        {
            get => formations;
            set
            {
                SetPackDirty();
                formations = value;
            }
        }

        public EnemyStats Boss
        {
            get => boss;
            set
            {
                SetPackDirty();
                boss = value;
            } 
        }

        private void SetPackDirty()
        {
#if UNITY_EDITOR
            EditorUtility.SetDirty(this);
#endif
        }
    }
}