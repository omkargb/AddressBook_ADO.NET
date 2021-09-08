using System;

namespace AddressBookSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to contact management program");
            ContactOperations co = new ContactOperations();
            co.GetContactDetails();
        }
    }
}
