
using FSM;

namespace RPG.MotionSystem
{
    public class EventCondition<T>: ICondition<T>
    {
        public string EventName;

        public bool isTriggered
        {
            get
            {
                if (m_isTriggered == true)
                {
                    m_isTriggered = false;
                }

                return m_isTriggered;
            }
        }

        private bool m_isTriggered = false;
        
        
        public EventCondition(string eventName, string nextStateName) : base(default, nextStateName)
        {
            EventName = eventName;
           // GameEventManager
        }

        public override bool CheckCondition(out string nextState)
        {
            throw new System.NotImplementedException();
        }

        public delegate void ProcessEventData(object[] args);
        private ProcessEventData m_ProcessEventData;
        
    }
}