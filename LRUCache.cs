using System;
using System.Collections.Generic;

namespace LRUCache
{
    class ListNode
    {
        public int val;
        public int key;
        public ListNode next;
        public ListNode prev;
        public ListNode(int k, int x) { this.val = x; this.next = null;  this.prev = null; this.key = k; }
    }


    public class LRUCache
    {
        readonly int capacity;
        int count;
        ListNode head;
        ListNode tail;

        Dictionary<int, ListNode> cache;

        public LRUCache(int capacity)
        {
            this.capacity = capacity;
            count = 0;
            head = null;
            tail = null;
            cache = new Dictionary<int, ListNode>();
        }

        public void put(int key, int value) {

            //If cache contains key
            if (cache.ContainsKey(key))
            {
                var node = cache[key];
                if (node.prev != null)
                {
                    node.prev.next = node.next;
                }
                if (node.next !=  null)
                {
                    node.next.prev = node.prev;
                }
                node.next = head;
                head.prev = node;
                head = node;
                head.prev = null;
            }
            else
            {
                var newNode = new ListNode(key,value);
                cache.Add(key, newNode);

                // If DLL is empty
                if(head == null)
                {
                    head = newNode;
                    tail = newNode;
                    count++;
                }
                else
                {
                    // capacity is full
                    if(count == capacity)
                    {
                        while (tail.next != null)
                        {
                            tail = tail.next;
                        }

                        int k = tail.key;
                        tail = tail.prev;
                        tail.next = null;
                        newNode.next = head;
                        head.prev = newNode;
                        head = newNode;
                        cache.Remove(k);
                    }
                    else
                    {
                        newNode.next = head;
                        head.prev = newNode;
                        head = newNode;
                        count++;
                    }
                }

            }
        }

        public int get(int key)
        {
            if(cache.ContainsKey(key))
            {
                var node = cache[key];
                if (node.prev != null)
                {
                    node.prev.next = node.next;
                }

                if (node.next != null)
                {
                    node.next.prev = node.prev;
                }
                node.next = head;
                head.prev = node;
                head = node;
                head.prev = null;

                return cache[key].val;
            }
            return -1;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            LRUCache cache = new LRUCache(2 /* capacity */ );

            cache.put(1, 1);
            cache.put(2, 2);
            Console.WriteLine(cache.get(1));       // returns 1
            cache.put(3, 3);    // evicts key 2
            Console.WriteLine(cache.get(2));       // returns -1 (not found)
            cache.put(4, 4);    // evicts key 1
            Console.WriteLine(cache.get(1));       // returns -1 (not found)
            Console.WriteLine(cache.get(3));       // returns 3
            Console.WriteLine(cache.get(4));       // returns 4
        }
    }
}
