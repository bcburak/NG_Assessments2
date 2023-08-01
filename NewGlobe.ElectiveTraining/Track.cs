namespace NewGlobe.ElectiveTraining
{
    public class Track
    {
        public List<Session> MorningSessions { get; private set; }
        public List<Session> AfternoonSessions { get; private set; }

        public Track()
        {
            MorningSessions = new List<Session>();
            AfternoonSessions = new List<Session>();
        }

        public void AddMorningSession(Session session)
        {
            MorningSessions.Add(session);
        }

        public void AddAfternoonSession(Session session)
        {
            AfternoonSessions.Add(session);
        }
    }
}
