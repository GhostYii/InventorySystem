/* Class Name: RightClickMenuAgent
 * Describe: Item's right click menu
 *           Include "Use/Equipment"
 *                   "Delete"
 *                   ["Sell"]
 *                   or other dynamic buttons
 * Author: Ghostyii
 * Create Time: 2016/5/19
 */

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace InventorySystem
{
    //[RequireComponent(typeof(Button))]
    public class RightClickMenuAgent : Selectable, IPointerClickHandler
    {
        [Space(10)]
        public ClickMenu menuType;
        public Text viewText;

        [SerializeField]
        private int count = 0;
        public int Count
        { get { return count; } }

        [Serializable]
        public class SubmitEvent : UnityEvent { }

        [FormerlySerializedAs("OnSubmit")]
        [SerializeField, Space(5)]
        private SubmitEvent m_OnClick = new SubmitEvent();

        public SubmitEvent OnClick
        {
            get { return m_OnClick; }
            set { m_OnClick = value; }
        }

        public void Submit()
        { 
            m_OnClick.Invoke();
            RemoveAllListeners();
        }

        void Start()
        {
            //base.Start();
            if (viewText)
                viewText.text = menuType.ToTextString();
            try
            { RightClickMenuManager.Instance.AddAgent(this); }
            catch (NullReferenceException)
            { Debug.Log("remember delete this if you build"); }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Start();
        }

        public virtual void SetListener(params UnityAction[] calls)
        {
            RemoveAllListeners();
            AddListener(calls);
        }
        public virtual void RemoveAllListeners()
        {
            m_OnClick.RemoveAllListeners();
            count = 0;
        }
        public virtual void RemoveListener(UnityAction call)
        {
            m_OnClick.RemoveListener(call);
            count--;
        }
        public virtual void AddListener(params UnityAction[] call)
        {
            foreach (var item in call)
            {
                m_OnClick.AddListener(item);
                count++;
            }
        }        
        public virtual int GetActionsCount()
        {
            return count + m_OnClick.GetPersistentEventCount();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
                Submit();

            RightClickMenuManager.Instance.Hide();
        }
    }

    //public
}