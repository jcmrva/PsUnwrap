using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;

namespace PsUnwrap
{
    public class PsUnwrap
    {

        // public T FindFirst<T>()
        // {
        //     return default(T);
        // }

        // public IEnumerable<T> FindAll<T>()
        // {
        //     return new List<T>();
        // }

        public string Unwrap(Collection<PSObject> psObjs)
        {
            return Unwrap<string>(psObjs);
        }

        public T Unwrap<T>(Collection<PSObject> psObjs)
        {
            foreach (var pso in psObjs)
            {
                if (pso?.BaseObject == null)
                {
                    return default(T);
                }
                else if (pso.BaseObject is T t)
                {
                    return t;
                }
                else if (pso.BaseObject is Hashtable ht)
                {
                    return Unwrap<T>(ht);
                }
                else if (pso.BaseObject is ArrayList al)
                {
                    return Unwrap<T>(al);
                }
                else if (pso.BaseObject is Array a)
                {
                    return Unwrap<T>(a);
                }
                else
                {
                    continue;
                }
            }

            return default(T);
        }

        public T Unwrap<T>(Hashtable ht)
        {
            foreach (DictionaryEntry de in ht)
            {
                if (de.Key is T k)
                {
                    return k;
                }
                else if (de.Value is T v)
                {
                    return v;
                }
                else
                {
                    continue;
                }
            }
            return default(T);
        }

        public T Unwrap<T>(ArrayList al)
        {
            foreach (var item in al)
            {
                if (item is T t)
                {
                    return t;
                }
                else
                {
                    continue;
                }
            }
            return default(T);
        }

        public T Unwrap<T>(Array a)
        {
            foreach (var item in a)
            {
                if (item is T t)
                {
                    return t;
                }
                else
                {
                    continue;
                }
            }
            return default(T);
        }
    }
}
