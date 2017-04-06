/* Class Name: RightClickMenuManager
 * Describe: This script manager the rightclickmenu.
 *           This is SINGLECLASS that you can use
 *           "RightClickManager.Instance.xxx" to call all public props
 * Author: Ghostyii
 * Create Time: 2016/5/19
 */ 
 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Events;

namespace InventorySystem
{
    public class RightClickMenuManager : MonoBehaviour
    {
        static public RightClickMenuManager Instance;
        public GameObject root; //All agents' root object, you best let it is the background ui.

        //All agents refrences. DON'T LET IT REPEAT!!!
        [SerializeField]
        private List<RightClickMenuAgent> agents = new List<RightClickMenuAgent>();
        public int AgentCount
        { get { return agents.Count; } }

        void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(this);
        }
        //Show the root object and agents that should be showed
        public void Show(Vector2 position)
        {
            SetEnabled();
            (transform as RectTransform).anchoredPosition = position;
            root.SetActive(true);
        }
        //Hide the root object
        public void Hide()
        { root.SetActive(false); }

        //Set which agent hide or show
        protected void SetEnabled()
        {
            int count = agents.Count;
            for (int i = 0; i < count; i++)
            {
                agents[i].gameObject.SetActive(agents[i].GetActionsCount() != 0);
            }
        }
        //Add agent to agents list
        public void AddAgent(RightClickMenuAgent agent)
        {
            if (!ContainsAgent(agent))
                agents.Add(agent);
        }
        //Return is contains some agent?
        public bool ContainsAgent(RightClickMenuAgent agent)
        { return agents.Contains(agent); }
        //Find the agent from agent list by clickmenu
        public RightClickMenuAgent GetAgent(ClickMenu func)
        {
            int count = agents.Count;
            for (int i = 0; i < count; i++)
            {
                if (agents[i].menuType == func)
                    return agents[i];
            }

            return null;
        }
        //Return all agents sum
        public int GetAgentsListenterSum()
        {
            int result = 0, count  = agents.Count;

            for (int i = 0; i < count; i++)
                result += agents[i].GetActionsCount();

            return result;
        }
        
        //Set agent
        public virtual void SetListenter(params RightClickAction[] actions)
        {
            RemoveAllListenters();
            AddListenter(actions);
        }
        public virtual bool AddListenter(ClickMenu agentType, params UnityAction[] calls)
        {
            var agent = GetAgent(agentType);
            if (agent == null)
                return false;
           
            agent.AddListener(calls);
            return true;
        }        
        public virtual bool AddListenter(params RightClickAction[] actions)
        {
            int length = actions.Length;
            for (int i = 0; i < length; i++)
                if (!AddListenter(actions[i].menu, actions[i].actions))
                    return false;

            return true;
            
        }

        public virtual void RemoveAllListenters()
        {
            for (int i = 0; i < AgentCount; i++)
                agents[i].RemoveAllListeners();
        }
        public virtual bool RemoveListenter(ClickMenu menu, UnityAction call)
        {
            RightClickMenuAgent agent = GetAgent(menu);
            if (agent == null)
                return false;

            agent.RemoveListener(call);
            return true;
        }
    }

    //public interface ISetListenterHandler
    //{
    //    public void SetListenter(ClickMenu agentType, params UnityAction[] calls);
    //}

    //public interface IAddListenterHandler
    //{
    //    public void AddListenter(ClickMenu agentType, params UnityAction[] calls);
    //}
}