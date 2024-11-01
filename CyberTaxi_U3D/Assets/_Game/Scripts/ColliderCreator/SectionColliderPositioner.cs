using BreakTheCycle;
using BreakTheCycle.CyberTaxi;
using BreakTheCycle.Util.Extensions;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


namespace BreakTheCycle
{
    public class SectionColliderPositioner : MonoBehaviour
    {
        [SerializeField] private GameObject _sectionColliderPrefab;
        [SerializeField] private List<SectionColliderData> _collidersData;

        private SectionColliderPoolService _sectionColliderPool;

        private void Awake()
        {
            _sectionColliderPool = ServiceLocator.Instance.GetService<SectionColliderPoolService>();
        }

        private void Start()
        {
            CreateColliders();
        }

        [ContextMenu("Create Colliders")]
        public void CreateColliders()
        {
            for (int i = 0; i < _collidersData.Count; i++)
            {
                SectionColliderData colliderData = _collidersData[i];
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

                    //RecyclableSectionCollider sectionObjectA = Instantiate(_sectionColliderPrefab).GetComponent<RecyclableSectionCollider>();
                    //RecyclableSectionCollider sectionObjectB = Instantiate(_sectionColliderPrefab).GetComponent<RecyclableSectionCollider>();

                    RecyclableSectionCollider sectionObjectA = _sectionColliderPool.GetSectionCollider();
                    RecyclableSectionCollider sectionObjectB = _sectionColliderPool.GetSectionCollider();
                    sectionObjectA.name = $"{j.ToString("D3")}A_RecyclableSectionCollider";
                    sectionObjectB.name = $"{j.ToString("D3")}B_RecyclableSectionCollider";
                    //Section Position
                    Vector3 positionA = new Vector3(xOrientation ? transform.position.x + offset : colliderData.offsetToSides, 1f, !xOrientation ? transform.position.z + offset : transform.position.z + colliderData.offsetToSides);
                    Vector3 positionB = new Vector3(xOrientation ? transform.position.x + offset : -colliderData.offsetToSides, 1f, !xOrientation ? transform.position.z + offset : transform.position.z + -colliderData.offsetToSides);
                    //Collider Size
                    Vector3 size = new Vector3(xOrientation ? colliderData.size : 0.1f, 3f, xOrientation ? 0.1f : colliderData.size);
                    //SetUp SectionObjects
                    sectionObjectA.SetUp(targetSection, positionA, size);
                    sectionObjectB.SetUp(targetSection, positionB, size);
                }
            }
        }

        [ContextMenu("Delete Colliders")]
        public void DeleteColliders()
        {
            foreach (SectionColliderData colliderData in _collidersData)
            {
                colliderData.section.DestroyChildren();
            }
        }
    }
}

