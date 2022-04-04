using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Workflow.ComponentModel.Design;
using System.Workflow.Activities;
using System.ComponentModel;
using System.Workflow.ComponentModel;
using System.Workflow.ComponentModel.Serialization;
using System.Xml;
using System.ComponentModel.Design.Serialization;
using System.Workflow.Runtime;
using System.Drawing.Design;
using FLTools;
using System.Collections;
using System.Drawing;
using System.IO;

namespace FLDesignerCore
{
    public class FLDesigner : Panel
    {
        private FLDesignSurface _surface;
        private IDesignerHost _host;
        private FLView _view;
        private CompositeActivity _flow;
        private FLDesignerLoader _loader;

        private Hashtable _services;
        private ContextMenu __ContextMenu;

        public FLDesigner()
        {
            _services = new Hashtable();
            ResetEnvi();
        }

        public WorkflowView WorkflowView
        {
            get
            {
                return _view;
            }
        }

        [Browsable(false)]
        public ContextMenu _ContextMenu
        {
            get
            {
                return __ContextMenu;
            }
            set
            {
                __ContextMenu = value;
            }
        }

        public void Set_ContextMenu(ContextMenu contextMenu)
        {
            __ContextMenu = contextMenu;
        }

        [Browsable(false)]
        public CompositeActivity Flow
        {
            get
            {
                return _flow;
            }
        }

