
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class IState<T>
    {
        protected T m_Manager;

        private List<ICondition<T>> m_Conditions;


        protected List<ICondition<T>> Conditions
        {
            get
            {
                if (m_Conditions == null)
                {
                    m_Conditions = new List<ICondition<T>>();
                }

                return m_Conditions;
            }
            set => m_Conditions = value;
        }

        public IState(T mManager)
        {
            m_Manager = mManager;
        }

        public virtual void OnEnterState()
        {
        }

        public virtual void OnUpdateState()
        {
        }

        public virtual void OnExitState()
        {
        }

        public virtual bool CheckConditions(out string nextStateName)
        {
            nextStateName = string.Empty;
            for (int i = 0; i < m_Conditions.Count; i++)
            {
                if (Conditions[i] != null && Conditions[i].CheckCondition(out nextStateName))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddCondition(ICondition<T> condition, int index = -1)
        {
            if (condition == null)
            {
                return;
            }

            if (index == -1)
            {
                Conditions.Add(condition);
            }
            else if (CollectionHelper.IsInList(Conditions, index))
            {
                Conditions.Insert(index, condition);
            }
        }

        public void RemoveCondition(int index)
        {
            if (CollectionHelper.IsInList(Conditions, index))
            {
                Conditions.RemoveAt(index);
            }
        }

        public void SwapCondition(int indexA, int indexB)
        {
            if (CollectionHelper.IsInList(Conditions, indexA) && CollectionHelper.IsInList(Conditions, indexB))
            {
                var temp1 = Conditions[indexA];
                var temp2 = Conditions[indexB];
                var temp3 = temp1;
                temp1 = temp2;
                temp2 = temp3;
            }
            else
            {
                UnityEngine.Debug.Log("Error");
            }
        }

        public void SwapCondition(int index, string name)
        {
            int indexB = FindCondtionIndex(name);
            SwapCondition(index, indexB);
        }

        public void SwapCondition(string nameA, string nameB)
        {
            int indexA = FindCondtionIndex(nameA);
            int indexB = FindCondtionIndex(nameB);
            SwapCondition(indexA, indexB);
        }

        public int FindCondtionIndex(string stateName)
        {
            try
            {
                return Conditions.FindIndex(s => s.NextStateName == stateName);
            }
            catch
            {
                return -1;
            }
        }

    }
}