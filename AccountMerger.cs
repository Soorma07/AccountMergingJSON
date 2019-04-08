using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace AccountMerging
{
    // Account Merger reads a JSON file passed in as a console argument and merge the accounts by email
    internal class AccountMerger
    {
        static void Main(string[] args)
        {
            int numberOfArguments = args.Length;
            if (numberOfArguments == 1)
            {
                string jsonFile = args[0];
                
                List<Account> accounts = ReadJSONFile(jsonFile);                

                if (accounts != null)
                {
                    List<Person> users = new List<Person>(accounts.Count);

                    foreach (var account in accounts)
                    {
                        foreach (var email in account.emails)
                        {
                            var matchingUsers = GetMatchingUsers(email, users);
                            if (!matchingUsers.Any())
                            {
                                var person = new Person()
                                {
                                    applications = new List<string>(){account.application},
                                    emails = new List<string>(account.emails),
                                    name = account.name
                                };

                                users.Add(person);
                            }
                            else
                            {
                                foreach (var user in matchingUsers)
                                {
                                    MergeAccount(user, account);
                                }
                            }
                        }
                    }

                    WriteJSONFile(users);
                }
                else
                {
                    Console.WriteLine("There were no accounts found or there was a problem reading the accounts.");
                }
            }
            else
            {
                Console.WriteLine("AccountMerging requires one json file argument. Please try again.");
            }
        }

        // reads the accounts in from the JSON file
        private static List<Account> ReadJSONFile(string jsonFile)
        {
            List<Account> accounts = null;

            try
                {
                    using (StreamReader streamReader = new StreamReader(jsonFile))
                    {
                        string jsonContent = streamReader.ReadToEnd();
                        accounts = JsonConvert.DeserializeObject<List<Account>>(jsonContent);
                    }
                }
                catch(IOException ioException)
                {
                    Console.WriteLine("There was a problem. IOException: " + ioException.Message);
                }
                catch(UnauthorizedAccessException e)
                {
                    Console.WriteLine("There was a problem. UnauthorizedAccessException: " + e.Message);
                }

            return accounts;
        }

        // outputs the merged accounts in JSON to the console
        private static void WriteJSONFile(List<Person> users)
        {    
            string jsonOutput = JsonConvert.SerializeObject(users);
            Console.WriteLine(jsonOutput);
        }

        // gets the matching person by emailToCompare
        private static List<Person> GetMatchingUsers(string emailToCompare, List<Person> users)
        {
            return users.Where(
                user => user.emails.Any(
                userEmail => userEmail.Equals(emailToCompare, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        // merges the accountToMerge for a person by email
        private static void MergeAccount(Person user, Account accountToMerge)
        {
            if (!user.applications.Contains(accountToMerge.application))
            {
                user.applications.Add(accountToMerge.application);
            }

            foreach (var email in accountToMerge.emails)
            {
                if (!user.emails.Contains(email))
                {
                    user.emails.Add(email);
                }
            }
        }
    }
}
