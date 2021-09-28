


using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;

namespace RPG.EventSystem
{
    public class GameEventGroup:GameBaseEvent
    {
        private List<GameBaseEvent> m_Events;
        
        public GameEventGroup(string eventName)
        {
            EventName = eventName;
        }

        public void AddEvent(GameEvent gameEvent)
        {
            if (m_Events == null)
            {
                m_Events = new List<GameBaseEvent>();
            }

            if (m_Events.Find(e=>e.EventName==gameEvent.EventName) != null)
            {
                return; 
            }
            
            m_Events.Add(gameEvent);
        }

        public GameBaseEvent GetEvent(string eventName)
        {
            Queue<GameBaseEvent> q  = new Queue<GameBaseEvent>();
            q.Enqueue(this);
            while (q.Count > 0)
            {
                GameEventGroup temp = q.Dequeue() as GameEventGroup;
                if (temp != null && temp.m_Events != null && temp.m_Events.Count > 0)
                {
                    var children = temp.m_Events;
                    foreach (var child in children)
                    {
                        if (child.EventName == eventName)
                            return child;
                        // else
                        // {
                        //     
                        // }
                    }
                }
            }

            return null;
        }

        public void EnableAllEvents(bool enable)
        {
            IsEnable = enable;
            if (m_Events == null) return;
            foreach (var item in m_Events)
            {
                if (item is GameEventGroup)
                {
                    (item as GameEventGroup).EnableAllEvents(enable);
                }
                else
                {
                    item.IsEnable = enable;
                }
            }
        }

        public void RemoveEvent(string eventName)
        {
            if (m_Events == null) return;
            foreach (var item in m_Events)
            {
                if (item.EventName == eventName)
                {
                    m_Events.Remove(item);
                    return;
                }
                else
                {
                    if (item is GameEventGroup)
                    {
                        (item as GameEventGroup).RemoveEvent(eventName);
                    }
                }
            }
        }

        public override void Update()
        {
            if (!IsEnable || m_Events == null)
            {
                return;
            }

            foreach (var item in m_Events)
            {
                if (item != null)
                {
                    item.Update();
                }
            }
        }



    }
}