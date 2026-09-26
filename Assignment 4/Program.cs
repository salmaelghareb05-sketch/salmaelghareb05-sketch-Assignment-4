using System.Security.Cryptography.X509Certificates;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text;
using BenchmarkDotNet.Running;

namespace Assignment_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] sessionNames =
           {
              "C# Basics",
              "Arrays",
              "Functions",
              "Date and Time",
              "Exception Handling"
           };

            DateTime[] sessionDates =
            {
            new DateTime(2026, 9, 10, 18, 0, 0),
            new DateTime(2026, 9, 13, 18, 0, 0),
            new DateTime(2026, 9, 17, 18, 0, 0),
            new DateTime(2026, 9, 20, 18, 0, 0),
            new DateTime(2026, 9, 24, 18, 0, 0)
            };

            int[] sessionDurations =
            {
            180,
            240,
            180,
            240,
            180
            };

            int choice;
            do
            {


                Console.WriteLine("Academy Schedule Analyzer");
                Console.WriteLine("===================================");

                Console.WriteLine("1. Display all sessions");
                Console.WriteLine("2. Search for a session");
                Console.WriteLine("3. Sort session names");
                Console.WriteLine("4. Reverse session names");
                Console.WriteLine("5. Find session index");
                Console.WriteLine("6. Check if session exists");
                Console.WriteLine("7. Show duration statistics");
                Console.WriteLine("8. Show session date details");
                Console.WriteLine("9. Show past and upcoming sessions");
                Console.WriteLine("10. Find next session");
                Console.WriteLine("11. Compare two session dates");
                Console.WriteLine("12. Read and validate a custom date");
                Console.WriteLine("13. Select session by index");
                Console.WriteLine("14. Validate session duration");
                Console.WriteLine("15. Generate report using string");
                Console.WriteLine("16. Generate report using StringBuilder");
                Console.WriteLine("0. Exit");
                choice = ReadOption();
                switch (choice)
                {
                    case 1:
                        DisplaySessionDetails(sessionNames, sessionDates, sessionDurations);
                        break;

                    case 2:
                        Console.Write("Enter session name: ");
                        string searchName = Console.ReadLine();
                        SearchSession(sessionNames, searchName);
                        break;

                    case 3:
                        string[] CopiedArray = new string[sessionNames.Length];
                        Array.Copy(sessionNames, CopiedArray, sessionNames.Length);
                        Array.Sort(CopiedArray);
                        Console.WriteLine("Sorted Session Names are");
                        foreach (string name in CopiedArray)
                        {
                            Console.WriteLine(name);
                        }
                        break;

                    case 4:
                        string[] CopiedArray2 = new string[sessionNames.Length];
                        Array.Copy(sessionNames, CopiedArray2, sessionNames.Length);
                        Array.Reverse(CopiedArray2);
                        Console.WriteLine("Reversed Session Names are");
                        foreach (string name in CopiedArray2)
                        {
                            Console.WriteLine(name);
                        }
                        break;

                    case5:

                        Console.WriteLine("Enter session name to find index");
                        string IndexName = Console.ReadLine();
                        int Index = Array.IndexOf(sessionNames, IndexName);
                        if (Index != -1)
                        {
                            Console.WriteLine($"Index: {Index}");
                        }
                        else
                        {
                            Console.WriteLine("Session not found");
                        }
                        break;

                    case 6:

                        Console.WriteLine("Check if a Session exists");
                        Console.WriteLine("Enter Session Name");
                        string FindName = Console.ReadLine();
                        bool sessionExists = Array.Exists(sessionNames, name => name == FindName);
                        if (sessionExists)
                        {
                            Console.WriteLine("Session exists..");
                        }
                        else
                        {
                            Console.WriteLine("Session  does not exist.");
                        }
                        break;

                    case 7:

                        Console.WriteLine($"Total Duration: {GetTotalDuration(sessionDurations)}");
                        Console.WriteLine($"Average Duration: {GetAverageDuration(sessionDurations)}");
                        Console.WriteLine($"Shortest Duration: {GetShortestduration(sessionDurations)}");
                        Console.WriteLine($"longest Duration: {GetLongestduration(sessionDurations)}");
                        break;

                    case 8:

                        Console.Write("Enter Session Name:");
                        string SessionName = Console.ReadLine();
                        int Index2 = Array.IndexOf(sessionNames, SessionName);
                        if (Index2 != -1)
                        {
                            DateTime SessionDate = sessionDates[Index2];
                            int duration2 = sessionDurations[Index2];
                            DateTime EndTime = SessionDate.AddMinutes(duration2);

                            Console.WriteLine($"Full Date is :  {SessionDate:dd , MMMM,YYYY}");
                            Console.WriteLine($"Day: {SessionDate.DayOfWeek}");
                            Console.WriteLine($"Year: {SessionDate.Year}");
                            Console.WriteLine($"Month: {SessionDate.Month}");
                            Console.WriteLine($"Day Number: {SessionDate.Day}");
                            Console.WriteLine($"Start Time:{SessionDate:hh:mm:tt}");
                            Console.WriteLine($"Duration: {duration2} minutes");
                            Console.WriteLine($"End Time: {EndTime}: hh: mm tt ");

                        }
                        else
                        {
                            Console.WriteLine("invalid Session Name");
                        }
                        break;

                    case 9:
                        Console.WriteLine("Past Sessions:");

                        for (int i = 0; i < sessionDates.Length; i++)
                        {
                            if (sessionDates[i] < DateTime.Now)
                            {
                                Console.WriteLine(
                                    $"{sessionNames[i]} - {sessionDates[i]:dd MMMM yyyy hh:mm tt}"
                                );
                            }
                        }

                        Console.WriteLine("Upcoming Sessions:");

                        for (int i = 0; i < sessionDates.Length; i++)
                        {
                            if (sessionDates[i] >= DateTime.Now)
                            {
                                Console.WriteLine(
                                    $"{sessionNames[i]} - {sessionDates[i]:dd MMMM yyyy hh:mm tt}"
                                );
                            }
                        }

                        break;



                    case 10:
                        Console.WriteLine("Select the Coming Session");

                        int nearestIndex = -1;

                        for (int i = 0; i < sessionDates.Length; i++)
                        {
                            if (sessionDates[i] < DateTime.Now)
                            {
                                continue;
                            }

                            if (nearestIndex == -1 || sessionDates[i] < sessionDates[nearestIndex])
                            {
                                nearestIndex = i;
                            }
                        }

                        if (nearestIndex != -1)
                        {
                            DateTime nextSession = sessionDates[nearestIndex];
                            TimeSpan remainingTime = nextSession - DateTime.Now;

                            Console.WriteLine($"Next Session: {sessionNames[nearestIndex]}");
                            Console.WriteLine($"Date: {nextSession:dd MMMM yyyy}");
                            Console.WriteLine($"Start Time: {nextSession:hh:mm tt}");
                            Console.WriteLine($"Time Remaining: {remainingTime.Days} days {remainingTime.Hours} hours");
                        }
                        else
                        {
                            Console.WriteLine("No upcoming sessions.");
                        }

                        break;

                    case 11:

                        Console.WriteLine("Enter Firts Session Name");
                        string Name1 = Console.ReadLine();
                        int FirstInd = Array.IndexOf(sessionNames, Name1);



                        Console.WriteLine("Enter second Session Name");
                        string Name2 = Console.ReadLine();
                        int SecondInd = Array.IndexOf(sessionNames, Name2);

                        if (FirstInd != -1 && SecondInd != -1)
                        {
                            TimeSpan diff = sessionDates[SecondInd] - sessionDates[FirstInd];

                            Console.WriteLine($"Difference: {diff.Days} days");
                            Console.WriteLine($"Total Hours: {diff.TotalHours} hours");

                        }
                        else
                        {
                            Console.WriteLine("invalid Session Names");
                        }
                        break;

                    case 12:

                        DateTime validDate = ReadValidDate();

                        Console.WriteLine($"Valid Date: {validDate:yyyy-MM-dd HH:mm}");

                        break;



                    case 13:
                        DisplaySessionByIndex(sessionNames);

                        break;

                    case 14:

                        try
                        {
                            int Duration = int.Parse(Console.ReadLine());
                            ValidateDuration(Duration);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        break;

                    case 15:
                        string Report = BuildReportUsingString(
                           sessionNames,
                           sessionDates,
                           sessionDurations);

                        Console.WriteLine(Report);
                        break;
                    case 16:
                        string report = BuildReportUsingStringBuilder(
                          sessionNames,
                          sessionDates,
                          sessionDurations);

                        Console.WriteLine(report);
                        break;


                    default:
                        Console.WriteLine("Invalid option. Please choose a number from 0 to 16.");
                        break;

                }
            }
            while (choice != 0);
          // BenchmarkRunner.Run<ScheduleBenchmark>();
          

        }


        public static void DisplaySessionDetails(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
        {
            Console.WriteLine("session Details are");
            for (int i = 0; i < sessionDates.Length; i++)
            {

                Console.WriteLine($"Session Name : {sessionNames[i]}");
                Console.WriteLine($" Date : {sessionDates[i].ToString("dd/MMMM/yyyy")}");
                Console.WriteLine($"Start Time : {sessionDates[i].ToString("hh:mm tt")}");
                Console.WriteLine($" Duration : {sessionDurations[i]} minutes");
                Console.WriteLine("-------------------------------------------------------------------------------");
            }


        }
        public static void DisplaySessionDetails(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations, string sessionName)
        {
            int x = Array.IndexOf(sessionNames,  sessionName);
            if (x != -1)
            {
                Console.WriteLine($"Session Name : {sessionNames[x]}");
                Console.WriteLine($"Date : {sessionDates[x]:dd MMMM yyyy}");
                Console.WriteLine($"Start Time : {sessionDates[x]:hh:mm tt}");
                Console.WriteLine($"Duration : {sessionDurations[x]} minutes");
            }
            else
            {
                Console.WriteLine("Session not found.");
            }
        }


        static void SearchSession(string[] sessionNames, string sessionName)
        {
            int index = Array.IndexOf(sessionNames, sessionName);

            if (index != -1)
            {
                Console.WriteLine($"Session found at index: {index}");
            }
            else
            {
                Console.WriteLine("Session not found.");
            }
        }



        static string SearchSession(string [] sessionNames, DateTime[] sessionDates , int[] sessionDurations , string EnterName)
            {

           
            int index = Array.IndexOf(sessionNames, EnterName);


                if (index != -1)
            {
                return($"Session Name : {sessionNames[index]}\n Date : {sessionDates[index].ToString("dd MMMM yyyy")}\nStart Time : {sessionDates[index].ToString("hh:mm tt")}\n Duration : {sessionDurations[index]} minutes");
            }

            return "Session not found";


            }

               static int GetTotalDuration(int[] sessionDurations)
        {
            int totalDuration = 0;
            foreach (var duration in sessionDurations)
                
            {
                
                totalDuration += duration;
               
            }
            return totalDuration;
        }
        static double GetAverageDuration(int[] sessionDurations)
        {
            
                
              double  AverageDuration = GetTotalDuration(sessionDurations) / sessionDurations.Length;
                return AverageDuration;
            
            
        }

        static int GetShortestduration(int[] sessionDuration)
        {
            int shortestDuration = sessionDuration[0];
            for(int i = 1; i < sessionDuration.Length; i++)
            {
                if (sessionDuration[i] < shortestDuration)
                {
                    shortestDuration = sessionDuration[i];
                }
            }
            return shortestDuration;
        }
        static int GetLongestduration(int[] sessionDuration)
        {
            int Longestduration = sessionDuration[0];
            for (int i=1; i < sessionDuration.Length; i++)
            {

                if (sessionDuration[i] > Longestduration)
                {
                    Longestduration = sessionDuration[i];
                }
               

            }
            return Longestduration;

        }
         public static DateTime GetSessionEndTime(DateTime startTime , int durations)
        {
            DateTime End_Time = startTime.AddMinutes(durations);

            return End_Time;

        }

        public static DateTime ReadSessionDate()
        {
            Console.WriteLine("Enter Session Date:");
            string input = Console.ReadLine();

            DateTime date;

            if (DateTime.TryParse(input, out date))
            {
                return date;
            }
            else
            {
                Console.WriteLine("Invalid date.");
                return DateTime.MinValue;
            }
        }

        public static string BuildReportUsingString(
    string[] sessionNames,
    DateTime[] sessionDates,
    int[] sessionDurations)
        {
            string Report = "";

            for (int i = 0; i < sessionNames.Length; i++)
            {
                Report += $"Session Name: {sessionNames[i]}\n";
                Report += $"Date: {sessionDates[i]:dd MMMM yyyy}\n";
                Report += $"Start Time: {sessionDates[i]:hh:mm tt}\n";
                Report += $"Duration: {sessionDurations[i]} minutes\n";
                Report += "------------------------------\n";
            }

            return Report;
        }
        public static string BuildReportUsingStringBuilder(
            string[] sessionNames,
            DateTime[] sessionDates,
            int[] sessionDurations)
            {
                StringBuilder report = new StringBuilder();

                for (int i = 0; i < sessionNames.Length; i++)
                {
                    report.AppendLine($"Session Name: {sessionNames[i]}");
                    report.AppendLine($"Date: {sessionDates[i]:dd MMMM yyyy}");
                    report.AppendLine($"Start Time: {sessionDates[i]:hh:mm tt}");
                    report.AppendLine($"Duration: {sessionDurations[i]} minutes");
                    report.AppendLine("------------------------------");
                }

                return report.ToString();
            }

      

      static  void Change(ref int[] values)
        {
            values = new int[] { 100, 200, 300 };
        }

        static void Change(int[] values)
        {
            values = new int[] { 100, 200, 300 };
        }

       static bool Info (string[] sessionNames, int[] sessionDurations , string sessionName, out int index,  out int duration)
        {
           
           index= Array.IndexOf(sessionNames, sessionName);

            index = Array.IndexOf(sessionNames, sessionName);

            if (index != -1)
            {
                duration = sessionDurations[index];
                return true;
            }

            duration = 0;
            return false;
        }

        static int CalculateTotalDuration(params int[] nums)
        {
            int Total = 0;
            foreach( int num in nums)
            {

                Total += num;
            }
            return Total;

        }

       
        static DateTime ReadValidDate()
        {
            while (true)
            {
                Console.Write("Enter date (yyyy-MM-dd HH:mm): ");
                string input = Console.ReadLine();

                DateTime date;

                if (DateTime.TryParseExact(input, "yyyy-MM-dd HH:mm", null, System.Globalization.DateTimeStyles.None, out date))
                {
                    return date;
                }
                else
                Console.WriteLine("Invalid date. Please try again.");
            }
        }

        
         static int ReadOption()
        {
            while (true)
            {
                Console.Write("Choose an option: ");
                string input = Console.ReadLine();

                try
                {
                    int option = int.Parse(input);
                    return option;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid menu option. Enter a number.");
                }
            }
        }
        static void DisplaySessionByIndex(string[] sessionNames)
        {

            Console.Write("Enter session index: "); 
            string input = Console.ReadLine();
            try 
            { int index = int.Parse(input); 
              Console.WriteLine($"Session: {sessionNames[index]}"); 
            } 
            catch (IndexOutOfRangeException) 
            {
                Console.WriteLine("The selected session index is out of range."); 
            }
            finally
            {
                Console.WriteLine("Input operation finished.");
            }


        }

       public static void ValidateDuration(int Duration) 
        {
            if (Duration <= 0) 
            { 
                throw new ArgumentException("Duration must be greater than zero."); 
            } 
            Console.WriteLine("Duration accepted.");
        }

        static string BuildScheduleReport(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
        {
            string result = "";
            for (int i = 0; i < sessionNames.Length; i++)
            {
                result += $"{sessionNames[i]} - {sessionDates[i]:dd/MM/yyyy hh:mm tt} - {sessionDurations[i]} minutes";
            }
            return result;

        }

        static string BuildScheduleReporStringBuilder(string[] sessionNames, DateTime[] sessionDates, int[] sessionDurations)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < sessionNames.Length; i++)
            {
                result.AppendLine($"{sessionNames[i]} - {sessionDates[i]:dd/MM/yyyy hh:mm tt} - {sessionDurations[i]} minutes");
            }

            return result.ToString(); ;
        }
















    }
}
