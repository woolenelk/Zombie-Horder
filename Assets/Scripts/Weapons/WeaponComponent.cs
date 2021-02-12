using System;
using Character;
using UnityEngine;



namespace Weapons
{
    public enum WeaponType
    {
        None,
        Pistol,
        MachineGun
    }

    [Serializable]
    public struct WeaponStats
    {
        public WeaponType type;
        public string WeaponName;
        public float Damage;
        public int BulletsInClip;
        public int ClipSize;
        public int BulletsAvailable;

        public float FireStartDelay;
        public float FireRate;
        public float FireDistance;
        public bool Repeating;

        public LayerMask WeaponHitLayer;
    }
    
    public class WeaponComponent : MonoBehaviour
    {
        public Transform HandPosition => GripIKLocation;
        [SerializeField] 
        private Transform GripIKLocation;
        [SerializeField]
        public WeaponStats WeaponStats;

        public bool Firing { get; private set; }
        public bool Reloading { get; private set; }


        protected WeaponHolder WeaponHolder;
        protected CrosshairScript Crosshair;
        protected Camera MainCamera;

        private void Awake()
        {
            MainCamera = Camera.main;
        }

        public void Initialize(WeaponHolder weaponHolder, CrosshairScript crosshair)
        {
            WeaponHolder = weaponHolder;
            Crosshair = crosshair;
        }


        public virtual void StartFiring()
        {
            Firing = true;
            if (WeaponStats.Repeating)
            {
                InvokeRepeating(nameof(FireWeapon),WeaponStats.FireStartDelay, WeaponStats.FireRate);
            }
            else
            {
                FireWeapon();
            }
        }
        
        public virtual void StopFiring()
        {
            Firing = false;
            CancelInvoke(nameof(FireWeapon));
        }

        protected virtual void FireWeapon()
        {
            Debug.Log("Firing Weapon");
            WeaponStats.BulletsInClip--;
        }

        public virtual void StartReloading()
        {
            Reloading = true;
            ReloadWeapon();
        }
        
        public virtual void StopReloading()
        {
            Reloading = false;
        }

        protected virtual void ReloadWeapon()
        {
            int bulletsToReload = WeaponStats.ClipSize- WeaponStats.BulletsAvailable ;
            if (bulletsToReload < 0)
            {
                WeaponStats.BulletsInClip = WeaponStats.ClipSize;
                WeaponStats.BulletsAvailable -= WeaponStats.ClipSize;
            }
            else
            {
                WeaponStats.BulletsInClip = WeaponStats.BulletsAvailable;
                WeaponStats.BulletsAvailable = 0;
            }
        }

     
    }
}
