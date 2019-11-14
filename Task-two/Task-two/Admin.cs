
namespace Task_two
{
    class Admin : User
    {
        public Admin(string nickName, string age) : base(nickName, age)
        {
            isAdmin = true;
            isAuthorized = true;
        }
    }
}
