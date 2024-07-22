using Enum;
using Gun;
using RepeatUtil.DesignPattern.SingletonPattern;
using System.Collections.Generic;
using UnityEngine;

namespace Manager
{
    public class CursorManager : Singleton<CursorManager>
    {
        [System.Serializable]
        public class CursorAnimation
        {
            public ShootType ShootType;
            public Vector2 Offset;
            public Texture2D[] TextureArray;
        }

        [SerializeField]
        private List<CursorAnimation> cursorAnimationList;

        [SerializeField]
        private GunSelector playerGunSelector;

        [SerializeField]
        private Transform splotlightArea;

        [SerializeField]
        private LayerMask groundLayerMark;

        private int currentFrame;
        private int frameCount;
        private CursorAnimation currentCursorAnimation;
        private float aimTimer;
        private RaycastHit hitInfo;
        
        protected override void Awake()
        {
            base.Awake();
            playerGunSelector.OnSwitchGun += PlayerGunSelector_OnSwitchGun;
        }

        private void PlayerGunSelector_OnSwitchGun(object sender, GunSelector.OnSwitchGunEventArgs e)
        {
            int indexCursor = FindIndexCursorByShootType(e.GunSO.ShootType);
            aimTimer = 0;
            if(indexCursor == -1)
            {
                Debug.LogError("Doesn't find " + e.GunSO.ShootType.ToString() + " cursor");
            }
            else
            {
                SetActiveCursor(cursorAnimationList[indexCursor]);
                splotlightArea.gameObject.SetActive(cursorAnimationList[indexCursor].ShootType == ShootType.AimRelease);
            }
        }

        public void SetActiveCursor(CursorAnimation cursorAnimation)
        {
            currentCursorAnimation = cursorAnimation;
            currentFrame = 0;
            frameCount = cursorAnimation.TextureArray.Length;
            Cursor.SetCursor(cursorAnimation.TextureArray[0], cursorAnimation.Offset, CursorMode.Auto);
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
                UpdateSpotlightPosition();

                UpdateAimAnimation();
            }
        }

        private void UpdateSpotlightPosition()
        {
            Ray ray = CameraManager.Instance.GetRayFromMousePosition();
            if (Physics.Raycast(ray, out hitInfo, 100, groundLayerMark))
            {
                Vector3 newPosition = new Vector3(hitInfo.point.x, splotlightArea.position.y, hitInfo.point.z);
                splotlightArea.position = newPosition;
            }
            else
            {
                splotlightArea.gameObject.SetActive(false);
            }
        }

        private void UpdateAimAnimation()
        {
            aimTimer = playerGunSelector.IsUnUsingGun ? 0 : aimTimer + Time.deltaTime;

            float processNormalize = Mathf.Clamp01(aimTimer / playerGunSelector.CurrentGunSO().AimDuration);
            currentFrame = Mathf.FloorToInt(processNormalize * (frameCount - 1));
            if (currentFrame >= 0 && currentFrame < currentCursorAnimation.TextureArray.Length)
            {
                Cursor.SetCursor(currentCursorAnimation.TextureArray[currentFrame], currentCursorAnimation.Offset, CursorMode.Auto);
            }
        }

    }
}
