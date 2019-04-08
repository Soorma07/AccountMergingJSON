# AccountMergingJSON
Merging accounts in JSON.
Takes a JSON file with accounts as a command line argument input and merges the accounts.
Accounts are considered unique per email address.
If there is a different name for the same account, the first name is used and the second is dropped.

sample runtime:
dotnet run ./accounts.json
