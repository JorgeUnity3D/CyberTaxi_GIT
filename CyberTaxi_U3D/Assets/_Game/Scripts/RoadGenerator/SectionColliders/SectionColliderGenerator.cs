using BreakTheCycle.Core.ServiceLocator;
using BreakTheCycle.Util.Extensions;
using System;
using System.Buffers;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

namespace BreakTheCycle.CyberTaxi
{
    public class SectionColliderGenerator : MonoBehaviour
    {
        [SerializeField] private List<SectionColliderData> _sectionCollidersData;

        #region CONTROL

        public RecyclableSectionCollider[] GenerateSectionColliders(SectionColliderPool sectionColliderPool)
        {
            int actualCollidersCount = 0;
            int maxCollidersCount = CalculateMaxColliderCount();
            RecyclableSectionCollider[] recyclableSectionColliders = ArrayPool<RecyclableSectionCollider>.Shared.Rent(maxCollidersCount); ;
                
            for (int i = 0; i < _sectionCollidersData.Count; i++)
            {
                SectionColliderData colliderData = _sectionCollidersData[i];
                if (!colliderData.enabled)
                {
                    continue;
                }

                Transform targetSection = colliderData.section;
                bool xOrientation = colliderData.orientation == ColliderOrientation.X;
                float center = xOrientation ? targetSection.localPosition.x : targetSection.localPosition.z;
                float max = center + center;
                float start = Mathf.Min(0, max);
                int requiredCount = Mathf.Max(1, colliderData.count / Mathf.RoundToInt(colliderData.size));
                for (int j = 0; j < requiredCount; j++)
                {
                    float offset = start + (j * colliderData.size);
                    if (Mathf.Abs(offset) < colliderData.offsetToCenter)
                    {
                        continue;
                    }
                    RecyclableSectionCollider sectionObjectA = sectionColliderPool.GetSectionCollider();
                    RecyclableSectionCollider sectionObjectB = sectionColliderPool.GetSectionCollider();
                    sectionObjectA.name = $"{j.ToString("D3")}A_RecyclableSectionCollider_{sectionObjectA.gameObject.GetInstanceID()}";
                    sectionObjectB.name = $"{j.ToString("D3")}B_RecyclableSectionCollider_{sectionObjectB.gameObject.GetInstanceID()}";
                    //Section Position
                    Vector3 positionA = new Vector3(xOrientation ? transform.position.x + offset : colliderData.offsetToSides, 1f, !xOrientation ? transform.position.z + offset : transform.position.z + colliderData.offsetToSides);
                    Vector3 positionB = new Vector3(xOrientation ? transform.position.x + offset : -colliderData.offsetToSides, 1f, !xOrientation ? transform.position.z + offset : transform.position.z + -colliderData.offsetToSides);
                    //Collider Size
                    Vector3 size = new Vector3(xOrientation ? colliderData.size : 0.1f, 3f, xOrientation ? 0.1f : colliderData.size);
                    //SetUp SectionObjects
                    sectionObjectA.SetUp(targetSection, positionA, size);
                    sectionObjectB.SetUp(targetSection, positionB, size);

                    recyclableSectionColliders[actualCollidersCount++] = sectionObjectA;
                    recyclableSectionColliders[actualCollidersCount++] = sectionObjectB;
                }
            }
            return recyclableSectionColliders;
        }

        #endregion

        #region UTIL

        public int CalculateMaxColliderCount()
        {
            int maxCount = 0;
            foreach (SectionColliderData _sectionColliderData in _sectionCollidersData)
            {
                if (_sectionColliderData.enabled)
                {
                    maxCount += CalculateMaxColliders(_sectionColliderData);
                }
            }
            //Debug.Log($"[SectionColliderGenerator] CalculaMax() -> maxCount {maxCount}");

            return maxCount;
        }

        private int CalculateMaxColliders(SectionColliderData sectionColliderData)
        {
            int requiredCount = Mathf.Max(1, sectionColliderData.count / Mathf.RoundToInt(sectionColliderData.size));
            int estimatedSkips = Mathf.CeilToInt(sectionColliderData.offsetToCenter / sectionColliderData.size);
            //Debug.Log($"[SectionColliderGenerator] CalculaMax() -> requiredCount {requiredCount} \n estimatedSkips {estimatedSkips} \n total: {(requiredCount - estimatedSkips) * 2}");
            return (requiredCount - estimatedSkips) * 2;
        }

        #endregion

    }
}