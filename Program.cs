﻿// TASK: 6. bryt ut static-metoder när du ser kodrepetitioner,
// för varje ny static-metod a. kompilera/kör/testa, b. stage/commit/push!

// TASK: 7. ta bort onödiga spårutskrifter,
// gör en enda a. kompilering/körning/test, b. stage/commit/push!

// TASK: 8. kommentera för att begripa koden, kommentera gärna alla metoder (static or dynamic) som
// du känner för,
// gör en enda a. kompilering/körning/test, b. stage/commit/push!

// TASK: 9. om du hittar variabler som du tycker är svårbegripliga, döp om dem till något begripligt,
// för varje variabeländring gör kompilera, gör en enda slutlig a. kompilering/körning/test,
// och sedan b. stage/commit/push!

// TASK: 10. plocka ut funktionaliteterna i if-else-kedjan genom att göra dem till metoder, som det passar
// ändamålet,
// för varje utbrytning a. kompilera/kör/testa, b. stage/commit/push!

// TASK: 11. bygg smarta konstruktorer, setters och getters (kanske även properties) som det passar
// ändamålet, men i synnerhet setters och getters för attributen phone och address,
// gör a. kompileringar/körningar/tester, b. stage/commit/push som det passar!

namespace dtp6_contacts
{
    class MainClass
    {
        static Person[] contactList = new Person[100];
        class Person
        {
            public string persname, surname, phone, address, birthdate;
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            Console.WriteLine("Hello and welcome to the contact list");
            Console.WriteLine("Avaliable commands: ");
            Console.WriteLine("  load        - load contact list data from the file address.lis");
            Console.WriteLine("  load /file/ - load contact list data from the file");
            Console.WriteLine("  new        - create new person");
            Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            Console.WriteLine("  quit        - quit the program");
            Console.WriteLine("  save         - save contact list data to the file previously loaded");
            Console.WriteLine("  save /file/ - save contact list data to the file");
            Console.WriteLine();
            do
            {
                Console.Write($"> ");
                commandLine = Console.ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                {
                    // NYI!
                    Console.WriteLine("Not yet implemented: safe quit");
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2)
                    {
                        lastFileName = "address.lis";
                        using (StreamReader infile = new StreamReader(lastFileName))
                        {
                            string line;
                            while ((line = infile.ReadLine()) != null)
                            {
                                Console.WriteLine(line);
                                string[] attrs = line.Split('|');
                                Person p = new Person();
                                p.persname = attrs[0];
                                p.surname = attrs[1];
                                string[] phones = attrs[2].Split(';');
                                p.phone = phones[0];
                                string[] addresses = attrs[3].Split(';');
                                p.address = addresses[0];
                                for (int ix = 0; ix < contactList.Length; ix++)
                                {
                                    if (contactList[ix] == null)
                                    {
                                        contactList[ix] = p;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        lastFileName = commandLine[1];
                        using (StreamReader infile = new StreamReader(lastFileName))
                        {
                            string line;
                            while ((line = infile.ReadLine()) != null)
                            {
                                Console.WriteLine(line);
                                string[] attrs = line.Split('|');
                                Person p = new Person();
                                p.persname = attrs[0];
                                p.surname = attrs[1];
                                string[] phones = attrs[2].Split(';');
                                p.phone = phones[0];
                                string[] addresses = attrs[3].Split(';');
                                p.address = addresses[0];
                                for (int ix = 0; ix < contactList.Length; ix++)
                                {
                                    if (contactList[ix] == null)
                                    {
                                        contactList[ix] = p;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2)
                    {
                        using (StreamWriter outfile = new StreamWriter(lastFileName))
                        {
                            foreach (Person p in contactList)
                            {
                                if (p != null)
                                    outfile.WriteLine($"{p.persname};{p.surname};{p.phone};{p.address};{p.birthdate}");
                            }
                        }
                    }
                    else
                    {
                        // NYI!
                        Console.WriteLine("Not yet implemented: save /file/");
                    }
                }
                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2)
                    {
                        Console.Write("personal name: ");
                        string persname = Console.ReadLine();
                        Console.Write("surname: ");
                        string surname = Console.ReadLine();
                        Console.Write("phone: ");
                        string phone = Console.ReadLine();
                    }
                    else
                    {
                        // NYI!
                        Console.WriteLine("Not yet implemented: new /person/");
                    }
                }
                else if (commandLine[0] == "help")
                {
                    Console.WriteLine("Avaliable commands: ");
                    Console.WriteLine("  delete       - emtpy the contact list");
                    Console.WriteLine("  delete /persname/ /surname/ - delete a person");
                    Console.WriteLine("  load        - load contact list data from the file address.lis");
                    Console.WriteLine("  load /file/ - load contact list data from the file");
                    Console.WriteLine("  new        - create new person");
                    Console.WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
                    Console.WriteLine("  quit        - quit the program");
                    Console.WriteLine("  save         - save contact list data to the file previously loaded");
                    Console.WriteLine("  save /file/ - save contact list data to the file");
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit");
        }
    }
}
