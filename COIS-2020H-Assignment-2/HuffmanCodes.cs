// Assignment 2 COIS 2020H
// Group Members:
// Alon Raigorodetsky ID: 0827093    Email: alonraigorodetsky@trentu.ca
// Edidiong Jairus    ID: 0866074    Email: Edidiongjairus@trentu.ca
// Reuben da Silva Freitas Filho ID: 0820206    Email:rdasilvafreitasfilho@trentu.ca

using System;
using System.Collections.Generic;

namespace COIS_2020H_Assignment_2
{
    internal class HuffmanCodes
    {
        // Node class for Huffman tree
        private class Node : IComparable
        {
            // Stored character (only meaningful for leaf nodes; '\0' for internal nodes)
            public char Character { get; set; }
            // Frequency (leaf) or combined subtree frequency (internal)
            public int Frequency { get; set; }
            // Left child (bit 0)
            public Node Left { get; set; }
            // Right child (bit 1)
            public Node Right { get; set; }

            public Node(char character, int frequency, Node left = null, Node right = null)
            {
                Character = character;
                Frequency = frequency;
                Left = left;
                Right = right;
            }

            // Compare nodes by frequency (ascending)
            public int CompareTo(object obj)
            {
                if (obj is Node other)
                    return Frequency.CompareTo(other.Frequency);
                throw new ArgumentException("Object is not a Node");
            }

            // True if leaf (no children)
            public bool IsLeaf => Left == null && Right == null;
        }

        // Huffman tree root used for code creation and decoding
        private Node HT;
        // Dictionary mapping characters to their Huffman code
        private Dictionary<char, string> D;

        // Constructor: Invokes AnalyzeText, Build and CreateCodes
        public HuffmanCodes(string S)
        {
            if (S == null) throw new ArgumentNullException(nameof(S)); // guard null
            int[] freq = AnalyzeText(S);
            Build(freq);
            D = new Dictionary<char, string>();
            CreateCodes();
        }

        // Task 1: Analyze text and return the frequency of each character in the given text
        private int[] AnalyzeText(string S)
        {
            int[] freq = new int[95]; // printable ASCII 32-126
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

            // Empty input (no printable ASCII) -> no tree
            if (pq.Count == 0)
            {
                HT = null;
                return;
            }

            // Build the tree
            while (pq.Count > 1)
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
            if (HT == null) return; // nothing to do

            // Special case: only one distinct character -> assign code "0"
            if (HT.IsLeaf)
            {
                D[HT.Character] = "0";
                return;
            }

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
            if (S == null) throw new ArgumentNullException(nameof(S));
            var sb = new System.Text.StringBuilder();
            foreach (char c in S)
            {
                // Get the code from Dictionary D using the char as the key
                if (D.TryGetValue(c, out string code))
                    sb.Append(code);
                // Characters not in dictionary (e.g., outside printable ASCII) are ignored
            }
            return sb.ToString();
        }

        // Task 5: Decode the given string of 0s and 1s and return the original text
        public string Decode(string S)
        {
            if (S == null) throw new ArgumentNullException(nameof(S));
            if (HT == null) return string.Empty; // nothing encoded

            // Single-character tree: every occurrence encoded as '0'
            if (HT.IsLeaf)
            {
                foreach (char bit in S)
                {
                    if (bit != '0')
                        throw new ArgumentException("Invalid encoded data for single-character Huffman tree.");
                }
                return new string(HT.Character, S.Length);
            }

            var result = new System.Text.StringBuilder();
            Node node = HT;

            foreach (char bit in S)
            {
                node = (bit == '0') ? node.Left : node.Right;
                if (node == null)
                    throw new ArgumentException("Invalid encoded data (traversal hit null).");

                if (node.IsLeaf)
                {
                    result.Append(node.Character);
                    node = HT; // restart from root for next code
                }
            }

            // If we did not end at the root, final code was incomplete
            if (node != HT)
                throw new ArgumentException("Incomplete final code in encoded data.");

            return result.ToString();
        }
    }
}