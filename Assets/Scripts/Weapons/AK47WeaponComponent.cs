using System;
using UnityEngine;

namespace Weapons
{
    public class AK47WeaponComponent : WeaponComponent
    {

        private Vector3 HitLocation;


        protected override void FireWeapon()
        {
            
            if (WeaponStats.BulletsInClip > 0 && !WeaponHolder.PlayerController.IsReloading && !WeaponHolder.PlayerController.IsRunning)
            {
                base.FireWeapon();
                Ray screenRay = MainCamera.ScreenPointToRay(new Vector3(Crosshair.CurrentMousePosition.x,
                        Crosshair.CurrentMousePosition.y, 0));

                if (!Physics.Raycast(screenRay, out RaycastHit hit,
                    WeaponStats.FireDistance, WeaponStats.WeaponHitLayer))
                    return;

                HitLocation = hit.point;
                Vector3 hitDirection = hit.point - MainCamera.transform.position;

                //Debug.DrawRay(MainCamera.transform.position, hitDirection * WeaponStats.FireDistance, Color.red);
                Debug.DrawLine(MainCamera.transform.position, HitLocation, Color.red, WeaponStats.FireRate);
            }
            else if (WeaponStats.BulletsInClip <= 0)
            {
                if (!WeaponHolder)
                    return;
                WeaponHolder.StartReloading();
            }

        }

        private void OnDrawGizmos()
        {
            if (HitLocation == Vector3.zero)
                return;

            Gizmos.DrawWireSphere(HitLocation, 0.25f);
        }

        //protected new void FireWeapon()
        //{
        //    Debug.Log("Firing Weapon");

        //    if (WeaponStats.BulletsInClip > 0 && !Reloading)
        //    {
        //        Ray screenRay = ViewCamera.ScreenPointToRay(new Vector3(Crosshair.CurrentMousePosition.x,
        //            Crosshair.CurrentMousePosition.y, 0));

        //        if (Physics.Raycast(screenRay, out RaycastHit hit, WeaponStats.FireDistance,
        //            WeaponStats.WeaponHitLayer))
        //        {
        //            Vector3 RayDirection = HitLocation.point - ViewCamera.transform.position;

        //            Debug.DrawRay(ViewCamera.transform.position, RayDirection * WeaponStats.FireDistance, Color.red);

        //            HitLocation = hit;
        //        };

        //        WeaponStats.BulletsInClip--;
        //    }
        //    else
        //    {
        //        StartReloading();
        //    }
        //}

        
    }
}
