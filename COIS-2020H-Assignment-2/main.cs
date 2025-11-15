using System;
using COIS_2020H_Assignment_2;

class main
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("A - Part A");
            Console.WriteLine("B - Part B");
            Console.WriteLine("C - Part C");
            Console.WriteLine("0 - Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine()?.Trim().ToUpper();

            switch (choice)
            {
                case "A":
                    // Part A code here
                    Console.WriteLine("You selected Part A.");
                    // Add code
                    break;

                case "B":
                    Console.WriteLine(" Hash Table with Sorted Buckets - Testing \n");

                    // Create hash table with Point keys and string values
                    HashTable<Point, string> hashTable = new HashTable<Point, string>(5);

                    Console.WriteLine("Test 1: Inserting Points");
                    Console.WriteLine("                        ");

                    hashTable.Insert(new Point(5, 3), "Point A");
                    hashTable.Insert(new Point(2, 7), "Point B");
                    hashTable.Insert(new Point(8, 1), "Point C");
                    hashTable.Insert(new Point(2, 4), "Point D");
                    hashTable.Insert(new Point(5, 9), "Point E");
                    hashTable.Insert(new Point(1, 1), "Point F");
                    hashTable.Insert(new Point(8, 8), "Point G");
                    hashTable.Insert(new Point(3, 5), "Point H");

                    Console.WriteLine("Inserted 8 points\n");

                    hashTable.DisplayBuckets();
                    hashTable.Output();

                    Console.WriteLine("Test 2: Retrieving Values");
                    Console.WriteLine("                         ");

                    Point searchPoint = new Point(5, 3);
                    if (hashTable.Retrieve(searchPoint, out string value))
                    {
                        Console.WriteLine($"Found: {searchPoint} -> {value}");
                    }

                    searchPoint = new Point(2, 7);
                    if (hashTable.Retrieve(searchPoint, out value))
                    {
                        Console.WriteLine($"Found: {searchPoint} -> {value}");
                    }

                    searchPoint = new Point(10, 10);
                    if (hashTable.Retrieve(searchPoint, out value))
                    {
                        Console.WriteLine($"Found: {searchPoint} -> {value}");
                    }
                    else
                    {
                        Console.WriteLine($"Not found: {searchPoint}");
                    }
                    Console.WriteLine();

                    Console.WriteLine("Test 3: Updating Existing Key");
                    Console.WriteLine("                             ");

                    hashTable.Insert(new Point(5, 3), "Point A Updated");
                    hashTable.Retrieve(new Point(5, 3), out value);
                    Console.WriteLine($"After update: (5, 3) -> {value}\n");

                    hashTable.Output();

                    Console.WriteLine("Test 4: Removing Points");
                    Console.WriteLine("                       ");

                    Point removePoint = new Point(2, 7);
                    if (hashTable.Remove(removePoint))
                    {
                        Console.WriteLine($"Successfully removed: {removePoint}");
                    }

                    removePoint = new Point(8, 1);
                    if (hashTable.Remove(removePoint))
                    {
                        Console.WriteLine($"Successfully removed: {removePoint}");
                    }

                    removePoint = new Point(99, 99);
                    if (!hashTable.Remove(removePoint))
                    {
                        Console.WriteLine($"Could not remove (not found): {removePoint}");
                    }
                    Console.WriteLine();

                    hashTable.Output();

                    Console.WriteLine("Test 5: MakeEmpty");
                    Console.WriteLine("                 ");

                    hashTable.MakeEmpty();
                    Console.WriteLine("Called MakeEmpty()\n");

                    hashTable.Output();

                    Console.WriteLine("Test 6: Point Comparison Tests");
                    Console.WriteLine("                              ");

                    Point p1 = new Point(3, 5);
                    Point p2 = new Point(3, 8);
                    Point p3 = new Point(5, 2);
                    Point p4 = new Point(3, 5);

                    Console.WriteLine($"{p1} compared to {p2}: {p1.CompareTo(p2)} (should be negative)");
                    Console.WriteLine($"{p3} compared to {p1}: {p3.CompareTo(p1)} (should be positive)");
                    Console.WriteLine($"{p1} compared to {p4}: {p1.CompareTo(p4)} (should be zero)");
                    Console.WriteLine($"{p1}.Equals({p4}): {p1.Equals(p4)} (should be True)");
                    Console.WriteLine($"{p1}.Equals({p2}): {p1.Equals(p2)} (should be False)");
                    Console.WriteLine($"{p1}.GetHashCode() == {p4}.GetHashCode(): {p1.GetHashCode() == p4.GetHashCode()} (should be True)");
                    Console.WriteLine();

                    Console.WriteLine(" All Tests Complete ");
                    break;

                case "C":
                    // Part C: Huffman code is here
                    Console.WriteLine("You selected Part C: Huffman Code.");
                    Console.Write("Enter text to encode and decode: ");
                    string inputC = Console.ReadLine() ?? string.Empty;

                    if (inputC.Length == 0)
                    {
                        Console.WriteLine("No input provided. Nothing to encode/decode.");
                        break;
                    }

                    var huffmanC = new HuffmanCodes(inputC);
                    string encodedC = huffmanC.Encode(inputC);
                    string decodedC = huffmanC.Decode(encodedC);
                    Console.WriteLine($"Encoded: {encodedC}");
                    Console.WriteLine($"Decoded: {decodedC}");
                    Console.WriteLine($"Original and Decoded are equal: {inputC == decodedC}");
                    break;

                case "0":
                    Console.WriteLine("Exiting program.");
                    return;

                default:
                    Console.WriteLine("Invalid input. Please enter A, B, C, or 0.");
                    break;
            }

            Console.WriteLine(); // Add a blank line for readability
        }
    }
}