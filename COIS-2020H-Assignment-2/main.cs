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
                    // Part B code here
                    Console.WriteLine("You selected Part B.");
                    // add code
                    break;

                case "C":
                    // Part C code here
                    Console.WriteLine("You selected Part C.");
                    string inputC = "eleven twelve thirteen fourteen"; // modify as needed for testing
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
