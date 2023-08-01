namespace NewGlobe.ElectiveTraining
{
    public class ElectiveTrainining
    {

        private const int MorningEndTime = 12 * 60; // 12:00 PM
        private const int LunchDuration = 60;
        private const int SharingSessionStartTime = 16 * 60; // 4:00 PM
        private const int SharingSessionEndTime = 17 * 60 + 30; // 5:30 PM

        public List<Track> ScheduleElectiveTraining(List<Session> sessions)
        {
            List<Session> sortedSessions = SortSessionsDescending(sessions);
            List<Track> tracks = new List<Track>();

            while (sortedSessions.Count > 0)
            {
                Track track = new Track();
                int currentTime = 9 * 60; // 9:00 AM

                ScheduleMorningSessions(sortedSessions, track, ref currentTime);
                currentTime += LunchDuration;
                ScheduleAfternoonSessions(sortedSessions, track, ref currentTime);
                track.AddAfternoonSession(new Session { Name = "Sharing Session", Duration = SharingSessionEndTime - currentTime });

                tracks.Add(track);
            }

            return tracks;
        }

        private List<Session> SortSessionsDescending(List<Session> sessions)
        {
            return sessions.OrderByDescending(s => s.Duration).ToList();
        }

        private void ScheduleMorningSessions(List<Session> sessions, Track track, ref int currentTime)
        {
            foreach (Session session in sessions.ToArray()) // Use ToArray to avoid modification exceptions
            {
                if (currentTime + session.Duration <= MorningEndTime)
                {
                    track.AddMorningSession(session);
                    currentTime += session.Duration;
                    sessions.Remove(session);
                }
            }
        }

        private void ScheduleAfternoonSessions(List<Session> sessions, Track track, ref int currentTime)
        {
            foreach (Session session in sessions.ToArray())
            {
                if (currentTime + session.Duration <= SharingSessionStartTime)
                {
                    track.AddAfternoonSession(session);
                    currentTime += session.Duration;
                    sessions.Remove(session);
                }
            }
        }

        public void PrintSchedule(List<Track> tracks)
        {
            for (int i = 0; i < tracks.Count; i++)
            {
                Console.WriteLine("Track " + (i + 1));
                Console.WriteLine("Time\t| Session Name\t| Duration");
                Console.WriteLine("  \t|\t|\t");

                int currentTime = 9 * 60; // 9:00 AM

                foreach (Session session in tracks[i].MorningSessions)
                {
                    Console.WriteLine($"{FormatTime(currentTime)} | {session.Name} | {FormatDuration(session.Duration)}");
                    currentTime += session.Duration;
                }

                Console.WriteLine($"{FormatTime(currentTime)} | Lunch\t|");
                currentTime += 60;

                foreach (Session session in tracks[i].AfternoonSessions)
                {
                    Console.WriteLine($"{FormatTime(currentTime)} | {session.Name} | {FormatDuration(session.Duration)}");
                    currentTime += session.Duration;
                }

                Console.WriteLine($"{FormatTime(currentTime)} | Sharing Session\t|");
                Console.WriteLine();
            }
        }

        private string FormatTime(int minutes)
        {
            int hours = minutes / 60;
            int mins = minutes % 60;
            return $"{hours:D2}:{mins:D2}AM";
        }

        private string FormatDuration(int minutes)
        {
            if (minutes == 5)
                return "lightning";
            else
                return $"{minutes}min";
        }
    }
}
