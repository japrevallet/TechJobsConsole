using System;
using System.Collections.Generic;

namespace TechJobsConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create two Dictionary vars to hold info for menu and data

            // Top-level menu options
            Dictionary<string, string> actionChoices = new Dictionary<string, string>();
            actionChoices.Add("search", "Search");
            actionChoices.Add("list", "List");

            // Column options
            Dictionary<string, string> columnChoices = new Dictionary<string, string>();
            columnChoices.Add("core competency", "Skill");
            columnChoices.Add("employer", "Employer");
            columnChoices.Add("location", "Location");
            columnChoices.Add("position type", "Position Type");
            columnChoices.Add("all", "All");

            Console.WriteLine("Welcome to LaunchCode's TechJobs App!");

            // Allow user to search/list until they manually quit with ctrl+c
            while (true)
            {

                string actionChoice = GetUserSelection("View Jobs", actionChoices);

                if (actionChoice.Equals("list"))
                {
                    string columnChoice = GetUserSelection("List", columnChoices);

                    if (columnChoice.Equals("all"))
                    {
                        PrintJobs(JobData.FindAll());
                    }
                    else
                    {
                        List<string> results = JobData.FindAll(columnChoice);

                        Console.WriteLine("\n*** All " + columnChoices[columnChoice] + " Values ***");
                        foreach (string item in results)
                        {
                            Console.WriteLine(item);
                        }
                    }
                }
                else // choice is "search"
                {
                    // How does the user want to search (e.g. by skill or employer)
                    string columnChoice = GetUserSelection("Search", columnChoices);

                    // What is their search term?
                    Console.WriteLine("\nSearch term: ");
                    string searchTerm = Console.ReadLine();

                    List<Dictionary<string, string>> searchResults;

                    // Fetch results
                    if (columnChoice.Equals("all"))
                    {
                        searchResults = JobData.FindByValue(searchTerm);
                        PrintJobs(searchResults);
                    }
                    else
                    {
                        searchResults = JobData.FindByColumnAndValue(columnChoice, searchTerm);
                        PrintJobs(searchResults);
                    }
                }
            }
        }

        /*
         * Returns the key of the selected item from the choices Dictionary
         */
        private static string GetUserSelection(string choiceHeader, Dictionary<string, string> choices)
        {
            int choiceIdx;
            bool isValidChoice = false;
            string[] choiceKeys = new string[choices.Count];

            int i = 0;
            foreach (KeyValuePair<string, string> choice in choices)
            {
                choiceKeys[i] = choice.Key;
                i++;
            } // 

            do
            {
                Console.WriteLine("\n" + choiceHeader + " by:"); // first line of user seen code. Uses hardcoded actionChoices the first time. The output, actionChoice (a dictionary("search","Search") or dictionary("list","List")) will be used the second time to input columnChoices, and return columnChoice

                for (int j = 0; j < choiceKeys.Length; j++)
                {
                    Console.WriteLine(j + " - " + choices[choiceKeys[j]]);
                } // iritates through and prints Key names from dictionary nexted to assigned values for user

                string input = Console.ReadLine(); //saves user input as string
                choiceIdx = int.Parse(input);  // saves int value of user input

                if (choiceIdx < 0 || choiceIdx >= choiceKeys.Length) // is this a cute way of making sure choiceIdx is never greater than 1?
                {
                    Console.WriteLine("Invalid choices. Try again.");
                }
                else
                {
                    isValidChoice = true;
                }

            } while (!isValidChoice);

            return choiceKeys[choiceIdx]; // returns ??? actionChoice = ? collumnChoice = ?
        }

        private static void PrintJobs(List<Dictionary<string, string>> someJobs)
        { 
           // List<Dictionary<string, string>> jobList = new List<Dictionary<string, string>>();      use this in your FindByValue()
           // Dictionary<string, string> jobContent = new Dictionary<string, string> { };


            foreach (Dictionary<string, string> someJob in someJobs)  //goes into each job listing
            {
                Console.WriteLine("\n");
                Console.WriteLine("*************************************");
                Console.WriteLine("*************************************");
                Console.WriteLine("\n");
                foreach (KeyValuePair<string, string> job in someJob) // goes into each data field for every individual dictionary
                {
                    string jobKey = ""; 
                    string jobValue = "";

                    jobKey = job.Key;
                    jobValue = job.Value;

                    
                    Console.WriteLine(jobKey + ": " + jobValue);

                }
            }
            Console.WriteLine("\n");
            Console.WriteLine("*************************************");
            Console.WriteLine("*************************************");

        }
    }
}