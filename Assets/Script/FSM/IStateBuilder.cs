
using System.Collections.Generic;


namespace FSM
{
    public abstract class IStateBuilder<T>
    {
        public bool IsBuild { get; protected set; }
        protected T m_Manager;

        public IStateBuilder(T manager)
        {
            m_Manager = manager;
        }

        public abstract Dictionary<string, IState<T>> CreateStates();

    }
}