

namespace FSM
{

    public abstract class ICondition<T>
    {
        public string NextStateName { get; private set; }

        protected T m_Manager;

        public ICondition(T manager, string nextStateName)
        {
            m_Manager = manager;
            NextStateName = nextStateName;
        }

        public abstract bool CheckCondition(out string nextState);

    }
}