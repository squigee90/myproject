using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTechnology
{
    class Program
    {
        static void Main(string[] args)
        {
            // Hash Table #1, generate hash value using "person name" in phone book

            // Step 1: Define our phone book data
            Console.WriteLine($"\n### Step 1: Define our phone book data ###");
           
            PhoneBookRecord[] phoneBook = new PhoneBookRecord[10];
            phoneBook[0] = new PhoneBookRecord("jaden Barker", "0412 123 450");
            phoneBook[1] = new PhoneBookRecord("Mell Davies", "0489 321 651");
            phoneBook[2] = new PhoneBookRecord("Phoenix Ellis", "0378 567 892");
            phoneBook[3] = new PhoneBookRecord("Erin Lewis", "0423 134 56A");
            phoneBook[4] = new PhoneBookRecord("Marley Roberts", "0456 789 834");
            phoneBook[5] = new PhoneBookRecord("eli Browning", "0364 365 215");
            phoneBook[6] = new PhoneBookRecord("@lliot Grant", "0417 765 706");
            phoneBook[7] = new PhoneBookRecord("Gabe Mitchell", "0435 432 417");
            phoneBook[8] = new PhoneBookRecord("99ogan Newton", "0398 963 698");
            phoneBook[9] = new PhoneBookRecord("Gail Mcbride", "0421 417 455");

            // Step 2: Check the data in our phone book array
            Console.WriteLine($"\n### Step 2: Check the data in our phone book array ###");
            Console.WriteLine($"\nPhone Book Records Initialized ({phoneBook.Length} records)");
            foreach (PhoneBookRecord record in phoneBook)
            {
                Console.WriteLine($"\t{record.Name} \t {record.PhoneNumber}");
            }
            Console.WriteLine($"End Of Phone Book Data\n");
            Console.ReadKey();

            // Expected hash table structure "firstLetterHashTable"  first ASCI letter into buckets
            // 0  : record -> record
            // 1  : null
            // 2  : record -> record -> record
            // 3  : record
            // 4  : null
            // ...
            // ...
            // 25 : record -> record -> record -> record -> record -> record -> record
            // 26 : record -> record
            //

            // Step 3: Define the hash table of List, called firstLetterHashTable
            Console.WriteLine($"\n### Step 3: Define the hash table of List, called firstLetterHashTable ###");
            List<PhoneBookRecord>[] firstLetterHashTable = new List<PhoneBookRecord>[27];   //new list of 27 elements
            // In hash talbe, for each table element, initialize the record list
            for (int i = 0; i < firstLetterHashTable.Length; i++)
            {
                firstLetterHashTable[i] = new List<PhoneBookRecord>();
            }

            // Step 4: Walk through the original phone book, put the each phone book element into hash table
            Console.WriteLine($"\n### Step 4: Walk through the original phone book, put the each phone book element into hash table ###");
            foreach (PhoneBookRecord record in phoneBook)
            {
                // call hash function to gerenate hash value
                int index = HashFirstLetter(record.Name);
                // the hash code is also the index of hash table. append the record to the corresponding list
                firstLetterHashTable[index].Add(record);
            }

            // Step 5: Dump table the verify the result.
            Console.WriteLine($"\n### Step 5: Dump table the verify the result ###");
            Console.WriteLine($"\nDump the hash table \"firstLetterHashTable\" to verify the results\n");
            for (int i = 0; i < firstLetterHashTable.Length; i++)
            {
                Console.Write($"[{i}]:\t");
                foreach (PhoneBookRecord record in firstLetterHashTable[i])
                {
                    Console.Write($"[{record.Name} / {record.PhoneNumber}] -> ");
                }
                Console.Write("\n");
            }

            // Step 6: Search hash table for record with given name.
            Console.WriteLine($"\n### Step 6: Search hash table for record with given name. ###");
            string nameToSearch = "Marley Roberts";        // the name to looking for
            PhoneBookRecord recordFound = null;            // used to store the record when found
            // 6.1, identify the index of firstLetterHashTable using hash function
            int hashIndex = HashFirstLetter(nameToSearch);

            // 6.2, now firstLetterHashTable[hashIndex] is the list
            //      if there is a "Marley Roberts", it has to be in this list
            //      lets loop the list and looking for the record
            foreach (PhoneBookRecord record in firstLetterHashTable[hashIndex])
            {
                if (record.Name == nameToSearch)
                {
                    // we hit a match
                    recordFound = record;
                }
            }

            // 6.3 now check wether we hit a match by examining if recordFound == null
            if (recordFound != null)
            {
                Console.WriteLine($"\nwe found that \"{nameToSearch}\" is in our phone book, and the number is {recordFound.PhoneNumber}\n");
            }
            else
            {
                Console.WriteLine($"\nWe did not find any {nameToSearch} in our phone book\n");
            }

            //**********************************************************************
            // Hash Table #2, generate hash value using "phone number" in phone book
            //**********************************************************************

            // A challenge for you
            // Start coding here
            // ...

            return;

        }

        // HashFirstLetter
        // Description: 
        //    map any string to 0~26 by examining the first character of the string, with the following rules
        //    1: a~z => 1~26
        //    2: A~Z => 1~26
        //    3. Any other characters in the ascii table => 0
        //
        //  mapping table
        // +------------------------------------------------------------------------------------------------------------------------+
        // |(anything else)  aA  bB  cC  dD  eE  fF  gG  hH  iI  jJ  kK  lL  mM  nN  oO  pP  qQ  rR  sS  tT  uU  vV  wW  xX  yY  zZ |
        // |        0        01  02  03  04  05  06  07  08  09  10  11  12  13  14  15  16  17  18  19  20  21  22  23  24  25  26 |
        // +------------------------------------------------------------------------------------------------------------------------+
        // Parameters:
        //    name: can be any string
        // Return:
        //    int value: 1~16 for a~z/A~Z; 0 for any other char
        static int HashFirstLetter(string name)
        {
            int index = 0;

            // extract the first letter from "name" string, and transform it to uppcase (to avoid dealing with a~z)
            char firstChar = Char.ToUpper(name[0]);

            // in ASCII table A~Z map to number 65~90. http://www.asciitable.com/
            // "(int)firstChar >= 65 && (int)firstChar <= 90" means to check if firstChar is in between A~Z
            if ((int)firstChar >= 65 && (int)firstChar <= 90)
            {
                // we use "(int)firstChar - 64" to map A~Z to 1~26
                index = (int)firstChar - 64;
            }
            else
            {
                // map all non A~Z/a~z char to 0
                index = 0;
            }
            return index;
        }

        // HashLastDigit
        // Description: 
        //    map any phone number string to "0"~"9" by examining the last digit of the string of number, with the following rules
        //        1. last digit is "0"~"9" => return 0~9
        //        2. last digit is not "0"~"9" => return 10
        // Parameters:
        //    phone: can be any string of numbers
        // Return:
        //    int value: 0~9 for "0"~"9"; 10 for any other char
        static int HashLastDigit(string phone)
        {
            int index = 10;

            // extract the last letter from "name" string, and transform it to uppcase (to avoid dealing with a~z)
            char lasterNumber = phone[phone.Length - 1];

            // in ASCII table "0"~"9" map to number 48~57. http://www.asciitable.com/
            // (int)lasterNumber >= 48 && (int)lasterNumber <= 57 mean to check if firstChar is in between A~Z
            if ((int)lasterNumber >= 48 && (int)lasterNumber <= 57)
            {
                // we use "(int)letter - 47" to map "0"~"9" to 0~9
                index = (int)lasterNumber - 47;
            }
            else
            {
                // map all non "0"/"9" char to 10
                index = 10;
            }
            return index;
        }
    }

    class PhoneBookRecord
    {
        public string Name;
        public string PhoneNumber;

        public PhoneBookRecord()
        {
            this.Name = "";
            this.PhoneNumber = "";
        }
        public PhoneBookRecord(string name, string phone)
        {
            this.Name = name;
            this.PhoneNumber = phone;
        }
    }
}

