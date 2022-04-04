using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.Remoting;
using System.Reflection;
using System.Data.SqlClient;
using Srvtools;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Configuration;
using FLTools;
using System.Collections.Generic;

namespace EEPNetFLClient
{
    public partial class frmClientMain : InfoForm, IShowForm
    {
        private IContainer components;
        private frmLogin fFrmLogin = null;
        public DataSet CurDataSet = null;

        private ArrayList MenuIDList = new ArrayList();
        private ArrayList CaptionList = new ArrayList();
        private ArrayList ParentList = new ArrayList();
        private ImageList IconList = new ImageList();
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem testToolStripMenuItem;
        private Panel panel1;
        private ComboBox infoCmbSolution;
        private System.Windows.Forms.Timer tmMessage;
        private Splitter splitter1;
        private MainMenu mainMenu1;
        private MenuItem menuItemSystem;
        private MenuItem menuItemSolution;
        private MenuItem menuItemDataBase;
        private MenuItem menuItemUG;
        private MenuItem menuItemExit;
        private MenuItem menuItemWindows;
        private MenuItem menuItemTreeView;
        private MenuItem menuItemHelp;
        private MenuItem menuItemAbout;
        private ImageList imglst;
        private Panel panel2;
        private PictureBox pictureBox1;
        private MenuItem menuItemCP;
        private Panel panel3;
        private Panel panFLContainer;
        private Splitter splitter2;
        private FLWizard wizToDoList;
        private FLWizard wizToDoHis;
        private Splitter splitter5;
        private ContextMenuStrip cmsToDoList;
        private ContextMenuStrip cmsToDoHis;
        private ToolStripMenuItem approveToolStripMenuItem;
        private ToolStripMenuItem rejectToolStripMenuItem;
        private ToolStripMenuItem returnToolStripMenuItem;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem refreshToolStripMenuItem;
        private ToolStripMenuItem returnToolStripMenuItem1;
        private ToolStripMenuItem openToolStripMenuItem1;
        private ToolStripMenuItem refreshToolStripMenuItem1;
        private System.Windows.Forms.Timer tmFlow;
        private Panel panel4;
        private PictureBox pictureBox2;
        private Splitter splitter6;
        private DataGridView dgvToDoHis;
        private Panel panToDoHisFilter;
        private CheckBox chkSubmitted;
        private ComboBox cmbToDoHisFilter;
        private DataGridView dgvOvertime;
        private Panel panOvertimeActive;
        private Label lblLevel;
        private ComboBox cmbLevel;
        private CheckBox chkActive;
        private Panel panFLInnerContainer;
        private DataGridView dgvToDoList;
        private Panel panToDoListFilter;
        private ComboBox cmbToDoListFilter;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private LinkLabel lnkToDoListRefresh;
        private LinkLabel lnkToDoHisRefresh;
        private LinkLabel lnkOvertimeRefresh;
        private ToolStripMenuItem flowDeleteToolStripMenuItem;
        private TabPage tabPage4;
        private Panel panel5;
        private LinkLabel lnkNotifyRefresh;
        private ComboBox cmbNotifyFilter;
        private DataGridView dgvNotify;
        private ContextMenuStrip cmsNotify;
        private ToolStripMenuItem openToolStripMenuItem2;
        private ToolStripMenuItem deleteToolStripMenuItem;
        private FLWizard wizNotify;
        private ToolStripMenuItem refreshToolStripMenuItem2;
        private Panel panToDoListQuery;
        private Button btnLstQueryCancel;
        private Button btnLstQueryOK;
        private TextBox txtLstRemark;
        private DateTimePicker dtpLstDateTo;
        private DateTimePicker dtpLstDateFrom;
        private TextBox txtLstFormPresent;
        private TextBox txtLstDstep;
        private ComboBox cmbLstFlow;
        private Label lblLstRemark;
        private Label lblLstDateTo;
        private Label lblLstDateFrom;
        private Label lblLstFormPresent;
        private Label lblLstUser;
        private Label lblLstDstep;
        private Label lblLstFlow;
        private LinkLabel lnkToDoListQuery;
        private Panel panel6;
        private Panel panHisQuery;
        private Panel panel8;
        private Button btnHisQueryOK;
        private Button btnHisQueryCancel;
        private TextBox txtHisRemark;
        private DateTimePicker dtpHisDateTo;
        private DateTimePicker dtpHisDateFrom;
        private TextBox txtHisFormPresent;
        private TextBox txtHisDstep;
        private ComboBox cmbHisFlow;
        private Label lblHisRemark;
        private Label lblHisDateTo;
        private Label lblHisDateFrom;
        private Label lblHisFormPresent;
        private Label lblHisSendTo;
        private Label lblHisDstep;
        private Label lblHisFlow;
        private LinkLabel lnkToDoHisQuery;
        private Panel panHis2Query;
        private Panel panel9;
        private Button btnHis2QueryOK;
        private Button btnHis2QueryCancel;
        private TextBox txtHis2Remark;
        private DateTimePicker dtpHis2DateTo;
        private DateTimePicker dtpHis2DateFrom;
        private TextBox txtHis2FormPresent;
        private TextBox txtHis2DStep;
        private ComboBox cmbHis2Flow;
        private Label lblHis2Remark;
        private Label lblHis2DateTo;
        private Label lblHis2DateFrom;
        private Label lblHis2FormPresent;
        private Label lblHis2Dstep;
        private Label lblHis2Flow;
        private ComboBox cmbLstUser;
        private ComboBox cmbHisSendTo;
        private Splitter splitter3;
        private Splitter splitter4;
        private Splitter splitter7;
        private FLWizard wizOvertime;
        private Panel panel7;
        private PictureBox pbGo;
        private TextBox tbGO;
        private PictureBox pbMyFavor;
        private TreeView tView;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem systemToolStripMenuItem;
        private ToolStripMenuItem solutionToolStripMenuItem;
        private ToolStripMenuItem dataBaseToolStripMenuItem;
        private ToolStripMenuItem changePasswordToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem windowsToolStripMenuItem;
        private ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private ToolStripMenuItem tileVerticalToolStripMenuItem;
        private ToolStripMenuItem treeViewToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutEEPNetClientToolStripMenuItem;
        private DataSet menuDataSet;

        [DllImport("KERNEL32.DLL", EntryPoint = "GetThreadLocale", SetLastError = true,
        CharSet = CharSet.Unicode, ExactSpelling = true,
        CallingConvention = CallingConvention.StdCall)]
        public static extern uint GetThreadLocale();

        private Srvtools.FormCollection fFormCollection;
        string[] lstStatus = null;

