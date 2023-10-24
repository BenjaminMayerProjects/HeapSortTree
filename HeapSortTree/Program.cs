using System;

class HeapSort<T> where T : IComparable<T>
{
    private T[] heap;
    public int HeapSize { get; private set; }

    public HeapSort(int capacity)
    {
        heap = new T[capacity];
        HeapSize = 0;
    }

    public void Insert(T item)
    {
        if (HeapSize == heap.Length)
        {
            throw new InvalidOperationException("Heap is full.");
        }

        int currentIndex = HeapSize;
        heap[HeapSize] = item;
        HeapSize++;

        while (currentIndex > 0 && heap[currentIndex].CompareTo(heap[(currentIndex - 1) / 2]) > 0)
        {
            Swap(currentIndex, (currentIndex - 1) / 2);
            currentIndex = (currentIndex - 1) / 2;
        }
    }

    public T ExtractMax()
    {
        if (HeapSize == 0)
        {
            throw new InvalidOperationException("Heap is empty.");
        }

        T max = heap[0];
        HeapSize--;

        if (HeapSize > 0)
        {
            heap[0] = heap[HeapSize];
            MaxHeapify(0);
        }

        return max;
    }

    public void HeapSortTree()
    {
        for (int i = HeapSize / 2 - 1; i >= 0; i--)
        {
            MaxHeapify(i);
        }

        for (int i = HeapSize - 1; i > 0; i--)
        {
            Swap(0, i);
            MaxHeapify(0);
        }
    }

    private void MaxHeapify(int index)
    {
        int left = 2 * index + 1;
        int right = 2 * index + 2;
        int largest = index;

        if (left < HeapSize && heap[left].CompareTo(heap[largest]) > 0)
        {
            largest = left;
        }

        if (right < HeapSize && heap[right].CompareTo(heap[largest]) > 0)
        {
            largest = right;
        }

        if (largest != index)
        {
            Swap(index, largest);
            MaxHeapify(largest);
        }
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}

class Program
{
    static void Main(string[] args)
    {
        HeapSort<int> heapSort = new HeapSort<int>(6);

        heapSort.Insert(12);
        heapSort.Insert(11);
        heapSort.Insert(13);
        heapSort.Insert(5);
        heapSort.Insert(6);
        heapSort.Insert(7);

        heapSort.HeapSortTree();

        Console.WriteLine("Sorted Array:");
        while (heapSort.HeapSize > 0)
        {
            int max = heapSort.ExtractMax();
            Console.Write(max + " ");
        }
    }
}

