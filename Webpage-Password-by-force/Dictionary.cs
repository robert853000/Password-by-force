using System.Windows.Controls;

namespace Webpage_Password_by_force
{
    class Dictionary
    {
        public string[] allUsers;
        public string[] allPasswords;
        public Dictionary()
        {

        }

        public void loadFromTextBox(TextBox userList, TextBox passwordList)
        {
            allUsers = userList.Text.Split('\n');
            allPasswords = passwordList.Text.Split('\n');
            int i;
            int l;
            for (i = 0, l = allUsers.Length; i < l; i++)
            {
                allUsers[i] = allUsers[i].Trim();
            }
            for (i = 0, l = allPasswords.Length; i < l; i++)
            {
                allPasswords[i] = allPasswords[i].Trim();
            }
        }
    }
}