using UnityEngine;

namespace FingerFighter.Utils
{
    public static class Rigidbody2DExt // IMPR to UU 
    {
        public static void AddAcceleration(this Rigidbody2D rb, Vector2 force)
            => rb.AddForce(force * rb.mass, ForceMode2D.Force);

        public static void AddVelocityChange(this Rigidbody2D rb, Vector2 force)
            => rb.AddForce(force * rb.mass, ForceMode2D.Impulse);
    }
}