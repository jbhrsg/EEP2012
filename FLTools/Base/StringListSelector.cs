using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace FLTools.Base
{
    public class StringListSelector
    {
        private ListBox _stringListBox = null;
        private string[] _selectedList = null;
        private IWindowsFormsEditorService _editService = null;

        private void ListClicked(object sender, EventArgs e)
        {
            if (null != _editService)
            {
                _editService.CloseDropDown();
            }
        }

        public StringListSelector(IWindowsFormsEditorService editorService, string[] selectedList)
        {
            _selectedList = selectedList;
            _editService = editorService;
        }

        public bool Execute(ref string selectedString)
        {
            if (null == _editService) return false;
            if (null == _selectedList) return false;

            _stringListBox = new ListBox();
            int iPos = -1;

            if (_selectedList != null)
            {
                foreach (string s in _selectedList)
                {
                    int iCur = _stringListBox.Items.Add(s);
                    if (s.Equals(selectedString)) iPos = iCur;
                }
            }

            if (-1 != iPos)
            {
                _stringListBox.SelectedIndex = iPos;
            }

            _stringListBox.Click += new EventHandler(ListClicked);
            _editService.DropDownControl(_stringListBox);

            if (null != _stringListBox.SelectedItem)
            {
                selectedString = _stringListBox.SelectedItem.ToString();
                return true;
            }
            else
            {
                selectedString = "";
                return false;
            }
        }
    }
}
