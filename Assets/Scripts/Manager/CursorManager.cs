using Enum;
using RepeatUtil.DesignPattern.SingletonPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Jobs;
using UnityEngine;

namespace Manager
{
    public class CursorManager : Singleton<CursorManager>
    {
        [SerializeField]
        private List<CursorAnimation> cursorAnimationList;

        [SerializeField]
        private GunSelector playerGunSelector;

        [SerializeField]
        private AimShoot playerAimShoot;

        private int currentFrame;
        private int frameCount;
        private float frameTimer;
        private CursorAnimation currentCursorAnimation;

        protected override void Awake()
        {
            base.Awake();
            playerGunSelector.OnSwitchGun += PlayerGunSelector_OnSwitchGun;
        }

        private void PlayerGunSelector_OnSwitchGun(object sender, GunSelector.OnSwitchGunEventArgs e)
        {
            int indexCursor = FindIndexCursorByShootType(e.GunSO.ShootType);
            if(indexCursor == -1)
            {
                Debug.LogError("Doesn't find " + e.GunSO.ShootType.ToString() + " cursor");
            }
            else
            {
                SetActiveCursor(cursorAnimationList[indexCursor]);
            }
        }

        private int FindIndexCursorByShootType(ShootType shootType)
        {
            for (int i = 0; i < cursorAnimationList.Count; i++)
            {
                if (cursorAnimationList[i].ShootType == shootType) return i;
            }
            return -1;
        }


        private void Update()
        {
            if (frameCount <= 1) return;

            if(currentCursorAnimation.ShootType == ShootType.AimRelease)
            {
                currentFrame = Mathf.FloorToInt(playerAimShoot.AimProcessNormalize() * (frameCount - 1));
                if (currentFrame >= 0 && currentFrame < currentCursorAnimation.TextureArray.Length)
                {
                    Cursor.SetCursor(currentCursorAnimation.TextureArray[currentFrame], currentCursorAnimation.Offset, CursorMode.Auto);
                }
            }
        }

        public void SetActiveCursor(CursorAnimation cursorAnimation)
        {
            currentCursorAnimation = cursorAnimation;
            currentFrame = 0;
            frameCount = cursorAnimation.TextureArray.Length;
            Cursor.SetCursor(cursorAnimation.TextureArray[0], cursorAnimation.Offset, CursorMode.Auto);
        }

        [System.Serializable]
        public class CursorAnimation
        {
            public ShootType ShootType;
            public Vector2 Offset;
            public Texture2D[] TextureArray;
        }
    }
}
