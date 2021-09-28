




namespace RPG.EventSystem
{
    public class GameBaseEvent
    {
        #region 事件名称
        private string m_EventName;
        public string EventName
        {
            get { return m_EventName; }
            protected set { m_EventName = value; }
        }
        #endregion

        #region 事件状态

        private bool m_IsEnable;

        public bool IsEnable
        {
            get
            {
                return m_IsEnable;
            }
            set
            {
                m_IsEnable = value;
            }
        }
        #endregion

        public virtual void Update()
        {
            
        }
    }
}