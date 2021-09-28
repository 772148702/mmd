namespace RPG.EventSystem
{
    public class GameEventManager
    {
        private static GameEventGroup m_root;

        public static GameEventGroup RootGroup
        {
            get
            {
                return m_root;
            }
            private set
            {
                m_root = value;
            }
        }
        
        
        
    }
}