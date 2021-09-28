


namespace RPG.EventSystem
{
    public class GameEvent:GameBaseEvent
    {
        //事件响应委托
        public delegate void ResponseHandle(params object[] args);
        protected ResponseHandle m_ResponseHandle;

        //触发条件委托
        public delegate bool CheckHandle(out object[] args);
        protected CheckHandle m_CheckHandle;
        
        //接收的参数
        protected object[] m_Args;

        public GameEvent(string eventName)
        {
            EventName = eventName;
        }

        public override void Update()
        {
            if (!IsEnable) return;

            if (IsEnable && m_CheckHandle(out m_Args))
            {
                m_ResponseHandle(m_Args);
            }
        }

        public void AddCheckHandle(CheckHandle checkHandle)
        {
            //m_CheckHandle -= checkHandle;
            m_CheckHandle += checkHandle;
        }

        public void RemoveCheckHandle(CheckHandle checkHandle)
        {
            m_CheckHandle -= checkHandle;
        }

        public void AddResponseHandle(ResponseHandle responseHandle)
        {
            m_ResponseHandle += responseHandle;
        }

        public void RemoveResponseHandle(ResponseHandle responseHandle)
        {
            m_ResponseHandle -= responseHandle;
        }
        
    }
}