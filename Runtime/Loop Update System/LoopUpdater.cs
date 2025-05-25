using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class LoopUpdater : MonoSingleton<LoopUpdater>
    {
        private HashSet<ILoopUpdateEntity> m_entitiesSet = new();
        private List<ILoopUpdateEntity> m_entitiesList = new();

        private ConcurrentQueue<ILoopUpdateEntity> m_addQueue = new();
        private ConcurrentQueue<ILoopUpdateEntity> m_removeQueue = new();

        private void Update()
        {
            // Apply queued adds/removes
            while (m_addQueue.TryDequeue(out var entityToAdd))
            {
                if (entityToAdd != null && m_entitiesSet.Add(entityToAdd))
                    m_entitiesList.Add(entityToAdd);
            }

            while (m_removeQueue.TryDequeue(out var entityToRemove))
            {
                if (entityToRemove != null && m_entitiesSet.Remove(entityToRemove))
                    m_entitiesList.Remove(entityToRemove);
            }

            float deltaTime = Time.deltaTime;

            for (int i = 0; i < m_entitiesList.Count; i++)
            {
                var entity = m_entitiesList[i];
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

        public static void EnableEntity(ILoopUpdateEntity entity)
        {
            if (entity == null) return;
            Instance.m_addQueue.Enqueue(entity);
        }

        public static void DisableEntity(ILoopUpdateEntity entity)
        {
            if (entity == null) return;
            Instance.m_removeQueue.Enqueue(entity);
        }
    }
}