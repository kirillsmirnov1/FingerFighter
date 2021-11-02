namespace FingerFighter.Control.Combat.Health
{
    public class CompositeEnemyHealth : AHealth
    {
        private EnemyHealth[] _healths;
        public override float BaseHealth => _healths.Length * _healths[0].BaseHealth;

        protected override void OnEnable()
        {
            _healths = GetComponentsInChildren<EnemyHealth>();
            for (int i = 0; i < _healths.Length; i++)
            {
                _healths[i].onHealthChange += OnSegmentHealthChange;
            }
        }

        private void OnDisable()
        {
            for (int i = 0; i < _healths.Length; i++)
            {
                _healths[i].onHealthChange -= OnSegmentHealthChange;
            }
        }

        private void OnSegmentHealthChange(float currentSegmentHealth)
        {
            lock (Lock)
            {
                var health = 0f;
                for (var i = 0; i < _healths.Length; i++)
                {
                    health += _healths[i].CurrentHealth;
                }
                CurrentHealth = health;
            }
            NotifyOnHealthChange();
        }
    }
}