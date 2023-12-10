using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quản_lý_phòng_trọ
{
    internal class CustomLinkedList<T>
    {
        public CustomNode<T> First { get; private set; }
        public CustomNode<T> Last { get; private set; }
        public int Count { get; private set; }
        public CustomNode<T> Node { get; private set; }

        public CustomLinkedList()
        {
            this.First = null;
            this.Last = null;
            Count = 0;
        }

        public void AddFirst(CustomNode<T> node)
        {
            if (this.First == null)
            {
                this.First = node;
                this.Last = node;
            }
            else
            {
                node.Next = this.First;
                this.First = node;
            }
            Count++;
        }

        public void AddLast(CustomNode<T> node)
        {
            if (this.First == null)
                AddFirst(node);
            else
            {
                this.Last.Next = node;
                this.Last = node;
                Count++;
            }
        }

        public void AddAfter(CustomNode<T> node, CustomNode<T> after)
        {
            if (this.Last == node)
            {
                AddLast(after);
            }
            else
            {
                after.Next = node.Next;
                node.Next = after;
                Count++;
            }
        }

        public CustomNode<T> FindNode(T node)
        {
            CustomNode<T> currentNode = First;
            while (currentNode != null && !currentNode.Data.Equals(node)) { currentNode = currentNode.Next; }
            return currentNode;
        }

        public void RemoveFirst()
        {
            if (this.First == null || this.Count == 0)
            {
                return;
            }
            First = First.Next;
            Count--;
        }

        public void Remove(CustomNode<T> node)
        {
            if (this.First == null || this.Count == 0)
            {
                return;
            }
            CustomNode<T> previousNode = First;
            CustomNode<T> currentNode = previousNode.Next;
            while (currentNode != null && currentNode != node)
            {
                previousNode = currentNode;
                currentNode = previousNode.Next;
            }
            if (currentNode != null)
            {
                previousNode.Next = currentNode.Next;
                currentNode = currentNode.Next;
                Count--;
            }
        }
    }

}
