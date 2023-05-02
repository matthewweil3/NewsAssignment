using Microsoft.CodeAnalysis.Elfie.Serialization;
using System.Collections.Generic;

namespace NewsAssignment.Interfaces
{
    public class UserConnectionManager : IUserConnectionManager
    {

        private static Dictionary<string, List<string>> userConnectionMap = new Dictionary<string, List<string>>();
        private static string userConnectionMapLocker = string.Empty;
        public void KeepUserConnection(string role, string connectionId)
        {
            lock (userConnectionMapLocker)
            {
                if (!userConnectionMap.ContainsKey(role))
                {
                    userConnectionMap[role] = new List<string>();
                }
                userConnectionMap[role].Add(connectionId);
            }
        }
        public void RemoveUserConnection(string connectionId)
        {
            //This method will remove the connectionId of user
            lock (userConnectionMapLocker)
            {
                foreach (var role in userConnectionMap.Keys)
                {
                    if (userConnectionMap.ContainsKey(role))
                    {
                        if (userConnectionMap[role].Contains(connectionId))
                        {
                            userConnectionMap[role].Remove(connectionId);
                            break;
                        }
                    }
                }
            }
        }
        public List<string> GetUserConnections(string role)
        {
            var conn = new List<string>();
            lock (userConnectionMapLocker)
            {
                conn = userConnectionMap[role];
            }
            return conn;
        }
    }
}
