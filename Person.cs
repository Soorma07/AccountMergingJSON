using System.Collections.Generic;

namespace AccountMerging
{
    // A person can have accounts with multiple emails and multiple applications.
    public class Person
    {
        public List<string> applications;
        public List<string> emails;
        public string name;
    }
}
