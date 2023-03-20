// TASK: 6. bryt ut static-metoder när du ser kodrepetitioner,
// för varje ny static-metod a. kompilera/kör/testa, b. stage/commit/push!
// DID: Bryt ut Streamreader till statisk
// TODO: Bryt ut hjälpustrift till statisk
// BUG:  Birthdate läses inte in

// TASK: 7. ta bort onödiga spårutskrifter,
// gör en enda a. kompilering/körning/test, b. stage/commit/push!

// TASK: 8. kommentera för att begripa koden, kommentera gärna alla metoder (static or dynamic) som
// du känner för, gör en enda a. kompilering/körning/test, b. stage/commit/push!

// TASK: 9. om du hittar variabler som du tycker är svårbegripliga, döp om dem till något begripligt,
// för varje variabeländring gör kompilera, gör en enda slutlig a. kompilering/körning/test,
// och sedan b. stage/commit/push!

// TASK: 10. plocka ut funktionaliteterna i if-else-kedjan genom att göra dem till metoder, som det passar
// ändamålet,
// för varje utbrytning a. kompilera/kör/testa, b. stage/commit/push!

// TASK: 11. bygg smarta konstruktorer, setters och getters (kanske även properties) som det passar
// ändamålet, men i synnerhet setters och getters för attributen phone och address,
// gör a. kompileringar/körningar/tester, b. stage/commit/push som det passar!
using static System.Console;
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
            WriteLine("Hello and welcome to the contact list");
            WriteHelp();
            do
            {
                Write($"> ");
                commandLine = ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                {
                    // NYI!
                    WriteLine("Not yet implemented: safe quit");
                }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2) // Load utan filnamn ändrat
                    {
                        lastFileName = "address.lis";
                        LoadAddressList(lastFileName);
                    }
                    else // Om filnamn specifierat
                    {
                        lastFileName = commandLine[1];
                        LoadAddressList(lastFileName);
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
                        WriteLine("Not yet implemented: save /file/");
                    }
                }
                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2)
                    {
                        Write("personal name: ");
                        string persname = ReadLine();
                        Write("surname: ");
                        string surname = ReadLine();
                        Write("phone: ");
                        string phone = ReadLine();
                    }
                    else
                    {
                        // NYI!
                        WriteLine("Not yet implemented: new /person/");
                    }
                }
                else if (commandLine[0] == "help")
                {
                    WriteHelp();
                }
                else
                {
                    WriteLine($"Unknown command: '{commandLine[0]}'");
                }
            } while (commandLine[0] != "quit");

            static void LoadAddressList(string lastFileName)
            {
                using (StreamReader infile = new StreamReader(lastFileName))
                {
                    string line;
                    while ((line = infile.ReadLine()) != null)
                    {
                        WriteLine(line);
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

        private static void WriteHelp()
        {
            WriteLine("Avaliable commands: ");
            WriteLine("  delete       - emtpy the contact list");
            WriteLine("  delete /persname/ /surname/ - delete a person");
            WriteLine("  load        - load contact list data from the file address.lis");
            WriteLine("  load /file/ - load contact list data from the file");
            WriteLine("  new        - create new person");
            WriteLine("  new /persname/ /surname/ - create new person with personal name and surname");
            WriteLine("  quit        - quit the program");
            WriteLine("  save         - save contact list data to the file previously loaded");
            WriteLine("  save /file/ - save contact list data to the file");
            WriteLine();

        }
    }
}
