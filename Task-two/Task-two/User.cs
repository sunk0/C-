
using System.Collections.Generic;
namespace Task_two
{
    class User
    {
        public string nickName;
        public string age;
        public List<string> publications;
        public bool isAuthorized;
        public bool isBlocked;
        public bool isAdmin;
        public User(string nickName, string age)
        {
            this.nickName = nickName;
            this.age = age;
            publications = new List<string>();
            isAuthorized = false;
            isBlocked = false;
            isAdmin = false;
        }
        public void addPublication(string publication)
        {
            publications.Add(publication);
        }
        public void removePublication(string publication)
        {
            publications.Remove(publication);
        }
        public void changeNickname(string nickname)
        {
            nickName = nickname;
        }
    }
}
