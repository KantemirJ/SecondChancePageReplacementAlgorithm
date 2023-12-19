using System;
using System.Xml.Linq;

class Page
{
    public string Name { get; set; }
    public string ArrivalTime { get; set; }
    public bool Referenced { get; set; }
    public Page? Next { get; set; } = null;

    public Page(string name, bool r) 
    {
        Name = name;
        ArrivalTime = DateTime.Now.ToString("HH:mm:ss");
        Referenced = r;
        Next = null;
    }

}

class MemoryManager
{
    private static Page head = null;
    private static Page tail = null;
    private static readonly int PageNuberLimit = 5;
    private static int PageCount = 0;

    public void AllocateNewPage(string name, bool r)
    {
        if (head == null)
        {
            head = new Page(name, r);
            tail = head;
            PageCount++;
        }
        else if (PageCount >= PageNuberLimit)
        {
            Console.WriteLine("Page Fault occurred!");
            while (true)
            {
                if (head.Referenced)
                {
                    head.Referenced = false;
                    head.ArrivalTime = DateTime.Now.ToString("HH:mm:ss");
                    tail.Next = head;
                    tail = head;
                    head = head.Next;
                    tail.Next = null;
                }
                else
                {
                    head = head.Next;
                    Page page = new Page(name, r);
                    tail.Next = page;
                    tail = page;
                    break;
                }
            }
        }
        else
        {
            Page page = new Page(name, r);
            tail.Next = page;
            tail = page;
            PageCount++;
        }
    }

    public void DisplayMemory()
    {
        Page current = head;

        Console.WriteLine("Page Linked List:[Name]  [Arrival Time]  [Reference Bit]");

        while (true)
        {
            Console.Write($"[{current.Name}][{current.ArrivalTime}][{current.Referenced}]");
            if (current.Next == null)
            {
                Console.WriteLine();
                break;
            }
            Console.Write("-->");
            current = current.Next;
        }
    }
    public bool StringToBool(string str)
    {
        if (str == "0")
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}

class Program
{
    static void Main()
    {
        MemoryManager memoryManager = new MemoryManager();
        Console.WriteLine(
                "Menu:\n" +
                "1-Allocate a new page(page name, reference bit)\n" +
                "2-Display Page List\n" +
                "3-Exit");
        while (true)
        {
            string[] command = Console.ReadLine().Split(" ");
            if (command[0] == "3")
            {
                break;
            }
            switch (command[0])
            {
                case "1":
                    memoryManager.AllocateNewPage(command[1], memoryManager.StringToBool(command[2]));
                    break;
                case "2":
                    memoryManager.DisplayMemory();
                    break;
            }
        }
    }
}
