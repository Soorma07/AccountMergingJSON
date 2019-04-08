using System.Collections.Generic;

namespace AccountMerging
{
    // An account for an application.
    // A user can have multiple emails.
    public class Account
    {
        public string application;
        public List<string> emails;
        public string name;
    }
}
