using FingerFighter.Model.LevelMaps;
using UnityEngine;

namespace FingerFighter.View.LevelMaps
{
    public class LevelMapDisplay : MonoBehaviour
    {
        [SerializeField] private LevelMapVariable levelMapVariable;
        [SerializeField] private GameObject roomMarkPrefab;
        [SerializeField] private GameObject connectionPrefab;

        private void Awake()
        {
            SpawnMap();
        }

        public void SpawnMap()
        {
            Debug.Log("TODO spawn map");
            ClearMap();
            // TODO spawn marks
            // TODO spawn connections 
        }

        public void ClearMap()
        {
            Debug.Log("TODO clean map");
        }
    }
}