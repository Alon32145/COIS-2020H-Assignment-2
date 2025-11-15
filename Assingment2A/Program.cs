// Assignment 2 COIS 2020H
// Group Members:
// Alon Raigorodetsky ID: 0827093    Email: alonraigorodetsky@trentu.ca
// Edidiong Jairus    ID: 0866074    Email: Edidiongjairus@trentu.ca
// Reuben da Silva Freitas Filho ID: 0820206    Email:rdasilvafreitasfilho@trentu.ca

using System;
public class PriorityItem : IComparable<PriorityItem>
{
    public int Value { get; set; }

    public PriorityItem(int value) => Value = value;

    public int CompareTo(PriorityItem other)
    {
        if (other == null) return 1;
        return Value.CompareTo(other.Value);
    }

    public override string ToString() => Value.ToString();
}
//   Queue with Heap Matrix

public class PriorityQueue<T> where T : class, IComparable<T>
{
    private T[,] H;         
    private int[] rowLengths; 
    private int numRows;
    private int numCols;
    private int count;

    
    // Constructor
   
    
    public PriorityQueue(int m, int n)
    {
        numRows = m;
        numCols = n;
        H = new T[m, n];
        rowLengths = new int[m];
        count = 0;
    }

    
    
    // Insertion
    
    public void Insert(T item)
    {
        if (count >= numRows * numCols)
            throw new InvalidOperationException("Priority queue is full");

        // first row with free space for find 
        int row = 0;
        while (row < numRows && rowLengths[row] == numCols) row++;

        int col = rowLengths[row];
        H[row, col] = item;
        rowLengths[row]++;
        count++;

        BubbleUp(row, col);
    }



    // Removing max

    public void Remove()
    {
        if (count == 0)
            throw new InvalidOperationException("Priority queue is empty");

        // last filled cell
        int lastRow = numRows - 1;
        while (lastRow >= 0 && rowLengths[lastRow] == 0) lastRow--;

        int lastCol = rowLengths[lastRow] - 1;

        // Move last into root
        H[0, 0] = H[lastRow, lastCol];
        H[lastRow, lastCol] = null;
        rowLengths[lastRow]--;
        count--;

        BubbleDown(0, 0);
    }

    
    public T Front()
    {
        if (count == 0)
            throw new InvalidOperationException("Priority queue is empty");
        return H[0, 0];
    }
    public bool Found(T item)
    {
        if (count == 0) return false;

        int row = 0;
        int col = rowLengths[0] - 1;

        // Searching downward/leftward
        while (row < numRows && col >= 0)
        {
            int cmp = H[row, col].CompareTo(item);

            if (cmp == 0) return true;
            else if (cmp > 0) row++; 
            else col--;             
        }

        return false;
    }

   
    public int Size() => count;

    
    public void Print()
    {
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (j < rowLengths[i])
                    Console.Write($"{H[i, j],-6} ");
                else
                    Console.Write("+∞     ");
            }
            Console.WriteLine();
        }
    }

   
    private void BubbleUp(int r, int c)
    {
        while (true)
        {
            bool moved = false;

            
            if (r > 0 && c < rowLengths[r - 1] &&
                H[r, c].CompareTo(H[r - 1, c]) > 0)
            {
                Swap(r, c, r - 1, c);
                r--;
                moved = true;
            }

            // Comparing Again 
            else if (c > 0 &&
                H[r, c].CompareTo(H[r, c - 1]) > 0)
            {
                Swap(r, c, r, c - 1);
                c--;
                moved = true;
            }

            if (!moved) break;
        }
    }

    private void BubbleDown(int r, int c)
    {
        while (true)
        {
            int bestR = r;
            int bestC = c;

            // Chekcing below and right
            if (c + 1 < rowLengths[r] &&
                H[r, c + 1].CompareTo(H[bestR, bestC]) > 0)
            {
                bestR = r;
                bestC = c + 1;
            }

            
            if (r + 1 < numRows &&
                c < rowLengths[r + 1] &&
                H[r + 1, c].CompareTo(H[bestR, bestC]) > 0)
            {
                bestR = r + 1;
                bestC = c;
            }

            if (bestR == r && bestC == c) break;

            Swap(r, c, bestR, bestC);
            r = bestR;
            c = bestC;
        }
    }

    private void Swap(int r1, int c1, int r2, int c2)
    {
        T tmp = H[r1, c1];
        H[r1, c1] = H[r2, c2];
        H[r2, c2] = tmp;
    }
}


// Tester class

class Program
{
    static void Main()
    {
        Console.WriteLine("Heap Matrix:\n");

        var pq = new PriorityQueue<PriorityItem>(4, 5);

        int[] values =
        {
            85, 93, 45, 53, 78,
            25, 42, 70, 91, 17,
            33, 67, 87, 99
        };

        foreach (int v in values)
            pq.Insert(new PriorityItem(v));

        pq.Print();

        Console.WriteLine($"\nMax = {pq.Front()}");
        Console.WriteLine($"Size = {pq.Size()}");
        Console.WriteLine($"Found 45? {pq.Found(new PriorityItem(45))}");
        Console.WriteLine($"Found 100? {pq.Found(new PriorityItem(100))}");

        pq.Remove();
        Console.WriteLine("\nAfter removing max:");
        pq.Print();
    }
}
