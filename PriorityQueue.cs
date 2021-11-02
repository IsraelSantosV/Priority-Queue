using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

namespace RWS.Utils {
    public sealed class PriorityQueue<TEntry> where TEntry : IPrioritizable {

        public LinkedList<TEntry> Entries { get; } = new LinkedList<TEntry>();

        public int Count() {
            return Entries.Count;
        }

        public TEntry Peek() {
            if (Entries.Any()) {
                return Entries.First.Value;
            }

            return default(TEntry);
        }

        public TEntry Dequeue() {
            if (Entries.Any()) {
                var itemTobeRemoved = Entries.First.Value;
                Entries.RemoveFirst();
                return itemTobeRemoved;
            }

            return default(TEntry);
        }

        public void Enqueue(TEntry entry) {
            var value = new LinkedListNode<TEntry>(entry);
            if (Entries.First == null) {
                Entries.AddFirst(value);
            }
            else {
                var ptr = Entries.First;
                while (ptr.Next != null && ptr.Value.Priority < entry.Priority) {
                    ptr = ptr.Next;
                }

                if (ptr.Value.Priority <= entry.Priority) {
                    Entries.AddAfter(ptr, value);
                }
                else {
                    Entries.AddBefore(ptr, value);
                }
            }
        }

        public void Clear() {
            while(Count() > 0) {
                Dequeue();
            }
        }
    }
}
