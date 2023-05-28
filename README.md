# MS-Unit Testing

This is Labb 5 in the Course Avancerad .NET at Campus Varberg in Sweden.
I will reuse an previous project called Team Lemon. It was an bank application that we made as our first group project.

## Places of Eventual Errors
I will look at the code that we have written and try to find some functions that are critical for the program to work perfectly.

### Point of Error : 1
I will do a MSUnit test for the function "InternalTransfer" inside of "AccountManagement.cs" to ensure that all transfers within the same user will go smooth and not fail.

Here is a list of things that I will test that can go wrong.
- [ ] Transfer money between two acconts with the same currencies (SE to SE)
- [ ] Transfer money between two acconts with differnt currencies (US to SE)
- [ ] Transfer money between two acconts with differnt currencies (SE to US)

### Point of Error : 2 
For the second MSUnit test I will test "MakeExternalTransfer" inside of "AccountManagement.cs" to ensure that all transfers to other accounts will go smooth and not fail.

Here is a list of things that I will test that can go wrong.
- [ ] Transfer money between two acconts on different users with the same currencies (SE to SE)
- [ ] Transfer money between two acconts on different users with differnt currencies (US to SE)
- [ ] Transfer money between two acconts on different users with differnt currencies (SE to US)

### Point of Error : 3 
For the third MSUnit tes tI will test "ValidatePassword" inside of "AccountManagement.cs" to ensure that the password provided matches with the correct password for that user.
- [ ] Validate with the correct password
- [ ] Test that you cant validate with an incorrect password