        public frmClientMain()
        {
            Application.EnableVisualStyles();
            InitializeComponent();
            fFormCollection = new Srvtools.FormCollection(this, typeof(FormItem));
            CliUtils.fCliMainHandle = this.Handle;

            if (File.Exists(Application.StartupPath + "\\EEPNetClientMain.jpg"))
            {
                this.BackgroundImage = Image.FromFile(Application.StartupPath + "\\EEPNetClientMain.jpg");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmClientMain));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.refreshToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tView = new System.Windows.Forms.TreeView();
            this.panel7 = new System.Windows.Forms.Panel();
            this.pbGo = new System.Windows.Forms.PictureBox();
            this.tbGO = new System.Windows.Forms.TextBox();
            this.pbMyFavor = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.infoCmbSolution = new System.Windows.Forms.ComboBox();
            this.tmMessage = new System.Windows.Forms.Timer(this.components);
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItemSystem = new System.Windows.Forms.MenuItem();
            this.menuItemSolution = new System.Windows.Forms.MenuItem();
            this.menuItemDataBase = new System.Windows.Forms.MenuItem();
            this.menuItemUG = new System.Windows.Forms.MenuItem();
            this.menuItemCP = new System.Windows.Forms.MenuItem();
            this.menuItemExit = new System.Windows.Forms.MenuItem();
            this.menuItemWindows = new System.Windows.Forms.MenuItem();
            this.menuItemTreeView = new System.Windows.Forms.MenuItem();
            this.menuItemHelp = new System.Windows.Forms.MenuItem();
            this.menuItemAbout = new System.Windows.Forms.MenuItem();
            this.imglst = new System.Windows.Forms.ImageList(this.components);
            this.panFLContainer = new System.Windows.Forms.Panel();
            this.panFLInnerContainer = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.panToDoListQuery = new System.Windows.Forms.Panel();
            this.splitter3 = new System.Windows.Forms.Splitter();
            this.cmbLstUser = new System.Windows.Forms.ComboBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btnLstQueryOK = new System.Windows.Forms.Button();
            this.btnLstQueryCancel = new System.Windows.Forms.Button();
            this.txtLstRemark = new System.Windows.Forms.TextBox();
            this.dtpLstDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpLstDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtLstFormPresent = new System.Windows.Forms.TextBox();
            this.txtLstDstep = new System.Windows.Forms.TextBox();
            this.cmbLstFlow = new System.Windows.Forms.ComboBox();
            this.lblLstRemark = new System.Windows.Forms.Label();
            this.lblLstDateTo = new System.Windows.Forms.Label();
            this.lblLstDateFrom = new System.Windows.Forms.Label();
            this.lblLstFormPresent = new System.Windows.Forms.Label();
            this.lblLstUser = new System.Windows.Forms.Label();
            this.lblLstDstep = new System.Windows.Forms.Label();
            this.lblLstFlow = new System.Windows.Forms.Label();
            this.dgvToDoList = new System.Windows.Forms.DataGridView();
            this.cmsToDoList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.approveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rejectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.returnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.flowDeleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panToDoListFilter = new System.Windows.Forms.Panel();
            this.lnkToDoListQuery = new System.Windows.Forms.LinkLabel();
            this.lnkToDoListRefresh = new System.Windows.Forms.LinkLabel();
            this.cmbToDoListFilter = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panHis2Query = new System.Windows.Forms.Panel();
            this.splitter7 = new System.Windows.Forms.Splitter();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btnHis2QueryOK = new System.Windows.Forms.Button();
            this.btnHis2QueryCancel = new System.Windows.Forms.Button();
            this.txtHis2Remark = new System.Windows.Forms.TextBox();
            this.dtpHis2DateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpHis2DateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtHis2FormPresent = new System.Windows.Forms.TextBox();
            this.txtHis2DStep = new System.Windows.Forms.TextBox();
            this.cmbHis2Flow = new System.Windows.Forms.ComboBox();
            this.lblHis2Remark = new System.Windows.Forms.Label();
            this.lblHis2DateTo = new System.Windows.Forms.Label();
            this.lblHis2DateFrom = new System.Windows.Forms.Label();
            this.lblHis2FormPresent = new System.Windows.Forms.Label();
            this.lblHis2Dstep = new System.Windows.Forms.Label();
            this.lblHis2Flow = new System.Windows.Forms.Label();
            this.panHisQuery = new System.Windows.Forms.Panel();
            this.splitter4 = new System.Windows.Forms.Splitter();
            this.cmbHisSendTo = new System.Windows.Forms.ComboBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.btnHisQueryOK = new System.Windows.Forms.Button();
            this.btnHisQueryCancel = new System.Windows.Forms.Button();
            this.txtHisRemark = new System.Windows.Forms.TextBox();
            this.dtpHisDateTo = new System.Windows.Forms.DateTimePicker();
            this.dtpHisDateFrom = new System.Windows.Forms.DateTimePicker();
            this.txtHisFormPresent = new System.Windows.Forms.TextBox();
            this.txtHisDstep = new System.Windows.Forms.TextBox();
            this.cmbHisFlow = new System.Windows.Forms.ComboBox();
            this.lblHisRemark = new System.Windows.Forms.Label();
            this.lblHisDateTo = new System.Windows.Forms.Label();
            this.lblHisDateFrom = new System.Windows.Forms.Label();
            this.lblHisFormPresent = new System.Windows.Forms.Label();
            this.lblHisSendTo = new System.Windows.Forms.Label();
            this.lblHisDstep = new System.Windows.Forms.Label();
            this.lblHisFlow = new System.Windows.Forms.Label();
            this.dgvToDoHis = new System.Windows.Forms.DataGridView();
            this.cmsToDoHis = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.returnToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panToDoHisFilter = new System.Windows.Forms.Panel();
            this.lnkToDoHisQuery = new System.Windows.Forms.LinkLabel();
            this.lnkToDoHisRefresh = new System.Windows.Forms.LinkLabel();
            this.chkSubmitted = new System.Windows.Forms.CheckBox();
            this.cmbToDoHisFilter = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgvNotify = new System.Windows.Forms.DataGridView();
            this.cmsNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel5 = new System.Windows.Forms.Panel();
            this.lnkNotifyRefresh = new System.Windows.Forms.LinkLabel();
            this.cmbNotifyFilter = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgvOvertime = new System.Windows.Forms.DataGridView();
            this.panOvertimeActive = new System.Windows.Forms.Panel();
            this.lnkOvertimeRefresh = new System.Windows.Forms.LinkLabel();
            this.lblLevel = new System.Windows.Forms.Label();
            this.cmbLevel = new System.Windows.Forms.ComboBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.splitter2 = new System.Windows.Forms.Splitter();
            this.splitter5 = new System.Windows.Forms.Splitter();
            this.tmFlow = new System.Windows.Forms.Timer(this.components);
            this.splitter6 = new System.Windows.Forms.Splitter();
            this.wizToDoList = new FLTools.FLWizard();
            this.wizToDoHis = new FLTools.FLWizard();
            this.wizNotify = new FLTools.FLWizard();
            this.wizOvertime = new FLTools.FLWizard();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.systemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.solutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataBaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.changePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.treeViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutEEPNetClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMyFavor)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panFLContainer.SuspendLayout();
            this.panFLInnerContainer.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panToDoListQuery.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDoList)).BeginInit();
            this.cmsToDoList.SuspendLayout();
            this.panToDoListFilter.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panHis2Query.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panHisQuery.SuspendLayout();
            this.panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDoHis)).BeginInit();
            this.cmsToDoHis.SuspendLayout();
            this.panToDoHisFilter.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotify)).BeginInit();
            this.cmsNotify.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOvertime)).BeginInit();
            this.panOvertimeActive.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // refreshToolStripMenuItem2
            // 
            this.refreshToolStripMenuItem2.Name = "refreshToolStripMenuItem2";
            this.refreshToolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.refreshToolStripMenuItem2.Text = "Refresh";
            this.refreshToolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.testToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(167, 26);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(166, 22);
            this.testToolStripMenuItem.Text = "Fetch Message...";
            this.testToolStripMenuItem.Click += new System.EventHandler(this.testToolStripMenuItem_Click_1);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.infoCmbSolution);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(194, 693);
            this.panel1.TabIndex = 11;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.tView);
            this.panel3.Controls.Add(this.panel7);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 20);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(176, 673);
            this.panel3.TabIndex = 17;
            // 
            // tView
            // 
            this.tView.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tView.Location = new System.Drawing.Point(0, 27);
            this.tView.Name = "tView";
            this.tView.Size = new System.Drawing.Size(176, 646);
            this.tView.TabIndex = 11;
            this.tView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tView_NodeMouseDoubleClick);
            this.tView.BeforeSelect += new System.Windows.Forms.TreeViewCancelEventHandler(this.tView_BeforeSelect);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.pbGo);
            this.panel7.Controls.Add(this.tbGO);
            this.panel7.Controls.Add(this.pbMyFavor);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel7.Location = new System.Drawing.Point(0, 0);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(176, 27);
            this.panel7.TabIndex = 10;
            // 
            // pbGo
            // 
            this.pbGo.Location = new System.Drawing.Point(120, 4);
            this.pbGo.Name = "pbGo";
            this.pbGo.Size = new System.Drawing.Size(24, 17);
            this.pbGo.TabIndex = 12;
            this.pbGo.TabStop = false;
            this.pbGo.MouseLeave += new System.EventHandler(this.pbGo_MouseLeave);
            this.pbGo.Click += new System.EventHandler(this.pbGo_Click);
            this.pbGo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbGo_MouseMove);
            // 
            // tbGO
            // 
            this.tbGO.Font = new System.Drawing.Font("PMingLiU", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.tbGO.Location = new System.Drawing.Point(6, 3);
            this.tbGO.Name = "tbGO";
            this.tbGO.Size = new System.Drawing.Size(111, 22);
            this.tbGO.TabIndex = 11;
            // 
            // pbMyFavor
            // 
            this.pbMyFavor.Location = new System.Drawing.Point(147, 4);
            this.pbMyFavor.Name = "pbMyFavor";
            this.pbMyFavor.Size = new System.Drawing.Size(23, 17);
            this.pbMyFavor.TabIndex = 0;
            this.pbMyFavor.TabStop = false;
            this.pbMyFavor.MouseLeave += new System.EventHandler(this.pbMyFavor_MouseLeave);
            this.pbMyFavor.Click += new System.EventHandler(this.pbMyFavor_Click);
            this.pbMyFavor.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pbMyFavor_MouseMove);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(176, 20);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(18, 673);
            this.panel2.TabIndex = 16;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(18, 16);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // infoCmbSolution
            // 
            this.infoCmbSolution.Dock = System.Windows.Forms.DockStyle.Top;
            this.infoCmbSolution.FormattingEnabled = true;
            this.infoCmbSolution.Location = new System.Drawing.Point(0, 0);
            this.infoCmbSolution.Name = "infoCmbSolution";
            this.infoCmbSolution.Size = new System.Drawing.Size(194, 20);
            this.infoCmbSolution.TabIndex = 5;
            this.infoCmbSolution.SelectedIndexChanged += new System.EventHandler(this.infoCmbSolution_SelectedIndexChanged);
            // 
            // tmMessage
            // 
            this.tmMessage.Interval = 30000;
            this.tmMessage.Tick += new System.EventHandler(this.tmMessage_Tick);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.Location = new System.Drawing.Point(194, 281);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 436);
            this.splitter1.TabIndex = 13;
            this.splitter1.TabStop = false;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSystem,
            this.menuItemWindows,
            this.menuItemHelp});
            // 
            // menuItemSystem
            // 
            this.menuItemSystem.Index = 0;
            this.menuItemSystem.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemSolution,
            this.menuItemDataBase,
            this.menuItemUG,
            this.menuItemCP,
            this.menuItemExit});
            this.menuItemSystem.Text = "System";
            // 
            // menuItemSolution
            // 
            this.menuItemSolution.Index = 0;
            this.menuItemSolution.Text = "Solution";
            this.menuItemSolution.Click += new System.EventHandler(this.menuItemSolution_Click);
            // 
            // menuItemDataBase
            // 
            this.menuItemDataBase.Index = 1;
            this.menuItemDataBase.Text = "database";
            this.menuItemDataBase.Click += new System.EventHandler(this.menuItemDataBase_Click);
            // 
            // menuItemUG
            // 
            this.menuItemUG.Index = 2;
            this.menuItemUG.Text = "users and groups";
            this.menuItemUG.Click += new System.EventHandler(this.menuItemUG_Click);
            // 
            // menuItemCP
            // 
            this.menuItemCP.Index = 3;
            this.menuItemCP.Text = "Change Password";
            this.menuItemCP.Click += new System.EventHandler(this.menuItemCP_Click);
            // 
            // menuItemExit
            // 
            this.menuItemExit.Index = 4;
            this.menuItemExit.Text = "E&xit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // menuItemWindows
            // 
            this.menuItemWindows.Index = 1;
            this.menuItemWindows.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemTreeView});
            this.menuItemWindows.Text = "Windows";
            // 
            // menuItemTreeView
            // 
            this.menuItemTreeView.Checked = true;
            this.menuItemTreeView.Index = 0;
            this.menuItemTreeView.Text = "treeView";
            this.menuItemTreeView.Click += new System.EventHandler(this.menuItemTreeView_Click);
            // 
            // menuItemHelp
            // 
            this.menuItemHelp.Index = 2;
            this.menuItemHelp.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItemAbout});
            this.menuItemHelp.Text = "&Help";
            // 
            // menuItemAbout
            // 
            this.menuItemAbout.Index = 0;
            this.menuItemAbout.Text = "&About";
            // 
            // imglst
            // 
            this.imglst.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imglst.ImageStream")));
            this.imglst.TransparentColor = System.Drawing.Color.Transparent;
            this.imglst.Images.SetKeyName(0, "default.ico");
            // 
            // panFLContainer
            // 
            this.panFLContainer.Controls.Add(this.panFLInnerContainer);
            this.panFLContainer.Controls.Add(this.panel4);
            this.panFLContainer.Dock = System.Windows.Forms.DockStyle.Top;
            this.panFLContainer.Location = new System.Drawing.Point(194, 24);
            this.panFLContainer.Name = "panFLContainer";
            this.panFLContainer.Size = new System.Drawing.Size(881, 257);
            this.panFLContainer.TabIndex = 18;
            // 
            // panFLInnerContainer
            // 
            this.panFLInnerContainer.Controls.Add(this.tabControl1);
            this.panFLInnerContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panFLInnerContainer.Location = new System.Drawing.Point(0, 0);
            this.panFLInnerContainer.Name = "panFLInnerContainer";
            this.panFLInnerContainer.Size = new System.Drawing.Size(881, 237);
            this.panFLInnerContainer.TabIndex = 24;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(881, 237);
            this.tabControl1.TabIndex = 3;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.panToDoListQuery);
            this.tabPage1.Controls.Add(this.dgvToDoList);
            this.tabPage1.Controls.Add(this.panToDoListFilter);
            this.tabPage1.Location = new System.Drawing.Point(4, 21);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(873, 212);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ToDoList";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // panToDoListQuery
            // 
            this.panToDoListQuery.Controls.Add(this.splitter3);
            this.panToDoListQuery.Controls.Add(this.cmbLstUser);
            this.panToDoListQuery.Controls.Add(this.panel6);
            this.panToDoListQuery.Controls.Add(this.txtLstRemark);
            this.panToDoListQuery.Controls.Add(this.dtpLstDateTo);
            this.panToDoListQuery.Controls.Add(this.dtpLstDateFrom);
            this.panToDoListQuery.Controls.Add(this.txtLstFormPresent);
            this.panToDoListQuery.Controls.Add(this.txtLstDstep);
            this.panToDoListQuery.Controls.Add(this.cmbLstFlow);
            this.panToDoListQuery.Controls.Add(this.lblLstRemark);
            this.panToDoListQuery.Controls.Add(this.lblLstDateTo);
            this.panToDoListQuery.Controls.Add(this.lblLstDateFrom);
            this.panToDoListQuery.Controls.Add(this.lblLstFormPresent);
            this.panToDoListQuery.Controls.Add(this.lblLstUser);
            this.panToDoListQuery.Controls.Add(this.lblLstDstep);
            this.panToDoListQuery.Controls.Add(this.lblLstFlow);
            this.panToDoListQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panToDoListQuery.Location = new System.Drawing.Point(3, 36);
            this.panToDoListQuery.Name = "panToDoListQuery";
            this.panToDoListQuery.Size = new System.Drawing.Size(867, 173);
            this.panToDoListQuery.TabIndex = 5;
            this.panToDoListQuery.Visible = false;
            // 
            // splitter3
            // 
            this.splitter3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter3.Location = new System.Drawing.Point(0, 120);
            this.splitter3.Name = "splitter3";
            this.splitter3.Size = new System.Drawing.Size(867, 3);
            this.splitter3.TabIndex = 18;
            this.splitter3.TabStop = false;
            // 
            // cmbLstUser
            // 
            this.cmbLstUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLstUser.FormattingEnabled = true;
            this.cmbLstUser.Location = new System.Drawing.Point(483, 15);
            this.cmbLstUser.Name = "cmbLstUser";
            this.cmbLstUser.Size = new System.Drawing.Size(128, 20);
            this.cmbLstUser.TabIndex = 17;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Gray;
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.btnLstQueryOK);
            this.panel6.Controls.Add(this.btnLstQueryCancel);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 123);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(867, 50);
            this.panel6.TabIndex = 16;
            // 
            // btnLstQueryOK
            // 
            this.btnLstQueryOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnLstQueryOK.Location = new System.Drawing.Point(303, 11);
            this.btnLstQueryOK.Name = "btnLstQueryOK";
            this.btnLstQueryOK.Size = new System.Drawing.Size(75, 23);
            this.btnLstQueryOK.TabIndex = 14;
            this.btnLstQueryOK.Text = "OK";
            this.btnLstQueryOK.UseVisualStyleBackColor = false;
            this.btnLstQueryOK.Click += new System.EventHandler(this.btnLstQueryOK_Click);
            // 
            // btnLstQueryCancel
            // 
            this.btnLstQueryCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnLstQueryCancel.Location = new System.Drawing.Point(392, 11);
            this.btnLstQueryCancel.Name = "btnLstQueryCancel";
            this.btnLstQueryCancel.Size = new System.Drawing.Size(75, 23);
            this.btnLstQueryCancel.TabIndex = 15;
            this.btnLstQueryCancel.Text = "Cancel";
            this.btnLstQueryCancel.UseVisualStyleBackColor = false;
            this.btnLstQueryCancel.Click += new System.EventHandler(this.btnLstQueryCancel_Click);
            // 
            // txtLstRemark
            // 
            this.txtLstRemark.Location = new System.Drawing.Point(483, 59);
            this.txtLstRemark.Name = "txtLstRemark";
            this.txtLstRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLstRemark.Size = new System.Drawing.Size(349, 21);
            this.txtLstRemark.TabIndex = 13;
            // 
            // dtpLstDateTo
            // 
            this.dtpLstDateTo.Checked = false;
            this.dtpLstDateTo.Location = new System.Drawing.Point(294, 57);
            this.dtpLstDateTo.Name = "dtpLstDateTo";
            this.dtpLstDateTo.ShowCheckBox = true;
            this.dtpLstDateTo.Size = new System.Drawing.Size(128, 21);
            this.dtpLstDateTo.TabIndex = 12;
            // 
            // dtpLstDateFrom
            // 
            this.dtpLstDateFrom.Checked = false;
            this.dtpLstDateFrom.Location = new System.Drawing.Point(84, 57);
            this.dtpLstDateFrom.Name = "dtpLstDateFrom";
            this.dtpLstDateFrom.ShowCheckBox = true;
            this.dtpLstDateFrom.Size = new System.Drawing.Size(128, 21);
            this.dtpLstDateFrom.TabIndex = 11;
            // 
            // txtLstFormPresent
            // 
            this.txtLstFormPresent.Location = new System.Drawing.Point(704, 14);
            this.txtLstFormPresent.Name = "txtLstFormPresent";
            this.txtLstFormPresent.Size = new System.Drawing.Size(128, 21);
            this.txtLstFormPresent.TabIndex = 10;
            // 
            // txtLstDstep
            // 
            this.txtLstDstep.Location = new System.Drawing.Point(294, 14);
            this.txtLstDstep.Name = "txtLstDstep";
            this.txtLstDstep.Size = new System.Drawing.Size(128, 21);
            this.txtLstDstep.TabIndex = 8;
            // 
            // cmbLstFlow
            // 
            this.cmbLstFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLstFlow.FormattingEnabled = true;
            this.cmbLstFlow.Location = new System.Drawing.Point(83, 15);
            this.cmbLstFlow.Name = "cmbLstFlow";
            this.cmbLstFlow.Size = new System.Drawing.Size(129, 20);
            this.cmbLstFlow.TabIndex = 7;
            // 
            // lblLstRemark
            // 
            this.lblLstRemark.Location = new System.Drawing.Point(417, 58);
            this.lblLstRemark.Name = "lblLstRemark";
            this.lblLstRemark.Size = new System.Drawing.Size(60, 20);
            this.lblLstRemark.TabIndex = 6;
            this.lblLstRemark.Text = "remark";
            this.lblLstRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLstDateTo
            // 
            this.lblLstDateTo.Location = new System.Drawing.Point(228, 58);
            this.lblLstDateTo.Name = "lblLstDateTo";
            this.lblLstDateTo.Size = new System.Drawing.Size(60, 20);
            this.lblLstDateTo.TabIndex = 5;
            this.lblLstDateTo.Text = "date to";
            this.lblLstDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLstDateFrom
            // 
            this.lblLstDateFrom.Location = new System.Drawing.Point(18, 58);
            this.lblLstDateFrom.Name = "lblLstDateFrom";
            this.lblLstDateFrom.Size = new System.Drawing.Size(60, 20);
            this.lblLstDateFrom.TabIndex = 4;
            this.lblLstDateFrom.Text = "date from";
            this.lblLstDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLstFormPresent
            // 
            this.lblLstFormPresent.Location = new System.Drawing.Point(613, 15);
            this.lblLstFormPresent.Name = "lblLstFormPresent";
            this.lblLstFormPresent.Size = new System.Drawing.Size(85, 20);
            this.lblLstFormPresent.TabIndex = 3;
            this.lblLstFormPresent.Text = "formPresent";
            this.lblLstFormPresent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLstUser
            // 
            this.lblLstUser.Location = new System.Drawing.Point(417, 15);
            this.lblLstUser.Name = "lblLstUser";
            this.lblLstUser.Size = new System.Drawing.Size(60, 20);
            this.lblLstUser.TabIndex = 2;
            this.lblLstUser.Text = "user";
            this.lblLstUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLstDstep
            // 
            this.lblLstDstep.Location = new System.Drawing.Point(228, 15);
            this.lblLstDstep.Name = "lblLstDstep";
            this.lblLstDstep.Size = new System.Drawing.Size(60, 20);
            this.lblLstDstep.TabIndex = 1;
            this.lblLstDstep.Text = "dstep";
            this.lblLstDstep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblLstFlow
            // 
            this.lblLstFlow.Location = new System.Drawing.Point(18, 15);
            this.lblLstFlow.Name = "lblLstFlow";
            this.lblLstFlow.Size = new System.Drawing.Size(60, 20);
            this.lblLstFlow.TabIndex = 0;
            this.lblLstFlow.Text = "flow";
            this.lblLstFlow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvToDoList
            // 
            this.dgvToDoList.AllowUserToAddRows = false;
            this.dgvToDoList.AllowUserToDeleteRows = false;
            this.dgvToDoList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToDoList.ContextMenuStrip = this.cmsToDoList;
            this.dgvToDoList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvToDoList.Location = new System.Drawing.Point(3, 36);
            this.dgvToDoList.MultiSelect = false;
            this.dgvToDoList.Name = "dgvToDoList";
            this.dgvToDoList.ReadOnly = true;
            this.dgvToDoList.RowTemplate.Height = 23;
            this.dgvToDoList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvToDoList.Size = new System.Drawing.Size(867, 173);
            this.dgvToDoList.TabIndex = 2;
            this.dgvToDoList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvToDoList_CellClick);
            this.dgvToDoList.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvToDoList_CellDoubleClick);
            this.dgvToDoList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvToDoList_DataBindingComplete);
            this.dgvToDoList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvToDoList_CellPainting);
            this.dgvToDoList.Sorted += new System.EventHandler(this.dgvToDoList_Sorted);
            // 
            // cmsToDoList
            // 
            this.cmsToDoList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.approveToolStripMenuItem,
            this.rejectToolStripMenuItem,
            this.returnToolStripMenuItem,
            this.openToolStripMenuItem,
            this.refreshToolStripMenuItem,
            this.flowDeleteToolStripMenuItem});
            this.cmsToDoList.Name = "contextMenuStrip1";
            this.cmsToDoList.Size = new System.Drawing.Size(137, 136);
            this.cmsToDoList.Opened += new System.EventHandler(this.cmsToDoList_Opened);
            // 
            // approveToolStripMenuItem
            // 
            this.approveToolStripMenuItem.Name = "approveToolStripMenuItem";
            this.approveToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.approveToolStripMenuItem.Text = "Approve";
            this.approveToolStripMenuItem.Click += new System.EventHandler(this.approveToolStripMenuItem_Click);
            // 
            // rejectToolStripMenuItem
            // 
            this.rejectToolStripMenuItem.Name = "rejectToolStripMenuItem";
            this.rejectToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.rejectToolStripMenuItem.Text = "Reject";
            this.rejectToolStripMenuItem.Click += new System.EventHandler(this.rejectToolStripMenuItem_Click);
            // 
            // returnToolStripMenuItem
            // 
            this.returnToolStripMenuItem.Name = "returnToolStripMenuItem";
            this.returnToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.returnToolStripMenuItem.Text = "Return";
            this.returnToolStripMenuItem.Click += new System.EventHandler(this.returnToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // flowDeleteToolStripMenuItem
            // 
            this.flowDeleteToolStripMenuItem.Name = "flowDeleteToolStripMenuItem";
            this.flowDeleteToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.flowDeleteToolStripMenuItem.Text = "Flow Delete";
            this.flowDeleteToolStripMenuItem.Click += new System.EventHandler(this.flowDeleteToolStripMenuItem_Click);
            // 
            // panToDoListFilter
            // 
            this.panToDoListFilter.BackColor = System.Drawing.Color.MintCream;
            this.panToDoListFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panToDoListFilter.Controls.Add(this.lnkToDoListQuery);
            this.panToDoListFilter.Controls.Add(this.lnkToDoListRefresh);
            this.panToDoListFilter.Controls.Add(this.cmbToDoListFilter);
            this.panToDoListFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panToDoListFilter.Location = new System.Drawing.Point(3, 3);
            this.panToDoListFilter.Name = "panToDoListFilter";
            this.panToDoListFilter.Size = new System.Drawing.Size(867, 33);
            this.panToDoListFilter.TabIndex = 1;
            // 
            // lnkToDoListQuery
            // 
            this.lnkToDoListQuery.AutoSize = true;
            this.lnkToDoListQuery.Location = new System.Drawing.Point(176, 9);
            this.lnkToDoListQuery.Name = "lnkToDoListQuery";
            this.lnkToDoListQuery.Size = new System.Drawing.Size(35, 12);
            this.lnkToDoListQuery.TabIndex = 5;
            this.lnkToDoListQuery.TabStop = true;
            this.lnkToDoListQuery.Text = "query";
            this.lnkToDoListQuery.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkToDoListQuery_LinkClicked);
            // 
            // lnkToDoListRefresh
            // 
            this.lnkToDoListRefresh.AutoSize = true;
            this.lnkToDoListRefresh.Location = new System.Drawing.Point(142, 9);
            this.lnkToDoListRefresh.Name = "lnkToDoListRefresh";
            this.lnkToDoListRefresh.Size = new System.Drawing.Size(47, 12);
            this.lnkToDoListRefresh.TabIndex = 4;
            this.lnkToDoListRefresh.TabStop = true;
            this.lnkToDoListRefresh.Text = "refresh";
            this.lnkToDoListRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkToDoListRefresh_LinkClicked);
            // 
            // cmbToDoListFilter
            // 
            this.cmbToDoListFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToDoListFilter.FormattingEnabled = true;
            this.cmbToDoListFilter.Location = new System.Drawing.Point(3, 6);
            this.cmbToDoListFilter.Name = "cmbToDoListFilter";
            this.cmbToDoListFilter.Size = new System.Drawing.Size(121, 20);
            this.cmbToDoListFilter.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panHis2Query);
            this.tabPage2.Controls.Add(this.panHisQuery);
            this.tabPage2.Controls.Add(this.dgvToDoHis);
            this.tabPage2.Controls.Add(this.panToDoHisFilter);
            this.tabPage2.Location = new System.Drawing.Point(4, 21);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(870, 212);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ToDoHis";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panHis2Query
            // 
            this.panHis2Query.Controls.Add(this.splitter7);
            this.panHis2Query.Controls.Add(this.panel9);
            this.panHis2Query.Controls.Add(this.txtHis2Remark);
            this.panHis2Query.Controls.Add(this.dtpHis2DateTo);
            this.panHis2Query.Controls.Add(this.dtpHis2DateFrom);
            this.panHis2Query.Controls.Add(this.txtHis2FormPresent);
            this.panHis2Query.Controls.Add(this.txtHis2DStep);
            this.panHis2Query.Controls.Add(this.cmbHis2Flow);
            this.panHis2Query.Controls.Add(this.lblHis2Remark);
            this.panHis2Query.Controls.Add(this.lblHis2DateTo);
            this.panHis2Query.Controls.Add(this.lblHis2DateFrom);
            this.panHis2Query.Controls.Add(this.lblHis2FormPresent);
            this.panHis2Query.Controls.Add(this.lblHis2Dstep);
            this.panHis2Query.Controls.Add(this.lblHis2Flow);
            this.panHis2Query.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panHis2Query.Location = new System.Drawing.Point(3, 39);
            this.panHis2Query.Name = "panHis2Query";
            this.panHis2Query.Size = new System.Drawing.Size(864, 170);
            this.panHis2Query.TabIndex = 17;
            this.panHis2Query.Visible = false;
            // 
            // splitter7
            // 
            this.splitter7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter7.Location = new System.Drawing.Point(0, 117);
            this.splitter7.Name = "splitter7";
            this.splitter7.Size = new System.Drawing.Size(864, 3);
            this.splitter7.TabIndex = 17;
            this.splitter7.TabStop = false;
            // 
            // panel9
            // 
            this.panel9.BackColor = System.Drawing.Color.Gray;
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.btnHis2QueryOK);
            this.panel9.Controls.Add(this.btnHis2QueryCancel);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel9.Location = new System.Drawing.Point(0, 120);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(864, 50);
            this.panel9.TabIndex = 16;
            // 
            // btnHis2QueryOK
            // 
            this.btnHis2QueryOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnHis2QueryOK.Location = new System.Drawing.Point(296, 17);
            this.btnHis2QueryOK.Name = "btnHis2QueryOK";
            this.btnHis2QueryOK.Size = new System.Drawing.Size(75, 23);
            this.btnHis2QueryOK.TabIndex = 14;
            this.btnHis2QueryOK.Text = "OK";
            this.btnHis2QueryOK.UseVisualStyleBackColor = false;
            this.btnHis2QueryOK.Click += new System.EventHandler(this.btnHis2QueryOK_Click);
            // 
            // btnHis2QueryCancel
            // 
            this.btnHis2QueryCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnHis2QueryCancel.Location = new System.Drawing.Point(391, 17);
            this.btnHis2QueryCancel.Name = "btnHis2QueryCancel";
            this.btnHis2QueryCancel.Size = new System.Drawing.Size(75, 23);
            this.btnHis2QueryCancel.TabIndex = 15;
            this.btnHis2QueryCancel.Text = "Cancel";
            this.btnHis2QueryCancel.UseVisualStyleBackColor = false;
            this.btnHis2QueryCancel.Click += new System.EventHandler(this.btnHis2QueryCancel_Click);
            // 
            // txtHis2Remark
            // 
            this.txtHis2Remark.Location = new System.Drawing.Point(519, 59);
            this.txtHis2Remark.Name = "txtHis2Remark";
            this.txtHis2Remark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHis2Remark.Size = new System.Drawing.Size(325, 21);
            this.txtHis2Remark.TabIndex = 13;
            // 
            // dtpHis2DateTo
            // 
            this.dtpHis2DateTo.Checked = false;
            this.dtpHis2DateTo.Location = new System.Drawing.Point(294, 57);
            this.dtpHis2DateTo.Name = "dtpHis2DateTo";
            this.dtpHis2DateTo.ShowCheckBox = true;
            this.dtpHis2DateTo.Size = new System.Drawing.Size(128, 21);
            this.dtpHis2DateTo.TabIndex = 12;
            // 
            // dtpHis2DateFrom
            // 
            this.dtpHis2DateFrom.Checked = false;
            this.dtpHis2DateFrom.Location = new System.Drawing.Point(84, 57);
            this.dtpHis2DateFrom.Name = "dtpHis2DateFrom";
            this.dtpHis2DateFrom.ShowCheckBox = true;
            this.dtpHis2DateFrom.Size = new System.Drawing.Size(128, 21);
            this.dtpHis2DateFrom.TabIndex = 11;
            // 
            // txtHis2FormPresent
            // 
            this.txtHis2FormPresent.Location = new System.Drawing.Point(519, 14);
            this.txtHis2FormPresent.Name = "txtHis2FormPresent";
            this.txtHis2FormPresent.Size = new System.Drawing.Size(128, 21);
            this.txtHis2FormPresent.TabIndex = 10;
            // 
            // txtHis2DStep
            // 
            this.txtHis2DStep.Location = new System.Drawing.Point(294, 14);
            this.txtHis2DStep.Name = "txtHis2DStep";
            this.txtHis2DStep.Size = new System.Drawing.Size(128, 21);
            this.txtHis2DStep.TabIndex = 8;
            // 
            // cmbHis2Flow
            // 
            this.cmbHis2Flow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHis2Flow.FormattingEnabled = true;
            this.cmbHis2Flow.Location = new System.Drawing.Point(83, 15);
            this.cmbHis2Flow.Name = "cmbHis2Flow";
            this.cmbHis2Flow.Size = new System.Drawing.Size(129, 20);
            this.cmbHis2Flow.TabIndex = 7;
            // 
            // lblHis2Remark
            // 
            this.lblHis2Remark.Location = new System.Drawing.Point(453, 59);
            this.lblHis2Remark.Name = "lblHis2Remark";
            this.lblHis2Remark.Size = new System.Drawing.Size(60, 20);
            this.lblHis2Remark.TabIndex = 6;
            this.lblHis2Remark.Text = "remark";
            this.lblHis2Remark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHis2DateTo
            // 
            this.lblHis2DateTo.Location = new System.Drawing.Point(228, 58);
            this.lblHis2DateTo.Name = "lblHis2DateTo";
            this.lblHis2DateTo.Size = new System.Drawing.Size(60, 20);
            this.lblHis2DateTo.TabIndex = 5;
            this.lblHis2DateTo.Text = "date to";
            this.lblHis2DateTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHis2DateFrom
            // 
            this.lblHis2DateFrom.Location = new System.Drawing.Point(18, 58);
            this.lblHis2DateFrom.Name = "lblHis2DateFrom";
            this.lblHis2DateFrom.Size = new System.Drawing.Size(60, 20);
            this.lblHis2DateFrom.TabIndex = 4;
            this.lblHis2DateFrom.Text = "date from";
            this.lblHis2DateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHis2FormPresent
            // 
            this.lblHis2FormPresent.Location = new System.Drawing.Point(428, 15);
            this.lblHis2FormPresent.Name = "lblHis2FormPresent";
            this.lblHis2FormPresent.Size = new System.Drawing.Size(85, 20);
            this.lblHis2FormPresent.TabIndex = 3;
            this.lblHis2FormPresent.Text = "formPresent";
            this.lblHis2FormPresent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHis2Dstep
            // 
            this.lblHis2Dstep.Location = new System.Drawing.Point(228, 15);
            this.lblHis2Dstep.Name = "lblHis2Dstep";
            this.lblHis2Dstep.Size = new System.Drawing.Size(60, 20);
            this.lblHis2Dstep.TabIndex = 1;
            this.lblHis2Dstep.Text = "dstep";
            this.lblHis2Dstep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHis2Flow
            // 
            this.lblHis2Flow.Location = new System.Drawing.Point(18, 15);
            this.lblHis2Flow.Name = "lblHis2Flow";
            this.lblHis2Flow.Size = new System.Drawing.Size(60, 20);
            this.lblHis2Flow.TabIndex = 0;
            this.lblHis2Flow.Text = "flow";
            this.lblHis2Flow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panHisQuery
            // 
            this.panHisQuery.Controls.Add(this.splitter4);
            this.panHisQuery.Controls.Add(this.cmbHisSendTo);
            this.panHisQuery.Controls.Add(this.panel8);
            this.panHisQuery.Controls.Add(this.txtHisRemark);
            this.panHisQuery.Controls.Add(this.dtpHisDateTo);
            this.panHisQuery.Controls.Add(this.dtpHisDateFrom);
            this.panHisQuery.Controls.Add(this.txtHisFormPresent);
            this.panHisQuery.Controls.Add(this.txtHisDstep);
            this.panHisQuery.Controls.Add(this.cmbHisFlow);
            this.panHisQuery.Controls.Add(this.lblHisRemark);
            this.panHisQuery.Controls.Add(this.lblHisDateTo);
            this.panHisQuery.Controls.Add(this.lblHisDateFrom);
            this.panHisQuery.Controls.Add(this.lblHisFormPresent);
            this.panHisQuery.Controls.Add(this.lblHisSendTo);
            this.panHisQuery.Controls.Add(this.lblHisDstep);
            this.panHisQuery.Controls.Add(this.lblHisFlow);
            this.panHisQuery.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panHisQuery.Location = new System.Drawing.Point(3, 39);
            this.panHisQuery.Name = "panHisQuery";
            this.panHisQuery.Size = new System.Drawing.Size(864, 170);
            this.panHisQuery.TabIndex = 6;
            this.panHisQuery.Visible = false;
            // 
            // splitter4
            // 
            this.splitter4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter4.Location = new System.Drawing.Point(0, 117);
            this.splitter4.Name = "splitter4";
            this.splitter4.Size = new System.Drawing.Size(864, 3);
            this.splitter4.TabIndex = 0;
            this.splitter4.TabStop = false;
            // 
            // cmbHisSendTo
            // 
            this.cmbHisSendTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHisSendTo.FormattingEnabled = true;
            this.cmbHisSendTo.Location = new System.Drawing.Point(483, 15);
            this.cmbHisSendTo.Name = "cmbHisSendTo";
            this.cmbHisSendTo.Size = new System.Drawing.Size(128, 20);
            this.cmbHisSendTo.TabIndex = 17;
            // 
            // panel8
            // 
            this.panel8.BackColor = System.Drawing.Color.Gray;
            this.panel8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel8.Controls.Add(this.btnHisQueryOK);
            this.panel8.Controls.Add(this.btnHisQueryCancel);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel8.Location = new System.Drawing.Point(0, 120);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(864, 50);
            this.panel8.TabIndex = 16;
            // 
            // btnHisQueryOK
            // 
            this.btnHisQueryOK.BackColor = System.Drawing.SystemColors.Control;
            this.btnHisQueryOK.Location = new System.Drawing.Point(293, 15);
            this.btnHisQueryOK.Name = "btnHisQueryOK";
            this.btnHisQueryOK.Size = new System.Drawing.Size(75, 23);
            this.btnHisQueryOK.TabIndex = 14;
            this.btnHisQueryOK.Text = "OK";
            this.btnHisQueryOK.UseVisualStyleBackColor = false;
            this.btnHisQueryOK.Click += new System.EventHandler(this.btnHisQueryOK_Click);
            // 
            // btnHisQueryCancel
            // 
            this.btnHisQueryCancel.BackColor = System.Drawing.SystemColors.Control;
            this.btnHisQueryCancel.Location = new System.Drawing.Point(384, 15);
            this.btnHisQueryCancel.Name = "btnHisQueryCancel";
            this.btnHisQueryCancel.Size = new System.Drawing.Size(75, 23);
            this.btnHisQueryCancel.TabIndex = 15;
            this.btnHisQueryCancel.Text = "Cancel";
            this.btnHisQueryCancel.UseVisualStyleBackColor = false;
            this.btnHisQueryCancel.Click += new System.EventHandler(this.btnHisQueryCancel_Click);
            // 
            // txtHisRemark
            // 
            this.txtHisRemark.Location = new System.Drawing.Point(483, 59);
            this.txtHisRemark.Name = "txtHisRemark";
            this.txtHisRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtHisRemark.Size = new System.Drawing.Size(349, 21);
            this.txtHisRemark.TabIndex = 13;
            // 
            // dtpHisDateTo
            // 
            this.dtpHisDateTo.Checked = false;
            this.dtpHisDateTo.Location = new System.Drawing.Point(294, 57);
            this.dtpHisDateTo.Name = "dtpHisDateTo";
            this.dtpHisDateTo.ShowCheckBox = true;
            this.dtpHisDateTo.Size = new System.Drawing.Size(128, 21);
            this.dtpHisDateTo.TabIndex = 12;
            // 
            // dtpHisDateFrom
            // 
            this.dtpHisDateFrom.Checked = false;
            this.dtpHisDateFrom.Location = new System.Drawing.Point(84, 57);
            this.dtpHisDateFrom.Name = "dtpHisDateFrom";
            this.dtpHisDateFrom.ShowCheckBox = true;
            this.dtpHisDateFrom.Size = new System.Drawing.Size(128, 21);
            this.dtpHisDateFrom.TabIndex = 11;
            // 
            // txtHisFormPresent
            // 
            this.txtHisFormPresent.Location = new System.Drawing.Point(704, 14);
            this.txtHisFormPresent.Name = "txtHisFormPresent";
            this.txtHisFormPresent.Size = new System.Drawing.Size(128, 21);
            this.txtHisFormPresent.TabIndex = 10;
            // 
            // txtHisDstep
            // 
            this.txtHisDstep.Location = new System.Drawing.Point(294, 14);
            this.txtHisDstep.Name = "txtHisDstep";
            this.txtHisDstep.Size = new System.Drawing.Size(128, 21);
            this.txtHisDstep.TabIndex = 8;
            // 
            // cmbHisFlow
            // 
            this.cmbHisFlow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHisFlow.FormattingEnabled = true;
            this.cmbHisFlow.Location = new System.Drawing.Point(83, 15);
            this.cmbHisFlow.Name = "cmbHisFlow";
            this.cmbHisFlow.Size = new System.Drawing.Size(129, 20);
            this.cmbHisFlow.TabIndex = 7;
            // 
            // lblHisRemark
            // 
            this.lblHisRemark.Location = new System.Drawing.Point(417, 58);
            this.lblHisRemark.Name = "lblHisRemark";
            this.lblHisRemark.Size = new System.Drawing.Size(60, 20);
            this.lblHisRemark.TabIndex = 6;
            this.lblHisRemark.Text = "remark";
            this.lblHisRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHisDateTo
            // 
            this.lblHisDateTo.Location = new System.Drawing.Point(228, 58);
            this.lblHisDateTo.Name = "lblHisDateTo";
            this.lblHisDateTo.Size = new System.Drawing.Size(60, 20);
            this.lblHisDateTo.TabIndex = 5;
            this.lblHisDateTo.Text = "date to";
            this.lblHisDateTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHisDateFrom
            // 
            this.lblHisDateFrom.Location = new System.Drawing.Point(18, 58);
            this.lblHisDateFrom.Name = "lblHisDateFrom";
            this.lblHisDateFrom.Size = new System.Drawing.Size(60, 20);
            this.lblHisDateFrom.TabIndex = 4;
            this.lblHisDateFrom.Text = "date from";
            this.lblHisDateFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHisFormPresent
            // 
            this.lblHisFormPresent.Location = new System.Drawing.Point(613, 15);
            this.lblHisFormPresent.Name = "lblHisFormPresent";
            this.lblHisFormPresent.Size = new System.Drawing.Size(85, 20);
            this.lblHisFormPresent.TabIndex = 3;
            this.lblHisFormPresent.Text = "formPresent";
            this.lblHisFormPresent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHisSendTo
            // 
            this.lblHisSendTo.Location = new System.Drawing.Point(417, 15);
            this.lblHisSendTo.Name = "lblHisSendTo";
            this.lblHisSendTo.Size = new System.Drawing.Size(60, 20);
            this.lblHisSendTo.TabIndex = 2;
            this.lblHisSendTo.Text = "sendto";
            this.lblHisSendTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHisDstep
            // 
            this.lblHisDstep.Location = new System.Drawing.Point(228, 15);
            this.lblHisDstep.Name = "lblHisDstep";
            this.lblHisDstep.Size = new System.Drawing.Size(60, 20);
            this.lblHisDstep.TabIndex = 1;
            this.lblHisDstep.Text = "dstep";
            this.lblHisDstep.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblHisFlow
            // 
            this.lblHisFlow.Location = new System.Drawing.Point(18, 15);
            this.lblHisFlow.Name = "lblHisFlow";
            this.lblHisFlow.Size = new System.Drawing.Size(60, 20);
            this.lblHisFlow.TabIndex = 0;
            this.lblHisFlow.Text = "flow";
            this.lblHisFlow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dgvToDoHis
            // 
            this.dgvToDoHis.AllowUserToAddRows = false;
            this.dgvToDoHis.AllowUserToDeleteRows = false;
            this.dgvToDoHis.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvToDoHis.ContextMenuStrip = this.cmsToDoHis;
            this.dgvToDoHis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvToDoHis.Location = new System.Drawing.Point(3, 39);
            this.dgvToDoHis.MultiSelect = false;
            this.dgvToDoHis.Name = "dgvToDoHis";
            this.dgvToDoHis.ReadOnly = true;
            this.dgvToDoHis.RowTemplate.Height = 23;
            this.dgvToDoHis.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvToDoHis.Size = new System.Drawing.Size(864, 170);
            this.dgvToDoHis.TabIndex = 3;
            this.dgvToDoHis.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvToDoHis_CellClick);
            this.dgvToDoHis.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvToDoHis_CellDoubleClick);
            this.dgvToDoHis.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvToDoHis_DataBindingComplete);
            this.dgvToDoHis.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvToDoHis_CellPainting);
            this.dgvToDoHis.Sorted += new System.EventHandler(this.dgvToDoHis_Sorted);
            // 
            // cmsToDoHis
            // 
            this.cmsToDoHis.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.returnToolStripMenuItem1,
            this.openToolStripMenuItem1,
            this.refreshToolStripMenuItem1});
            this.cmsToDoHis.Name = "contextMenuStrip1";
            this.cmsToDoHis.Size = new System.Drawing.Size(113, 70);
            this.cmsToDoHis.Opened += new System.EventHandler(this.cmsToDoHis_Opened);
            // 
            // returnToolStripMenuItem1
            // 
            this.returnToolStripMenuItem1.Name = "returnToolStripMenuItem1";
            this.returnToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.returnToolStripMenuItem1.Text = "Return";
            this.returnToolStripMenuItem1.Click += new System.EventHandler(this.returnToolStripMenuItem1_Click);
            // 
            // openToolStripMenuItem1
            // 
            this.openToolStripMenuItem1.Name = "openToolStripMenuItem1";
            this.openToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem1.Text = "Open";
            this.openToolStripMenuItem1.Click += new System.EventHandler(this.openToolStripMenuItem1_Click);
            // 
            // refreshToolStripMenuItem1
            // 
            this.refreshToolStripMenuItem1.Name = "refreshToolStripMenuItem1";
            this.refreshToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.refreshToolStripMenuItem1.Text = "Refresh";
            this.refreshToolStripMenuItem1.Click += new System.EventHandler(this.refreshToolStripMenuItem1_Click);
            // 
            // panToDoHisFilter
            // 
            this.panToDoHisFilter.BackColor = System.Drawing.Color.MintCream;
            this.panToDoHisFilter.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panToDoHisFilter.Controls.Add(this.lnkToDoHisQuery);
            this.panToDoHisFilter.Controls.Add(this.lnkToDoHisRefresh);
            this.panToDoHisFilter.Controls.Add(this.chkSubmitted);
            this.panToDoHisFilter.Controls.Add(this.cmbToDoHisFilter);
            this.panToDoHisFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panToDoHisFilter.Location = new System.Drawing.Point(3, 3);
            this.panToDoHisFilter.Name = "panToDoHisFilter";
            this.panToDoHisFilter.Size = new System.Drawing.Size(864, 36);
            this.panToDoHisFilter.TabIndex = 2;
            // 
            // lnkToDoHisQuery
            // 
            this.lnkToDoHisQuery.AutoSize = true;
            this.lnkToDoHisQuery.DisabledLinkColor = System.Drawing.Color.Blue;
            this.lnkToDoHisQuery.Location = new System.Drawing.Point(231, 11);
            this.lnkToDoHisQuery.Name = "lnkToDoHisQuery";
            this.lnkToDoHisQuery.Size = new System.Drawing.Size(35, 12);
            this.lnkToDoHisQuery.TabIndex = 6;
            this.lnkToDoHisQuery.TabStop = true;
            this.lnkToDoHisQuery.Text = "query";
            this.lnkToDoHisQuery.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkToDoHisQuery.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkToDoHisQuery_LinkClicked);
            // 
            // lnkToDoHisRefresh
            // 
            this.lnkToDoHisRefresh.AutoSize = true;
            this.lnkToDoHisRefresh.DisabledLinkColor = System.Drawing.Color.Blue;
            this.lnkToDoHisRefresh.Location = new System.Drawing.Point(198, 11);
            this.lnkToDoHisRefresh.Name = "lnkToDoHisRefresh";
            this.lnkToDoHisRefresh.Size = new System.Drawing.Size(47, 12);
            this.lnkToDoHisRefresh.TabIndex = 5;
            this.lnkToDoHisRefresh.TabStop = true;
            this.lnkToDoHisRefresh.Text = "refresh";
            this.lnkToDoHisRefresh.VisitedLinkColor = System.Drawing.Color.Blue;
            this.lnkToDoHisRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkToDoHisRefresh_LinkClicked);
            // 
            // chkSubmitted
            // 
            this.chkSubmitted.AutoSize = true;
            this.chkSubmitted.Location = new System.Drawing.Point(130, 11);
            this.chkSubmitted.Name = "chkSubmitted";
            this.chkSubmitted.Size = new System.Drawing.Size(15, 14);
            this.chkSubmitted.TabIndex = 2;
            this.chkSubmitted.UseVisualStyleBackColor = true;
            this.chkSubmitted.CheckedChanged += new System.EventHandler(this.chkSubmitted_CheckedChanged);
            // 
            // cmbToDoHisFilter
            // 
            this.cmbToDoHisFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbToDoHisFilter.FormattingEnabled = true;
            this.cmbToDoHisFilter.Location = new System.Drawing.Point(3, 8);
            this.cmbToDoHisFilter.Name = "cmbToDoHisFilter";
            this.cmbToDoHisFilter.Size = new System.Drawing.Size(121, 20);
            this.cmbToDoHisFilter.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgvNotify);
            this.tabPage4.Controls.Add(this.panel5);
            this.tabPage4.Location = new System.Drawing.Point(4, 21);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(870, 212);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Notify";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgvNotify
            // 
            this.dgvNotify.AllowUserToAddRows = false;
            this.dgvNotify.AllowUserToDeleteRows = false;
            this.dgvNotify.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotify.ContextMenuStrip = this.cmsNotify;
            this.dgvNotify.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNotify.Location = new System.Drawing.Point(0, 33);
            this.dgvNotify.MultiSelect = false;
            this.dgvNotify.Name = "dgvNotify";
            this.dgvNotify.ReadOnly = true;
            this.dgvNotify.RowTemplate.Height = 23;
            this.dgvNotify.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvNotify.Size = new System.Drawing.Size(870, 179);
            this.dgvNotify.TabIndex = 3;
            this.dgvNotify.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNotify_CellClick);
            this.dgvNotify.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvNotify_CellDoubleClick);
            this.dgvNotify.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgvNotify_DataBindingComplete);
            this.dgvNotify.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvNotify_CellPainting);
            this.dgvNotify.Sorted += new System.EventHandler(this.dgvNotify_Sorted);
            // 
            // cmsNotify
            // 
            this.cmsNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem2,
            this.deleteToolStripMenuItem,
            this.refreshToolStripMenuItem2});
            this.cmsNotify.Name = "contextMenuStrip1";
            this.cmsNotify.Size = new System.Drawing.Size(113, 70);
            // 
            // openToolStripMenuItem2
            // 
            this.openToolStripMenuItem2.Name = "openToolStripMenuItem2";
            this.openToolStripMenuItem2.Size = new System.Drawing.Size(112, 22);
            this.openToolStripMenuItem2.Text = "Open";
            this.openToolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.flowDeleteToolStripMenuItem_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.MintCream;
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.lnkNotifyRefresh);
            this.panel5.Controls.Add(this.cmbNotifyFilter);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(0, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(870, 33);
            this.panel5.TabIndex = 2;
            // 
            // lnkNotifyRefresh
            // 
            this.lnkNotifyRefresh.AutoSize = true;
            this.lnkNotifyRefresh.Location = new System.Drawing.Point(130, 9);
            this.lnkNotifyRefresh.Name = "lnkNotifyRefresh";
            this.lnkNotifyRefresh.Size = new System.Drawing.Size(47, 12);
            this.lnkNotifyRefresh.TabIndex = 4;
            this.lnkNotifyRefresh.TabStop = true;
            this.lnkNotifyRefresh.Text = "refresh";
            this.lnkNotifyRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkNotifyRefresh_LinkClicked);
            // 
            // cmbNotifyFilter
            // 
            this.cmbNotifyFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNotifyFilter.FormattingEnabled = true;
            this.cmbNotifyFilter.Location = new System.Drawing.Point(3, 6);
            this.cmbNotifyFilter.Name = "cmbNotifyFilter";
            this.cmbNotifyFilter.Size = new System.Drawing.Size(121, 20);
            this.cmbNotifyFilter.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgvOvertime);
            this.tabPage3.Controls.Add(this.panOvertimeActive);
            this.tabPage3.Location = new System.Drawing.Point(4, 21);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(870, 212);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "overtime";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgvOvertime
            // 
            this.dgvOvertime.AllowUserToAddRows = false;
            this.dgvOvertime.AllowUserToDeleteRows = false;
            this.dgvOvertime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvOvertime.DefaultCellStyle = dataGridViewCellStyle4;
            this.dgvOvertime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOvertime.Location = new System.Drawing.Point(0, 28);
            this.dgvOvertime.MultiSelect = false;
            this.dgvOvertime.Name = "dgvOvertime";
            this.dgvOvertime.ReadOnly = true;
            this.dgvOvertime.RowTemplate.Height = 23;
            this.dgvOvertime.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOvertime.Size = new System.Drawing.Size(870, 184);
            this.dgvOvertime.TabIndex = 2;
            this.dgvOvertime.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOvertime_CellDoubleClick);
            // 
            // panOvertimeActive
            // 
            this.panOvertimeActive.BackColor = System.Drawing.Color.MintCream;
            this.panOvertimeActive.Controls.Add(this.lnkOvertimeRefresh);
            this.panOvertimeActive.Controls.Add(this.lblLevel);
            this.panOvertimeActive.Controls.Add(this.cmbLevel);
            this.panOvertimeActive.Controls.Add(this.chkActive);
            this.panOvertimeActive.Dock = System.Windows.Forms.DockStyle.Top;
            this.panOvertimeActive.Location = new System.Drawing.Point(0, 0);
            this.panOvertimeActive.Name = "panOvertimeActive";
            this.panOvertimeActive.Size = new System.Drawing.Size(870, 28);
            this.panOvertimeActive.TabIndex = 0;
            // 
            // lnkOvertimeRefresh
            // 
            this.lnkOvertimeRefresh.AutoSize = true;
            this.lnkOvertimeRefresh.Location = new System.Drawing.Point(220, 7);
            this.lnkOvertimeRefresh.Name = "lnkOvertimeRefresh";
            this.lnkOvertimeRefresh.Size = new System.Drawing.Size(47, 12);
            this.lnkOvertimeRefresh.TabIndex = 5;
            this.lnkOvertimeRefresh.TabStop = true;
            this.lnkOvertimeRefresh.Text = "refresh";
            this.lnkOvertimeRefresh.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkOvertimeRefresh_LinkClicked);
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(69, 7);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(35, 12);
            this.lblLevel.TabIndex = 2;
            this.lblLevel.Text = "level";
            // 
            // cmbLevel
            // 
            this.cmbLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevel.FormattingEnabled = true;
            this.cmbLevel.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.cmbLevel.Location = new System.Drawing.Point(123, 3);
            this.cmbLevel.Name = "cmbLevel";
            this.cmbLevel.Size = new System.Drawing.Size(76, 20);
            this.cmbLevel.TabIndex = 1;
            this.cmbLevel.SelectedIndexChanged += new System.EventHandler(this.cmbLevel_SelectedIndexChanged);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(3, 5);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(60, 16);
            this.chkActive.TabIndex = 0;
            this.chkActive.Text = "active";
            this.chkActive.UseVisualStyleBackColor = true;
            this.chkActive.CheckedChanged += new System.EventHandler(this.chkActive_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(0, 237);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(881, 20);
            this.panel4.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = global::EEPNetFLClient.Properties.Resources.d4;
            this.pictureBox2.Location = new System.Drawing.Point(861, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(20, 20);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // splitter2
            // 
            this.splitter2.Dock = System.Windows.Forms.DockStyle.Right;
            this.splitter2.Location = new System.Drawing.Point(1075, 24);
            this.splitter2.Name = "splitter2";
            this.splitter2.Size = new System.Drawing.Size(3, 696);
            this.splitter2.TabIndex = 19;
            this.splitter2.TabStop = false;
            // 
            // splitter5
            // 
            this.splitter5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitter5.Location = new System.Drawing.Point(0, 717);
            this.splitter5.Name = "splitter5";
            this.splitter5.Size = new System.Drawing.Size(1075, 3);
            this.splitter5.TabIndex = 22;
            this.splitter5.TabStop = false;
            // 
            // tmFlow
            // 
            this.tmFlow.Enabled = true;
            this.tmFlow.Interval = 180000;
            this.tmFlow.Tick += new System.EventHandler(this.tmFlow_Tick);
            // 
            // splitter6
            // 
            this.splitter6.BackColor = System.Drawing.SystemColors.Control;
            this.splitter6.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter6.Location = new System.Drawing.Point(197, 281);
            this.splitter6.Name = "splitter6";
            this.splitter6.Size = new System.Drawing.Size(878, 3);
            this.splitter6.TabIndex = 25;
            this.splitter6.TabStop = false;
            // 
            // wizToDoList
            // 
            this.wizToDoList.Active = true;
            this.wizToDoList.BindingObject = this.dgvToDoList;
            this.wizToDoList.SqlMode = FLTools.ESqlMode.ToDoList;
            // 
            // wizToDoHis
            // 
            this.wizToDoHis.Active = true;
            this.wizToDoHis.BindingObject = this.dgvToDoHis;
            this.wizToDoHis.SqlMode = FLTools.ESqlMode.ToDoHis;
            // 
            // wizNotify
            // 
            this.wizNotify.Active = true;
            this.wizNotify.BindingObject = this.dgvNotify;
            this.wizNotify.SqlMode = FLTools.ESqlMode.Notify;
            // 
            // wizOvertime
            // 
            this.wizOvertime.Active = true;
            this.wizOvertime.BindingObject = this.dgvOvertime;
            this.wizOvertime.SqlMode = FLTools.ESqlMode.Delay;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.systemToolStripMenuItem,
            this.windowsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1078, 24);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // systemToolStripMenuItem
            // 
            this.systemToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.solutionToolStripMenuItem,
            this.dataBaseToolStripMenuItem,
            this.changePasswordToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.systemToolStripMenuItem.Name = "systemToolStripMenuItem";
            this.systemToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.systemToolStripMenuItem.Text = "System";
            // 
            // solutionToolStripMenuItem
            // 
            this.solutionToolStripMenuItem.Name = "solutionToolStripMenuItem";
            this.solutionToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.solutionToolStripMenuItem.Text = "Solution";
            this.solutionToolStripMenuItem.Click += new System.EventHandler(this.solutionToolStripMenuItem_Click);
            // 
            // dataBaseToolStripMenuItem
            // 
            this.dataBaseToolStripMenuItem.Name = "dataBaseToolStripMenuItem";
            this.dataBaseToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.dataBaseToolStripMenuItem.Text = "DataBase";
            this.dataBaseToolStripMenuItem.Click += new System.EventHandler(this.dataBaseToolStripMenuItem_Click);
            // 
            // changePasswordToolStripMenuItem
            // 
            this.changePasswordToolStripMenuItem.Name = "changePasswordToolStripMenuItem";
            this.changePasswordToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.changePasswordToolStripMenuItem.Text = "ChangePassword";
            this.changePasswordToolStripMenuItem.Click += new System.EventHandler(this.changePasswordToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // windowsToolStripMenuItem
            // 
            this.windowsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tileHorizontalToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.treeViewToolStripMenuItem});
            this.windowsToolStripMenuItem.Name = "windowsToolStripMenuItem";
            this.windowsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.windowsToolStripMenuItem.Text = "Windows";
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.tileHorizontalToolStripMenuItem.Text = "TileHorizontal";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontalToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.tileVerticalToolStripMenuItem.Text = "TileVertical";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.tileVerticalToolStripMenuItem_Click);
            // 
            // treeViewToolStripMenuItem
            // 
            this.treeViewToolStripMenuItem.Checked = true;
            this.treeViewToolStripMenuItem.CheckOnClick = true;
            this.treeViewToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.treeViewToolStripMenuItem.Name = "treeViewToolStripMenuItem";
            this.treeViewToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.treeViewToolStripMenuItem.Text = "TreeView";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutEEPNetClientToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutEEPNetClientToolStripMenuItem
            // 
            this.aboutEEPNetClientToolStripMenuItem.Name = "aboutEEPNetClientToolStripMenuItem";
            this.aboutEEPNetClientToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.aboutEEPNetClientToolStripMenuItem.Text = "About EEP.Net Client";
            this.aboutEEPNetClientToolStripMenuItem.Click += new System.EventHandler(this.aboutEEPNetClientToolStripMenuItem_Click);
            // 
            // frmClientMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.ClientSize = new System.Drawing.Size(1078, 720);
            this.Controls.Add(this.splitter6);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.panFLContainer);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter5);
            this.Controls.Add(this.splitter2);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "frmClientMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EEP.NET Client";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.frmClientMain_Closing);
            this.Load += new System.EventHandler(this.WinForm_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbGo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbMyFavor)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panFLContainer.ResumeLayout(false);
            this.panFLInnerContainer.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.panToDoListQuery.ResumeLayout(false);
            this.panToDoListQuery.PerformLayout();
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDoList)).EndInit();
            this.cmsToDoList.ResumeLayout(false);
            this.panToDoListFilter.ResumeLayout(false);
            this.panToDoListFilter.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.panHis2Query.ResumeLayout(false);
            this.panHis2Query.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panHisQuery.ResumeLayout(false);
            this.panHisQuery.PerformLayout();
            this.panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvToDoHis)).EndInit();
            this.cmsToDoHis.ResumeLayout(false);
            this.panToDoHisFilter.ResumeLayout(false);
            this.panToDoHisFilter.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotify)).EndInit();
            this.cmsNotify.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOvertime)).EndInit();
            this.panOvertimeActive.ResumeLayout(false);
            this.panOvertimeActive.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        [STAThread]
        static void Main()
        {
            String s;
            s = Application.StartupPath + "\\";
            CliUtils.LoadLoginServiceConfig(s + "EEPNetFLClient.exe.config");

            CliUtils.fClientLang = GetClientLanguage();
            CliUtils.fClientSystem = "Win";

            try
            {
                Application.Run(new frmClientMain());
            }
            catch
            {
                Application.Exit();
            }
        }

        static private SYS_LANGUAGE GetClientLanguage()
        {
            uint dwlang = GetThreadLocale();
            ushort wlang = (ushort)dwlang;
            ushort wprilangid = (ushort)(wlang & 0x3FF);
            ushort wsublangid = (ushort)(wlang >> 10);

            if (0x09 == wprilangid)
                return SYS_LANGUAGE.ENG;
            else if (0x04 == wprilangid)
            {
                if (0x01 == wsublangid)
                    return SYS_LANGUAGE.TRA;
                else if (0x02 == wsublangid)
                    return SYS_LANGUAGE.SIM;
                else if (0x03 == wsublangid)
                    return SYS_LANGUAGE.HKG;
                else
                    return SYS_LANGUAGE.TRA;
            }
            else if (0x11 == wprilangid)
                return SYS_LANGUAGE.JPN;
            else
                return SYS_LANGUAGE.ENG;
        }

        private void SaveToClientXML(string sLoginUser, string sLoginDB, string sCurrentProject)
        {
            String sfile = Application.StartupPath + "\\EEPNetClient.xml";
            string sUser = sLoginUser;
            string sDB = sLoginDB;
            string sSol = sCurrentProject;
            string stemp = "";
            XmlDocument xml = new XmlDocument();
            if (File.Exists(sfile))
            {
                xml.Load(sfile);
                XmlNode el = xml.DocumentElement;
                foreach (XmlNode xNode in el.ChildNodes)
                {

                    if (xNode.Name.ToUpper().Equals("USER"))
                    {
                        stemp = xNode.InnerText.Trim();
                        string[] ss = stemp.Split(new char[] { ',' });
                        foreach (string s in ss)
                        {
                            if (!s.Equals(sLoginUser))
                                sUser = sUser + "," + s;
                        }
                    }
                    else if (xNode.Name.ToUpper().Equals("DATABASE"))
                    {
                        stemp = xNode.InnerText.Trim();
                        string[] ss = stemp.Split(new char[] { ',' });
                        foreach (string s in ss)
                        {
                            if (!s.Equals(sLoginDB))
                                sDB = sDB + "," + s;
                        }
                    }
                    else if (xNode.Name.ToUpper().Equals("SOLUTION"))
                    {
                        stemp = xNode.InnerText.Trim();
                        string[] ss = stemp.Split(new char[] { ',' });
                        foreach (string s in ss)
                        {
                            if (!s.Equals(sCurrentProject))
                                sSol = sSol + "," + s;
                        }
                    }
                }

                File.Delete(sfile);
            }
            else
            {
                sUser = sLoginUser; sDB = sLoginDB; sSol = sCurrentProject;
            }

            FileStream aFileStream = new FileStream(sfile, FileMode.Create);
            try
            {
                XmlTextWriter w = new XmlTextWriter(aFileStream, new System.Text.ASCIIEncoding());
                w.Formatting = Formatting.Indented;
                w.WriteStartElement("LoginInfo");

                w.WriteStartElement("User");
                w.WriteValue(sUser);
                w.WriteEndElement();

                w.WriteStartElement("DataBase");
                w.WriteValue(sDB);
                w.WriteEndElement();

                w.WriteStartElement("Solution");
                w.WriteValue(sSol);
                w.WriteEndElement();

                w.WriteEndElement();
                w.Close();
            }
            finally
            {
                aFileStream.Close();
            }
        }

        private bool Register(bool isShowMessage)
        {
            string message = "";
            bool rtn = CliUtils.Register(ref message);
            if (rtn)
            {
                CliUtils.GetSysXml(Application.StartupPath + @"\sysmsg.xml");
            }
            else
            {
                if (isShowMessage)
                {
                    MessageBox.Show(message);
                }
            }

            return rtn;
        }

        bool logincanel = false;
        private static ArrayList ReMenu = new ArrayList();
        private void WinForm_Load(object sender, System.EventArgs e)
        {
            for (int count = 0; count < this.mainMenu1.MenuItems.Count; count++)
                ReMenu.Add(this.mainMenu1.MenuItems[count]);

            bool freg = Register(false);
            fFrmLogin = new frmLogin(freg);

            if (fFrmLogin.ShowDialog(this) == DialogResult.OK)
            {
                if (freg == false)
                {
                    if (!Register(true))
                    {
                        this.Close();
                    }
                }
                CheckUser();
                //TrayIcon support...
                //tmMessage.Enabled = true;
                SetMenuText();
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                CliUtils.fLoginUser = string.Empty;
                Close();
            }
        }

        private void SetMenuText()
        {
            string menutext = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "Menu");
            if (menutext.Length > 0)
            {
                string[] list = menutext.Split(';');
                systemToolStripMenuItem.Text = list[0];
                solutionToolStripMenuItem.Text = list[1];
                dataBaseToolStripMenuItem.Text = list[2];
                changePasswordToolStripMenuItem.Text = list[3];
                exitToolStripMenuItem.Text = list[4];
                windowsToolStripMenuItem.Text = list[5];
                tileHorizontalToolStripMenuItem.Text = list[6];
                tileVerticalToolStripMenuItem.Text = list[7];
                treeViewToolStripMenuItem.Text = list[8];
                helpToolStripMenuItem.Text = list[9];
                aboutEEPNetClientToolStripMenuItem.Text = list[10];
            }
        }

        private void CheckUser()
        {
            CheckUser(false);
        }

        private void CheckUser(bool relogin)
        {
            CliUtils.fLoginUser = fFrmLogin.GetUserId();
            CliUtils.fLoginPassword = fFrmLogin.GetPwd();
            CliUtils.fLoginDB = fFrmLogin.GetDB();
            CliUtils.fCurrentProject = fFrmLogin.GetCurProject();
            LoginResult result = LoginResult.Success;
            if (CliUtils.fLoginUser.Contains("'"))
            {
                result = LoginResult.UserNotFound;
            }
            else
            {
                string sParam = CliUtils.fLoginUser + ':' + CliUtils.fLoginPassword + ':' + CliUtils.fLoginDB;
                if (relogin)
                {
                    sParam += ":1";
                }
                else
                {
                    sParam += ":0";
                }
                object[] myRet = CliUtils.CallMethod("GLModule", "CheckUser", new object[] { (object)sParam });
                result = (LoginResult)myRet[1];
                switch (result)
                {
                    case LoginResult.UserNotFound:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserNotFound");
                            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case LoginResult.PasswordError:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserOrPasswordError");
                            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case LoginResult.UserLogined:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserIsLogined");
                            MessageBox.Show(this, string.Format(message, CliUtils.fLoginUser), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                        }
                    case LoginResult.RequestReLogin:
                        {
                            string message = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPWebNetClient", "WinSysMsg", "msg_UserReLogined");
                            if (MessageBox.Show(string.Format(message, CliUtils.fLoginUser)
                                , "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                            {
                                CheckUser(true);
                            }
                            else
                            {
                                CliUtils.fLoginUser = string.Empty;
                                this.Close();
                            }
                            return;
                        }
                    default:
                        {
                            CliUtils.fUserName = myRet[2].ToString();
                            CliUtils.fLoginUser = myRet[3].ToString();
                            CliUtils.GetPasswordPolicy();
                            myRet = CliUtils.CallMethod("GLModule", "GetUserGroup", new object[] { CliUtils.fLoginUser });
                            if (myRet != null && (int)myRet[0] == 0)
                            {
                                CliUtils.fGroupID = myRet[1].ToString();
                            }
                            myRet = CliUtils.CallMethod("GLModule", "GetUserRole", new object[] { });
                            if (myRet != null && (int)myRet[0] == 0)
                            {
                                CliUtils.Roles = myRet[1].ToString();
                                CliUtils.OrgRoles = myRet[2].ToString();
                                CliUtils.OrgShares = myRet[3].ToString();
                            }
                            DoLoad();
                            SaveToClientXML(CliUtils.fLoginUser, CliUtils.fLoginDB, CliUtils.fCurrentProject);
                            LoadWF();
                            break;
                        }
                }
            }
            if (result != LoginResult.Success)
            {
                if (fFrmLogin.ShowDialog(this) == DialogResult.OK)
                {
                    CheckUser();
                }
                else
                {
                    CliUtils.fLoginUser = string.Empty;
                    this.Close();
                }
            }
        }

        private void fillCombo(ComboBox combo, DataTable src, string field, string prompt)
        {
            combo.Items.Add("<--" + prompt + "-->");
            foreach (DataRow row in src.Rows)
            {
                combo.Items.Add(row[field].ToString());
            }
            combo.SelectedIndex = 0;
        }

        private void LoadWF()
        {
            string selOption = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "SelectOption");

            DataTable list = null;
            object[] ret1 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT DISTINCT SYS_TODOLIST.FLOW_DESC FROM SYS_TODOLIST" });
            if (ret1 != null && (int)ret1[0] == 0)
            {
                list = ((DataSet)ret1[1]).Tables[0];
            }

            this.fillCombo(this.cmbToDoListFilter, list, "FLOW_DESC", selOption);
            this.cmbToDoListFilter.SelectedIndexChanged += new EventHandler(cmbToDoListFilter_SelectedIndexChanged);

            this.fillCombo(this.cmbNotifyFilter, list, "FLOW_DESC", selOption);
            this.cmbNotifyFilter.SelectedIndexChanged += new EventHandler(cmbNotifyFilter_SelectedIndexChanged);

            this.fillCombo(this.cmbToDoHisFilter, list, "FLOW_DESC", selOption);
            this.cmbToDoHisFilter.SelectedIndexChanged += new EventHandler(cmbToDoHisFilter_SelectedIndexChanged);

            this.fillCombo(this.cmbLstFlow, list, "FLOW_DESC", selOption);

            this.fillCombo(this.cmbHisFlow, list, "FLOW_DESC", selOption);

            this.fillCombo(this.cmbHis2Flow, list, "FLOW_DESC", selOption);

            DataTable users = null;
            object[] ret2 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT USERS.USERID, USERS.USERNAME FROM USERS WHERE USERS.USERID IN (SELECT USERGROUPS.USERID FROM USERGROUPS WHERE USERGROUPS.GROUPID IN(SELECT GROUPS.GROUPID FROM GROUPS WHERE GROUPS.ISROLE='Y'))" });
            if (ret2 != null && (int)ret2[0] == 0)
            {
                users = ((DataSet)ret2[1]).Tables[0];
            }
            this.fillCombo(this.cmbLstUser, users, "USERNAME", selOption);

            DataTable roles = null;
            object[] ret3 = CliUtils.CallMethod("GLModule", "ExcuteWorkFlow", new object[] { "SELECT GROUPID, GROUPNAME FROM GROUPS WHERE ISROLE='Y'" });
            if (ret3 != null && (int)ret3[0] == 0)
            {
                roles = ((DataSet)ret3[1]).Tables[0];
            }
            this.fillCombo(this.cmbHisSendTo, roles, "GROUPNAME", selOption);


            this.dgvToDoList.AutoGenerateColumns = false;
            this.dgvToDoHis.AutoGenerateColumns = false;
            this.dgvNotify.AutoGenerateColumns = false;
            this.dgvOvertime.AutoGenerateColumns = false;

            this.wizToDoList.Refresh();
            this.wizToDoHis.Refresh();
            this.wizNotify.Refresh();

            setToDoListColumns();
            setToDoHistColumns();
            setNotifyColumns();
            setOvertimeColumns();

            this.cmbLevel.SelectedIndex = 2;

            setToDoListOvertimeWarning();
            setToDoHisOvertimeWarning();
            setNotifyOvertimeWarning();

            string[] overtimeCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "OvertimeColumns").Split(',');
            this.lblLevel.Text = overtimeCols[7];
            this.chkActive.Text = overtimeCols[8];

            string[] uiTexts = SysMsg.GetSystemMessage(CliUtils.fClientLang, "Web", "webClientMainFlow", "UIText").Split(';');
            this.lnkToDoListRefresh.Text = uiTexts[1];
            this.lnkToDoHisRefresh.Text = uiTexts[1];
            this.lnkNotifyRefresh.Text = uiTexts[1];
            this.lnkOvertimeRefresh.Text = uiTexts[1];
            this.lnkToDoListQuery.Text = uiTexts[17];
            this.lnkToDoHisQuery.Text = uiTexts[17];
            this.tabPage1.Text = uiTexts[2];
            this.tabPage2.Text = uiTexts[3];
            this.tabPage3.Text = uiTexts[4];
            this.tabPage4.Text = uiTexts[16];
            //cmsToDoList
            this.approveToolStripMenuItem.Text = uiTexts[6];
            this.rejectToolStripMenuItem.Text = uiTexts[7];
            this.returnToolStripMenuItem.Text = uiTexts[8];
            this.openToolStripMenuItem.Text = uiTexts[9];
            this.refreshToolStripMenuItem.Text = uiTexts[10];
            //cmsToDoHis
            this.returnToolStripMenuItem1.Text = uiTexts[14];
            this.openToolStripMenuItem1.Text = uiTexts[9];
            this.refreshToolStripMenuItem1.Text = uiTexts[10];
            //cmsNotify
            this.openToolStripMenuItem2.Text = uiTexts[9];
            this.deleteToolStripMenuItem.Text = uiTexts[15];
            this.refreshToolStripMenuItem2.Text = uiTexts[10];

            string[] queryCaptions = SysMsg.GetSystemMessage(CliUtils.fClientLang, "Web", "webClientMainFlow", "QueryCaption").Split(';');
            this.lblLstFlow.Text = queryCaptions[0];
            this.lblLstDstep.Text = queryCaptions[1];
            this.lblLstUser.Text = queryCaptions[2];
            this.lblLstFormPresent.Text = queryCaptions[3];
            this.lblLstRemark.Text = queryCaptions[4];
            this.lblLstDateFrom.Text = queryCaptions[5];
            this.lblLstDateTo.Text = queryCaptions[6];
            this.btnLstQueryOK.Text = queryCaptions[9];
            this.btnLstQueryCancel.Text = queryCaptions[10];

            this.lblHisFlow.Text = queryCaptions[0];
            this.lblHisDstep.Text = queryCaptions[1];
            this.lblHisSendTo.Text = queryCaptions[7];
            this.lblHisFormPresent.Text = queryCaptions[3];
            this.lblHisDateFrom.Text = queryCaptions[5];
            this.lblHisDateTo.Text = queryCaptions[6];
            this.lblHisRemark.Text = queryCaptions[4];
            this.btnHisQueryOK.Text = queryCaptions[9];
            this.btnHisQueryCancel.Text = queryCaptions[10];

            this.lblHis2Flow.Text = queryCaptions[0];
            this.lblHis2Dstep.Text = queryCaptions[1];
            this.lblHis2FormPresent.Text = queryCaptions[3];
            this.lblHis2DateFrom.Text = queryCaptions[5];
            this.lblHis2DateTo.Text = queryCaptions[6];
            this.lblHis2Remark.Text = queryCaptions[4];
            this.btnHis2QueryOK.Text = queryCaptions[9];
            this.btnHis2QueryCancel.Text = queryCaptions[10];

            this.chkActive.Checked = true;

            lstStatus = SysMsg.GetSystemMessage(CliUtils.fClientLang, "FLDesigner", "FLDesigner", "Item3").Split(',');
        }

        private void setToDoListOvertimeWarning()
        {
            foreach (DataGridViewRow dgvRow in this.dgvToDoList.Rows)
            {
                DataRowView row = (DataRowView)dgvRow.DataBoundItem;
                if (IsOverTime(row))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.ForeColor = Color.Red;
                    dgvRow.DefaultCellStyle = style;
                }
            }
        }

        private void setNotifyOvertimeWarning()
        {
            foreach (DataGridViewRow dgvRow in this.dgvNotify.Rows)
            {
                DataRowView row = (DataRowView)dgvRow.DataBoundItem;
                if (IsOverTime(row))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.ForeColor = Color.Red;
                    dgvRow.DefaultCellStyle = style;
                }
            }
        }

        private void setToDoHisOvertimeWarning()
        {
            foreach (DataGridViewRow dgvRow in this.dgvToDoHis.Rows)
            {
                DataRowView row = (DataRowView)dgvRow.DataBoundItem;
                if (IsOverTime(row))
                {
                    DataGridViewCellStyle style = new DataGridViewCellStyle();
                    style.ForeColor = Color.Red;
                    dgvRow.DefaultCellStyle = style;
                }
            }
        }

        private TimeSpan WorkTimeSpan(DateTime nowTime, DateTime updateTime, bool weekendSensible, List<string> extDates)
        {
            TimeSpan span = new TimeSpan();
            if (weekendSensible)
            {
                if (nowTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    nowTime = nowTime.Date.AddSeconds(-1);
                }
                else if (nowTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    nowTime = nowTime.Date.AddDays(-1).AddSeconds(-1);
                }

                if (updateTime.DayOfWeek == DayOfWeek.Saturday)
                {
                    updateTime = updateTime.Date.AddDays(2);
                }
                else if (updateTime.DayOfWeek == DayOfWeek.Sunday)
                {
                    updateTime = updateTime.Date.AddDays(1);
                }
            }
            span = nowTime - updateTime;
            if (weekendSensible)
            {
                int weekends = span.Days / 7;
                int i = nowTime.DayOfWeek - updateTime.DayOfWeek;
                if (i < 0)
                    weekends++;
                span = span.Subtract(new TimeSpan(2 * weekends, 0, 0, 0));
            }
            int extDays = 0;
            if (extDates == null) return span;
            foreach (string extDate in extDates)
            {
                if (Convert.ToDateTime(extDate).CompareTo(nowTime) < 0
                    && Convert.ToDateTime(extDate).CompareTo(updateTime) > 0)
                {
                    if (weekendSensible)
                    {
                        if (Convert.ToDateTime(extDate).DayOfWeek != DayOfWeek.Saturday
                            && Convert.ToDateTime(extDate).DayOfWeek != DayOfWeek.Sunday)
                        {
                            extDays++;
                        }
                    }
                    else
                    {
                        extDays++;
                    }
                }
            }
            span = span.Subtract(new TimeSpan(extDays, 0, 0, 0));
            return span;
        }

        private bool IsOverTime(DataRowView row)
        {
            string TIME_UNIT = row["TIME_UNIT"].ToString();
            string FLOWURGENT = row["FLOWURGENT"].ToString();
            string UPDATE_WHOLE_TIME = row["UPDATE_WHOLE_TIME"].ToString();
            string UPDATE_DATE = UPDATE_WHOLE_TIME.Substring(0, UPDATE_WHOLE_TIME.IndexOf(' '));
            string UPDATE_TIME = UPDATE_WHOLE_TIME.Substring(UPDATE_WHOLE_TIME.IndexOf(' ') + 1);
            string URGENT_TIME = row["URGENT_TIME"].ToString();
            string EXP_TIME = row["EXP_TIME"].ToString();

            if (TIME_UNIT == "Day" && FLOWURGENT == "1")
            {
                if (Convert.ToDecimal(URGENT_TIME) == Decimal.Zero) return false;
                TimeSpan span = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), this.IgnoreWeekends, null);
                int overtimes = span.Days - Convert.ToInt32(Convert.ToDecimal(URGENT_TIME));
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            else if (TIME_UNIT == "Day" && FLOWURGENT == "0")
            {
                if (Convert.ToDecimal(EXP_TIME) == Decimal.Zero) return false;
                TimeSpan span = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), this.IgnoreWeekends, null);
                int overtimes = span.Days - Convert.ToInt32(Convert.ToDecimal(EXP_TIME));
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            else if (TIME_UNIT == "Hour" && FLOWURGENT == "1")
            {
                if (Convert.ToDecimal(URGENT_TIME) == Decimal.Zero) return false;
                TimeSpan spanDay = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), this.IgnoreWeekends, null);
                int spanHour = DateTime.Now.Hour - Convert.ToDateTime(UPDATE_TIME).Hour;
                int overtimes = spanDay.Days * 8 + spanHour - Convert.ToInt32(Convert.ToDecimal(URGENT_TIME));
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            else if (TIME_UNIT == "Hour" && FLOWURGENT == "0")
            {
                if (Convert.ToDecimal(EXP_TIME) == Decimal.Zero) return false;
                TimeSpan spanDay = WorkTimeSpan(DateTime.Now.Date, Convert.ToDateTime(UPDATE_DATE), this.IgnoreWeekends, null);
                int spanHour = DateTime.Now.Hour - Convert.ToDateTime(UPDATE_TIME).Hour;
                int overtimes = spanDay.Days * 8 + spanHour - Convert.ToInt32(Convert.ToDecimal(EXP_TIME));
                if (overtimes >= 0)
                {
                    return true;
                }
            }
            return false;
        }

        private void cmbToDoListFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbToDoListFilter.SelectedIndex > 0)
            {
                this.wizToDoList.Filter = "SYS_TODOLIST.FLOW_DESC='" + cmbToDoListFilter.Text + "'";
                this.wizToDoList.Refresh();
                this.setToDoListOvertimeWarning();
            }
        }

        private void cmbNotifyFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbNotifyFilter.SelectedIndex > 0)
            {
                this.wizNotify.Filter = "SYS_TODOLIST.FLOW_DESC='" + cmbNotifyFilter.Text + "'";
                this.wizNotify.Refresh();
                this.setNotifyOvertimeWarning();
            }
        }

        private void cmbToDoHisFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbToDoHisFilter.SelectedIndex > 0)
            {
                //this.wizToDoHis.Filter = "SYS_TODOLIST.FLOW_DESC='" + cmbToDoHisFilter.Text + "'";
                if (wizToDoHis.SqlMode == FLTools.ESqlMode.ToDoHis)
                    this.wizToDoHis.Filter = "SYS_TODOLIST.FLOW_DESC='" + cmbToDoHisFilter.Text + "'";
                else if (wizToDoHis.SqlMode == FLTools.ESqlMode.FlowRunOver)
                    this.wizToDoHis.Filter = "SYS_TODOHIS.FLOW_DESC='" + cmbToDoHisFilter.Text + "'";
            }
            else
                this.wizToDoHis.Filter = "";
            this.wizToDoHis.Refresh();
            if (this.wizToDoHis.SqlMode == ESqlMode.ToDoHis)
            {
                this.setToDoHisOvertimeWarning();
            }
        }

        private void setToDoListColumns()
        {
            DataGridViewImageColumn urgentColumn = new DataGridViewImageColumn(false);
            urgentColumn.Name = "colUrgent";
            urgentColumn.HeaderText = "";
            urgentColumn.Image = global::EEPNetFLClient.Properties.Resources._064160;
            urgentColumn.Width = 20;
            urgentColumn.Frozen = true;
            this.dgvToDoList.Columns.Insert(0, urgentColumn);

            DataGridViewImageColumn importantColumn = new DataGridViewImageColumn(false);
            importantColumn.Name = "colImportant";
            importantColumn.HeaderText = "";
            importantColumn.Image = global::EEPNetFLClient.Properties.Resources._064080;
            importantColumn.Width = 20;
            importantColumn.Frozen = true;
            this.dgvToDoList.Columns.Insert(0, importantColumn);

            string[] toDoListCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "ToDoListColumns").Split(',');
            //FLOW_DESC
            DataGridViewTextBoxColumn flDescColumn = new DataGridViewTextBoxColumn();
            flDescColumn.Name = "colFlDesc";
            flDescColumn.HeaderText = toDoListCols[0];
            flDescColumn.Width = 100;
            flDescColumn.DataPropertyName = "FLOW_DESC";
            this.dgvToDoList.Columns.Add(flDescColumn);
            //D_STEP_ID
            DataGridViewTextBoxColumn dStepIdColumn = new DataGridViewTextBoxColumn();
            dStepIdColumn.Name = "colDStepId";
            dStepIdColumn.HeaderText = toDoListCols[1];
            dStepIdColumn.Width = 80;
            dStepIdColumn.DataPropertyName = "D_STEP_ID";
            this.dgvToDoList.Columns.Add(dStepIdColumn);
            //FORM_PRESENTATION_CT
            DataGridViewTextBoxColumn formPresentationCtColumn = new DataGridViewTextBoxColumn();
            formPresentationCtColumn.Name = "colFormPresentationCT";
            formPresentationCtColumn.HeaderText = toDoListCols[2];
            formPresentationCtColumn.Width = 100;
            formPresentationCtColumn.DataPropertyName = "FORM_PRESENT_CT";
            this.dgvToDoList.Columns.Add(formPresentationCtColumn);
            //USERNAME
            DataGridViewTextBoxColumn userNameColumn = new DataGridViewTextBoxColumn();
            userNameColumn.Name = "colUserName";
            userNameColumn.HeaderText = toDoListCols[3];
            userNameColumn.Width = 80;
            userNameColumn.DataPropertyName = "USERNAME";
            this.dgvToDoList.Columns.Add(userNameColumn);
            //REMARK
            DataGridViewTextBoxColumn remarkColumn = new DataGridViewTextBoxColumn();
            remarkColumn.Name = "colRemark";
            remarkColumn.HeaderText = toDoListCols[4];
            remarkColumn.Width = 120;
            remarkColumn.DataPropertyName = "REMARK";
            this.dgvToDoList.Columns.Add(remarkColumn);
            //UPDATE_WHOLE_TIME
            DataGridViewTextBoxColumn updateWholeTimeColumn = new DataGridViewTextBoxColumn();
            updateWholeTimeColumn.Name = "colUpdateWholeTime";
            updateWholeTimeColumn.HeaderText = toDoListCols[5];
            updateWholeTimeColumn.Width = 120;
            updateWholeTimeColumn.DataPropertyName = "UPDATE_WHOLE_TIME";
            this.dgvToDoList.Columns.Add(updateWholeTimeColumn);
            //STATUS
            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.Name = "colStatus";
            statusColumn.HeaderText = toDoListCols[6];
            statusColumn.Width = 80;
            statusColumn.DataPropertyName = "STATUS";
            this.dgvToDoList.Columns.Add(statusColumn);
        }

        private void setToDoHistColumns()
        {
            this.dgvToDoHis.Columns.Clear();
            if (this.wizToDoHis.SqlMode == ESqlMode.ToDoHis)
            {
                #region ToDoHis
                DataGridViewImageColumn urgentColumn = new DataGridViewImageColumn(false);
                urgentColumn.Name = "colUrgent";
                urgentColumn.HeaderText = "";
                urgentColumn.Image = global::EEPNetFLClient.Properties.Resources._064160;
                urgentColumn.Width = 20;
                urgentColumn.Frozen = true;
                this.dgvToDoHis.Columns.Insert(0, urgentColumn);

                DataGridViewImageColumn importantColumn = new DataGridViewImageColumn(false);
                importantColumn.Name = "colImportant";
                importantColumn.HeaderText = "";
                importantColumn.Image = global::EEPNetFLClient.Properties.Resources._064080;
                importantColumn.Width = 20;
                importantColumn.Frozen = true;
                this.dgvToDoHis.Columns.Insert(0, importantColumn);

                string[] toDoListCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "ToDoListColumns").Split(',');
                this.chkSubmitted.Text = toDoListCols[10];
                //FLOW_DESC
                DataGridViewTextBoxColumn flDescColumn = new DataGridViewTextBoxColumn();
                flDescColumn.Name = "colFlDesc";
                flDescColumn.HeaderText = toDoListCols[0];
                flDescColumn.Width = 100;
                flDescColumn.DataPropertyName = "FLOW_DESC";
                this.dgvToDoHis.Columns.Add(flDescColumn);

                DataGridViewTextBoxColumn flapplicantColumn = new DataGridViewTextBoxColumn();
                flapplicantColumn.Name = "colFlApplicant";
                flapplicantColumn.HeaderText = "申请人";
                flapplicantColumn.Width = 100;
                flapplicantColumn.DataPropertyName = "APPLICANT";
                this.dgvToDoHis.Columns.Add(flapplicantColumn);



                //D_STEP_ID
                DataGridViewTextBoxColumn dStepIdColumn = new DataGridViewTextBoxColumn();
                dStepIdColumn.Name = "colDStepId";
                dStepIdColumn.HeaderText = toDoListCols[1];
                dStepIdColumn.Width = 80;
                dStepIdColumn.DataPropertyName = "D_STEP_ID";
                this.dgvToDoHis.Columns.Add(dStepIdColumn);
                //FORM_PRESENTATION_CT
                DataGridViewTextBoxColumn formPresentationCtColumn = new DataGridViewTextBoxColumn();
                formPresentationCtColumn.Name = "colFormPresentationCT";
                formPresentationCtColumn.HeaderText = toDoListCols[2];
                formPresentationCtColumn.Width = 100;
                formPresentationCtColumn.DataPropertyName = "FORM_PRESENT_CT";
                this.dgvToDoHis.Columns.Add(formPresentationCtColumn);
                //SENDTO_NAME
                DataGridViewTextBoxColumn sendToNameColumn = new DataGridViewTextBoxColumn();
                sendToNameColumn.Name = "colSendToName";
                sendToNameColumn.HeaderText = toDoListCols[7];
                sendToNameColumn.Width = 80;
                sendToNameColumn.DataPropertyName = "SENDTO_NAME";
                this.dgvToDoHis.Columns.Add(sendToNameColumn);
                //REMARK
                DataGridViewTextBoxColumn remarkColumn = new DataGridViewTextBoxColumn();
                remarkColumn.Name = "colRemark";
                remarkColumn.HeaderText = toDoListCols[4];
                remarkColumn.Width = 120;
                remarkColumn.DataPropertyName = "REMARK";
                this.dgvToDoHis.Columns.Add(remarkColumn);
                //UPDATE_WHOLE_TIME
                DataGridViewTextBoxColumn updateWholeTimeColumn = new DataGridViewTextBoxColumn();
                updateWholeTimeColumn.Name = "colUpdateWholeTime";
                updateWholeTimeColumn.HeaderText = toDoListCols[5];
                updateWholeTimeColumn.Width = 120;
                updateWholeTimeColumn.DataPropertyName = "UPDATE_WHOLE_TIME";
                this.dgvToDoHis.Columns.Add(updateWholeTimeColumn);
                //STATUS
                DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
                statusColumn.Name = "colStatus";
                statusColumn.HeaderText = toDoListCols[6];
                statusColumn.Width = 80;
                statusColumn.DataPropertyName = "STATUS";
                this.dgvToDoHis.Columns.Add(statusColumn);
                #endregion
            }
            else if (this.wizToDoHis.SqlMode == ESqlMode.FlowRunOver)
            {
                #region FlowRunOver
                string[] toDoListCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "ToDoListColumns").Split(',');
                this.chkSubmitted.Text = toDoListCols[10];
                //FLOW_DESC
                DataGridViewTextBoxColumn flDescColumn = new DataGridViewTextBoxColumn();
                flDescColumn.Name = "colFlDesc";
                flDescColumn.HeaderText = toDoListCols[0];
                flDescColumn.Width = 100;
                flDescColumn.DataPropertyName = "FLOW_DESC";
                this.dgvToDoHis.Columns.Add(flDescColumn);
                //D_STEP_ID
                DataGridViewTextBoxColumn stepIdColumn = new DataGridViewTextBoxColumn();
                stepIdColumn.Name = "colStepId";
                stepIdColumn.HeaderText = toDoListCols[1];
                stepIdColumn.Width = 100;
                stepIdColumn.DataPropertyName = "D_STEP_ID";
                this.dgvToDoHis.Columns.Add(stepIdColumn);
                //FORM_PRESENT_CT
                DataGridViewTextBoxColumn formPresentationCtColumn = new DataGridViewTextBoxColumn();
                formPresentationCtColumn.Name = "colFormPresentationCT";
                formPresentationCtColumn.HeaderText = toDoListCols[2];
                formPresentationCtColumn.Width = 100;
                formPresentationCtColumn.DataPropertyName = "FORM_PRESENT_CT";
                this.dgvToDoHis.Columns.Add(formPresentationCtColumn);
                //REMARK
                DataGridViewTextBoxColumn remarkColumn = new DataGridViewTextBoxColumn();
                remarkColumn.Name = "colRemark";
                remarkColumn.HeaderText = toDoListCols[4];
                remarkColumn.Width = 120;
                remarkColumn.DataPropertyName = "REMARK";
                this.dgvToDoHis.Columns.Add(remarkColumn);
                //UPDATE_WHOLE_TIME
                DataGridViewTextBoxColumn updateWholeTimeColumn = new DataGridViewTextBoxColumn();
                updateWholeTimeColumn.Name = "colUpdateWholeTime";
                updateWholeTimeColumn.HeaderText = toDoListCols[5];
                updateWholeTimeColumn.Width = 120;
                updateWholeTimeColumn.DataPropertyName = "UPDATE_WHOLE_TIME";
                this.dgvToDoHis.Columns.Add(updateWholeTimeColumn);
                #endregion
            }
        }

        private void setNotifyColumns()
        {
            DataGridViewImageColumn urgentColumn = new DataGridViewImageColumn(false);
            urgentColumn.Name = "colUrgent";
            urgentColumn.HeaderText = "";
            urgentColumn.Image = global::EEPNetFLClient.Properties.Resources._064160;
            urgentColumn.Width = 20;
            urgentColumn.Frozen = true;
            this.dgvNotify.Columns.Insert(0, urgentColumn);

            DataGridViewImageColumn importantColumn = new DataGridViewImageColumn(false);
            importantColumn.Name = "colImportant";
            importantColumn.HeaderText = "";
            importantColumn.Image = global::EEPNetFLClient.Properties.Resources._064080;
            importantColumn.Width = 20;
            importantColumn.Frozen = true;
            this.dgvNotify.Columns.Insert(0, importantColumn);

            string[] toDoListCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "ToDoListColumns").Split(',');
            //FLOW_DESC
            DataGridViewTextBoxColumn flDescColumn = new DataGridViewTextBoxColumn();
            flDescColumn.Name = "colFlDesc";
            flDescColumn.HeaderText = toDoListCols[0];
            flDescColumn.Width = 100;
            flDescColumn.DataPropertyName = "FLOW_DESC";
            this.dgvNotify.Columns.Add(flDescColumn);
            //D_STEP_ID
            DataGridViewTextBoxColumn dStepIdColumn = new DataGridViewTextBoxColumn();
            dStepIdColumn.Name = "colDStepId";
            dStepIdColumn.HeaderText = toDoListCols[1];
            dStepIdColumn.Width = 80;
            dStepIdColumn.DataPropertyName = "D_STEP_ID";
            this.dgvNotify.Columns.Add(dStepIdColumn);
            //FORM_PRESENTATION_CT
            DataGridViewTextBoxColumn formPresentationCtColumn = new DataGridViewTextBoxColumn();
            formPresentationCtColumn.Name = "colFormPresentationCT";
            formPresentationCtColumn.HeaderText = toDoListCols[2];
            formPresentationCtColumn.Width = 100;
            formPresentationCtColumn.DataPropertyName = "FORM_PRESENT_CT";
            this.dgvNotify.Columns.Add(formPresentationCtColumn);
            //USERNAME
            DataGridViewTextBoxColumn userNameColumn = new DataGridViewTextBoxColumn();
            userNameColumn.Name = "colUserName";
            userNameColumn.HeaderText = toDoListCols[3];
            userNameColumn.Width = 80;
            userNameColumn.DataPropertyName = "USERNAME";
            this.dgvNotify.Columns.Add(userNameColumn);
            //REMARK
            DataGridViewTextBoxColumn remarkColumn = new DataGridViewTextBoxColumn();
            remarkColumn.Name = "colRemark";
            remarkColumn.HeaderText = toDoListCols[4];
            remarkColumn.Width = 120;
            remarkColumn.DataPropertyName = "REMARK";
            this.dgvNotify.Columns.Add(remarkColumn);
            //UPDATE_WHOLE_TIME
            DataGridViewTextBoxColumn updateWholeTimeColumn = new DataGridViewTextBoxColumn();
            updateWholeTimeColumn.Name = "colUpdateWholeTime";
            updateWholeTimeColumn.HeaderText = toDoListCols[5];
            updateWholeTimeColumn.Width = 120;
            updateWholeTimeColumn.DataPropertyName = "UPDATE_WHOLE_TIME";
            this.dgvNotify.Columns.Add(updateWholeTimeColumn);
            //STATUS
            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.Name = "colStatus";
            statusColumn.HeaderText = toDoListCols[6];
            statusColumn.Width = 80;
            statusColumn.DataPropertyName = "STATUS";
            this.dgvNotify.Columns.Add(statusColumn);
        }

        private void setOvertimeColumns()
        {
            string[] overtimeCols = SysMsg.GetSystemMessage(CliUtils.fClientLang, "EEPNetClient", "FrmClientMain", "OvertimeColumns").Split(',');
            //FLOW_DESC
            DataGridViewTextBoxColumn flDescColumn = new DataGridViewTextBoxColumn();
            flDescColumn.Name = "colFlDesc";
            flDescColumn.HeaderText = overtimeCols[0];
            flDescColumn.Width = 100;
            flDescColumn.DataPropertyName = "FLOW_DESC";
            this.dgvOvertime.Columns.Add(flDescColumn);
            //D_STEP_ID
            DataGridViewTextBoxColumn dStepIdColumn = new DataGridViewTextBoxColumn();
            dStepIdColumn.Name = "colDStepId";
            dStepIdColumn.HeaderText = overtimeCols[1];
            dStepIdColumn.Width = 80;
            dStepIdColumn.DataPropertyName = "D_STEP_ID";
            this.dgvOvertime.Columns.Add(dStepIdColumn);
            //FORM_PRESENTATION_CT
            DataGridViewTextBoxColumn formPresentationCtColumn = new DataGridViewTextBoxColumn();
            formPresentationCtColumn.Name = "colFormPresentationCT";
            formPresentationCtColumn.HeaderText = overtimeCols[2];
            formPresentationCtColumn.Width = 100;
            formPresentationCtColumn.DataPropertyName = "FORM_PRESENT_CT";
            this.dgvOvertime.Columns.Add(formPresentationCtColumn);
            //SENDTO_DETAIL
            DataGridViewTextBoxColumn sendToDetailColumn = new DataGridViewTextBoxColumn();
            sendToDetailColumn.Name = "colSendToDetail";
            sendToDetailColumn.HeaderText = overtimeCols[3];
            sendToDetailColumn.Width = 100;
            sendToDetailColumn.DataPropertyName = "SENDTO_DETAIL";
            this.dgvOvertime.Columns.Add(sendToDetailColumn);
            //REMARK
            DataGridViewTextBoxColumn remarkColumn = new DataGridViewTextBoxColumn();
            remarkColumn.Name = "colRemark";
            remarkColumn.HeaderText = overtimeCols[4];
            remarkColumn.Width = 100;
            remarkColumn.DataPropertyName = "REMARK";
            this.dgvOvertime.Columns.Add(remarkColumn);
            //UPDATE_WHOLE_TIME
            DataGridViewTextBoxColumn wholeTimeColumn = new DataGridViewTextBoxColumn();
            wholeTimeColumn.Name = "colWholeTime";
            wholeTimeColumn.HeaderText = overtimeCols[5];
            wholeTimeColumn.Width = 100;
            wholeTimeColumn.DataPropertyName = "UPDATE_WHOLE_TIME";
            this.dgvOvertime.Columns.Add(wholeTimeColumn);
            //OVERTIME
            DataGridViewTextBoxColumn overtimeColumn = new DataGridViewTextBoxColumn();
            overtimeColumn.Name = "colOvertime";
            overtimeColumn.HeaderText = overtimeCols[6];
            overtimeColumn.Width = 100;
            overtimeColumn.DataPropertyName = "OVERTIME";
            this.dgvOvertime.Columns.Add(overtimeColumn);
        }

        bool solutionLoad = false;
        public void DoLoad()
        {
            DataSet dsSolution = new DataSet();
            object[] myRet1 = CliUtils.CallMethod("GLModule", "GetSolution", null);
            if ((null != myRet1) && (0 == (int)myRet1[0]))
                dsSolution = ((DataSet)myRet1[1]);
            this.infoCmbSolution.DataSource = dsSolution;
            string strTableName = dsSolution.Tables[0].TableName;
            this.infoCmbSolution.DisplayMember = strTableName + ".itemname";
            this.infoCmbSolution.ValueMember = strTableName + ".itemtype";
            int i = dsSolution.Tables[0].Rows.Count;
            for (int j = 0; j < i; j++)
            {
                if (dsSolution.Tables[0].Rows[j]["itemtype"].ToString().ToUpper() == CliUtils.fCurrentProject.ToUpper())
                {
                    this.infoCmbSolution.SelectedValue = dsSolution.Tables[0].Rows[j]["itemtype"].ToString();
                }
            }
            solutionLoad = true;
            mainMenu1.MenuItems.Clear();
            for (int count = 0; count < ReMenu.Count; count++)
                mainMenu1.MenuItems.Add((MenuItem)ReMenu[count]);
            tView.Nodes.Clear();
            ItemToGet();
            foreach (TreeNode node in tView.Nodes)
            {
                node.Expand();
            }
        }

        private DataSet menuFavorDataSet;
        private DataSet groupFavorDataSet;
        private void ItemToGet()
        {
            ClearItems();
            if (this.infoCmbSolution.SelectedValue != null)
            {
                object[] LoginUser = new object[1];
                LoginUser[0] = CliUtils.fLoginUser;
                object[] strParam = new object[2];
                strParam[0] = this.infoCmbSolution.SelectedValue.ToString();
                strParam[1] = "F";

                string strCaption = SetMenuLanguage();

                object[] isTableExist = CliUtils.CallMethod("GLModule", "isTableExist", new object[] { "MENUFAVOR" });
                if (isTableExist != null && Convert.ToInt16(isTableExist[0]) == 0 && Convert.ToInt16(isTableExist[1]) == 0)
                {
                    object[] myRet1 = CliUtils.CallMethod("GLModule", "FetchFavorMenus", strParam);
                    if ((null != myRet1) && (0 == (int)myRet1[0]))
                    {
                        menuFavorDataSet = (DataSet)(myRet1[1]);
                        groupFavorDataSet = (DataSet)(myRet1[2]);
                    }
                    int menuFavorCount = menuFavorDataSet.Tables[0].Rows.Count;
                    if (menuFavorCount > 0)
                    {
                        SYS_LANGUAGE language = CliUtils.fClientLang;
                        String favor = SysMsg.GetSystemMessage(language, "EEPNetClient", "FavorMenu", "Favor");

                        MenuIDList.Add("MyFavor");
                        CaptionList.Add(favor);
                        ParentList.Add("");
                        IconList.Images.Add("MyFavor", imglst.Images[0]);

                        for (int i = 0; i < groupFavorDataSet.Tables[0].Rows.Count; i++)
                        {
                            if (groupFavorDataSet.Tables[0].Rows[i]["GROUPNAME"] != null && groupFavorDataSet.Tables[0].Rows[i]["GROUPNAME"].ToString() != "")
                            {
                                MenuIDList.Add(groupFavorDataSet.Tables[0].Rows[i]["GROUPNAME"].ToString());
                                CaptionList.Add(groupFavorDataSet.Tables[0].Rows[i]["GROUPNAME"].ToString());
                                ParentList.Add("MyFavor");
                            }
                        }

                        for (int i = 0; i < menuFavorCount; i++)
                        {
                            if (!MenuIDList.Contains(menuFavorDataSet.Tables[0].Rows[i]["MENUID"].ToString()))
                            {
                                MenuIDList.Add(menuFavorDataSet.Tables[0].Rows[i]["MENUID"].ToString());
                                if (strCaption != "")
                                {
                                    if (menuFavorDataSet.Tables[0].Rows[i][strCaption].ToString() != "")
                                        CaptionList.Add(menuFavorDataSet.Tables[0].Rows[i][strCaption].ToString());
                                    else
                                        CaptionList.Add(menuFavorDataSet.Tables[0].Rows[i]["CAPTION"].ToString());
                                }
                                else
                                {
                                    CaptionList.Add(menuFavorDataSet.Tables[0].Rows[i]["CAPTION"].ToString());
                                }

                                if (menuFavorDataSet.Tables[0].Rows[i]["GROUPNAME"] == null || menuFavorDataSet.Tables[0].Rows[i]["GROUPNAME"].ToString() == "")
                                    ParentList.Add("MyFavor");
                                else
                                    ParentList.Add(menuFavorDataSet.Tables[0].Rows[i]["GROUPNAME"].ToString());

                                //new add by ccm
                                try
                                {
                                    byte[] blob = (byte[])menuFavorDataSet.Tables[0].Rows[i]["IMAGE"];

                                    MemoryStream stmblob = new MemoryStream(blob);

                                    try
                                    {
                                        IconList.Images.Add(menuFavorDataSet.Tables[0].Rows[i]["MENUID"].ToString(), Image.FromStream(stmblob));
                                    }
                                    catch
                                    {
                                        IconList.Images.Add(menuFavorDataSet.Tables[0].Rows[i]["MENUID"].ToString(), imglst.Images[0]);
                                    }
                                }
                                catch
                                {
                                    IconList.Images.Add(menuFavorDataSet.Tables[0].Rows[i]["MENUID"].ToString(), imglst.Images[0]);
                                }
                            }
                        }
                    }
                }

                object[] myRet = CliUtils.CallMethod("GLModule", "FetchMenus", strParam);
                if ((null != myRet) && (0 == (int)myRet[0]))
                {
                    menuDataSet = (DataSet)(myRet[1]);
                }
                int menuCount = menuDataSet.Tables[0].Rows.Count;
                for (int i = 0; i < menuCount; i++)
                {
                    MenuIDList.Add(menuDataSet.Tables[0].Rows[i]["menuid"].ToString());
                    if (strCaption != "")
                    {
                        if (menuDataSet.Tables[0].Rows[i][strCaption].ToString().Trim() != "")
                            CaptionList.Add(menuDataSet.Tables[0].Rows[i][strCaption].ToString());
                        else
                            CaptionList.Add(menuDataSet.Tables[0].Rows[i]["caption"].ToString());
                    }
                    else
                    {
                        CaptionList.Add(menuDataSet.Tables[0].Rows[i]["caption"].ToString());
                    }
                    ParentList.Add(menuDataSet.Tables[0].Rows[i]["parent"].ToString());
                    try
                    {
                        byte[] blob = (byte[])menuDataSet.Tables[0].Rows[i]["image"];

                        MemoryStream stmblob = new MemoryStream(blob);

                        try
                        {
                            IconList.Images.Add(menuDataSet.Tables[0].Rows[i]["menuid"].ToString(), Image.FromStream(stmblob));
                        }
                        catch
                        {
                            IconList.Images.Add(menuDataSet.Tables[0].Rows[i]["menuid"].ToString(), imglst.Images[0]);
                        }
                    }
                    catch
                    {
                        IconList.Images.Add(menuDataSet.Tables[0].Rows[i]["menuid"].ToString(), imglst.Images[0]);
                    }
                }

                for (int i = 0; i < MenuIDList.Count; i++)
                {
                    if (ParentList[i].ToString().Trim().Length == 0)
                    {
                        ListMainID.Add(MenuIDList[i].ToString());
                        ListMainCaption.Add(CaptionList[i].ToString());
                    }
                    else
                    {
                        ListChildrenID.Add(MenuIDList[i].ToString());
                        ListOwnerParentID.Add(ParentList[i].ToString());
                        ListChildrenCaption.Add(CaptionList[i].ToString());
                    }
                }
                tView.ImageList = IconList;
                initializeTopMenu();
                initializeMenu(MenuIDList, CaptionList, ParentList);
                initializeTreeView();
            }
        }

        private void ClearItems()
        {
            MenuIDList.Clear();
            CaptionList.Clear();
            ParentList.Clear();
            IconList.Images.Clear();
            ListMainID.Clear();
            ListMainCaption.Clear();
            ListChildrenID.Clear();
            ListOwnerParentID.Clear();
            ListChildrenCaption.Clear();
            tView.Nodes.Clear();

            List<ToolStripItem> items = new List<ToolStripItem>();
            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                if (menuStrip1.Items[i].Tag != null)
                {
                    items.Add(menuStrip1.Items[i]);
                }
            }
            for (int i = 0; i < items.Count; i++)
            {
                menuStrip1.Items.Remove(items[i]);
            }
        }

        private string SetMenuLanguage()
        {
            string strCaption = "";
            switch (GetClientLanguage())
            {
                case SYS_LANGUAGE.ENG:
                    strCaption = "caption0";
                    break;
                case SYS_LANGUAGE.TRA:
                    strCaption = "caption1";
                    break;
                case SYS_LANGUAGE.SIM:
                    strCaption = "caption2";
                    break;
                case SYS_LANGUAGE.HKG:
                    strCaption = "caption3";
                    break;
                case SYS_LANGUAGE.JPN:
                    strCaption = "caption4";
                    break;
                case SYS_LANGUAGE.LAN1:
                    strCaption = "caption5";
                    break;
                case SYS_LANGUAGE.LAN2:
                    strCaption = "caption6";
                    break;
                case SYS_LANGUAGE.LAN3:
                    strCaption = "caption7";
                    break;
            }
            return strCaption;
        }

        private void initializeTopMenu()
        {
            for (int i = 0; i < ListMainID.Count; i++)
            {
                ToolStripMenuItem item = new ToolStripMenuItem();
                item.Text = ListMainCaption[i].ToString();
                item.Image = IconList.Images[ListMainID[i].ToString()];
                item.Tag = ListMainID[i].ToString();
                item.Click += new EventHandler(menu_Click);
                this.menuStrip1.Items.Insert(this.menuStrip1.Items.Count - 2, item);
            }

            List<ToolStripItem> emptyItems = new List<ToolStripItem>();

            for (int i = 0; i < menuStrip1.Items.Count; i++)
            {
                if (menuStrip1.Items[i].Tag != null)
                {
                    InitializeItem((ToolStripMenuItem)menuStrip1.Items[i]);
                    if (IsEmptyFolderItem((ToolStripMenuItem)menuStrip1.Items[i]))
                    {
                        emptyItems.Add(menuStrip1.Items[i]);
                    }
                }
            }
            foreach (ToolStripMenuItem item in emptyItems)
            {
                menuStrip1.Items.Remove(item);
            }
        }

        private void InitializeItem(ToolStripMenuItem item)
        {
            for (int i = 0; i < ListChildrenID.Count; i++)
            {
                if (item.Tag.ToString() == ListOwnerParentID[i].ToString())
                {
                    ToolStripMenuItem itemChild = new ToolStripMenuItem();
                    itemChild.Text = ListChildrenCaption[i].ToString();
                    itemChild.Image = IconList.Images[ListChildrenID[i].ToString()];
                    itemChild.Tag = ListChildrenID[i].ToString();
                    itemChild.Click += new EventHandler(menu_Click);
                    item.DropDownItems.Add(itemChild);
                }
            }

            for (int i = 0; i < item.DropDownItems.Count; i++)
            {
                InitializeItem((ToolStripMenuItem)item.DropDownItems[i]);
            }
        }

        private bool IsEmptyFolderItem(ToolStripMenuItem item)
        {
            if (item != null && item.DropDownItems.Count == 0)
            {
                if (item.Tag != null)
                {
                    DataRow[] dr = menuDataSet.Tables[0].Select(string.Format("MENUID='{0}'", item.Tag));
                    if (dr.Length > 0)
                    {
                        return (dr[0]["PACKAGE"] == DBNull.Value || dr[0]["PACKAGE"].ToString().Length == 0);
                    }
                }
            }
            return false;
        }

        private void ClearMenuList()
        {
            menuListMainID.Clear();
            menuListMainCaption.Clear();
            menuListChildrenID.Clear();
            menuListOwnerParentID.Clear();
            menuListChildrenCaption.Clear();
        }

        ArrayList menuListMainID = new ArrayList();
        ArrayList menuListMainCaption = new ArrayList();
        ArrayList menuListChildrenID = new ArrayList();
        ArrayList menuListOwnerParentID = new ArrayList();
        ArrayList menuListChildrenCaption = new ArrayList();
        private void initializeMenu(ArrayList menuIDList, ArrayList menuCaptionList, ArrayList menuParentIDList)
        {
            ClearMenuList();
            int MenuCount = menuIDList.Count;
            for (int ix = 0; ix < MenuCount; ix++)
            {
                if (menuParentIDList[ix].ToString() == string.Empty)
                {
                    menuListMainID.Add(menuIDList[ix].ToString());
                    menuListMainCaption.Add(menuCaptionList[ix].ToString());
                }
                else
                {
                    menuListChildrenID.Add(menuIDList[ix].ToString());
                    menuListOwnerParentID.Add(menuParentIDList[ix].ToString());
                    menuListChildrenCaption.Add(menuCaptionList[ix].ToString());
                }
            }
            int i = menuListMainID.Count;
            // add root
            MenuItem[] menuMain = new MenuItem[i];
            MenuItem[] temp = new MenuItem[this.mainMenu1.MenuItems.Count];
            for (int count = 1; count < this.mainMenu1.MenuItems.Count; count++)
                temp[count] = this.mainMenu1.MenuItems[count];
            for (int count = 1; count < temp.Length; count++)
                this.mainMenu1.MenuItems.Remove(temp[count]);

            for (int j = 0; j < i; j++)
            {
                menuMain[j] = new MenuItem();
                this.mainMenu1.MenuItems.Add(menuMain[j]);
                //this.menuItemMainMenu.MenuItems.Add(menuMain[j]);
                menuMain[j].Name = menuListMainID[j].ToString();
                menuMain[j].Text = menuListMainCaption[j].ToString();
                menuMain[j].Tag = menuListMainID[j].ToString();
                menuMain[j].Click += new System.EventHandler(menu_Click);
            }

            for (int count = 1; count < temp.Length; count++)
                this.mainMenu1.MenuItems.Add(temp[count]);

            // add root's submenu.
            int p = menuListChildrenID.Count;

            for (int q = p - 1; q >= 0; q--)
            {
                if (menuListMainID.Contains(menuListOwnerParentID[q]))
                {
                    int x = menuListMainID.IndexOf(menuListOwnerParentID[q]);
                    MenuItem menuChild = new MenuItem();
                    menuChild.Name = menuListChildrenID[q].ToString();
                    menuChild.Text = menuListChildrenCaption[q].ToString();
                    menuChild.Tag = menuListChildrenID[q].ToString();
                    menuChild.Click += new System.EventHandler(menu_Click);
                    menuChild.Select += new EventHandler(menu_Select);
                    menuMain[x].MenuItems.Add(0, menuChild);

                    menuListChildrenID.RemoveAt(q);
                    menuListOwnerParentID.RemoveAt(q);
                    menuListChildrenCaption.RemoveAt(q);
                }
            }
        }

        private void ClearTreeList()
        {
            ListMainID.Clear();
            ListMainCaption.Clear();
            ListChildrenID.Clear();
            ListOwnerParentID.Clear();
            ListChildrenCaption.Clear();
        }

        ArrayList ListMainID = new ArrayList();
        ArrayList ListMainCaption = new ArrayList();
        ArrayList ListChildrenID = new ArrayList();
        ArrayList ListOwnerParentID = new ArrayList();
        ArrayList ListChildrenCaption = new ArrayList();
        private void initializeTreeView()
        {
            for (int i = 0; i < ListMainID.Count; i++)
            {
                TreeNode nodeMain = new TreeNode();
                tView.Nodes.Add(nodeMain);
                nodeMain.Text = ListMainCaption[i].ToString();
                nodeMain.SelectedImageKey = ListMainID[i].ToString();
                nodeMain.ImageKey = ListMainID[i].ToString();
                nodeMain.Tag = ListMainID[i].ToString();
            }

            List<TreeNode> emptynodes = new List<TreeNode>();
            for (int i = 0; i < tView.Nodes.Count; i++)
            {
                InitializeNode(tView.Nodes[i]);
                if (TreeViewLevel != 1)
                {
                    if (IsEmptyFolderNode(tView.Nodes[i]))
                    {
                        emptynodes.Add(tView.Nodes[i]);
                    }
                }
            }
            foreach (TreeNode node in emptynodes)
            {
                tView.Nodes.Remove(node);
            }
            tView.ExpandAll();
        }

        private bool IsEmptyFolderNode(TreeNode node)
        {
            if (node != null && node.Nodes.Count == 0)
            {
                if (node.Tag != null)
                {
                    DataRow[] dr = menuDataSet.Tables[0].Select(string.Format("MENUID='{0}'", node.Tag));
                    if (dr.Length > 0)
                    {
                        return (dr[0]["PACKAGE"] == DBNull.Value || dr[0]["PACKAGE"].ToString().Length == 0);
                    }
                }
            }
            return false;
        }

        private void InitializeNode(TreeNode node)
        {
            if (TreeViewLevel == -1 || node.Level < TreeViewLevel - 1)
            {
                for (int i = 0; i < ListChildrenID.Count; i++)
                {
                    if (node.Tag.ToString() == ListOwnerParentID[i].ToString())
                    {
                        TreeNode nodeChild = new TreeNode();
                        nodeChild.Text = ListChildrenCaption[i].ToString();
                        nodeChild.SelectedImageKey = ListChildrenID[i].ToString();
                        nodeChild.ImageKey = ListChildrenID[i].ToString();
                        nodeChild.Tag = ListChildrenID[i].ToString();
                        node.Nodes.Add(nodeChild);
                    }
                }

                for (int i = 0; i < node.Nodes.Count; i++)
                {
                    InitializeNode(node.Nodes[i]);
                }
            }
        }

        private void frmClientMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!logincanel)
            {
                CliUtils.CallMethod("GLModule", "LogOut", new object[] { (object)(CliUtils.fLoginUser) });
            }
        }

        private void menuItemTreeView_Click(object sender, EventArgs e)
        {
            if (this.menuItemTreeView.Checked == true)
            {
                this.menuItemTreeView.Checked = false;
                this.panel1.Visible = false;
            }
            else
            {
                this.menuItemTreeView.Checked = true;
                this.panel1.Visible = true;
            }
            this.Refresh();
        }

        private void menu_Click(object sender, EventArgs e)
        {
            string strText = ((ToolStripMenuItem)sender).Tag.ToString();
            showForm(strText, ((ToolStripMenuItem)sender).Text);
        }

        private void menu_Select(object sender, EventArgs e)
        {
            if (sender != null && sender is MenuItem)
            {
                MenuItem item = (MenuItem)sender;
                int p = menuListChildrenID.Count;

                for (int q = p - 1; q >= 0; q--)
                {
                    if (item.Name == menuListOwnerParentID[q].ToString())
                    {
                        MenuItem menuChild = new MenuItem();
                        menuChild.Name = menuListChildrenID[q].ToString();
                        menuChild.Text = menuListChildrenCaption[q].ToString();
                        menuChild.Tag = menuListChildrenID[q].ToString();
                        menuChild.Click += new System.EventHandler(menu_Click);
                        menuChild.Select += new EventHandler(menu_Select);
                        item.MenuItems.Add(0, menuChild);

                        menuListChildrenID.RemoveAt(q);
                        menuListOwnerParentID.RemoveAt(q);
                        menuListChildrenCaption.RemoveAt(q);
                    }
                }
            }
        }

        private void tView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            int ChildrenNum = tView.SelectedNode.GetNodeCount(true);
            if (ChildrenNum == 0)
            {
                string strText = ((TreeView)sender).SelectedNode.Tag.ToString();
                showForm(strText);
            }
        }

        private void InternalFormClosed(object sender, FormClosedEventArgs e)
        {
            IInfoForm aForm = (IInfoForm)sender;
            fFormCollection.RemovePackageForm(aForm.GetPackageName(), aForm.GetFormName());
        }

        private bool bAbort = false;
        private void ShowProgressBar()
        {
            ProgressForm aForm = new ProgressForm();
            aForm.Show();
            while (!bAbort || aForm.progressBar1.Value < 99)
            {
                if (aForm.progressBar1.Value + 3 > 100)
                {
                    aForm.progressBar1.Value = 1;
                }
                else
                {
                    aForm.progressBar1.Value += 3;
                }
                Thread.Sleep(5);
            }
        }

        private void CheckAndDownLoad(string sFullPath, string sDll)
        {
            object[] oRet = null;
            DateTime d = File.GetLastWriteTime(sFullPath);//取得最后时间

            Thread t = new Thread(new ThreadStart(ShowProgressBar));
            t.Start();
            try
            {
                oRet = CliUtils.CallMethod("GLModule", "CheckAndDownLoad", new object[] { CliUtils.fCurrentProject, sDll, d, true });
            }
            finally
            {
                bAbort = true;
                t.Join();
            }

            if (null != oRet && ((int)oRet[0] == 0) && ((int)oRet[1] == 0))
            {
                byte[] bs = (byte[])oRet[3];
                DateTime ds = (DateTime)oRet[2];
                string sPath = Path.GetDirectoryName(sFullPath);
                if (!Directory.Exists(sPath))
                    Directory.CreateDirectory(sPath);
                File.WriteAllBytes(sFullPath, bs);
                File.SetLastWriteTime(sFullPath, ds);
            }
        }

        public void showForm(string PackageName, string FormName, string ItemParam, StringDictionary FLParams)
        {
            showForm(PackageName, FormName, ItemParam, "", FLParams);
        }

        public void showForm(string ControlText)
        {
            string PackageName = "";
            string FormName = "";
            string ItemParam = "";
            string ModuleType = "";
            int i = menuDataSet.Tables[0].Rows.Count;
            for (int j = 0; j < i; j++)
            {
                if (ControlText == menuDataSet.Tables[0].Rows[j]["menuid"].ToString())
                {
                    PackageName = menuDataSet.Tables[0].Rows[j]["package"].ToString();
                    FormName = menuDataSet.Tables[0].Rows[j]["form"].ToString();
                    ItemParam = menuDataSet.Tables[0].Rows[j]["itemparam"].ToString();
                    ModuleType = menuDataSet.Tables[0].Rows[j]["moduletype"].ToString();
                    break;
                }
            }
            showForm(PackageName, FormName, ItemParam, ModuleType, null);
        }

        public void showForm(string PackageName, string FormName, string ItemParam, string ModuleType, StringDictionary FLParams)
        {
            if (PackageName == "" && ModuleType != "O")
            {
                return;
            }
            FormItem f = fFormCollection.FindFormItem(PackageName, FormName);
            if (FLParams == null && (null != f) && (!f.MultiInstance))
            {
                foreach (Form frm in this.MdiChildren)
                {
                    Type t = frm.GetType();
                    if (t.Namespace == f.PackageName && frm.Name == f.FormName)
                    {
                        if (FLParams != null && FLParams is StringDictionary)
                        {
                            InfoForm form = (InfoForm)frm;
                            form.fLItemParamters = FLParams as StringDictionary;
                            Type type = form.GetType();
                            FieldInfo[] fi = type.GetFields(BindingFlags.Instance
                                | BindingFlags.NonPublic
                                | BindingFlags.Public);
                            for (int i = 0; i < fi.Length; i++)
                            {
                                object obj = fi[i].GetValue(form);
                                if (obj is FLNavigator)
                                {
                                    FLNavigator nav = (FLNavigator)obj;
                                    nav.LoadNavigator();
                                }
                            }
                        }
                        frm.Activate();
                    }
                    else if (t.Namespace == "EEPNetFLClient" && frm.Name == "FLowDesingerForm" && ModuleType == "O")
                    {
                        string path = PackageName + "\\" + FormName + ".xoml";
                        object[] objs = CliUtils.CallFLMethod("GetFLDefinitionXmlString", new object[] { path });
                        if (objs[0] != null && (int)objs[0] == 0)
                        {
                            string xml = objs[1] as string;
                            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xml);
                            MemoryStream stream = new MemoryStream(bytes);
                            string ffn = CliUtils.CallFLMethod("GetFLServerPath", new object[] { path })[1].ToString();

                            ((FLowDesingerForm)frm).Stream = stream;
                            ((FLowDesingerForm)frm).FlowFileName = ffn;
                            ((FLowDesingerForm)frm).doLoad();
                            frm.Activate();
                        }
                    }
                }
                return;//如果已经有对象，并且不许多个，则退出
            }

            if (ModuleType == "O")
            {
                string path = PackageName + "\\" + FormName + ".xoml";
                object[] objs = CliUtils.CallFLMethod("GetFLDefinitionXmlString", new object[] { path });
                if (objs[0] != null && (int)objs[0] == 0)
                {
                    string xml = objs[1] as string;
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(xml);
                    MemoryStream stream = new MemoryStream(bytes);

                    FLowDesingerForm flDesingerForm = new FLowDesingerForm(this, stream);
                    string ffn = CliUtils.CallFLMethod("GetFLServerPath", new object[] { path })[1].ToString();
                    flDesingerForm.FlowFileName = ffn;
                    flDesingerForm.MdiParent = this;
                    ((IInfoForm)flDesingerForm).SetPackageForm(PackageName, FormName);
                    fFormCollection.AddPackageForm(PackageName, FormName).MultiInstance = ((IInfoForm)flDesingerForm).GetMultiInstance();
                    ((Form)flDesingerForm).FormClosed += InternalFormClosed;
                    flDesingerForm.Show();
                    return;
                }
            }

            string s = Application.StartupPath + "\\" + CliUtils.fCurrentProject + "\\";
            string strPackage = s + PackageName + ".dll";

            Assembly a = null;
            bool bLoaded = DllContainer.DllLoaded(strPackage);

            if (!bLoaded || !File.Exists(strPackage)) CheckAndDownLoad(strPackage, PackageName + ".dll");
            try
            {
                a = Assembly.LoadFrom(strPackage);
            }
            finally
            {
                if (!bLoaded) DllContainer.AddDll(strPackage);
            }

            Type myType = a.GetType(PackageName + "." + FormName);

            if (myType != null)
            {
                //try
                //{
                    object obj = Activator.CreateInstance(myType);
                    PropertyInfo myprop = myType.GetProperty("MdiParent");
                    myprop.SetValue(obj, this, null);

                    ((IInfoForm)obj).SetPackageForm(PackageName, FormName);
                    fFormCollection.AddPackageForm(PackageName, FormName).MultiInstance = ((IInfoForm)obj).GetMultiInstance();
                    ((Form)obj).FormClosed += InternalFormClosed;
                    ((InfoForm)obj).ItemParamters = ItemParam;
                    if (FLParams != null && FLParams is StringDictionary)
                    {
                        ((InfoForm)obj).fLItemParamters = FLParams as StringDictionary;
                    }
                    ((Control)obj).Show();
                    //if (((Form)obj).WindowState == FormWindowState.Maximized)
                    //{
                    //    ((Form)obj).Hide();
                    //    ((Form)obj).WindowState = FormWindowState.Normal;
                    //    ((Form)obj).WindowState = FormWindowState.Maximized;
                    //    ((Form)obj).Show();
                    //}
                //}
                //catch (Exception ex)
                //{
                //    ShowErrorMessage(ex);
                //}
            }
            else
            {
                MessageBox.Show(string.Format("Form: {0} doesn't exist", FormName));
            }
        }

        private void ShowErrorMessage(Exception ex)
        {
            if (ex.InnerException != null)
                ShowErrorMessage(ex);
            else
                MessageBox.Show(ex.Message);
        }

        private string GetXomlClientPath(string packageName, string formName)
        {
            string cdir = Application.StartupPath + "\\WorkFlow\\";
            if (!Directory.Exists(cdir))
            {
                Directory.CreateDirectory(cdir);
            }
            string cpath = ((packageName != "") ? (cdir + packageName + "\\" + formName) : (cdir + formName)) + ".xoml";
            try
            {
                string spath = "";
                object[] obj = CliUtils.CallMethod("GLModule", "GetServerPath", null);
                if (Convert.ToInt16(obj[0]) == 0)
                    spath = (string)obj[1];

                if (!string.IsNullOrEmpty(spath))
                {
                    CliUtils.DownLoad(spath + cpath.Substring(cpath.IndexOf(@"\WorkFlow\")), cpath);
                }
            }
            catch
            {
                return "";
            }
            return cpath;
        }

        public ComboBox cmbSolution
        {
            get
            {
                return this.infoCmbSolution;
            }
        }

        private void menuItemSolution_Click(object sender, EventArgs e)
        {
            CurProject aForm = new CurProject(this);
            aForm.ShowDialog();
        }

        private void menuItemDataBase_Click(object sender, EventArgs e)
        {
            frmDataBase dbForm = new frmDataBase(this);
            dbForm.ShowDialog();
        }

        private void menuItemUG_Click(object sender, EventArgs e)
        {
            frmUsersAndGroups ugForm = new frmUsersAndGroups();
            ugForm.ShowDialog();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void infoCmbSolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (solutionLoad)
            {
                int length = this.MdiChildren.Length;
                for (int i = 0; i < length; i++)
                {
                    this.MdiChildren[0].Close();
                    if (i + MdiChildren.Length >= length)
                    {
                        cmbSolution.SelectedIndexChanged -= new EventHandler(infoCmbSolution_SelectedIndexChanged);
                        this.cmbSolution.SelectedValue = CliUtils.fCurrentProject;//设回原来的solution
                        cmbSolution.SelectedIndexChanged += new EventHandler(infoCmbSolution_SelectedIndexChanged);
                        return;
                    }
                }
                mainMenu1.MenuItems.Clear();
                for (int count = 0; count < ReMenu.Count; count++)
                    mainMenu1.MenuItems.Add((MenuItem)ReMenu[count]);
                //menuItemMainMenu.MenuItems.Clear();
                tView.Nodes.Clear();
                ItemToGet();
                tView.ExpandAll();
                CliUtils.fCurrentProject = this.cmbSolution.SelectedValue.ToString();
            }
        }

        private void testToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage();

            DataSet ds = CliUtils.GetMessage();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                frm.dataGridView1.DataSource = ds;
                frm.dataGridView1.DataMember = "Message";
                frm.Location = new Point(Screen.AllScreens[0].WorkingArea.Width - frm.Size.Width, Screen.AllScreens[0].WorkingArea.Height - frm.Size.Height);
                frm.Show();
            }
        }

        private void tmMessage_Tick(object sender, EventArgs e)
        {
            frmMessage frm = new frmMessage();

            DataSet ds = CliUtils.GetMessage();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                frm.dataGridView1.DataSource = ds;
                frm.dataGridView1.DataMember = "Message";
                frm.Location = new Point(Screen.AllScreens[0].WorkingArea.Width - frm.Size.Width, Screen.AllScreens[0].WorkingArea.Height - frm.Size.Height);
                frm.Show();
            }
        }

        private int lx;
        private int sx;
        bool flag = false;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (flag == false)
            {
                lx = pictureBox1.Location.X;
                sx = panel1.Size.Width;
                this.pictureBox1.Image = global::EEPNetFLClient.Properties.Resources.d1;
                this.pictureBox1.Location = new System.Drawing.Point(0, this.pictureBox1.Location.Y);
                this.panel1.Size = new System.Drawing.Size(this.pictureBox1.Size.Width, this.panel1.Size.Height);
                flag = true;
            }
            else
            {
                this.pictureBox1.Image = global::EEPNetFLClient.Properties.Resources.d2;
                this.pictureBox1.Location = new System.Drawing.Point(lx, this.pictureBox1.Location.Y);
                this.panel1.Size = new System.Drawing.Size(sx, this.panel1.Size.Height);
                flag = false;
            }
            this.Refresh();
        }

        private int locY;
        private int sizY;
        bool flag1 = false;
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (flag1 == false)
            {
                locY = pictureBox2.Location.Y;
                sizY = panFLContainer.Size.Height;
                this.pictureBox2.Image = global::EEPNetFLClient.Properties.Resources.d3;
                this.pictureBox2.Location = new System.Drawing.Point(this.pictureBox2.Location.X, 0);
                this.panFLContainer.Size = new System.Drawing.Size(this.panFLContainer.Size.Width, this.pictureBox2.Height);


                flag1 = true;
            }
            else
            {
                this.pictureBox2.Image = global::EEPNetFLClient.Properties.Resources.d4;
                this.pictureBox2.Location = new System.Drawing.Point(this.pictureBox2.Location.X, locY);
                this.panFLContainer.Size = new System.Drawing.Size(this.panFLContainer.Size.Width, sizY);


                flag1 = false;
            }
            this.Refresh();
        }

        private void menuItemCP_Click(object sender, EventArgs e)
        {
            frmUserPWD fupwd = new frmUserPWD();
            fupwd.ShowDialog();
        }

        private void tView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.Level > 0 && e.Node.Nodes.Count == 0)
            {
                int i = ListChildrenID.Count;
                for (int j = i - 1; j >= 0; j--)
                {
                    if (e.Node.ImageKey == ListOwnerParentID[j].ToString())
                    {
                        TreeNode nodeChild = new TreeNode();
                        nodeChild.Text = ListChildrenCaption[j].ToString();
                        nodeChild.SelectedImageKey = ListChildrenID[j].ToString();
                        nodeChild.ImageKey = ListChildrenID[j].ToString();
                        nodeChild.Tag = ListChildrenID[j].ToString();
                        e.Node.Nodes.Insert(0, nodeChild);

                        ListChildrenID.RemoveAt(j);
                        ListOwnerParentID.RemoveAt(j);
                        ListChildrenCaption.RemoveAt(j);
                    }
                }
            }
        }

        private void dgvToDoList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvToDoList.CurrentRow != null)
                this.wizToDoList.OpenFlowDetail(this.dgvToDoList.CurrentRow.DataBoundItem);
        }

        private void dgvNotify_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvNotify.CurrentRow != null)
                this.wizNotify.OpenFlowDetail(this.dgvNotify.CurrentRow.DataBoundItem);
        }

        private void lnkToDoListRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.wizToDoList.Refresh();
            this.setToDoListOvertimeWarning();
        }

        private void lnkNotifyRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.wizNotify.Refresh();
            this.setNotifyOvertimeWarning();
        }

        private void lnkToDoHisRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.wizToDoHis.Refresh();
            if(this.wizToDoHis.SqlMode == ESqlMode.ToDoHis)
                this.setToDoHisOvertimeWarning();
        }

        private void lnkOvertimeRefresh_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void approveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.wizToDoList.Approve();
            this.wizToDoList.Refresh();
            this.wizToDoHis.Refresh();
        }

        private void rejectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.wizToDoList.Reject();
            this.wizToDoList.Refresh();
            this.wizToDoHis.Refresh();
        }

        private void returnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.wizToDoList.Return();
            this.wizToDoList.Refresh();
            this.wizToDoHis.Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvToDoList.CurrentRow != null)
                this.wizToDoList.OpenFlowDetail(dgvToDoList.CurrentRow.DataBoundItem);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.wizToDoList.Refresh();
        }

        private void flowDeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.wizNotify.DeleteNotify();
        }

        private void dgvToDoList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvToDoList.Rows)
            {
                row.ContextMenuStrip = this.cmsToDoList;
            }
        }

        private void dgvToDoHis_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvToDoHis.Rows)
            {
                row.ContextMenuStrip = this.cmsToDoHis;
            }
        }

        private void dgvNotify_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in this.dgvNotify.Rows)
            {
                row.ContextMenuStrip = this.cmsNotify;
            }
        }

        private void dgvToDoHis_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvToDoHis.CurrentRow != null)
                this.wizToDoHis.OpenFlowDetail(this.dgvToDoHis.CurrentRow.DataBoundItem);
        }

        private void returnToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.dgvToDoHis.SelectedRows != null && this.dgvToDoHis.SelectedRows.Count == 1)
            {
                DataRowView row = (DataRowView)this.dgvToDoHis.SelectedRows[0].DataBoundItem;
                string stepid = row["D_STEP_ID"].ToString();
                this.wizToDoHis.Return(stepid);
            }
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (this.dgvToDoHis.CurrentRow != null)
                this.wizToDoHis.OpenFlowDetail(this.dgvToDoHis.CurrentRow.DataBoundItem);
        }

        private void cmsToDoList_Opened(object sender, EventArgs e)
        {
            if (this.dgvToDoList.CurrentRow != null)
            {
                DataRowView rowView = this.dgvToDoList.CurrentRow.DataBoundItem as DataRowView;
                string flowNavMode = rowView["FLNAVIGATOR_MODE"].ToString();
                switch (flowNavMode)
                {
                    case "0": //Submit
                    case "3": //Notify
                    case "4": //Inquery
                    case "6": //None
                        this.approveToolStripMenuItem.Enabled = false;
                        this.rejectToolStripMenuItem.Enabled = false;
                        this.returnToolStripMenuItem.Enabled = false;
                        this.openToolStripMenuItem.Enabled = true;
                        break;
                    case "1": //Approve
                    case "5": //Continue
                        this.approveToolStripMenuItem.Enabled = true;
                        this.rejectToolStripMenuItem.Enabled = false;
                        this.returnToolStripMenuItem.Enabled = true;
                        this.openToolStripMenuItem.Enabled = true;
                        break;
                    case "2": //Return
                        this.approveToolStripMenuItem.Enabled = false;
                        this.rejectToolStripMenuItem.Enabled = true;
                        this.returnToolStripMenuItem.Enabled = false;
                        this.openToolStripMenuItem.Enabled = true;
                        break;
                }

                string status = rowView["STATUS"].ToString();
                if (status == "A")
                {
                    this.returnToolStripMenuItem.Enabled = false;
                }
                object plusRoles = rowView["PLUSROLES"];
                if (plusRoles != null && plusRoles.ToString().Trim() != "")
                {
                    this.approveToolStripMenuItem.Enabled = false;
                    this.returnToolStripMenuItem.Enabled = false;
                }
            }
            else
            {
                this.approveToolStripMenuItem.Enabled = false;
                this.rejectToolStripMenuItem.Enabled = false;
                this.returnToolStripMenuItem.Enabled = false;
                this.openToolStripMenuItem.Enabled = false;
                this.refreshToolStripMenuItem.Enabled = true;
            }
        }

        private void cmsToDoHis_Opened(object sender, EventArgs e)
        {
            if (this.dgvToDoHis.CurrentRow != null)
            {
                if (this.wizToDoHis.SqlMode == ESqlMode.ToDoHis)
                {
                    this.returnToolStripMenuItem1.Enabled = true;
                }
                else if (this.wizToDoHis.SqlMode == ESqlMode.FlowRunOver)
                {
                    this.returnToolStripMenuItem1.Enabled = false;
                }
            }
            else
            {
                this.returnToolStripMenuItem1.Enabled = false;
                this.openToolStripMenuItem1.Enabled = false;
                this.refreshToolStripMenuItem1.Enabled = true;
            }
        }

        private void dgvToDoList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.dgvToDoList.Columns["colImportant"].Index == e.ColumnIndex)
                {
                    object obj = dgvToDoList.Rows[e.RowIndex].DataBoundItem;
                    if (obj != null && obj is DataRowView)
                    {
                        DataRowView rowView = (DataRowView)obj;
                        if (rowView["FLOWIMPORTANT"].ToString() == "0")
                        {
                            paintNullImageCell(dgvToDoList, e);
                        }
                    }
                }
                else if (this.dgvToDoList.Columns["colUrgent"].Index == e.ColumnIndex)
                {
                    object obj = dgvToDoList.Rows[e.RowIndex].DataBoundItem;
                    if (obj != null && obj is DataRowView)
                    {
                        DataRowView rowView = (DataRowView)obj;
                        if (rowView["FLOWURGENT"].ToString() == "0")
                        {
                            paintNullImageCell(dgvToDoList, e);
                        }
                    }
                }
                else if (this.dgvToDoList.Columns["colStatus"].Index == e.ColumnIndex)
                {
                    paintStatusCell(this.dgvToDoList, e);
                }
            }
        }

        private void dgvNotify_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (this.dgvNotify.Columns["colImportant"].Index == e.ColumnIndex)
                {
                    object obj = dgvNotify.Rows[e.RowIndex].DataBoundItem;
                    if (obj != null && obj is DataRowView)
                    {
                        DataRowView rowView = (DataRowView)obj;
                        if (rowView["FLOWIMPORTANT"].ToString() == "0")
                        {
                            paintNullImageCell(dgvNotify, e);
                        }
                    }
                }
                else if (this.dgvNotify.Columns["colUrgent"].Index == e.ColumnIndex)
                {
                    object obj = dgvNotify.Rows[e.RowIndex].DataBoundItem;
                    if (obj != null && obj is DataRowView)
                    {
                        DataRowView rowView = (DataRowView)obj;
                        if (rowView["FLOWURGENT"].ToString() == "0")
                        {
                            paintNullImageCell(dgvNotify, e);
                        }
                    }
                }
                else if (this.dgvToDoList.Columns["colStatus"].Index == e.ColumnIndex)
                {
                    paintStatusCell(this.dgvNotify, e);
                }
            }
        }

        private void dgvToDoHis_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex >= 0)
            {

                if (this.dgvToDoHis.Columns["colImportant"] != null && this.dgvToDoHis.Columns["colImportant"].Index == e.ColumnIndex)
                {
                    object obj = dgvToDoHis.Rows[e.RowIndex].DataBoundItem;
                    if (obj != null && obj is DataRowView)
                    {
                        DataRowView rowView = (DataRowView)obj;
                        if (rowView["FLOWIMPORTANT"].ToString() == "0")
                        {
                            paintNullImageCell(dgvToDoHis, e);
                        }
                    }
                }
                else if (this.dgvToDoHis.Columns["colUrgent"] != null && this.dgvToDoHis.Columns["colUrgent"].Index == e.ColumnIndex)
                {
                    object obj = dgvToDoHis.Rows[e.RowIndex].DataBoundItem;
                    if (obj != null && obj is DataRowView)
                    {
                        DataRowView rowView = (DataRowView)obj;
                        if (rowView["FLOWURGENT"].ToString() == "0")
                        {
                            paintNullImageCell(dgvToDoHis, e);
                        }
                    }
                }
                else if (this.dgvToDoList.Columns["colStatus"].Index == e.ColumnIndex)
                {
                    paintStatusCell(this.dgvToDoHis, e);
                }
            }
        }

        private void paintNullImageCell(DataGridView grid, DataGridViewCellPaintingEventArgs e)
        {
            Rectangle newRect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 2, e.CellBounds.Height - 2);
            using (Brush gridBrush = new SolidBrush(grid.GridColor),
                backColorBrush = new SolidBrush(e.CellStyle.BackColor),
                selectionBackColorBrush = new SolidBrush(e.CellStyle.SelectionBackColor))
            {
                using (Pen gridLinePen = new Pen(gridBrush))
                {
                    if (grid[e.ColumnIndex, e.RowIndex].Selected)
                    {
                        e.Graphics.FillRectangle(selectionBackColorBrush, e.CellBounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                    }
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left - 1, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left - 1, e.CellBounds.Top - 1, e.CellBounds.Right - 1, e.CellBounds.Top - 1);
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left - 1, e.CellBounds.Top - 1, e.CellBounds.Left - 1, e.CellBounds.Bottom - 1);
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    e.Handled = true;
                }
            }
        }

        void paintStatusCell(DataGridView grid, DataGridViewCellPaintingEventArgs e)
        {
            Rectangle newRect = new Rectangle(e.CellBounds.X + 1, e.CellBounds.Y + 1, e.CellBounds.Width - 4, e.CellBounds.Height - 4);
            using (Brush gridBrush = new SolidBrush(grid.GridColor), backColorBrush = new SolidBrush(e.CellStyle.BackColor), selectionBackColorBrush = new SolidBrush(e.CellStyle.SelectionBackColor))
            {
                using (Pen gridLinePen = new Pen(gridBrush))
                {
                    // Erase the cell.
                    if (grid.Rows[e.RowIndex].Selected)
                    {
                        e.Graphics.FillRectangle(selectionBackColorBrush, e.CellBounds);
                    }
                    else
                    {
                        e.Graphics.FillRectangle(backColorBrush, e.CellBounds);
                    }

                    // Draw the grid lines (only the right and bottom lines;
                    // DataGridView takes care of the others).
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Left, e.CellBounds.Bottom - 1, e.CellBounds.Right - 1, e.CellBounds.Bottom - 1);
                    e.Graphics.DrawLine(gridLinePen, e.CellBounds.Right - 1, e.CellBounds.Top, e.CellBounds.Right - 1, e.CellBounds.Bottom);
                }
            }
            using (Brush foreColorBrush = new SolidBrush(e.CellStyle.ForeColor), selectionForeColorBrush = new SolidBrush(e.CellStyle.SelectionForeColor))
            {
                string formatValue = "";
                switch (e.Value.ToString())
                {
                    case "Z":
                        formatValue = lstStatus[0];
                        break;
                    case "N":
                        formatValue = lstStatus[1];
                        break;
                    case "NR":
                        formatValue = lstStatus[2];
                        break;
                    case "NF":
                        formatValue = lstStatus[3];
                        break;
                    case "X":
                        formatValue = lstStatus[4];
                        break;
                    case "A":
                        formatValue = lstStatus[5];
                        break;
                }

                if (grid.Rows[e.RowIndex].Selected)
                {
                    e.Graphics.DrawString(formatValue, e.CellStyle.Font, selectionForeColorBrush, e.CellBounds.X + 2, e.CellBounds.Y + 4);
                }
                else
                {
                    e.Graphics.DrawString(formatValue, e.CellStyle.Font, foreColorBrush, e.CellBounds.X + 2, e.CellBounds.Y + 4);
                }
                e.Handled = true;
            }
        }

        private void dgvToDoList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                dgvToDoList.Rows[e.RowIndex].ContextMenuStrip = this.cmsToDoList;
        }

        private void dgvNotify_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                dgvNotify.Rows[e.RowIndex].ContextMenuStrip = this.cmsNotify;
        }

        private void dgvToDoHis_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
                dgvToDoHis.Rows[e.RowIndex].ContextMenuStrip = this.cmsToDoHis;
        }

        private void chkSubmitted_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkSubmitted.Checked)
            {
                this.wizToDoHis.SqlMode = ESqlMode.FlowRunOver;
                this.wizToDoHis.Filter = "";
            }
            else
            {
                this.wizToDoHis.SqlMode = ESqlMode.ToDoHis;
                this.wizToDoHis.Filter = "";
            }
            this.wizToDoHis.Refresh();
            setToDoHistColumns();
            if (!this.chkSubmitted.Checked)
                setToDoHisOvertimeWarning();
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            GetOvertimeList();
        }

        public bool IgnoreWeekends = true;
        private void GetOvertimeList()
        {
            if (this.chkActive.Checked)
            {
                this.wizOvertime.Refresh(this.cmbLevel.SelectedIndex, IgnoreWeekends, null);
            }
            else
            {
                this.dgvOvertime.DataSource = null;
            }
        }

        private void dgvOvertime_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (this.dgvOvertime.CurrentRow != null)
                this.wizOvertime.OpenFlowDetail(this.dgvOvertime.CurrentRow.DataBoundItem);
        }

        private void cmbLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.chkActive.Checked)
            {
                object[] param = new object[5] { CliUtils.fLoginUser, this.cmbLevel.SelectedIndex, this.IgnoreWeekends, null, true };
                object[] obj = CliUtils.CallMethod("GLModule", "FLOvertimeList", param);
                if (Convert.ToInt16(obj[0]) == 0)
                {
                    DataTable tab = obj[1] as DataTable;
                    this.dgvOvertime.DataSource = tab;
                }
            }
            else
            {
                this.dgvOvertime.DataSource = null;
            }
        }

        private void dgvToDoList_Sorted(object sender, EventArgs e)
        {
            this.setToDoListOvertimeWarning();
        }

        private void dgvNotify_Sorted(object sender, EventArgs e)
        {
            this.setNotifyOvertimeWarning();
        }

        private void dgvToDoHis_Sorted(object sender, EventArgs e)
        {
            //modify by lily 2011/8/29 已結案部分不需要判斷是否超時
            if (!this.chkSubmitted.Checked)
                this.setToDoHisOvertimeWarning();
        }

        private void tmFlow_Tick(object sender, EventArgs e)
        {
            this.wizToDoList.Refresh();
            this.wizToDoHis.Refresh();
            this.wizNotify.Refresh();
            this.setToDoListOvertimeWarning();
            if (this.wizToDoHis.SqlMode != ESqlMode.FlowRunOver)
            {
                this.setToDoHisOvertimeWarning();
            }
            this.setNotifyOvertimeWarning();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 1 && !this.chkSubmitted.Checked)
            {
                this.setToDoHisOvertimeWarning();
            }
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (this.dgvNotify.CurrentRow != null)
                this.wizNotify.OpenFlowDetail(this.dgvNotify.CurrentRow.DataBoundItem);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            this.wizNotify.Refresh();
        }

        private void refreshToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.wizToDoHis.Refresh();
        }

        const String oracleDateFormat = "'yyyy/MM/dd hh24:mi:ss'";
        const String informixDateFormat = "'%Y-%m-%d %H:%M:%S'";
        private void btnLstQueryOK_Click(object sender, EventArgs e)
        {
            String connectMark = "+";
            String DBAlias = CliUtils.fLoginDB;
            object[] xx = CliUtils.CallMethod("GLModule", "GetSplitSysDB2", new object[] { DBAlias });
            if (xx[0].ToString() == "0")
                DBAlias = xx[1].ToString();
            object[] myRet = CliUtils.CallMethod("GLModule", "GetDataBaseType", new object[] { DBAlias });
            if (myRet != null && myRet[0].ToString() == "0")
            {
                switch (myRet[1].ToString())
                {
                    case "1": connectMark = "+"; break;
                    case "2": connectMark = "+"; break;
                    case "3": connectMark = "||"; break;
                    case "4": connectMark = "||"; break;
                    case "5": connectMark = "||"; break;
                    case "6": connectMark = "||"; break;
                }
            }

            string filter = "";
            if (this.cmbLstFlow.SelectedIndex > 0)
            {
                filter += "SYS_TODOLIST.FLOW_DESC='" + this.cmbLstFlow.Text + "' AND ";
            }
            if (!string.IsNullOrEmpty(this.txtLstDstep.Text))
            {
                filter += "SYS_TODOLIST.D_STEP_ID LIKE '" + this.txtLstDstep.Text + "%' AND ";
            }
            if (this.cmbLstUser.SelectedIndex > 0)
            {
                filter += "SYS_TODOLIST.USERNAME='" + this.cmbLstUser.Text + "' AND ";
            }
            if (!string.IsNullOrEmpty(this.txtLstFormPresent.Text))
            {
                filter += "SYS_TODOLIST.FORM_PRESENT_CT LIKE '%" + this.txtLstFormPresent.Text + "%' AND ";
            }
            if (this.dtpLstDateFrom.Checked)
            {
                if (myRet != null && myRet[0].ToString() == "0")
                {
                    switch (myRet[1].ToString())
                    {
                        case "1": 
                            filter += "CAST((SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS DATETIME) > '" + this.dtpLstDateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "2": 
                            filter += "(SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) > '" + this.dtpLstDateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "3": 
                            filter += "to_date(SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME, " + oracleDateFormat + ") > to_date('" + this.dtpLstDateFrom.Value.ToShortDateString() + "', " + oracleDateFormat + ") AND ";
                            break;
                        case "4":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) > to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpLstDateFrom.Value.ToShortDateString());
                            break;
                        case "5":
                            filter += string.Format("CONCAT(SYS_TODOLIST.UPDATE_DATE, ' ', SYS_TODOLIST.UPDATE_TIME) > '{0}'  AND ", this.dtpLstDateFrom.Value.ToShortDateString());
                            break;
                        case "6":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) > to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpLstDateFrom.Value.Year + "-" + this.dtpLstDateFrom.Value.Month + "-" + this.dtpLstDateFrom.Value.Day + " 00:00:00");
                            break;
                    }
                }
            }
            if (this.dtpLstDateTo.Checked)
            {
                if (myRet != null && myRet[0].ToString() == "0")
                {
                    switch (myRet[1].ToString())
                    {
                        case "1": 
                            filter += "CAST((SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS DATETIME) < '" + this.dtpLstDateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "2": 
                            filter += "(SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) < '" + this.dtpLstDateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "3": 
                            filter += "to_date(SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME, " + oracleDateFormat + ") < to_date('" + this.dtpLstDateTo.Value.ToShortDateString() + " 23:59:59', " + oracleDateFormat + ") AND ";
                            break;
                        case "4":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) < to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpLstDateTo.Value.ToShortDateString() + " 23:59:59");
                            break;
                        case "5":
                            filter += "CONCAT(SYS_TODOLIST.UPDATE_DATE, ' ', SYS_TODOLIST.UPDATE_TIME) < '" + this.dtpLstDateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "6":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) < to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpLstDateTo.Value.Year + "-" + this.dtpLstDateTo.Value.Month + "-" + this.dtpLstDateTo.Value.Day + " 23:59:59");
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.txtLstRemark.Text))
            {
                filter += "SYS_TODOLIST.REMARK LIKE '%" + this.txtLstRemark.Text + "%' AND ";
            }

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.Substring(0, filter.LastIndexOf(" AND "));
            }

            this.wizToDoList.Refresh(filter);

            this.panToDoListQuery.Visible = false;
        }

        private void btnLstQueryCancel_Click(object sender, EventArgs e)
        {
            this.panToDoListQuery.Visible = false;
        }

        private void lnkToDoListQuery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.panToDoListQuery.Visible = true;
        }

        private void lnkToDoHisQuery_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(!this.chkSubmitted.Checked)
            {
                this.panHisQuery.Visible = true;
                this.panHis2Query.Visible = false;
                this.chkSubmitted.Enabled = false;
            }
            else
            {
                this.panHis2Query.Visible = true;
                this.panHisQuery.Visible = false;
                this.chkSubmitted.Enabled = false;
            }
        }

        private void btnHisQueryOK_Click(object sender, EventArgs e)
        {
            String connectMark = "+";
            String DBAlias = CliUtils.fLoginDB;
            object[] xx = CliUtils.CallMethod("GLModule", "GetSplitSysDB2", new object[] { DBAlias });
            if (xx[0].ToString() == "0")
                DBAlias = xx[1].ToString();
            object[] myRet = CliUtils.CallMethod("GLModule", "GetDataBaseType", new object[] { DBAlias });
            if (myRet != null && myRet[0].ToString() == "0")
            {
                switch (myRet[1].ToString())
                {
                    case "1": connectMark = "+"; break;
                    case "2": connectMark = "+"; break;
                    case "3": connectMark = "||"; break;
                    case "4": connectMark = "||"; break;
                    case "5": connectMark = "||"; break;
                    case "6": connectMark = "||"; break;
                }
            }

            string filter = "";
            if (this.cmbHisFlow.SelectedIndex > 0)
            {
                filter += "SYS_TODOLIST.FLOW_DESC='" + this.cmbHisFlow.Text + "' AND ";
            }
            if (!string.IsNullOrEmpty(this.txtHisDstep.Text))
            {
                filter += "SYS_TODOLIST.D_STEP_ID LIKE '" + this.txtHisDstep.Text + "%' AND ";
            }
            if (this.cmbHisSendTo.SelectedIndex > 0)
            {
                filter += "SYS_TODOLIST.SENDTO_NAME='" + this.cmbHisSendTo.Text + "' AND ";
            }
            if (!string.IsNullOrEmpty(this.txtHisFormPresent.Text))
            {
                filter += "SYS_TODOLIST.FORM_PRESENT_CT LIKE '%" + this.txtHisFormPresent.Text + "%' AND ";
            }
            if (this.dtpHisDateFrom.Checked)
            {
                if (myRet != null && myRet[0].ToString() == "0")
                {
                    switch (myRet[1].ToString())
                    {
                        case "1":
                            filter += "CAST((SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS DATETIME) > '" + this.dtpHisDateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "2":
                            filter += "CAST((SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS DATETIME) > '" + this.dtpHisDateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "3":
                            filter += "to_date(SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME, " + oracleDateFormat + ") > to_date('" + this.dtpHisDateFrom.Value.ToShortDateString() + "', " + oracleDateFormat + ") AND ";
                            break;
                        case "4":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) > to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHisDateFrom.Value.ToShortDateString());
                            break;
                        case "5":
                            filter += "CONCAT(SYS_TODOLIST.UPDATE_DATE, ' ', SYS_TODOLIST.UPDATE_TIME) > '" + this.dtpHisDateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "6":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) > to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHisDateFrom.Value.Year + "-" + this.dtpHisDateFrom.Value.Month + "-" + this.dtpHisDateFrom.Value.Day + " 00:00:00");
                            break;
                    }
                }
            }
            if (this.dtpHisDateTo.Checked)
            {
                if (myRet != null && myRet[0].ToString() == "0")
                {
                    switch (myRet[1].ToString())
                    {
                        case "1":
                            filter += "CAST((SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS DATETIME) < '" + this.dtpHisDateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "2":
                            filter += "CAST((SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME) AS DATETIME) < '" + this.dtpHisDateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "3":
                            filter += "to_date(SYS_TODOLIST.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOLIST.UPDATE_TIME, " + oracleDateFormat + ") < to_date('" + this.dtpHisDateTo.Value.ToShortDateString() + " 23:59:59', " + oracleDateFormat + ") AND ";
                            break;
                        case "4":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) < to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHisDateTo.Value.ToShortDateString() + " 23:59:59");
                            break;
                        case "5":
                            filter += "CONCAT(SYS_TODOLIST.UPDATE_DATE, ' ', SYS_TODOLIST.UPDATE_TIME) < '" + this.dtpHisDateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "6":
                            filter += string.Format("to_date(SYS_TODOLIST.UPDATE_DATE {0} ' ' {0} SYS_TODOLIST.UPDATE_TIME, {1}) < to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHisDateTo.Value.Year + "-" + this.dtpHisDateTo.Value.Month + "-" + this.dtpHisDateTo.Value.Day + " 23:59:59");
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.txtHisRemark.Text))
            {
                filter += "SYS_TODOLIST.REMARK LIKE '%" + this.txtHisRemark.Text + "%' AND ";
            }

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.Substring(0, filter.LastIndexOf(" AND "));
            }

            this.wizToDoHis.Refresh(filter);

            this.panHisQuery.Visible = false;
            this.chkSubmitted.Enabled = true;
        }

        private void btnHisQueryCancel_Click(object sender, EventArgs e)
        {
            this.panHisQuery.Visible = false;
            this.chkSubmitted.Enabled = true;
        }

        private void btnHis2QueryOK_Click(object sender, EventArgs e)
        {
            String connectMark = "+";
            String DBAlias = CliUtils.fLoginDB;
            object[] xx = CliUtils.CallMethod("GLModule", "GetSplitSysDB2", new object[] { DBAlias });
            if (xx[0].ToString() == "0")
                DBAlias = xx[1].ToString();
            object[] myRet = CliUtils.CallMethod("GLModule", "GetDataBaseType", new object[] { DBAlias });
            if (myRet != null && myRet[0].ToString() == "0")
            {
                switch (myRet[1].ToString())
                {
                    case "1": connectMark = "+"; break;
                    case "2": connectMark = "+"; break;
                    case "3": connectMark = "||"; break;
                    case "4": connectMark = "||"; break;
                    case "5": connectMark = "||"; break;
                    case "6": connectMark = "||"; break;
                }
            }

            string filter = "";
            if (this.cmbHis2Flow.SelectedIndex > 0)
            {
                filter += "SYS_TODOHIS.FLOW_DESC='" + this.cmbHis2Flow.Text + "' AND ";
            }
            if (!string.IsNullOrEmpty(this.txtHis2DStep.Text))
            {
                filter += "SYS_TODOHIS.D_STEP_ID LIKE '" + this.txtHis2DStep.Text + "%' AND ";
            }
            if (!string.IsNullOrEmpty(this.txtHis2FormPresent.Text))
            {
                filter += "SYS_TODOHIS.FORM_PRESENT_CT LIKE '%'" + this.txtHis2FormPresent.Text + "%' AND ";
            }
            if (this.dtpHis2DateFrom.Checked)
            {
                if (myRet != null && myRet[0].ToString() == "0")
                {
                    switch (myRet[1].ToString())
                    {
                        case "1":
                            filter += "CAST((SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) AS DATETIME) > '" + this.dtpHis2DateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "2":
                            filter += "CAST((SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) AS DATETIME) > '" + this.dtpHis2DateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "3":
                            filter += "to_date(SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME, " + oracleDateFormat + ") > to_date('" + this.dtpHis2DateFrom.Value.ToShortDateString() + "', " + oracleDateFormat + ") AND ";
                            break;
                        case "4":
                            filter += string.Format("to_date(SYS_TODOHIS.UPDATE_DATE {0} ' ' {0} SYS_TODOHIS.UPDATE_TIME, {1}) > to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHis2DateFrom.Value.ToShortDateString());
                            break;
                        case "5":
                            filter += "CONCAT(SYS_TODOHIS.UPDATE_DATE, ' ', SYS_TODOHIS.UPDATE_TIME) > '" + this.dtpHis2DateFrom.Value.ToShortDateString() + "' AND ";
                            break;
                        case "6":
                            filter += string.Format("to_date(SYS_TODOHIS.UPDATE_DATE {0} ' ' {0} SYS_TODOHIS.UPDATE_TIME, {1}) > to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHis2DateFrom.Value.Year + "-" + this.dtpHis2DateFrom.Value.Month + "-" + this.dtpHis2DateFrom.Value.Day + " 00:00:00");
                            break;
                    }
                }
            }
            if (this.dtpHis2DateTo.Checked)
            {
                if (myRet != null && myRet[0].ToString() == "0")
                {
                    switch (myRet[1].ToString())
                    {
                        case "1":
                            filter += "CAST((SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) AS DATETIME) < '" + this.dtpHis2DateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "2":
                            filter += "CAST((SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME) AS DATETIME) < '" + this.dtpHis2DateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "3":
                            filter += "to_date(SYS_TODOHIS.UPDATE_DATE " + connectMark + " ' ' " + connectMark + " SYS_TODOHIS.UPDATE_TIME, " + oracleDateFormat + ") < to_date('" + this.dtpHis2DateTo.Value.ToShortDateString() + " 23:59:59', " + oracleDateFormat + ") AND ";
                            break;
                        case "4":
                            filter += string.Format("to_date(SYS_TODOHIS.UPDATE_DATE {0} ' ' {0} SYS_TODOHIS.UPDATE_TIME, {1}) < to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHis2DateTo.Value.ToShortDateString() + " 23:59:59");
                            break;
                        case "5":
                            filter += "CONCAT(SYS_TODOHIS.UPDATE_DATE, ' ', SYS_TODOHIS.UPDATE_TIME) < '" + this.dtpHis2DateTo.Value.ToShortDateString() + " 23:59:59' AND ";
                            break;
                        case "6":
                            filter += string.Format("to_date(SYS_TODOHIS.UPDATE_DATE {0} ' ' {0} SYS_TODOHIS.UPDATE_TIME, {1}) < to_date('{2}', {1})  AND ", connectMark, informixDateFormat, this.dtpHis2DateTo.Value.Year + "-" + this.dtpHis2DateTo.Value.Month + "-" + this.dtpHis2DateTo.Value.Day + " 23:59:59");
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(this.txtHis2Remark.Text))
            {
                filter += "SYS_TODOHIS.REMARK LIKE '%" + this.txtHis2Remark.Text + "%' AND ";
            }

            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.Substring(0, filter.LastIndexOf(" AND "));
            }

            this.wizToDoHis.Refresh(filter);

            this.panHis2Query.Visible = false;
            this.chkSubmitted.Enabled = true;
        }

        private void btnHis2QueryCancel_Click(object sender, EventArgs e)
        {
            this.panHis2Query.Visible = false;
            this.chkSubmitted.Enabled = true;
        }

        private void pbGo_Click(object sender, EventArgs e)
        {
            if (tbGO.Text == "")
            {
                MessageBox.Show("Please enter a menu first.");
            }
            else
            {
                Boolean flag = false;
                for (int i = 0; i < this.tView.Nodes.Count; i++)
                {
                    if (compareCaption(tbGO.Text, this.tView.Nodes[i].Text.ToString()))
                    {
                        showForm(this.tView.Nodes[i].Tag.ToString(), tView.Nodes[i].Text);
                        flag = true;
                        break;
                    }
                    flag = getChildNode(this.tView.Nodes[i], tbGO.Text);
                    if (flag) break;
                }
                if (!flag)
                {
                    MessageBox.Show("The menu you entered is not exist.");
                }
            }
        }

        public Boolean getChildNode(TreeNode tn, String text)
        {
            if (compareCaption(text, tn.Text.ToString()))
            {
                showForm(tn.Tag.ToString(), tn.Text);
                return true; ;
            }
            if (tn.Nodes.Count > 0)
            {
                for (int i = 0; i < tn.Nodes.Count; i++)
                {
                    if (compareCaption(text, tn.Nodes[i].Text.ToString()))
                    {
                        showForm(tn.Nodes[i].Tag.ToString(), tn.Nodes[i].Text);
                        return true; ;
                    }
                    if (getChildNode(tn.Nodes[i], text)) return true;
                }
            }
            return false;
        }

        private bool compareCaption(String text, String nodeText)
        {
            if (nodeText.StartsWith(text))
                return true;
            else
                return false;
        }

        private void showForm(string id, string text)
        {
            showForm(id);
        }


        private void pbMyFavor_Click(object sender, EventArgs e)
        {
            //frmFavorMenu ffm = new frmFavorMenu(MenuIDList, CaptionList, ParentList);
            //ffm.ShowDialog();
            //if (ffm.result)
            //{
            //    ItemToGet();
            //}
        }

        private void pbGo_MouseLeave(object sender, EventArgs e)
        {
            string s = Application.StartupPath + "\\Resources\\MenuGO.gif";
            this.pbGo.ImageLocation = s;
        }

        private void pbGo_MouseMove(object sender, MouseEventArgs e)
        {
            string s = Application.StartupPath + "\\Resources\\MenuGO2.gif";
            this.pbGo.ImageLocation = s;
        }

        private void pbMyFavor_MouseLeave(object sender, EventArgs e)
        {
            string s = Application.StartupPath + "\\Resources\\AddFavor.png";
            this.pbMyFavor.ImageLocation = s;
        }

        private void pbMyFavor_MouseMove(object sender, MouseEventArgs e)
        {
            string s = Application.StartupPath + "\\Resources\\AddFavor2.png";
            this.pbMyFavor.ImageLocation = s;
        }

        private void aboutEEPNetClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormVersion form = new FormVersion("About EEP.Net Client");
            form.ShowDialog(this);
        }

        private void tileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileVertical);
        }

        private void tileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void solutionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CurProject aForm = new CurProject(this);
            aForm.ShowDialog();
        }

        private void dataBaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDataBase dbForm = new frmDataBase(this);
            dbForm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserPWD fupwd = new frmUserPWD();
            fupwd.ShowDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}