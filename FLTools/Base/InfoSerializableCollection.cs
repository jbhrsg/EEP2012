using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FLTools.Base
{
    [Serializable]
    public abstract class InfoSerializableCollection : CollectionBase, IList
    {
        private Type _itemType = null;

        public Type ItemType
        {
            get
            {
                return _itemType;
            }
        }

        public InfoSerializableCollection(Type itemType)
            : this()
        {
            _itemType = itemType;
        }

        public InfoSerializableCollection()
        {
        }


        #region IList Members

        public Boolean IsReadOnly
        {
            get
            {
                return InnerList.IsReadOnly;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return (object)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    if (value is InfoSerializableItem)
                    {
                        InnerList[index] = value;
                    }
                }
            }
        }

        public InfoSerializableItem this[string ItemName]
        {
            get
            {
                return GetItem(ItemName);
            }
            set
            {
                int index = GetItemIndex(ItemName);

                this[index] = value;

            }
        }

        public InfoSerializableItem this[int index]
        {
            get
            {
                return (InfoSerializableItem)InnerList[index];
            }
            set
            {
                if (index > -1 && index < Count)
                {
                    InnerList[index] = value;
                }
            }
        }

        public Int32 IndexOf(String itemName)
        {
            if (null == itemName) return -1;

            return GetItemIndex(itemName);
        }

        void System.Collections.IList.RemoveAt(int index)
        {
            InfoSerializableItem aItem = GetItem(index);
            if (null != aItem)
            {
                InnerList.RemoveAt(index);
            }
        }

        public void RemoveAt(string itemName)
        {
            int index = GetItemIndex(itemName);

            if (index > -1)
                RemoveAt(index);
        }

        public void Insert(int index, object value)
        {
            if (value is InfoSerializableItem)
            {
                InnerList.Insert(index, value);
            }
        }

        public void Remove(object value)
        {
            if (value is InfoSerializableItem)
            {
                InnerList.Remove(value);
            }
        }

        bool System.Collections.IList.Contains(object value)
        {
            return InnerList.Contains(value);
        }

        void System.Collections.IList.Clear()
        {
            InnerList.Clear();
        }

        int System.Collections.IList.IndexOf(object value)
        {
            return InnerList.IndexOf(value);
        }

        public Int32 Add(object value)
        {
            int index = -1;
            if (value is InfoSerializableItem)
            {
                int iPos = GetItemIndex(value);
                if (iPos == -1)
                {
                    index = InnerList.Add(value);
                }
            }
            return index;
        }

        public Boolean IsFixedSize
        {
            get
            {
                return InnerList.IsFixedSize;
            }
        }

        #endregion

        public int GetItemIndex(string s)
        {
            int iPos = -1;
            for (int i = 0; i < InnerList.Count; i++)
                if (((InfoSerializableItem)InnerList[i]).Name.Equals(s))
                {
                    iPos = i;
                    break;
                }
            return iPos;
        }

        public int GetItemIndex(object value)
        {
            int iPos = -1;
            for (int i = 0; i < InnerList.Count; i++)
                if (((InfoSerializableItem)InnerList[i]) == value)
                {
                    iPos = i;
                    break;
                }
            return iPos;
        }

        public InfoSerializableItem GetItem(string s)
        {
            InfoSerializableItem itemRet = null;
            int iPos = GetItemIndex(s);
            if (-1 != iPos)
                itemRet = (InfoSerializableItem)(InnerList[iPos]);
            return itemRet;
        }

        public InfoSerializableItem GetItem(int index)
        {
            InfoSerializableItem itemRet = null;
            if (-1 != index && index < Count)
                itemRet = (InfoSerializableItem)(InnerList[index]);
            return itemRet;
        }
    }
}
