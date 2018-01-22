// http://msdn.microsoft.com/en-us/library/d5x73970.aspx
// When you define a generic class, ...

using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using CSharp.cc.Xml;

namespace CSharp.cc
{
    public class GenericList<T> where T : XmlData
    {
        private class Node
        {
            private Node next;
            private T data;

            public Node(T t)
            {
                next = null;
                data = t;
            }

            public Node Next
            {
                get { return next; }
                set { next = value; }
            }

            public T Data
            {
                get { return data; }
                set { data = value; }
            }
        }

        private Node head;

        public GenericList() //constructor
        {
            head = null;
        }

        public void AddHead(T t)
        {
            Node n = new Node(t);
            n.Next = head;
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        //public T FindFirstOccurrence(string s)
        //{
        //    Node current = head;
        //    T t = null;

        //    while (current != null)
        //    {
        //        //The constraint enables access to the Name property.
        //        if (current.Data.Name == s)
        //        {
        //            t = current.Data;
        //            break;
        //        }
        //        else
        //        {
        //            current = current.Next;
        //        }
        //    }
        //    return t;
        //}
    }
}


