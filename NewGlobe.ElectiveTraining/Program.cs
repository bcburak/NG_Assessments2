// See https://aka.ms/new-console-template for more information


namespace NewGlobe.ElectiveTraining
{
    public class Program
    {
        static void Main(string[] args)
        {

            ElectiveTrainining electiveTrainining = new ElectiveTrainining();
            List<Session> sessions = new List<Session>
            {
                // Add all the sessions with their names and durations here
                new Session { Name = "Organising Parents for Academy Improvements", Duration = 60 },
                new Session { Name = "Teaching Innovations in the Pipeline", Duration = 45 },
                // Add more sessions...
            };

            List<Track> tracks = electiveTrainining.ScheduleElectiveTraining(sessions);

            electiveTrainining.PrintSchedule(tracks);
        }

    }
}