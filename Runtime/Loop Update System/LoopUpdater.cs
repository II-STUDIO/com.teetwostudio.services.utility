using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class LoopUpdater : MonoSingleton<LoopUpdater>
    {
        private HashSet<ILoopUpdateEntity> m_entities = new(200);

        private static readonly List<ILoopUpdateEntity> m_tempList = new(200);

        private void Update()
        {
            lock (m_entities)
            {
                if (m_entities.Count == 0)
                    return;

                float deltaTime = Time.deltaTime;

                m_tempList.Clear();
                m_tempList.AddRange(m_entities);

                for (int i = 0; i < m_tempList.Count; i++)
                {
                    var entity = m_tempList[i];
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
        }

        public static void EnableEntity(ILoopUpdateEntity entity)
        {
            lock (Instance.m_entities)
                Instance.m_entities.Add(entity);
        }

        public static void DisableEntity(ILoopUpdateEntity entity)
        {
            lock (Instance.m_entities)
                Instance.m_entities.Remove(entity);
        }
    }
}