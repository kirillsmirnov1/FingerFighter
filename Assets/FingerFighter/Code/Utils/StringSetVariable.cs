using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityUtils.Variables;

namespace FingerFighter.Utils
{
    [CreateAssetMenu(menuName = "Variables/StringSetVariable", fileName = "StringSetVariable", order = 0)]
    public class StringSetVariable : StringArrayVariable
    {
        private HashSet<string> _hashSet;
        private HashSet<string> HashSet
        {
            get
            {
                if (_hashSet == null)
                {
                    RegenerateHashSet();
                }

                return _hashSet;
            }
        }

        private void RegenerateHashSet()
        {
            _hashSet = new HashSet<string>(Value);
        }

        protected override void OnDataChanged()
        {
            RegenerateHashSet();
            base.OnDataChanged();
        }

        public void Add(string str)
        {
            HashSet.Add(str.ToLower());
            Value = _hashSet.ToArray();
        }

        public bool Has(string str)
            => HashSet.Contains(str.ToLower());
    }
}