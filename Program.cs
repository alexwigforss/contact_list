// DID_TASK: 6. bryt ut static-metoder när du ser kodrepetitioner,
// för varje ny static-metod a. kompilera/kör/testa, b. stage/commit/push!
// DID: Bryt ut Streamreader till statisk
// DID: Bryt ut hjälpustrift till statisk
// DID: Bytt ';' mot '|' i streamWritern
// DID: Brutit ut även stresmWritern (även om den inte repeterar sig kommer den behöva göra det när stringsplit implementeras)
// DID: Brutit ut prompt för new person.

// DIDTASK: 7. ta bort onödiga spårutskrifter,
// gör en enda a. kompilering/körning/test, b. stage/commit/push!
// DID: Tagit bort WriteLine Från LoadFromfile
// NOTE: NYI raderna behåller jag, har svårt att se dem som onödiga då
//  dem påminner mig om vilken funktionalitet som ska in.

// DID_TASK: 8. kommentera för att begripa koden, kommentera gärna alla metoder (static or dynamic) som
// du känner för, gör en enda a. kompilering/körning/test, b. stage/commit/push!

// DID_TASK: 9. om du hittar variabler som du tycker är svårbegripliga, döp om dem till något begripligt,
// för varje variabeländring gör kompilera, gör en enda slutlig a. kompilering/körning/test,
// och sedan b. stage/commit/push!

// DID_TASK: 10. plocka ut funktionaliteterna i if-else-kedjan genom att göra dem till metoder, som det passar
// ändamålet,
// för varje utbrytning a. kompilera/kör/testa, b. stage/commit/push!

// DID: Byt contactList Array mot Lista

// DOIN: 11. bygg smarta konstruktorer, setters och getters (kanske även properties) som det passar
// ändamålet, men i synnerhet setters och getters för attributen phone och address,
// gör a. kompileringar/körningar/tester, b. stage/commit/push som det passar!

// FIXME:  Birthdate default,
using static System.Console;
namespace dtp6_contacts
{
    class MainClass
    {
        static List<Person> contactList = new List<Person>();
        class Person
        {
            private string persname, surname, phone, address, birthdate;
            public string Persname { get => persname; set => persname = value; }
            public string Surname { get => surname; set => surname = value; }
            public string Phone { get => phone; set => phone = value; }
            public string Address { get => address; set => address = value; }
            public string Birthdate { get => birthdate; set => birthdate = value; }

            public Person()
            {
                persname = "";
                surname = "";
                phone = "";
                address = "";
                birthdate = "";
            }
            public Person(string Persname, string Surname, string Phone, string Address, string Birthdate = "unknown")
            {
                persname = Persname;
                surname = Surname;
                phone = Phone;
                address = Address;
                birthdate = Birthdate;
            }
            public string GetPhone() => phone;
            public string GetAddress() => address;
            public void SetPhone(string phoneNr) { phone = phoneNr; }
            public void SetAddress(string streetAddress) { address = streetAddress; }
            public override string ToString() => $"{persname} {surname} {phone} {address}";
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            string[] commandLine;
            WriteLine("Hello and welcome to the contact list");
            WriteHelp();
            do  // MAIN_LOOP där användaren gör sina val
            {
                Write($"> ");
                commandLine = ReadLine().Split(' ');
                if (commandLine[0] == "quit")
                { WriteLine("Not yet implemented: safe quit"); }
                else if (commandLine[0] == "load")
                {
                    if (commandLine.Length < 2) { lastFileName = "address.lis"; LoadAddressList(lastFileName); }
                    else { lastFileName = commandLine[1]; LoadAddressList(lastFileName); }
                }
                else if (commandLine[0] == "save")
                {
                    if (commandLine.Length < 2) { SaveToFile(lastFileName); }
                    else { WriteLine("Not yet implemented: save /file/"); } // Om kommandot är fler än ett ord. TBD
                }
                else if (commandLine[0] == "new")
                {
                    if (commandLine.Length < 2) { NewPersonPrompt(); }
                    else { WriteLine("Not yet implemented: new /person/"); } // Om kommandot är fler än ett ord. TBD
                }
                else if (commandLine[0] == "list")
                {
                    // Array.ForEach(contactList, cl => cl.ToString());
                    foreach (var contact in contactList)
                    {
                        WriteLine(contact.ToString());
                    }
                }
                else if (commandLine[0] == "help") { WriteHelp(); }
                else { WriteLine($"Unknown command: '{commandLine[0]}'"); }
            } while (commandLine[0] != "quit");

        }
        /// <summary>
        /// Load Adresses from File Provided
        /// </summary>
        /// <param name="lastFileName"></param>
        // FIXME:  När man laddar rad med flera tele/adresser sparas bara den första.
        static void LoadAddressList(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    string[] attrs = line.Split('|');
                    Person person = new Person();
                    person.Persname = attrs[0];
                    person.Surname = attrs[1];
                    string[] phones = attrs[2].Split(';');
                    person.SetPhone(phones[0]);
                    string[] addresses = attrs[3].Split(';');
                    person.SetAddress(addresses[0]);
                    contactList.Add(person);
                }
            }
        }
        /// <summary>
        /// Promt to get Data for Person To add
        /// </summary>
        // TBD: Add optional params for persname & surname.
        private static void NewPersonPrompt()
        {
            Write("personal name: ");
            string persname = ReadLine();
            Write("surname: ");
            string surname = ReadLine();
            Write("phone: ");
            string phone = ReadLine();
            // NYI: Actually adding the persson
        }
        /// <summary>
        /// Save Adresses from contactList to Filename Provided
        /// </summary>
        /// <param name="lastFileName"></param>
        private static void SaveToFile(string lastFileName)
        {
            //using (StreamWriter outfile = new StreamWriter(lastFileName))
            using (StreamWriter outfile = new StreamWriter("output.lst"))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        outfile.WriteLine($"{p.Persname}|{p.Surname}|{p.GetPhone}|{p.GetAddress}|{p.Birthdate}");
                        // FIXME: Ta bort birthdate
                        // FIXME: 
                }
            }
        }
        /// <summary>
        /// Writes All Allowed Commands
        /// </summary>
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
