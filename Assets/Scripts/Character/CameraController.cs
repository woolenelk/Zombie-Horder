using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CameraController : MonoBehaviour
    {
        
        [SerializeField] 
        private float RotationSpeed = 1f;
        [SerializeField] 
        private float HorizontalDamping = 1f;
        [SerializeField]
        private GameObject FollowTarget;

        private Transform FollowTargetTransform;

        private Vector2 PreviousMouseDelta;
        
        // Start is called before the first frame update
        private void Start()
        {
            FollowTargetTransform = FollowTarget.transform;
            PreviousMouseDelta = Vector2.zero;
        }

        //public void OnLook(InputAction.CallbackContext obj)
        //{
        //    Vector2 aimValue = obj.ReadValue<Vector2>();

        //    Quaternion addedRotation= Quaternion.AngleAxis(Mathf.Lerp(PreviousMouseDelta.x, aimValue.x, 1f / HorizontalDamping) * RotationSpeed,
        //            transform.up
        //        );
        //    FollowTargetTransform.rotation *= addedRotation;

        //    PreviousMouseDelta = aimValue;

        //    transform.rotation = Quaternion.Euler(0, FollowTargetTransform.rotation.eulerAngles.y, 0);

        //    FollowTargetTransform.localEulerAngles = Vector3.zero;

        //}



        public void OnLook(InputValue delta)
        {
            Vector2 aimValue = delta.Get<Vector2>();

            FollowTargetTransform.rotation *=
                Quaternion.AngleAxis(
                    Mathf.Lerp(PreviousMouseDelta.x, aimValue.x, 1f / HorizontalDamping) * RotationSpeed,
                    transform.up
                );
            PreviousMouseDelta = aimValue;

            transform.rotation = Quaternion.Euler(0, FollowTargetTransform.rotation.eulerAngles.y, 0);

            FollowTargetTransform.localEulerAngles = Vector3.zero;

            
        }
    }
}
