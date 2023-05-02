using System.Collections.Generic;

namespace NewsAssignment.Interfaces
{
    public interface IUserConnectionManager
    {
        void KeepUserConnection(string role, string connectionId);
        void RemoveUserConnection(string connectionId);
        List<string> GetUserConnections(string role);
    }
}
