using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Services
{
    public class LoopUpdater : MonoSingleton<LoopUpdater>
    {
        private HashSet<ILoopUpdateEntity> m_entities = new(200);

        private void Update()
        {
            lock (m_entities)
            {
                if (m_entities.Count == 0)
                    return;

                float deltaTime = Time.deltaTime;

                var entryValue = m_entities.ToHashSet();

                foreach (ILoopUpdateEntity entity in entryValue)
                {
                    if (entity == null)
                        continue;

                    if (!entity.IsUpdatable)
                        continue;

                    try
                    {
                        entity.LoopUpdateEvent(deltaTime);
                    }
                    catch (Exception e)
                    {
                        Debug.LogException(e);
                        continue;
                    }
                }
            }
        }

        public static void EnableEntity(ILoopUpdateEntity entity)
        {
            lock (Instance.m_entities)
            {
                if (Instance.m_entities.Contains(entity))
                    return;

                Instance.m_entities.Add(entity);
            }
        }

        public static void DisableEntity(ILoopUpdateEntity entity)
        {
            lock (Instance.m_entities)
            {
                if (!Instance.m_entities.Contains(entity))
                    return;

                Instance.m_entities.Remove(entity);
            }
        }
    }
}