using System;
using System.Collections.Generic;

namespace COIS_2020H_Assignment_2
{
    internal class HuffmanCodes
    {
        // Node class for Huffman tree
        private class Node : IComparable
        {
            public char Character { get; set; }
            public int Frequency { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public Node(char character, int frequency, Node left = null, Node right = null)
            {
                Character = character;
                Frequency = frequency;
                Left = left;
                Right = right;
            }

            // Compare nodes by frequency
            public int CompareTo(object obj)
            {
                if (obj is Node other)
                    return Frequency.CompareTo(other.Frequency);
                throw new ArgumentException("Object is not a Node");
            }

            // Check if node is a leaf
            public bool IsLeaf => Left == null && Right == null;
        }

        private Node HT; // Huffman tree to create codes and decode text
        private Dictionary<char, string> D; // Dictionary to store the codes for each character

        // Constructor: Invokes AnalyzeText, Build and CreateCodes 
        public HuffmanCodes(string S)
        {
            int[] freq = AnalyzeText(S);
            Build(freq);
            D = new Dictionary<char, string>();
            CreateCodes();
        }

        // Task 1: Analyze text and Return the frequency of each character in the given text 
        private int[] AnalyzeText(string S)
        {
            int[] freq = new int[95]; // ASCII 32-126
            foreach (char c in S)
            {
                if (c >= 32 && c <= 126)
                    freq[c - 32]++;
            }
            return freq;
        }

        // Task 2: Build a Huffman tree based on the character frequencies greater than 0 
        private void Build(int[] F)
        {
            var pq = new PriorityQueue<Node, int>();
            for (int i = 0; i < F.Length; i++)
            {
                if (F[i] > 0) // Only consider characters with frequency > 0
                {
                    char c = (char)(i + 32);
                    pq.Enqueue(new Node(c, F[i]), F[i]);
                }
            }

            while (pq.Count > 1) // Build the tree
            {
                pq.TryDequeue(out Node left, out int freqLeft);
                pq.TryDequeue(out Node right, out int freqRight);
                var parent = new Node('\0', freqLeft + freqRight, left, right);
                pq.Enqueue(parent, parent.Frequency);
            }

            pq.TryDequeue(out HT, out _);
        }

        // Task 3: Create codes by traversing Huffman tree
        private void CreateCodes()
        {
            void Traverse(Node node, string code)
            {
                if (node == null) return;
                if (node.IsLeaf)
                {
                    D[node.Character] = code; // Store the codes in Dictionary D using the char as the key 
                }
                else
                {
                    Traverse(node.Left, code + "0");
                    Traverse(node.Right, code + "1");
                }
            }
            Traverse(HT, "");
        }

        // Task 4: Encode the given text and return a string of 0s and 1s
        public string Encode(string S)
        {
            var sb = new System.Text.StringBuilder();
            foreach (char c in S)
            {
                if (D.TryGetValue(c, out string code)) // Get the code from Dictionary D using the char as the key
                    sb.Append(code);
            }
            return sb.ToString();
        }

        // Task 5:  Decode the given string of 0s and 1s and return the original text
        public string Decode(string S)
        {
            var result = new System.Text.StringBuilder();
            Node node = HT;
            foreach (char bit in S)
            {
                node = (bit == '0') ? node.Left : node.Right;
                if (node.IsLeaf)
                {
                    result.Append(node.Character);
                    node = HT;
                }
            }
            return result.ToString();
        }
    }
}
