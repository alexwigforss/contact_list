/*

• (DOIN) bygg på kod så att du får innehåll i saknad funktionalitet
new,                
    refac addPerson ur streamreader till statisk
new /person/,       done
list,               done
list /person/,      done
delete,             <--
delete /person/,
save /file/,
och quit o.s.v.!

• hantera eventuellt felaktiga argument med try-catch-satser, bli av med de förkättrade gröna
ormarna som säger "Dereference of a possibly null reference."
 */
using static System.Console;
using System.Linq;

namespace dtp6_contacts
{
    class MainClass
    {
        static List<Person> contactList = new List<Person>();
        class Person
        {
            private string persname, surname, birthdate;
            private List<string> phoneList;
            private List<string> addressList;
            public string Persname { get => persname; set => persname = value; }
            public string Surname { get => surname; set => surname = value; }
            public string Birthdate { get => birthdate; set => birthdate = value; }

            public Person(string Persname, string Surname, string Birthdate = "unknown")
            {
                persname = Persname;
                surname = Surname;
                phoneList = new List<string>();
                addressList = new List<string>();
                birthdate = Birthdate;
            }
            public string GetPhone() => string.Join(';', phoneList);
            public string GetAddress() => string.Join(';', addressList);
            public void AddPhone(string phoneNr) { phoneList.Add(phoneNr); }
            public void AddAddress(string streetAddress) { addressList.Add(streetAddress); }
            public override string ToString() => $"{persname} | {surname} | {GetPhone()} | {GetAddress()} | {birthdate}";
        }
        public static void Main(string[] args)
        {
            string lastFileName = "address.lis";
            WriteLine("Hello and welcome to the contact list");
            WriteHelp();
            string[] commandLine = new string[3] { null!, null!, null! }; // KomIhåg

            do  // MAIN_LOOP där användaren gör sina val
            {
                Write($"> ");
                commandLine = ReadLine().Split(' '); // TBD This Annoying Stinky Filthy Snake :/
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
                    else if (commandLine.Length < 3) { NewPersonPrompt(commandLine[1]); }
                    else if (commandLine.Length < 4) { NewPersonPrompt(commandLine[1], commandLine[2]); }
                }
                else if (commandLine[0] == "list")
                {
                    if (commandLine.Length < 2) { foreach (Person contact in contactList)
                        { WriteLine(contact.ToString()); } }
                    else if (commandLine.Length < 3)
                    {
                        // DESC: Jämför commandLine[1] i allas för och efternamn
                        foreach (Person c in contactList.Where(c => c.Persname.ToLower() == commandLine[1].ToLower())) { WriteLine(c.ToString()); }
                        foreach (Person c in contactList.Where(c => c.Surname.ToLower() == commandLine[1].ToLower())) { WriteLine(c.ToString()); }
                        // TBD: Faktorisera ut de här ↑ eländiga raderna till extern method.
                    }   // TBD: Den kan man sen återanvända här 
                    else if (commandLine.Length < 4)
                    {
                        WriteLine("Not yet implemented: list /pers /sur");
                    }
                }
                else if (commandLine[0] == "help") { WriteHelp(); }
                else { WriteLine($"Unknown command: '{commandLine[0]}'"); }
            } while ((commandLine[0] != "quit") && (commandLine[0] != "exit"));

        }
        /// <summary>
        /// Load Adresses from File Provided
        /// </summary>
        /// <param name="lastFileName"></param>
        // FIXED:  När man laddar rad med flera tele/adresser sparas bara den första.
        static void LoadAddressList(string lastFileName)
        {
            using (StreamReader infile = new StreamReader(lastFileName))
            {
                string line;
                while ((line = infile.ReadLine()) != null)
                {
                    string[] attrs = line.Split('|');
                    AddPerson(attrs);
                }
            }
        }

        private static void AddPerson(string[] attrs)
        {
            Person person = new Person(attrs[0], attrs[1], attrs[4]);
            foreach (string ph in attrs[2].Split(';')) { person.AddPhone(ph); }
            foreach (string ph in attrs[3].Split(';')) { person.AddAddress(ph); }
            contactList.Add(person);
        }

        /// <summary>
        /// Promt to get Data for Person To add
        /// </summary>
        // TBD: Add optional params for persname & surname.
        //      so we can jump over ReadLine()s we dont need.
        private static void NewPersonPrompt(string? persname = null, string? surname = null)
        {
            if (persname == null) // TBD More checks
            {
                Write("personal name: ");
                persname = ReadLine();
            }
            while (surname == null) // TBD More checks
            {
                Write("surname: ");
                surname = ReadLine();
            }
            Write("phone: ");
            string? phone = ReadLine();
            Write("address: ");
            string? adress = ReadLine();
            Write("birthday: ");
            string? birthday = ReadLine();
            string[] attrs = new string[] { persname, surname, phone, adress, birthday };
            AddPerson(attrs);
            // NYI: Actually adding the persson

        }
        /// <summary>
        /// Save Adresses from contactList to Filename Provided
        /// </summary>
        /// <param name="lastFileName"></param>
        private static void SaveToFile(string lastFileName)
        {
            //using (StreamWriter outfile = new StreamWriter(lastFileName))
            //string str = "";
            using (StreamWriter outfile = new StreamWriter("output.lst"))
            {
                foreach (Person p in contactList)
                {
                    if (p != null)
                        // str += $"{p.Persname}|{p.Surname}|";
                        outfile.WriteLine($"{p.Persname}|{p.Surname}|{p.GetPhone()}|{p.GetAddress()}|{p.Birthdate}");
                    // FIXME: birthdate
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
/* FAS 3 Donelist
• (DID) byt ut arrayer mot List<T>, för att sätta in ny person använd metoden Add, för att ta bort person använd RemoveAt! 
DID: implementerat listan i Save / Load & List
FIXED: Birthday

• (DID) gör om attributen phone och address, och implementera detta i setters och getters för dessa attribut!
• (DID) gör sedan om dessa till listor
 */
// FAS 2 Donelist
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
// DID: 11. bygg smarta konstruktorer, setters och getters (kanske även properties) som det passar
// ändamålet, men i synnerhet setters och getters för attributen phone och address,
// gör a. kompileringar/körningar/tester, b. stage/commit/push som det passar!
// DIDFIX:  Birthdate default,
