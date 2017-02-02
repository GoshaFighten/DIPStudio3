using System.ComponentModel;
using System;
namespace DIPStudioCore.Base {
    public abstract class SortedBindingList<T> : BindingList<T>, IDisposable
        where T : FrameElement
    {
        protected override void InsertItem(int index, T item)
        {
            index = SortItem(item);
            base.InsertItem(index, item);
            item.Owner = this;
        }

        private int SortItem(T item)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (GetElementByIndex(i).Time > item.Time)
                {
                    return i;
                }
            }
            return this.Count == 0 ? 0 : this.Count;
        }

        protected override void ClearItems()
        {
            foreach (T t in this)
            {
                t.Dispose();
            }
            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            GetElementByIndex(index).Owner = null;
            base.RemoveItem(index);
        }

        protected override void SetItem(int index, T item)
        {
            throw new NotSupportedException();
        }

        // Fields...
        private string fName;

        public string Name
        {
            get { return fName; }
            set
            {
                if (fName == value)
                {
                    return;
                }
                fName = value;
                OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
        }

        //public T this[double time] {
        //    get { return FindFrameByTime(time); }
        //}
        /// <summary>
        /// Возвращает кадр для времени
        /// </summary>
        /// <param name="time">Время кадра</param>
        /// <returns>Кадр</returns>
        public new T this[int time]
        {
            get { return FindFrameByTime(time); }
        }
        public T GetElementByIndex(int index)
        {
            return base[index];
        }
        
        protected T FindFrameByTime(int time)
        {
            if (this.Count == 0 || GetElementByIndex(0).Time > time)
                return null;
            for (int i = 0; i < this.Count - 1; i++)
            {
                T current = GetElementByIndex(i);
                T next = GetElementByIndex(i + 1);
                if (current.Time <= time && next.Time > time)
                {
                    return time - current.Time < next.Time - time ? current : next;
                }
            }
            return GetElementByIndex(this.Count - 1);
        }

        public void Dispose()
        {
            ClearItems();
        }
    }
}
