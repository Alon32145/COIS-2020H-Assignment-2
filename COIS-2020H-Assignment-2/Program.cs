// Assignment 2 COIS 2020H
// Group Members:
// Alon Raigorodetsky ID: 0827093    Email: alonraigorodetsky@trentu.ca
// Edidiong Jairus    ID: 0866074    Email: Edidiongjairus@trentu.ca
// Reuben da Silva Freitas Filho ID: 0820206    Email:rdasilvafreitasfilho@trentu.ca

using System;
using System.Collections.Generic;

// Point class representing a 2D coordinate
public class Point : IComparable<Point>
{
    public int X { get; set; }
    public int Y { get; set; }

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    // Compare first by X, then by Y
    public int CompareTo(Point other)
    {
        if (other == null) return 1;
        
        int xComparison = X.CompareTo(other.X);
        if (xComparison != 0)
            return xComparison;
        
        return Y.CompareTo(other.Y);
    }

    public override int GetHashCode()
    {
        unchecked
        {
            int hash = 17;
            hash = hash * 31 + X.GetHashCode();
            hash = hash * 31 + Y.GetHashCode();
            return hash;
        }
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Point))
            return false;
        
        Point other = (Point)obj;
        return X == other.X && Y == other.Y;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}

// Node class for the linked list
public class Node<TKey, TValue> where TKey : IComparable<TKey>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
    public Node<TKey, TValue> Next { get; set; }

    public Node()
    {
        Next = null;
    }

    public Node(TKey key, TValue value)
    {
        Key = key;
        Value = value;
        Next = null;
    }
}

public class HashTable<TKey, TValue> where TKey : IComparable<TKey>
{
    private Node<TKey, TValue>[] table;
    private int size;

    public HashTable(int tableSize = 10)
    {
        size = tableSize;
        table = new Node<TKey, TValue>[size];
        
        // Initialize each bucket with a header node
        for (int i = 0; i < size; i++)
        {
            table[i] = new Node<TKey, TValue>();
        }
    }

    // Hash function to map key to bucket index
    private int Hash(TKey key)
    {
        int hashCode = key.GetHashCode();
        return Math.Abs(hashCode) % size;
    }

    // Remove all entries from the hash table
    public void MakeEmpty()
    {
        for (int i = 0; i < size; i++)
        {
            table[i].Next = null;
        }
    }

    public void Insert(TKey key, TValue value)
    {
        if (key == null)
            throw new ArgumentNullException(nameof(key));

        int bucket = Hash(key);
        Node<TKey, TValue> current = table[bucket];
        Node<TKey, TValue> previous = null;

        // Traverse to find insertion position
        while (current.Next != null)
        {
            int comparison = key.CompareTo(current.Next.Key);
            
            if (comparison == 0)
            {
                // Key already exists, update value
                current.Next.Value = value;
                return;
            }
            else if (comparison < 0)
            {
                // Found insertion point (key is smaller)
                break;
            }
            
            current = current.Next;
        }

        // Put new node after current
        Node<TKey, TValue> newNode = new Node<TKey, TValue>(key, value);
        newNode.Next = current.Next;
        current.Next = newNode;
    }

    // Remove a key-value pair
    public bool Remove(TKey key)
    {
        if (key == null)
            return false;

        int bucket = Hash(key);
        Node<TKey, TValue> current = table[bucket];

        while (current.Next != null)
        {
            int comparison = key.CompareTo(current.Next.Key);
            
            if (comparison == 0)
            {
                // Found the key, remove it
                current.Next = current.Next.Next;
                return true;
            }
            else if (comparison < 0)
            {
                // Key would be before this position, so not found
                return false;
            }
            
            current = current.Next;
        }

        return false;
    }

    // Retrieve value associated with key
    public bool Retrieve(TKey key, out TValue value)
    {
        value = default(TValue);
        
        if (key == null)
            return false;

        int bucket = Hash(key);
        Node<TKey, TValue> current = table[bucket].Next;

        while (current != null)
        {
            int comparison = key.CompareTo(current.Key);
            
            if (comparison == 0)
            {
                // Found the key
                value = current.Value;
                return true;
            }
            else if (comparison < 0)
            {
                // Key would be before this position, so not found
                return false;
            }
            
            current = current.Next;
        }

        return false;
    }

    // Output all key value pairs in sorted order
    public void Output()
    {
        // Collect all the key value pairs
        List<KeyValuePair<TKey, TValue>> allPairs = new List<KeyValuePair<TKey, TValue>>();

        for (int i = 0; i < size; i++)
        {
            Node<TKey, TValue> current = table[i].Next;
            
            while (current != null)
            {
                allPairs.Add(new KeyValuePair<TKey, TValue>(current.Key, current.Value));
                current = current.Next;
            }
        }

        // Sort all pairs by key
        allPairs.Sort((pair1, pair2) => pair1.Key.CompareTo(pair2.Key));

        // Output sorted pairs
        Console.WriteLine("Hash Table Contents (Sorted by Key):");
        Console.WriteLine("                                    ");
        
        if (allPairs.Count == 0)
        {
            Console.WriteLine("(empty)");
        }
        else
        {
            foreach (var pair in allPairs)
            {
                Console.WriteLine($"Key: {pair.Key}, Value: {pair.Value}");
            }
        }
        
        Console.WriteLine();
    }

    // Helper method to display bucket contents
    public void DisplayBuckets()
    {
        Console.WriteLine("Bucket Contents:");
        for (int i = 0; i < size; i++)
        {
            Console.Write($"Bucket {i}: ");
            Node<TKey, TValue> current = table[i].Next;
            
            if (current == null)
            {
                Console.WriteLine("(empty)");
            }
            else
            {
                while (current != null)
                {
                    Console.Write($"[{current.Key}, {current.Value}] ");
                    current = current.Next;
                }
                Console.WriteLine();
            }
        }
        Console.WriteLine();
    }
}