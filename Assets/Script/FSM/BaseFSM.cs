using System.Collections.Generic;
using UnityEditor;

namespace FSM
{
    public class BaseFSM<T>
    {
        private Dictionary<string, IState<T>> m_States;

        protected Dictionary<string, IState<T>> States
        {
            get
            {
                if (m_States == null)
                {
                    m_States = new Dictionary<string, IState<T>>();
                }

                return m_States;
            }
        }

        private IState<T> m_CurState;

        protected IState<T> CurState
        {
            get { return m_CurState; }
            set { m_CurState = value; }
        }

        private IState<T> m_DefaultState;
        protected IStateBuilder<T> m_StateBuilder;

        public BaseFSM(IStateBuilder<T> builder)
        {
            m_StateBuilder = builder;
            m_States = builder.CreateStates();
        }

        public void Update()
        {
            if (m_CurState == null || m_States == null || !m_StateBuilder.IsBuild)
            {
                return;
            }

            m_CurState.OnUpdateState();
            if (m_CurState.CheckConditions(out string nextStateName))
            {
                if (States.TryGetValue(nextStateName, out IState<T> state))
                {
                    ChangeState(state);
                }
            }
        }

        public void SetDefaultState(string stateName, bool startImmediately = true)
        {
            if (States.ContainsKey(stateName))
            {
                m_DefaultState = States[stateName];
                m_CurState = m_DefaultState;
                if (startImmediately)
                {
                    m_CurState.OnEnterState();
                }
            }
            else
            {
                UnityEngine.Debug.Log(("Game Error:The state wa not in BaseFSM states"));
            }
        }

        private void ChangeState(IState<T> nextState)
        {
            if (m_CurState != null)
            {
                m_CurState.OnExitState();
            }

            if (nextState != null)
            {
                m_CurState = nextState;
                m_CurState.OnEnterState();
            }

        }

    }
}