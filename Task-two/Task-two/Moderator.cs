
namespace Task_two
{
    class Moderator : User
    {
        public Moderator(string nickName, string age) : base(nickName, age)
        {
            isAuthorized = true;
        }
    }
}
