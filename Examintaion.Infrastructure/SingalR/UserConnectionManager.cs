namespace Examintaion.Infrastructure.SingalR
{
    public static class UserConnectionManager
    {
        public static readonly Dictionary<string, HashSet<string>> _connection = [];

        public static void AddConnection(string userId, string connectionId)
        {
            if (!_connection.ContainsKey(userId))
            {
                _connection[userId] = [];
            }
            _connection[userId].Add(connectionId);
        }

        public static void RemoveConnection(string userId)
        {
            if (_connection.ContainsKey(userId))
            {
                _connection.Remove(userId);
            }
        }

        public static IReadOnlyCollection<string> GetConnections(string userId)
        {
            if (_connection.ContainsKey(userId))
            {
                return _connection[userId].ToList().AsReadOnly();
            }
            return Array.Empty<string>();
        }
    }
}
