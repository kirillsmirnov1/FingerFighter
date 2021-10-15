using System;
using UnityEditor;

namespace FingerFighter.Model.Scenes
{
    [Serializable]
    public struct SceneNameReference // IMPR move to UU
    {
        public string sceneName;
#if UNITY_EDITOR
        public SceneAsset sceneAsset;
        public void SerializeName()
        {
            try
            {
                sceneName = sceneAsset.name;
            }
            catch (NullReferenceException) { }
        }
#endif
    }
}