using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp26
{
    class Program
    {
        static void Main(string[] args)
        {
            Container<int> container = 
                new Container<int>();
            container.Add(10);
            container.Add(20);
            container.Add(30);
            container.Add(40);

            container.Delete(50);
            foreach (var val in container)
            {
                Console.WriteLine(val);
            }
        }
    }

    class Item<T>
    { 
        // значение
        public T Value { get; set; }
        //ссылка на объект впереди
        public Item<T> Previous { get; set; } 
        //ссылка на объект сзади
        public Item<T> Next { get; set; }
    }

    public class Container<T> : IEnumerable<T>
    {
        Item<T> head;
        Item<T> tail;

        Item<T> current;
        public IEnumerator<T> GetEnumerator()
        {
            current = head;
            do
            {
                yield return current.Value;
                current = current.Next;
            }
            while (current != null);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        internal void Add(T val)
        {
            if (head == null)
            {
                tail = head = new Item<T> { Value = val };
                head.Next = tail;
            }
            else
            {
                var newTail = new Item<T> { Value = val };
                tail.Next = newTail;
                newTail.Previous = tail;
                tail = newTail;
            }
        }

        internal void Delete(T val)
        {
            Item<T> search = Search(head, val);
            if (search == null)
            {
                Console.WriteLine($"Элемент {val} не найден");
                return;
            }
            if (search == head)
            {
                head = search.Next;
                head.Previous = null;
            }
            else if (search == tail)
            {
                tail = search.Previous;
                tail.Next = null;
            }
            else
            {
                search.Previous.Next = search.Next;
                search.Next.Previous = search.Previous;
            }
        }

        private Item<T> Search(Item<T> node, T val)
        {
            if (node.Value.Equals(val))
                return node;
            if (node.Next == null)
                return null;
            return Search(node.Next, val);
        }
    }
}