        /// <summary>
        /// 取得服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <returns></returns>
        public object _GetService(Type serviceType)
        {
            if (_surface != null)
            {
                return _surface.GetService(serviceType);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="serviceInstance"></param>
        public void AddService(Type serviceType, object serviceInstance)
        {
            if (_services.ContainsKey(serviceType))
            {
                _services.Remove(serviceType);
            }

            _services.Add(serviceType, serviceInstance);
            AddServicesToSurface();
        }

        /// <summary>
        /// 添加服务
        /// </summary>
        private void AddServicesToSurface()
        {
            if (_surface != null)
            {
                ICollection keys = _services.Keys;
                foreach (object o in keys)
                {
                    _surface.AddService((Type)o, _services[o]);
                }
            }
        }

        /// <summary>
        /// 重设环境
        /// </summary>
        private void ResetEnvi()
        {
            if (this._view != null && Controls.Contains(_view))
            {
                Controls.Remove(_view);
            }

            DesignSurface surface = new DesignSurface();
            IServiceProvider provider = surface.GetService(typeof(IServiceProvider)) as IServiceProvider;

            _surface = new FLDesignSurface(provider);
            AddServicesToSurface();

            _host = (IDesignerHost)_surface.GetService(typeof(IDesignerHost));
        }

        /// <summary>
        /// 载入空流程
        /// </summary>
        public void LoadDefaultWorkflow()
        {
            LoadWorkflow(string.Empty);
        }

        /// <summary>
        /// 装载流程
        /// </summary>
        /// <param name="stream">流程XOML的文件流</param>
        public void LoadWorkflow(Stream stream)
        {
            ResetEnvi();

            if (_surface != null)
            {
                _flow = ((CompositeActivity)(_host.CreateComponent(typeof(FLSequentialWorkflow))));
                _loader = new FLDesignerLoader(stream, _flow);

                _surface.BeginLoad(_loader);

                _view = new FLView(((IServiceProvider)(this._surface)));
                _view.ContextMenu = __ContextMenu;
                _view.DoubleClick += new EventHandler(_view_DoubleClick);
                _view.KeyDown += new KeyEventHandler(_view_KeyDown);
                _view.KeyPress += new KeyPressEventHandler(_view_KeyPress);
                _view.KeyUp += new KeyEventHandler(_view_KeyUp);

                Controls.Add(this._view);
                ISelectionService selectionService = (ISelectionService)(_surface.GetService(typeof(ISelectionService)));
                if (selectionService != null)
                {
                    selectionService.SelectionChanged += new EventHandler(new EventHandler(SelectionChanged));
                    IComponent[] o = new IComponent[] { _flow };
                    selectionService.SetSelectedComponents(o);
                }
                IComponentChangeService compChangeService = (IComponentChangeService)(_surface.GetService(typeof(IComponentChangeService)));
                if (compChangeService != null)
                {
                    compChangeService.ComponentRemoved += new ComponentEventHandler(compChangeService_ComponentRemoved);
                }
            }
        }

        void compChangeService_ComponentRemoved(object sender, ComponentEventArgs e)
        {
            ActivityDeletedEventArgs e1 = new ActivityDeletedEventArgs(e.Component.Site.Name, _flow);

            OnActivityDeleted(sender, e1);
        }

        /// <summary>
        /// 装载流程
        /// </summary>
        /// <param name="fileName">XOML文件</param>
        public void LoadWorkflow(string fileName)
        {
            ResetEnvi();

            if (_surface != null)
            {
                _flow = ((CompositeActivity)(_host.CreateComponent(typeof(FLSequentialWorkflow))));
                _loader = new FLDesignerLoader(fileName, _flow);

                _surface.BeginLoad(_loader);

                _view = new FLView(((IServiceProvider)(this._surface)));
                _view.ContextMenu = __ContextMenu;
                _view.DoubleClick += new EventHandler(_view_DoubleClick);
                _view.KeyDown += new KeyEventHandler(_view_KeyDown);
                _view.KeyPress += new KeyPressEventHandler(_view_KeyPress);
                _view.KeyUp += new KeyEventHandler(_view_KeyUp);

                Controls.Add(this._view);

                ISelectionService selectionService = (ISelectionService)(_surface.GetService(typeof(ISelectionService)));
                if (selectionService != null)
                {
                    selectionService.SelectionChanged += new EventHandler(new EventHandler(SelectionChanged));
                    IComponent[] o = new IComponent[] { _flow };
                    selectionService.SetSelectedComponents(o);
                }
                IComponentChangeService compChangeService = (IComponentChangeService)(_surface.GetService(typeof(IComponentChangeService)));
                if (compChangeService != null)
                {
                    compChangeService.ComponentRemoved += new ComponentEventHandler(compChangeService_ComponentRemoved);
                }
            }
        }

        /// <summary>
        /// KeyUp事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _view_KeyUp(object sender, KeyEventArgs e)
        {
            Activity[] o = GetSelectedActivity();
            _KeyUpEventArgs e1 = null;

            if (o != null && o.Length == 1)
            {
                e1 = new _KeyUpEventArgs(e, o[0], _flow);
            }
            else
            {
                e1 = new _KeyUpEventArgs(e, o, _flow);
            }

            _OnKeyUp(sender, e1);
        }

        /// <summary>
        /// KeyPress事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _view_KeyPress(object sender, KeyPressEventArgs e)
        {
            Activity[] o = GetSelectedActivity();
            _KeyPressEventArgs e1 = null;

            if (o != null && o.Length == 1)
            {
                e1 = new _KeyPressEventArgs(e, o[0], _flow);
            }
            else
            {
                e1 = new _KeyPressEventArgs(e, o, _flow);
            }

            _OnKeyPress(sender, e1);
        }

        /// <summary>
        /// KeyDown事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _view_KeyDown(object sender, KeyEventArgs e)
        {
            Activity[] o = GetSelectedActivity();
            _KeyDownEventArgs e1 = null;
            if (o != null && o.Length == 1)
            {
                e1 = new _KeyDownEventArgs(e, o[0], _flow);
            }
            else
            {
                e1 = new _KeyDownEventArgs(e, o, _flow);
            }

            _OnKeyDown(sender, e1);
        }

        /// <summary>
        /// DoubleClick事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _view_DoubleClick(object sender, EventArgs e)
        {
            Activity[] o = GetSelectedActivity();
            _DoubleClickEventArgs e1 = null;

            if (o != null && o.Length == 1)
            {
                e1 = new _DoubleClickEventArgs(o[0], _flow);
            }
            else
            {
                e1 = new _DoubleClickEventArgs(o, _flow);
            }

            _OnDoubleClick(sender, e1);
        }

        /// <summary>
        /// SelectionChanged事件响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectionChanged(Object sender, EventArgs e)
        {
            Activity[] o = GetSelectedActivity();
            ActivitySelectedEventArgs e1 = null;

            if (o != null && o.Length == 1)
            {
                e1 = new ActivitySelectedEventArgs(o[0], _flow);
            }
            else
            {
                e1 = new ActivitySelectedEventArgs(o, _flow);
            }

            OnActivitySelected(sender, e1);
        }

        /// <summary>
        /// 取得被选中的Activity
        /// </summary>
        /// <returns></returns>
        public Activity[] GetSelectedActivity()
        {
            try
            {
                ISelectionService service = ((ISelectionService)(_surface.GetService(typeof(ISelectionService))));
                if (service != null && service.SelectionCount != 0)
                {
                    Activity[] o = new Activity[service.SelectionCount];
                    service.GetSelectedComponents().CopyTo(o, 0);
                    return o;
                    //if (o[0] != null && o[0] is IComponent)
                    //{
                    //    return ((Activity)(o[0]));
                    //}
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 保持流程
        /// </summary>
        /// <param name="fileName"></param>
        public void Save(string fileName)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            XmlWriter writer = XmlWriter.Create(fileName);

            DesignerSerializationManager serializationManager = new DesignerSerializationManager();

            serializationManager.CreateSession();
            serializer.Serialize(serializationManager, writer, this._flow);

            writer.Flush();
            writer.Close();
        }

        /// <summary>
        /// 序列化Activity
        /// </summary>
        /// <param name="activity">被序列化的Activity</param>
        /// <param name="writer">序列化后的XmlWriter</param>
        public void SerializeActivity(Activity activity, XmlWriter writer)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            DesignerSerializationManager serializationManager = new DesignerSerializationManager();

            serializationManager.CreateSession();
            serializer.Serialize(serializationManager, writer, activity);
        }

        /// <summary>
        /// 反序列化Activity
        /// </summary>
        /// <param name="reader">XmlReader流</param>
        /// <returns></returns>
        public Activity DeserializeActivity(XmlReader reader)
        {
            WorkflowMarkupSerializer serializer = new WorkflowMarkupSerializer();
            DesignerSerializationManager serializationManager = new DesignerSerializationManager();

            serializationManager.CreateSession();
            object deserializedObject = serializer.Deserialize(serializationManager, reader);

            return (deserializedObject as Activity);
        }

        /// <summary>
        /// 添加一个Activity
        /// </summary>
        /// <param name="activity"></param>
        public void AddActivity(Activity activity)
        {
            Activity[] o = GetSelectedActivity();
            if (o != null && o.Length == 1)
            {
                Activity selectedActivity = o[0];

                if (selectedActivity == null)
                {
                    _flow.Activities.Add(activity);
                }
                else
                {
                    CompositeActivity parentActivity = null;
                    if (selectedActivity is CompositeActivity)
                    {
                        parentActivity = (CompositeActivity)selectedActivity;
                        parentActivity.Activities.Add(activity);
                    }
                    else
                    {
                        parentActivity = (CompositeActivity)selectedActivity.Parent;
                        int i = parentActivity.Activities.IndexOf(selectedActivity);
                        parentActivity.Activities.Insert(i + 1, activity);
                    }
                }

                List<Activity> children = new List<Activity>();
                GetActivityAndChildren(activity, children);
                foreach (Activity a in children)
                {
                    _host.RootComponent.Site.Container.Add(a);
                }
                _view.Update();
            }
        }

        /// <summary>
        /// 取得一个Activity和它的所有子Activity
        /// </summary>
        /// <param name="activity"></param>
        /// <param name="children"></param>
        public void GetActivityAndChildren(Activity activity, List<Activity> children)
        {
            children.Add(activity);
            if (activity is CompositeActivity)
            {
                CompositeActivity compositeActivity = (CompositeActivity)activity;
                foreach (Activity a in compositeActivity.Activities)
                {
                    if (a is CompositeActivity)
                    {
                        GetActivityAndChildren(a, children);
                    }
                    else
                    {
                        children.Add(a);
                    }
                }
            }
        }

        /// <summary>
        /// 删除一个Activity
        /// </summary>
        /// <param name="activity"></param>
        public void DeleteActivity(Activity activity)
        {
            if (activity.Parent != null && activity.Parent is CompositeActivity)
            {
                ((CompositeActivity)activity.Parent).Activities.Remove(activity);

                _host.RootComponent.Site.Container.Remove(activity);
                _view.Update();
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);

            if (this._view != null)
            {
                this._view.Width = this.Width;
                this._view.Height = this.Height;
            }
        }

        public void InvokeStandardCommand(CommandID cmd)
        {
            try
            {
                IMenuCommandService service;

                service = (IMenuCommandService)_surface.GetService(typeof(IMenuCommandService));

                if (service != null)
                {
                    service.GlobalInvoke(cmd);
                }
            }
            catch
            {

            }
        }

        public delegate void ActivitySelectedEventHandler(object sender, ActivitySelectedEventArgs e);
        public delegate void ActivityDeletedEventHandler(object sender, ActivityDeletedEventArgs e);
        public delegate void _DoubleClickEventHandler(object sender, _DoubleClickEventArgs e);
        public delegate void _KeyDownEventHandler(object sender, _KeyDownEventArgs e);
        public delegate void _KeyPressEventHandler(object sender, _KeyPressEventArgs e);
        public delegate void _KeyUpEventHandler(object sender, _KeyUpEventArgs e);

        protected void OnActivitySelected(object sender, ActivitySelectedEventArgs value)
        {
            if (ActivitySelected != null)
            {
                ActivitySelected(sender, value);
            }
        }

        protected void OnActivityDeleted(object sender, ActivityDeletedEventArgs value)
        {
            if (ActivityDeleted != null)
            {
                ActivityDeleted(sender, value);
            }
        }

        protected void _OnDoubleClick(object sender, _DoubleClickEventArgs value)
        {
            if (_DoubleClick != null)
            {
                _DoubleClick(sender, value);
            }
        }

        protected void _OnKeyDown(object sender, _KeyDownEventArgs value)
        {
            if (_KeyDown != null)
            {
                _KeyDown(sender, value);
            }
        }

        protected void _OnKeyPress(object sender, _KeyPressEventArgs value)
        {
            if (_KeyPress != null)
            {
                _KeyPress(sender, value);
            }
        }

        protected void _OnKeyUp(object sender, _KeyUpEventArgs value)
        {
            if (_KeyUp != null)
            {
                _KeyUp(sender, value);
            }
        }

        public event ActivitySelectedEventHandler ActivitySelected;
        public event ActivityDeletedEventHandler ActivityDeleted;
        public event _DoubleClickEventHandler _DoubleClick;
        public event _KeyDownEventHandler _KeyDown;
        public event _KeyPressEventHandler _KeyPress;
        public event _KeyUpEventHandler _KeyUp;
    }

    public sealed class ActivitySelectedEventArgs : EventArgs
    {
        private object _selectedActivity;
        private CompositeActivity _rootActivity;

        public ActivitySelectedEventArgs()
            : this(null, null)
        {
        }

        public ActivitySelectedEventArgs(object selectedActivity, CompositeActivity rootActivity)
            : base()
        {
            _selectedActivity = selectedActivity;
            _rootActivity = rootActivity;
        }

        public object SelectedActivity
        {
            get
            {
                return _selectedActivity;
            }
        }

        public CompositeActivity RootActivity
        {
            get
            {
                return _rootActivity;
            }
        }
    }

    public sealed class ActivityDeletedEventArgs : EventArgs
    {
        public ActivityDeletedEventArgs(string deletedActivityName, CompositeActivity rootActivity)
            : base()
        {
            _deletedActivity = deletedActivityName;
            _rootActivity = rootActivity;
        }

        private string _deletedActivity;
        private CompositeActivity _rootActivity;

        public string DeletedActivity
        {
            get { return _deletedActivity; }
        }

        public CompositeActivity RootActivity
        {
            get { return _rootActivity; }
        }
    }

    public sealed class _DoubleClickEventArgs : EventArgs
    {
        private object _selectedActivity;
        private CompositeActivity _rootActivity;

        public _DoubleClickEventArgs()
            : this(null, null)
        {
        }

        public _DoubleClickEventArgs(object selectedActivity, CompositeActivity rootActivity)
            : base()
        {
            _selectedActivity = selectedActivity;
            _rootActivity = rootActivity;
        }

        public object SelectedActivity
        {
            get
            {
                return _selectedActivity;
            }
        }

        public CompositeActivity RootActivity
        {
            get
            {
                return _rootActivity;
            }
        }
    }

    public sealed class _KeyDownEventArgs : KeyEventArgs
    {
        private object _selectedActivity;
        private CompositeActivity _rootActivity;

        public _KeyDownEventArgs(KeyEventArgs e)
            : this(e, null, null)
        {
        }

        public _KeyDownEventArgs(KeyEventArgs e, object selectedActivity, CompositeActivity rootActivity)
            : base(e.KeyData)
        {
            _selectedActivity = selectedActivity;
            _rootActivity = rootActivity;
        }

        public object SelectedActivity
        {
            get
            {
                return _selectedActivity;
            }
        }

        public CompositeActivity RootActivity
        {
            get
            {
                return _rootActivity;
            }
        }
    }

    public sealed class _KeyPressEventArgs : KeyPressEventArgs
    {
        private object _selectedActivity;
        private CompositeActivity _rootActivity;

        public _KeyPressEventArgs(KeyPressEventArgs e)
            : this(e, null, null)
        {
        }

        public _KeyPressEventArgs(KeyPressEventArgs e, object selectedActivity, CompositeActivity rootActivity)
            : base(e.KeyChar)
        {
            _selectedActivity = selectedActivity;
            _rootActivity = rootActivity;
        }

        public object SelectedActivity
        {
            get
            {
                return _selectedActivity;
            }
        }

        public CompositeActivity RootActivity
        {
            get
            {
                return _rootActivity;
            }
        }
    }

    public sealed class _KeyUpEventArgs : KeyEventArgs
    {
        private object _selectedActivity;
        private CompositeActivity _rootActivity;

        public _KeyUpEventArgs(KeyEventArgs e)
            : this(e, null, null)
        {
        }

        public _KeyUpEventArgs(KeyEventArgs e, object selectedActivity, CompositeActivity rootActivity)
            : base(e.KeyData)
        {
            _selectedActivity = selectedActivity;
            _rootActivity = rootActivity;
        }

        public object SelectedActivity
        {
            get
            {
                return _selectedActivity;
            }
        }

        public CompositeActivity RootActivity
        {
            get
            {
                return _rootActivity;
            }
        }
    }
}
