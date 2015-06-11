using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwordAndScaleTake2
{
        class Heap
        {
            List<PathSquare> heap;

            public Heap()
            {
                heap = new List<PathSquare>();
            }

            public void push(PathSquare elem)
            {
                this.heap.Add(elem);
                this.percolateUp();

            }

            public PathSquare peek()
            {
                return this.heap[0];
            }

            public PathSquare pop()
            {
                if (this.heap.Count > 0)
                {
                    PathSquare root = this.heap[0];
                    PathSquare last = this.heap[this.heap.Count - 1];
                    this.heap[0] = last;
                    this.heap.RemoveAt(this.heap.Count - 1);
                    if (this.heap.Count > 0)
                    {

                        this.percolateDown();
                    }
                    return root;

                }
                return default(PathSquare);
            }

            public void percolateDown()
            {
                int idx = 0;
                PathSquare x = heap[idx];
                while (idx * 2 < this.heap.Count)
                {
                    int child = idx * 2;
                    if ((child + 1 < this.heap.Count) && (heap[child + 1].getScore() < heap[child].getScore()))
                    {
                        child++;
                    }
                    if (x.getScore() > heap[child].getScore())
                    {
                        heap[idx] = heap[child]; // child moves up
                        idx = child;             // next one to check
                    }
                    else
                        break;
                }
                // in right spot
                heap[idx] = x;
            }

            public void printout()
            {
                for (int i = 0; i < heap.Count; i++)
                {
                    Console.WriteLine(heap[i].getScore() + ", " + heap.Count);
                }
                if (heap.Count == 0)
                    Console.WriteLine("000000000000");
            }

            public bool contains(PathSquare n)
            {
                return heap.Contains(n);
            }

            public void percolateUp()
            {
                int idx = this.heap.Count - 1;
                PathSquare x = this.heap[idx];
                for (; idx > 1 && x.getScore() < heap[idx / 2].getScore(); idx = idx / 2)
                {
                    this.heap[idx] = this.heap[idx / 2];
                }
                heap[idx] = x;
            }
        }
    }
