using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class LoopUpdater : MonoSingleton<LoopUpdater>
    {
        private static readonly HashSet<ILoopUpdateEntity> _entities = new(200);

        // Reused list to avoid GC on each frame
        private static readonly List<ILoopUpdateEntity> _iterationList = new(200);

        private void Update()
        {
            if (_entities.Count == 0)
                return;

            float deltaTime = Time.deltaTime;

            // Copy to reusable list to avoid modifying the collection during iteration
            CopyEntitiesToList();

            int interationCount = _iterationList.Count;

            for (int i = 0; i < interationCount; i++)
            {
                var entity = _iterationList[i];
                if (entity == null || !entity.IsUpdatable)
                    continue;

                try
                {
                    entity.LoopUpdateEvent(deltaTime);
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        private void CopyEntitiesToList()
        {
            _iterationList.Clear();
            foreach (var entity in _entities)
            {
                if (entity != null)
                    _iterationList.Add(entity);
            }
        }

        public static void EnableEntity(ILoopUpdateEntity entity)
        {
            if (entity == null)
                return;

            if (!_entities.Contains(entity))
            {
                _entities.Add(entity);
            }
        }

        public static void DisableEntity(ILoopUpdateEntity entity)
        {
            if (entity == null)
                return;

            _entities.Remove(entity);
        }
    }
}
