using UnityEditor;
using UnityEngine;

namespace FingerFighter.Model.EnemyFormations
{
    [CreateAssetMenu(menuName = "Data/EnemyFormationPack", fileName = "EnemyFormationPack", order = 0)]
    public class EnemyFormationPack : ScriptableObject
    {
        [SerializeField] private EnemyFormation[] formations;

        public EnemyFormation[] Formations
        {
            get => formations;
            set
            {
#if UNITY_EDITOR
                EditorUtility.SetDirty(this);
#endif
                formations = value;
            }
        }
    }
}