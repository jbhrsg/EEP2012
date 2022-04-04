using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using FLCore;
using System.Collections;

namespace FLTools.ComponentModel
{
    [Serializable]
    public class FLActivity : IFLActivity
    {
        private string _description;
        private string _name;
        private FLActivityExecutionStatus _executionStatus;
        private List<FLActivity> _childFLActivities;
        private bool _isUpperParallelAnd;
        private string _upperParallelBranch;
        private string _upperParallel;
        private string _location;
        private bool _enabled;

        [NonSerialized]
        private Hashtable _allChildFLActivities;

        public FLActivity() : this("")
        {
        }

        public FLActivity(string name)
        {
            _location = string.Empty;
            _upperParallel = string.Empty;
            _upperParallelBranch = string.Empty;
            _name = name;
            _childFLActivities = new List<FLActivity>();
            _executionStatus = FLActivityExecutionStatus.Initialized;
        }

        [XmlAttribute("IsUpperParallelAnd")]
        public bool IsUpperParallelAnd
        {
            get
            {
                return _isUpperParallelAnd;
            }
            set
            {
                _isUpperParallelAnd = value;
            }
        }

        [XmlAttribute("UpperParallel")]
        public string UpperParallel
        {
            get
            {
                return _upperParallel;
            }
            set
            {
                _upperParallel = value;
            }
        }

        [XmlAttribute("Location")]
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }

        [XmlAttribute("UpperParallelBranch")]
        public string UpperParallelBranch
        {
            get
            {
                return _upperParallelBranch;
            }
            set
            {
                _upperParallelBranch = value;
            }
        }

        [XmlAttribute("Description")]
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        [XmlAttribute("Name")]
        public string Name
        {
            get
            {
               return _name;
            }
            set
            {
                _name = value;
            }
        }

        [XmlAttribute("Enabled")]
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                _enabled = value;
            }
        }

        [XmlAttribute("ExecutionStatus")]
        public FLActivityExecutionStatus ExecutionStatus
        {
            get
            {
                return _executionStatus;
            }
            set
            {
                _executionStatus = value;
            }
        }

        private bool _allowSendBack = true;
        [XmlAttribute("AllowSendBack")]
        public bool AllowSendBack
        {
            get
            {
                return _allowSendBack;
            }
            set
            {
                _allowSendBack = value;
            }
        }

        [XmlElement("FLActivities")]
        public List<FLActivity> ChildFLActivities
        {
            get
            {
                return _childFLActivities;
            }
            set
            {
                _childFLActivities = value;
            }
        }

        private FLActivity _previsouActvity;
        [XmlElement("PreviousActivities")]
        public FLActivity PreviousActivity
        {
            get
            {
                return _previsouActvity;
            }
            set
            {
                _previsouActvity = value;
            }
        }

        private List<FLActivity> _nextActivies = new List<FLActivity>();
        [XmlElement("NextActivities")]
        public List<FLActivity> NextActivities
        {
            get
            {
                return _nextActivies;
            }
            set
            {
                _nextActivies = value;
            }
        }

        private List<FLActivity> _executedActivies = new List<FLActivity>();
        [XmlElement("ExecutedActivities")]
        public List<FLActivity> ExecutedActivities
        {
            get
            {
                return _executedActivies;
            }
            set
            {
                _executedActivies = value;
            }
        }

        public void AddFLActivity(IFLActivity activity)
        {
            _childFLActivities.Add((FLActivity)activity);
        }

        public void ClearActivities()
        {
            _childFLActivities.Clear();
        }

        // 取子节点的时候，子节点包括节点自己。
        private void InitAllChildFLActivities(FLActivity flActivity)
        {
            if (!_allChildFLActivities.Contains(flActivity.Name))
            {
                _allChildFLActivities.Add(flActivity.Name, flActivity);
            }
            else
            {
                _allChildFLActivities[flActivity.Name] = flActivity;
            }

            foreach (FLActivity a in flActivity.ChildFLActivities)
            {
                InitAllChildFLActivities(a);
            }
        }

        private void InitAllChildFLActivities()
        {
            InitAllChildFLActivities(this);
        }

        public Hashtable GetAllChildFLActivities()
        {
            if (_allChildFLActivities == null)
            {
                _allChildFLActivities = new Hashtable();

                InitAllChildFLActivities();
            }

            return _allChildFLActivities;
        }

        public FLActivity GetFLActivityByName(string name)
        {
            //if (_allChildFLActivities == null)
            //{
                _allChildFLActivities = new Hashtable();

                InitAllChildFLActivities();
            //}

            return (FLActivity)_allChildFLActivities[name];
        }

        public List<FLActivity> GetFLActivitiesByType(Type type)
        {
            List<FLActivity> activities = new List<FLActivity>();

            //if (_allChildFLActivities == null)
            //{
                _allChildFLActivities = new Hashtable();

                InitAllChildFLActivities();
            //}

            ICollection values = _allChildFLActivities.Values;
            foreach (FLActivity a in values)
            {
                if (a.GetType().Name == type.Name)
                {
                    activities.Add(a);
                }
            }

            return activities;
        }

        public List<FLActivity> GetIControlFLsByUpperParallel(string upperParallel)
        {
            List<FLActivity> activities = new List<FLActivity>();

            if (_allChildFLActivities == null)
            {
                _allChildFLActivities = new Hashtable();

                InitAllChildFLActivities();
            }

            ICollection values = _allChildFLActivities.Values;
            foreach (FLActivity a in values)
            {
                if (a.UpperParallel == upperParallel && a is IControlFL)
                {
                    activities.Add(a);
                }
            }

            return activities;
        }

        public List<FLActivity> GetIControlFLsByUpperParallelBranch(string upperParallelBranch)
        {
            List<FLActivity> activities = new List<FLActivity>();

            if (_allChildFLActivities == null)
            {
                _allChildFLActivities = new Hashtable();

                InitAllChildFLActivities();
            }

            ICollection values = _allChildFLActivities.Values;
            foreach (FLActivity a in values)
            {
                if (a.UpperParallelBranch == upperParallelBranch && a is IControlFL)
                {
                    activities.Add(a);
                }
            }

            return activities;
        }

        public virtual void Execute()
        {
            ExecutionStatus = FLActivityExecutionStatus.Executed;
        }

        public virtual void InitExecStatus()
        {
            ExecutionStatus = FLActivityExecutionStatus.Initialized;
            if (this.GetType() != typeof(FLTools.ComponentModel.FLRootActivity))
            {
               
                    foreach (var activity in this.ChildFLActivities)
                    {
                        activity.InitExecStatus();
                    }
                
            }
        }

        public virtual void Return()
        {
            ExecutionStatus = FLActivityExecutionStatus.Returned;
        }
    }
}
