using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Task_two
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Dictionary<string,User> users = new Dictionary<string, User>();
            User pesho = new Admin("adm", "50");
            users.Add("adm", pesho);
            List<string> allPublications = new List<string>();
            string line = Console.ReadLine();
            while (line != "quit")
            {
                if (line.Equals("info"))
                {
                    int count = 0;
                    int blockedUsers = 0;
                    foreach (var user in users.Keys)
                    {
                        count++;
                    }
                    Console.WriteLine("There are " + count + " users");
                    foreach (var user in users.Values)
                    {
                        if (user.isAdmin)
                        {
                            Console.WriteLine(user.nickName + " -Administrator, " + user.publications.Count + " posts.");
                        }
                        else if (user.isAuthorized)
                        {
                            Console.WriteLine(user.nickName + " -Moderator, " + user.publications.Count + " posts.");
                        }
                        else
                        {
                            Console.WriteLine(user.nickName + " -User, " + user.publications.Count + " posts.");
                        }
                        if (user.isBlocked)
                        {
                            blockedUsers++;
                        }

                    }
                    if (blockedUsers >= 1)
                    {
                        Console.WriteLine("There are " +blockedUsers + " blocked users");
                    }
                    else
                    {
                        Console.WriteLine("There aren't any blocked users ");
                    }


                    var maxKey = users.Aggregate((l, r) => int.Parse(l.Value.age) > int.Parse(r.Value.age) ? l : r).Key;
                    var minKey = users.Aggregate((l, r) => int.Parse(l.Value.age) < int.Parse(r.Value.age) ? l : r).Key;
                    var maxValue = users[maxKey].age;
                    var minValue = users[minKey].age;
                    Console.WriteLine("oldest " + maxKey + " " + maxValue);
                    Console.WriteLine("youngest " + minKey + " " + minValue);
                    goto again;
                }
                string[] tokens;
                string name;
                string action;
                try
                {
                     tokens = line.Split(" ");
                     name = tokens[0];
                     action = tokens[1];
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unknown command!");
                    goto again;
                }
                if (action.Equals("rename"))
                {
                    string subject = stringBuilder.AppendJoin(" ", tokens.Skip(2)).ToString();
                    if (users.ContainsKey(subject))
                    {
                        Console.WriteLine("Username already exists");
                    }
                    else
                    {
                        var current = users[name];
                        users[name].changeNickname(subject);
                        users.Remove(name);
                        users.Add(subject,current);

                        Console.WriteLine(name + " is know as " + subject);
                    }
                    
                    goto again;
                }
                if (users.ContainsKey(name) && users[name].isAuthorized && users[name].isAdmin)
                {
                    string subject = stringBuilder.AppendJoin(" ", tokens.Skip(2)).ToString();
                    string[] splittedSubject;
                    string subjectName;
                    string subjectAge;
                    if (action.Equals("add_user"))
                    {
                        try
                        {
                             splittedSubject = subject.Split(" ");
                             subjectName = splittedSubject[0];
                             subjectAge = splittedSubject[1];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Unknown command!");
                            goto again;
                        }

                        if (users.ContainsKey(subjectName))
                        {
                            Console.WriteLine("User with that name already exists");
                        }
                        else
                        {
                            User user = new User(subjectName, subjectAge);
                            users.Add(subjectName, user);
                            Console.WriteLine("User " + subjectName + " has been added");
                        }
                    }
                    else if (action.Equals("add_moderator"))
                    {
                        try
                        {
                            splittedSubject = subject.Split(" ");
                            subjectName = splittedSubject[0];
                            subjectAge = splittedSubject[1];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Unknown command!");
                            goto again;
                        }
                        User moderator = new Moderator(subjectName, subjectAge);
                        users.Add(subjectName, moderator);
                        Console.WriteLine("Moderator " + subjectName + " has been added");
                    }
                    else if (action.Equals("remove_user"))
                    {
                        try
                        {
                            splittedSubject = subject.Split(" ");
                            subjectName = splittedSubject[0];
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Unknown command!");
                            goto again;
                        }
                        users.Remove(subjectName);
                        Console.WriteLine("User " + subjectName + " has been removed");
                    }
                    stringBuilder.Clear();
                }
                 if (users.ContainsKey(name) && users[name].isAuthorized)
                {
                    string subject = stringBuilder.AppendJoin(" ", tokens.Skip(2)).ToString();
                    if (action.Equals("block"))
                    {
                        try
                        {
                            var splittedSubject = subject.Split(" ");
                            var subjectName = splittedSubject[0];
                            users[subjectName].isBlocked = true;
                            Console.WriteLine(subjectName + " has been blocked");
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("No such user");
                        }
                      
                    }
                    else if (action.Equals("unblock"))
                    {
                        var splittedSubject = subject.Split(" ");
                        var subjectName = splittedSubject[0];
                        users[subjectName].isBlocked = false;
                        Console.WriteLine(subjectName + " has been unblocked");
                    }
                  
                    stringBuilder.Clear();
                }
                 if (users.ContainsKey(name) && !users[name].isBlocked)
                 {
                     string subject = stringBuilder.AppendJoin(" ", tokens.Skip(2)).ToString();
                    if (action.Equals("post"))
                     {
                         users[name].addPublication(subject);
                         allPublications.Add(subject);
                         Console.WriteLine(users[name].nickName + " has posted " + subject);
                     }
                    else if (action.Equals("remove_post"))
                    {
                        if (users[name].isAuthorized)
                        {
                            int currentPost = int.Parse(subject);
                            var current = allPublications[currentPost];
                            allPublications.RemoveAt(currentPost);
                            foreach (var user in users.Values)
                            {
                                user.removePublication(current);
                            }
                            Console.WriteLine(users[name].nickName + " has removed post " + currentPost);
                        }
                        else if (users[name].nickName.Equals(name))
                        {
                            
                            try
                            {
                                int currentPost = int.Parse(subject);
                                users[name].publications.RemoveAt(currentPost);
                                allPublications.RemoveAt(currentPost);
                                Console.WriteLine(users[name].nickName + " has removed post " + currentPost);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("No such publication or unauthorized");
                                goto again;
                            }
                        }
                    }
                    stringBuilder.Clear();
                }
                 else if (users[name].isBlocked)
                 {
                     Console.WriteLine("Post not created user blocked");
                 }
                 if (users.ContainsKey(name))
                 {
                     string subject = stringBuilder.AppendJoin(" ", tokens.Skip(2)).ToString();
                     
                    if (action.Equals("view_post"))
                     {
                         try
                         {
                             int current = int.Parse(subject);
                            Console.WriteLine(allPublications[current]);
                         }
                         catch (Exception e)
                         {
                             Console.WriteLine("No such post");
                         }
                     }
                    else if (action.Equals("view_all_posts"))
                    {
                        try
                        {
                            users[subject].publications.ForEach(c => Console.WriteLine(c));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("No such user");
                        }
                    }
                 }
                 again:
                stringBuilder.Clear();
                line = Console.ReadLine();
            }
        }
    }
}
