using System.Collections;
using System.Collections.Generic;


namespace Framework.Collections
{
    public class Queue<T> : IQueue, IQueue<T>
    {
        private List<T> list;

        public Queue()
        {
            list = new List<T>();
        }

        public int Count
        {
            get { return list.Count; }
        }

        public object Get()
        {
            if (list.Count == 0)
                return null;
            object ret = list[0];
            list.RemoveAt(0);
            return ret;
        }

        public void Add(object obj)
        {
            list.Add((T)obj);
        }
        public void Add(T obj)
        {
            list.Add(obj);
        }
    }
}