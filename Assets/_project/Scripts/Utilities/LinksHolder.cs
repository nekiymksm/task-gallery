using System.Collections;

namespace _project.Scripts.Utilities
{
    public class LinksHolder
    {
        private static LinksHolder _instance;

        private ArrayList _list;

        private LinksHolder()
        {
            _list = new ArrayList();
        }
 
        public static LinksHolder GetInstance()
        {
            if (_instance == null)
            {
                _instance = new LinksHolder();
            }
                
            return _instance;
        }

        public void Hold<T>(T item)
        {
            _list.Add(item);
        }

        public object Take<T>()
        {
            var item = new object();
            
            for (int i = 0; i < _list.Count; i++)
            {
                if (_list[i].GetType() == typeof(T))
                {
                    item = _list[i];
                    _list.RemoveAt(i);
                    
                    return item;
                }
            }

            return item;
        }
    }
}