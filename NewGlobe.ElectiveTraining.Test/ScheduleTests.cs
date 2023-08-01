using NewGlobe.ElectiveTraining;

public class ScheduleTests
{
    [Fact]
    public void TestScheduleElectiveTraining()
    {
        List<Session> sessions = new List<Session>
        {
            new Session { Name = "Organising Parents for Academy Improvements", Duration = 60 },
            new Session { Name = "Teaching Innovations in the Pipeline", Duration = 45 },
            // Add more sessions...
        };

        ElectiveTrainining schedule = new ElectiveTrainining();
        List<Track> actualTracks = schedule.ScheduleElectiveTraining(sessions);

        List<List<string>> expectedTracks = new List<List<string>>
        {
            // Add the expected tracks here based on the updated test output
            new List<string>
            {
                "09:00AM | Organising Parents for Academy Improvements | 60min",
                "10:00AM | Teaching Innovations in the Pipeline | 45min",
                "10:45AM | ...",
                // Add more morning sessions...
                "12:00PM | Lunch |",
                "01:00PM | ...",
                // Add more afternoon sessions...
                "05:00PM | Sharing Session |"
            },
            // Add more tracks...
        };

        AssertTracksEqual(expectedTracks, actualTracks);
    }

    // Add more test methods for other scenarios

    private void AssertTracksEqual(List<List<string>> expectedTracks, List<Track> actualTracks)
    {
        Assert.Equal(expectedTracks.Count, actualTracks.Count);

        for (int i = 0; i < expectedTracks.Count; i++)
        {
            List<string> actualMorningSessions = actualTracks[i].MorningSessions.ConvertAll(s => FormatSessionString(s, 9, 0));
            List<string> actualAfternoonSessions = actualTracks[i].AfternoonSessions.ConvertAll(s => FormatSessionString(s, 13, 0));

            if (expectedTracks[i].Contains("Lunch"))
            {
                int lunchIndex = expectedTracks[i].IndexOf("Lunch");
                List<string> expectedMorningSessions = expectedTracks[i].GetRange(0, lunchIndex);
                List<string> expectedAfternoonSessions = expectedTracks[i].GetRange(lunchIndex + 1, expectedTracks[i].Count - lunchIndex - 1);

                Assert.Equal(expectedMorningSessions, actualMorningSessions);
                Assert.Equal(expectedAfternoonSessions, actualAfternoonSessions);
            }

        }
    }

    private string FormatSessionString(Session session, int startHour, int startMinute)
    {
        int hours = startHour + (startMinute + session.Duration) / 60;
        int mins = (startMinute + session.Duration) % 60;
        return $"{FormatTime(startHour, startMinute)} | {session.Name} | {FormatDuration(session.Duration)}" +
               $" - {FormatTime(hours, mins)}";
    }

    private string FormatTime(int hours, int minutes)
    {
        return $"{hours:D2}:{minutes:D2}AM";
    }

    private string FormatDuration(int minutes)
    {
        if (minutes == 5)
            return "lightning";
        else
            return $"{minutes}min";
    }
}

