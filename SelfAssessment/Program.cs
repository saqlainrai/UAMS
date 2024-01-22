using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SelfAssessment.BL;

namespace SelfAssessment
{
    internal class Program
    {
        static bool admissionOpen = true;
        // This public static async Task is used instead of static to use await keyword async. lly
        public static async Task Main(string[] args)
        {
            List<Subject> allSubjects = new List<Subject>();
            List<Student> allStudents = new List<Student>();
            List<DegreeProgram> allDegreePrograms = new List<DegreeProgram>();
            List<User> allUsers = new List<User>()
            {
                new User("admin", "1234", "Admin") { }
            };

            // there is no need to return the list from function
            // as it is automatically passed by reference and changes are maded in main
            loadSubjectsData(allSubjects);
            loadDegreesData(allDegreePrograms);
            //loadUserData(allUsers);
            loadStudentData(allStudents, allDegreePrograms);
            //loadUserDataFromFile(allUsers);
            //loadRandomData();

            // Student Variables
            string studentName;
            int age;
            int fscMarks;
            int ecatMarks;
            int preference;
            // Degree Variables
            string degreeName;
            int duration;
            int seats;
            int noOfSubjects;
            // Subject Variables
            string code;
            string type;
            int creditHours;
            double fees;

            Console.ForegroundColor = ConsoleColor.Black;
            Console.BackgroundColor = ConsoleColor.White;

            string option;
            bool mainLoopRunning = true;
            bool starter = true;
            bool list = true;
            while (mainLoopRunning)
            {
                if (starter == true)
                {
                    Console.Clear();
                    header();
                    starter = false;
                }
                else
                {
                    ClearScreen();
                }
                string mainOption = loginScreen();
                // Apply Online for Admission
                if (mainOption == "1")
                {
                    if (admissionOpen == true)
                    {
                        ClearScreen();
                        List<string> temp = new List<string>();
                        string tempName;
                        Console.Write("Enter your Name: ");
                        studentName = Console.ReadLine();

                        Console.WriteLine();

                        Console.Write("Enter your age: ");
                        age = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine();

                        Console.Write("Enter your FSC Marks: ");
                        fscMarks = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine();

                        Console.Write("Enter your ECAT Marks: ");
                        ecatMarks = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine();
                        if (allDegreePrograms.Count != 0)
                        {
                            Console.WriteLine("Following Degree are offered by the University");
                            Console.WriteLine();
                        }
                        foreach (DegreeProgram program in allDegreePrograms)
                        {
                            Console.WriteLine(program.name);
                        }

                        Console.WriteLine();

                        Console.Write("Enter your Number of Preferences: ");
                        preference = Convert.ToInt32(Console.ReadLine());

                        if (preference != 0)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Note: Enter the Name of Preferences Sequentially");
                            Console.WriteLine();
                        }
                        for (int x = 0; x < preference;)
                        {
                            Console.Write("Enter the Name of " + (x + 1) + " Degree Program: ");
                            tempName = Console.ReadLine();
                            int newIndex = findProgram(tempName, allDegreePrograms);
                            if (newIndex != -1)
                            {
                                // Important
                                // we have created shallow copy so, if name will be updated
                                // it will be automatically updated in student list
                                temp.Add(tempName);
                                x++;
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("Now Such Program Is Offered by University");
                                Console.WriteLine("Try Again");
                                Console.WriteLine();
                            }
                        }
                        allStudents.Add(AddStudent(studentName, age, fscMarks, ecatMarks, preference, temp));
                        addStudentToFile(studentName, age, fscMarks, ecatMarks, preference, temp);
                        //temp.Clear();

                        Console.WriteLine();
                        Console.WriteLine("Your Data has been recorded successfully");
                        Console.WriteLine();
                        Console.WriteLine("Wait for the Merit List!!!");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Admission are Closed");
                        Console.WriteLine("You can not Apply Now!!!");
                    }
                }
                // Exsisting Acccount
                else if (mainOption == "2")
                {
                    ClearScreen();
                    string newOption;
                    bool loopRunning = true;
                    while (loopRunning)
                    {
                        newOption = existingAccount();
                        // Admin Menu
                        if (newOption == "1")
                        {
                            ClearScreen();
                            Console.Write("Enter User Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter your Password: ");
                            string pass = Console.ReadLine();
                            string check = findVisitor(allUsers, name, pass);

                            if (check == "Admin")
                            {
                                bool adminLoop = true;
                                while (adminLoop)
                                {
                                    ClearScreen();
                                    option = adminMenu();
                                    // Add Students
                                    if (option == "1")
                                    {
                                        ClearScreen();
                                        List<string> temp = new List<string>();
                                        string tempName;
                                        Console.Write("Enter Name of Student: ");
                                        studentName = Console.ReadLine();

                                        Console.WriteLine();

                                        Console.Write("Enter age of Student: ");
                                        age = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine();

                                        Console.Write("Enter FSC Marks of Student: ");
                                        fscMarks = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine();

                                        Console.Write("Enter ECAT Marks of Student: ");
                                        ecatMarks = Convert.ToInt32(Console.ReadLine());

                                        Console.WriteLine();
                                        if (allDegreePrograms.Count != 0)
                                        {
                                            Console.WriteLine("Following Degree are offered by the University");
                                            Console.WriteLine();
                                        }
                                        foreach (DegreeProgram program in allDegreePrograms)
                                        {
                                            Console.WriteLine(program.name);
                                        }

                                        Console.WriteLine();

                                        Console.Write("Enter your Number of Preferences: ");
                                        preference = Convert.ToInt32(Console.ReadLine());

                                        if (preference != 0)
                                        {
                                            Console.WriteLine();
                                            Console.WriteLine("Note: Enter the Name of Preferences Sequentially");
                                            Console.WriteLine();
                                        }
                                        for (int x = 0; x < preference;)
                                        {
                                            Console.Write("Enter the Name of " + (x + 1) + " Degree Program: ");
                                            tempName = Console.ReadLine();
                                            int newIndex = findProgram(tempName, allDegreePrograms);
                                            if (newIndex != -1)
                                            {
                                                // Important
                                                // we have created shallow copy so, if name will be updated
                                                // it will be automatically updated in student list
                                                temp.Add(tempName);
                                                x++;
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("Now Such Program Is Offered by University");
                                                Console.WriteLine("Try Again");
                                                Console.WriteLine();
                                            }
                                        }
                                        allStudents.Add(AddStudent(studentName, age, fscMarks, ecatMarks, preference, temp));
                                        addStudentToFile(studentName, age, fscMarks, ecatMarks, preference, temp);

                                        Console.WriteLine();
                                        Console.WriteLine("Your Data has been recorded successfully");
                                        Console.WriteLine();
                                        Console.WriteLine("Wait for the Merit List!!!");
                                    }
                                    // Add Degree Program
                                    else if (option == "2")
                                    {
                                        List<Subject> tempList = new List<Subject>();
                                        Console.Write("Enter Degree Name: ");
                                        degreeName = Console.ReadLine();
                                        Console.Write("Enter Degree Duration: ");
                                        duration = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Enter Seats for Degree: ");
                                        seats = Convert.ToInt32(Console.ReadLine());
                                        Console.Write("Enter how many Subjects to Add: ");
                                        noOfSubjects = Convert.ToInt32(Console.ReadLine());
                                        for (int x = 0; x < noOfSubjects; x++)
                                        {
                                            Console.Write("Enter Subject Code: ");
                                            code = Console.ReadLine();
                                            Console.Write("Enter Subject Type: ");
                                            type = Console.ReadLine();
                                            Console.Write("Enter Subject Credit Hours: ");
                                            creditHours = Convert.ToInt32(Console.ReadLine());
                                            Console.Write("Enter Subject Fees: ");
                                            fees = Convert.ToInt32(Console.ReadLine());
                                            //allDegreePrograms[allDegreePrograms.Count()-1].subjects.Add(AddSubject(code, type, creditHours, fees));
                                            tempList.Add(AddSubject(code, type, creditHours, fees));
                                        }
                                        allDegreePrograms.Add(AddDegreeProgram(degreeName, duration, seats, noOfSubjects, tempList));
                                    }
                                    // Generate Merit
                                    else if (option == "3")
                                    {
                                        if (list == true)
                                        {
                                            list = false;
                                            if (admissionOpen != true)
                                            {
                                                calculateMerit(allStudents);
                                                allStudents = sortDescendingList(allStudents);
                                                generateMeritLists(allStudents, allDegreePrograms);
                                                Console.WriteLine("Merit List Generated Successfully");
                                                Console.WriteLine("Do you want to see the Merit List(Yes/No): ");
                                                string negation = Console.ReadLine();
                                                if (negation == "Yes")
                                                {
                                                    Console.WriteLine("Enter the Program for which you want to See Merit List: ");
                                                    string viewMeritList = Console.ReadLine();
                                                    int number = findProgram(viewMeritList, allDegreePrograms);
                                                    List<Student> registeredStudents = null;
                                                    if (number != -1)
                                                    {
                                                        registeredStudents = allDegreePrograms[number].returnRegisteredStudents();
                                                        for (int x = 0; x < registeredStudents.Count; x++)
                                                        {
                                                            Console.WriteLine(registeredStudents[x].name + "     " + registeredStudents[x].merit);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        Console.WriteLine("Degree Program Not Found!!!");
                                                    }
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Successfully Teminated!");
                                                }
                                                for (int x = 0; x < allStudents.Count; x++)
                                                {
                                                    if (allStudents[x].regDegree != null)
                                                    {
                                                        allUsers.Add(new User(allStudents[x].name, allStudents[x].fscMarks.ToString(), "Student"));
                                                        //allUsers.Add(new User(allStudents[x].name, $"{allStudents[x].fscMarks}", "Student"));
                                                    }
                                                }
                                                //addUserDatatoFile(allStudents);
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("Admissions are Open.");
                                                Console.WriteLine("Merit List Will be Generated After Closing of Admissions");
                                                Console.WriteLine();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The Merit List are already Generated");
                                            Console.WriteLine("This act is not allowed again");
                                        }
                                    }
                                    // View Registered Students
                                    else if (option == "4")
                                    {
                                        ClearScreen();
                                        Console.WriteLine("{0,-15}{1,-10}{2,-20}{3,-20}{4,-10}", "Name", "Age", "FSC Marks", "ECAT Marks", "Merit");
                                        Console.WriteLine();
                                        for (int x = 0; x < allStudents.Count; x++)
                                        {
                                            Console.WriteLine("{0,-15}{1,-10}{2,-20}{3,-20}{4,-10}", allStudents[x].name, allStudents[x].age, allStudents[x].fscMarks, allStudents[x].ecatMarks, allStudents[x].merit);
                                        }
                                    }
                                    // View Registered Degree Programs
                                    else if (option == "5")
                                    {
                                        ClearScreen();
                                        Console.WriteLine("Total No of Degrees Offered by the University: {0}", allDegreePrograms.Count);
                                        Console.WriteLine();
                                        for (int x = 0; x < allDegreePrograms.Count; x++)
                                        {
                                            Console.WriteLine("Name of {0} Degree Program          : {1}", (x + 1), allDegreePrograms[x].name);
                                            Console.WriteLine("Duration of this Degree Program   : {0} years", allDegreePrograms[x].duration);
                                            Console.WriteLine("Available Seats in this Program   : {0}", allDegreePrograms[x].seats);
                                            if (allDegreePrograms[x].subjects.Count != 0)
                                            {
                                                Console.WriteLine("Following Subjects are Offered in this Program");
                                                Console.WriteLine();
                                                for (int y = 0; y < allDegreePrograms[x].subjects.Count; y++)
                                                {
                                                    Console.WriteLine("Name of {0} Subject: {1}", (y + 1), allDegreePrograms[x].subjects[y].code);
                                                }
                                            }
                                            Console.WriteLine();

                                        }
                                    }
                                    // View Student of a Specific Degree Program
                                    else if (option == "6")
                                    {
                                        if (admissionOpen != true)
                                        {
                                            Console.Write("Enter the Degree Program: ");
                                            string enterProgram = Console.ReadLine();
                                            int number = findProgram(enterProgram, allDegreePrograms);
                                            if (number != -1)
                                            {
                                                Console.WriteLine("Name of Degree Program: " + allDegreePrograms[number].name);
                                                Console.WriteLine();
                                                List<Student> arziStudents = allDegreePrograms[number].returnRegisteredStudents();
                                                Console.WriteLine("{0,-20} {1,-20}", "Name", "Merit");
                                                for (int x = 0; x < arziStudents.Count; x++)
                                                {
                                                    Console.WriteLine("{0,-20} {1,-20}", arziStudents[x].name, arziStudents[x].merit);
                                                }
                                                Console.WriteLine();
                                            }
                                            else
                                            {
                                                Console.WriteLine();
                                                Console.WriteLine("No Such Program Was Offered By the University");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The Admissions are Open");
                                            Console.WriteLine("Result Will be Shown after Generation of Merit List");
                                        }
                                    }
                                    // View Students and their registered Degree Program
                                    else if (option == "7")
                                    {
                                        ClearScreen();
                                        if (admissionOpen != true)
                                        {
                                            Console.WriteLine("   {0,-15} {1,-20} {2,-20}", "Name", "Merit", "Status");
                                            foreach (Student s in allStudents)
                                            {
                                                if (s.regDegree != null)
                                                {
                                                    Console.WriteLine("   {0,-15} {1,-20} {2,-20}", s.name, s.merit, s.regDegree.name);
                                                }
                                                else
                                                {
                                                    Console.WriteLine("   {0,-15} {1,-20} {2,-20}", s.name, s.merit, "Un-Registered");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Admission Open ");
                                            Console.WriteLine("Registration will be done after Generation of Merit List");
                                        }
                                    }
                                    // Calculate Fees for registered Students
                                    else if (option == "8")
                                    {

                                    }
                                    // Toggle the Admissionn Status
                                    else if (option == "9")
                                    {
                                        if (admissionOpen == false)
                                        {
                                            admissionOpen = true;
                                        }
                                        else
                                        {
                                            admissionOpen = false;
                                        }
                                        Console.WriteLine();
                                        //updateAdmissionStatus();
                                        Console.WriteLine("Task Completed Successfully!");
                                    }
                                    // all degrees and their students
                                    else if (option == "A")
                                    {
                                        foreach (DegreeProgram temp in allDegreePrograms)
                                        {
                                            Console.WriteLine("Name of Degree Program: " + temp.name);
                                            Console.WriteLine();
                                            List<Student> arziStudents = temp.returnRegisteredStudents();
                                            for (int x = 0; x < arziStudents.Count; x++)
                                            {
                                                Console.WriteLine("Name: {0}     Merit: {1}", arziStudents[x].name, arziStudents[x].merit);
                                            }
                                            Console.WriteLine();
                                        }
                                    }
                                    // Specific Student
                                    else if (option == "B")
                                    {
                                        ClearScreen();
                                        Console.Write("Enter the Name of Student:  ");
                                        Console.WriteLine();
                                        string Nname = Console.ReadLine();
                                        int number = findStudent(Nname, allStudents);
                                        if (number != -1)
                                        {
                                            if (allStudents[number].regDegree != null)
                                            {
                                                Console.WriteLine("Name of Student    : " + allStudents[number].name);
                                                Console.WriteLine("Age of Student     : " + allStudents[number].age);
                                                Console.WriteLine("FSC Marks          : " + allStudents[number].fscMarks);
                                                Console.WriteLine("ECAT Marks         : " + allStudents[number].ecatMarks);
                                                Console.WriteLine("Registered Degree  : " + allStudents[number].regDegree.name);
                                                Console.WriteLine("Calculated Merit   : " + allStudents[number].merit);
                                            }
                                            else
                                            {
                                                Console.WriteLine("Name of Student    : " + allStudents[number].name);
                                                Console.WriteLine("Age of Student     : " + allStudents[number].age);
                                                Console.WriteLine("FSC Marks          : " + allStudents[number].fscMarks);
                                                Console.WriteLine("ECAT Marks         : " + allStudents[number].ecatMarks);
                                                Console.WriteLine("Calculated Merit   : " + allStudents[number].merit);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Student Not Found");
                                        }
                                    }
                                    // Exit Case
                                    else if (option == "0")
                                    {
                                        adminLoop = false;
                                    }
                                    // Else Case
                                    else
                                    {
                                        Console.WriteLine("Invalid Value");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("No Such Admin Found");
                            }
                            ClearScreen();
                        }
                        // Student Menu
                        else if (newOption == "2")
                        {
                            int refindex = 0;
                            ClearScreen();
                            Console.Write("Enter User Name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter your Password: ");
                            string pass = Console.ReadLine();
                            string check = findVisitor(allUsers, name, pass);
                            if (check == "Student")
                            {
                                for(int i = 0;i<allStudents.Count;i++)
                                {
                                    if (allStudents[i].name == name && allStudents[i].fscMarks == int.Parse(pass))
                                    {
                                        refindex = i;
                                    }
                                }
                                bool studentLoop = true;
                                while (studentLoop)
                                {
                                    ClearScreen();
                                    option = studentMenu();
                                    // Register a Subject
                                    if (option == "1")
                                    {
                                        Console.WriteLine("Following are the Subjects Offered in your Degree: ");
                                        Console.WriteLine();
                                        for (int x = 0; x < allStudents[refindex].regDegree.subjects.Count; x++)
                                        {
                                            Console.WriteLine(allStudents[refindex].regDegree.subjects[x].code);
                                        }
                                        Console.Write("Enter the Code of Subject: ");
                                        string desiredCourse = Console.ReadLine();
                                        bool foundSuccess = false, matchSuccess = false;
                                        Subject s = null;
                                        foreach(Subject s1 in allStudents[refindex].regDegree.subjects)
                                        {
                                            if (s1.code == desiredCourse)
                                            {
                                                //allStudents[refindex].regSubjects.Add(s1);
                                                //Console.WriteLine("Course Registered Successfully!!!");

                                                foundSuccess = true;
                                                s = new Subject(s1.code, s1.type, s1.creditHours, s1.fees);
                                            }
                                        }
                                        if (foundSuccess)
                                        {
                                            foreach(Subject subject in allStudents[refindex].regSubjects)
                                            {
                                                if (subject.code == s.code)
                                                {
                                                    matchSuccess = true;
                                                }
                                            }
                                            if (!matchSuccess)
                                            {
                                                Console.WriteLine("Course Registered Successfully");
                                                allStudents[refindex].regSubjects.Add(s);
                                            }
                                            else
                                            {
                                                Console.WriteLine("This Course is Already Registered");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No Such course is offered");
                                        }
                                    }
                                    // view my details
                                    else if (option == "2")
                                    {
                                        Console.WriteLine("Name:              " + allStudents[refindex].name);
                                        Console.WriteLine("Age:               " + allStudents[refindex].age);
                                        Console.WriteLine("ECAT Marks:        " + allStudents[refindex].ecatMarks);
                                        Console.WriteLine("FSC Marks:         " + allStudents[refindex].fscMarks);
                                        Console.WriteLine("Registered Degree: " + allStudents[refindex].regDegree.name);
                                        Console.WriteLine("Merit:             " + allStudents[refindex].merit);
                                    }
                                    // View Students in my Degree Program
                                    else if (option == "3")
                                    {
                                        DegreeProgram temp = allStudents[refindex].regDegree;
                                        List<Student> studentList = new List<Student>();
                                        for (int x=0;x<allDegreePrograms.Count;x++)
                                        {
                                            if (allDegreePrograms[x].name == temp.name)
                                            {
                                                studentList = allDegreePrograms[x].registeredStudents;
                                            }
                                        }
                                        foreach(Student student in studentList)
                                        {
                                            Console.WriteLine(student.name + "  " + student.merit);
                                        }
                                    }
                                    // View Subjects Offered in my Degree Program
                                    else if (option == "4")
                                    {
                                        Console.WriteLine("Following are the Subjects Offered in your Degree: ");
                                        Console.WriteLine();
                                        for (int x = 0; x < allStudents[refindex].regDegree.subjects.Count; x++)
                                        {
                                            Console.WriteLine(allStudents[refindex].regDegree.subjects[x].code);
                                        }
                                    }
                                    // View my registered Subjects
                                    else if (option == "5")
                                    {
                                        if (allStudents[refindex].regSubjects.Count != 0)
                                        {
                                            Console.WriteLine("{0, -15} {1, -15} {2, -15} {3, -15}", "Code", "Type", "Credit Hours", "Fees");
                                            foreach (Subject s in allStudents[refindex].regSubjects)
                                            {
                                                Console.WriteLine("{0, -15} {1, -15} {2, -15} {3, -15}", s.code, s.type, s.creditHours, s.fees);
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No Subject is registered Yet");
                                        }
                                    }
                                    // View my Degree Fees
                                    else if (option == "6")
                                    {
                                        double total =0;

                                        if (allStudents[refindex].regSubjects.Count != 0)
                                        {
                                            Console.WriteLine("{0, -15} {1, -15} {2, -15}", "Code", "Credit Hours", "Fees");
                                            foreach (Subject s in allStudents[refindex].regSubjects)
                                            {
                                                Console.WriteLine("{0, -15} {1, -15} {2, -15}", s.code, s.creditHours, s.fees);
                                                total += s.fees;
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("You are not enrolled in any Subject");
                                        }
                                        Console.WriteLine("Following is your Total Fees: " + total);
                                    }
                                    // Exit Case
                                    else if (option == "7")
                                    {
                                        studentLoop = false;
                                    }
                                    // Else Case
                                    else
                                    {
                                        Console.WriteLine("Invalid Option");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("No Such Student Found");
                            }
                            ClearScreen();
                        }
                        // Exit Menu
                        else if (newOption == "3")
                        {
                            loopRunning = false;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Option");
                        }
                        ClearScreen();
                    }
                }
                // Showing the results
                else if (mainOption == "3")
                {
                    if (admissionOpen != true)
                    {
                        if (allStudents[0].merit != 0)
                        {
                            Console.Write("Enter the Degree Program: ");
                            string enterProgram = Console.ReadLine();
                            int number = findProgram(enterProgram, allDegreePrograms);
                            if (number != -1)
                            {
                                Console.WriteLine("Name of Degree Program: " + allDegreePrograms[number].name);
                                Console.WriteLine();
                                List<Student> arziStudents = allDegreePrograms[number].returnRegisteredStudents();
                                Console.WriteLine("{0,-20} {1,-20}", "Name", "Merit");
                                for (int x = 0; x < arziStudents.Count; x++)
                                {
                                    Console.WriteLine("{0,-20} {1,-20}", arziStudents[x].name, arziStudents[x].merit);
                                }
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine();
                                Console.WriteLine("No Such Program Was Offered By the University");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Merit List is not Displayed Yet");
                            Console.WriteLine("Wait for the Date");
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("The Admissions are Open");
                        Console.WriteLine("Result Will be Displayed after Generation of Merit List");
                    }
                }
                // Exit Case
                else if (mainOption == "4")
                {
                    mainLoopRunning = false;
                }
                // Else Case
                else
                {
                    Console.WriteLine("Invalid Input!");
                }
            }
        }
        static void header()
        {
            Console.WriteLine("                     _    _            __  __   _____           ");
            Console.WriteLine("                    | |  | |    /\\    |  \\/  | / ____|        ");
            Console.WriteLine("                    | |  | |   /  \\   | \\  / || (___          ");
            Console.WriteLine("                    | |  | |  / /\\ \\  | |\\/| | \\___ \\      ");
            Console.WriteLine("                    | |__| | / ____ \\ | |  | | ____) |         ");
            Console.WriteLine("                     \\____/ /_/    \\_\\|_|  |_||_____/        ");
            Console.WriteLine();
            Console.WriteLine("%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%");
            Console.WriteLine();
        }
        static string loginScreen()
        {
            string option;
            Console.WriteLine("                 ************************************************   ");
            Console.WriteLine("               *             1. APPLY  FOR ADMISSION             *  ");
            Console.WriteLine("                 ************************************************   ");

            Console.WriteLine();

            Console.WriteLine("                 ************************************************   ");
            Console.WriteLine("               *               2. EXISTING ACCOUNT               *  ");
            Console.WriteLine("                 ************************************************   ");

            Console.WriteLine();

            Console.WriteLine("                 ************************************************   ");
            Console.WriteLine("               *               3. CHECK THE RESULT               *  ");
            Console.WriteLine("                 ************************************************   ");

            Console.WriteLine();

            Console.WriteLine("                 ************************************************   ");
            Console.WriteLine("               *                   4. EXIT                       *  ");
            Console.WriteLine("                 ************************************************   ");

            Console.WriteLine();

            Console.Write("Your Option:  ");
            option = Console.ReadLine();
            return option;
        }
        static string studentMenu()
        {
            string option;
            Console.WriteLine("1. Register Subject");
            Console.WriteLine("2. View My Details");
            Console.WriteLine("3. View Registered Students in My Degree Program");
            Console.WriteLine("4. View subjects offered in my Degree Program");
            Console.WriteLine("5. View My registered Subjects");
            Console.WriteLine("6. View my Fees for my Degree Program");
            Console.WriteLine("7. Exit");
            Console.Write("Your Option....");
            option = Console.ReadLine();
            return option;
        }
        static string adminMenu()
        {
            string option;
            Console.WriteLine("1. Add Student");
            Console.WriteLine("2. Add Degree Program");
            Console.WriteLine("3. Generate Merit");
            Console.WriteLine("4. View Registered Students");
            Console.WriteLine("5. View Registered Degree Programs");
            Console.WriteLine("6. View Students of a Specific Program");
            Console.WriteLine("7. View Students and their Registered Degrees");
            Console.WriteLine("8. Calculate Fees for Registered Students");
            Console.WriteLine("9. Toggle the Admission Status");
            Console.WriteLine("A. View all Degrees and their registered Students");
            Console.WriteLine("B. View Detail of a Specific Student");
            Console.WriteLine("0. Exit");
            Console.Write("Your Option....");
            option = Console.ReadLine();
            return option;
        }
        static string existingAccount()
        {
            string option;
            Console.WriteLine("                 ************************************************   ");
            Console.WriteLine("               *               1.   A D M I N                    *  ");
            Console.WriteLine("                 ************************************************   ");

            Console.WriteLine();

            Console.WriteLine("                 ************************************************   ");
            Console.WriteLine("               *              2.   S T U D E N T                 *  ");
            Console.WriteLine("                 ************************************************   ");

            Console.WriteLine();

            Console.WriteLine("                 ************************************************   ");
            Console.WriteLine("               *               3.    E X I T                     *  ");
            Console.WriteLine("                 ************************************************   ");

            Console.Write("Your Option:   ");
            option = Console.ReadLine();
            return option;
        }
        static Student AddStudent(string name, int age, int fscMarks, int ecatMarks, int preference, List<string> temp)
        {
            Student student = new Student(name, age, fscMarks, ecatMarks, preference, temp);
            return student;
        }
        static Subject AddSubject(string code, string type, int creditHours, double fees)
        {
            Subject subject = new Subject(code, type, creditHours, fees);
            return subject;
        }
        static DegreeProgram AddDegreeProgram(string name, int duration, int seats, int noOfSubjects, List<Subject> subjects)
        {
            DegreeProgram degreeProgram = new DegreeProgram(name, duration, seats, noOfSubjects, subjects);
            return degreeProgram;
        }
        static string findVisitor(List<User> allUsers, string name, string pass)
        {
            for (int i = 0; i < allUsers.Count; i++)
            {
                if (name == allUsers[i].name && pass == allUsers[i].password)
                {
                    return allUsers[i].role;
                }
            }
            return "";
        }
        static void ClearScreen()
        {
            Console.WriteLine();
            Console.WriteLine("Press any Key to Continue.........");
            Console.ReadKey();
            Console.Clear();
            header();
        }
        static void loadStudentData(List<Student> allStudents, List<DegreeProgram> allDegreePrograms)
        {
            string name;
            int age;
            int fscMarks;
            int ecatMarks;
            int preference;

            string line;
            string path = "studentsData.txt";
            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                List<string> temp = new List<string>();
                // coz, we want to add a new list of strings every time 
                // if we declare it above it will will concatenate preferences
                line = file.ReadLine();
                string[] dataArray = line.Split(',');
                name = dataArray[0];
                age = int.Parse(dataArray[1]);
                fscMarks = int.Parse(dataArray[2]);
                ecatMarks = int.Parse(dataArray[3]);
                preference = int.Parse(dataArray[4]);
                for (int x = 0; x < preference; x++)
                {
                    string provide = dataArray[x + 5];
                    int number = findProgram(provide, allDegreePrograms);
                    if (number != -1)
                    {
                        temp.Add(provide);
                    }
                }
                allStudents.Add(AddStudent(name, age, fscMarks, ecatMarks, preference, temp));
            }
            file.Close();
        }
        static void loadDegreesData(List<DegreeProgram> allDegreeProgram)
        {
            string name;
            int duration;
            int seats;
            int noOfSubjects;

            string code;
            string type;
            int creditHours;
            double fees;

            string line;
            string[] dataArray;
            string path = "DegreesData.txt";
            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                List<Subject> temp = new List<Subject>();
                line = file.ReadLine();
                dataArray = line.Split(',');
                name = dataArray[0];
                duration = int.Parse(dataArray[1]);
                seats = int.Parse(dataArray[2]);
                noOfSubjects = int.Parse(dataArray[3]);

                for (int x = 0; x < noOfSubjects; x++)
                {
                    line = file.ReadLine();
                    dataArray = line.Split(',');
                    code = dataArray[0];
                    type = dataArray[1];
                    creditHours = int.Parse(dataArray[2]);
                    fees = double.Parse(dataArray[3]);

                    temp.Add(AddSubject(code, type, creditHours, fees));
                }
                allDegreeProgram.Add(AddDegreeProgram(name, duration, seats, noOfSubjects, temp));
            }
            file.Close();
        }
        static void addStudentToFile(string name, int age, int fscMarks, int ecatMarks, int pref, List<string> preferences)
        {
            string path = "studentsData.txt";
            StreamWriter file = new StreamWriter(path, true);
            file.Write(name + "," + age + "," + fscMarks + "," + ecatMarks + "," + pref + ",");
            for (int i = 0; i < preferences.Count; i++)
            {
                if (i == preferences.Count - 1)
                {
                    file.WriteLine(preferences[i]);
                    break;
                }
                file.Write(preferences[i] + ",");
            }
            file.Flush();
            file.Close();
        }
        static void loadUserData(List<User> allUsers)
        {
            string name;
            string password;
            string role;

            string line;
            string path = "UsersData.txt";

            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                line = file.ReadLine();
                string[] dataArray = line.Split(',');
                name = dataArray[0];
                password = dataArray[1];
                role = dataArray[2];
                allUsers.Add(AddUser(name, password, role));
            }
            file.Close();
        }
        static void loadSubjectsData(List<Subject> allSubjects)
        {
            string path = "subjectsData.txt";
            string line;
            string code;
            string type;
            int creditHours;
            double fees;
            StreamReader file = new StreamReader(path);
            while (!file.EndOfStream)
            {
                line = file.ReadLine();
                string[] dataArray = line.Split(',');
                code = dataArray[0];
                type = dataArray[1];
                creditHours = int.Parse(dataArray[2]);
                fees = int.Parse(dataArray[3]);
                allSubjects.Add(new Subject(code, type, creditHours, fees));
            }
            file.Close();
        }
        static User AddUser(string name, string password, string role)
        {
            User newUser = new User(name, password, role);
            return newUser;
        }
        static List<Student> sortDescendingList(List<Student> dataObjects)
        {
            List<Student> list = dataObjects.OrderByDescending(student => student.merit).ToList();
            return list;
        }
        static void calculateMerit(List<Student> dataObjects)
        {
            // merit Critereia
            // merit = 60% fsc + 40% ecat
            foreach (Student student in dataObjects)
            {
                student.merit = ((student.fscMarks / 1100.0f) * 60f) + ((student.ecatMarks / 400.0f) * 40f);
            }
        }
        static void generateMeritLists(List<Student> allStudents, List<DegreeProgram> allDegreePrograms)
        {
            // this student list is already sorted in descending order
            for (int x = 0; x < allStudents.Count; x++)
            {
                for (int p = 0; p < allStudents[x].preference; p++)
                {
                    // preferred degree is the list of desired degrees by a specific student
                    string desiredDegree = allStudents[x].preferredDegrees[p];
                    int number = findProgram(desiredDegree, allDegreePrograms);
                    // this function will return index of degree program in list of allDegreePrograms
                    if (number != -1 && allDegreePrograms[number].seats != 0)
                    {
                        // regDegree is that degree program in which student got final admision
                        allStudents[x].regDegree = new DegreeProgram(allDegreePrograms[number]);
                        Student admitStudent = new Student();
                        admitStudent = allStudents[x];
                        // admitStudent is the present student
                        List<Student> thisDegreeStudents = new List<Student>();
                        thisDegreeStudents = allDegreePrograms[number].returnRegisteredStudents();
                        // thisDegreeStudents are those students who got admission in any respective degree program
                        thisDegreeStudents.Add(admitStudent);
                        allDegreePrograms[number].seats--;
                        p = allStudents[x].preference;
                        // to break the internal for loop
                    }
                    else
                    {
                        allStudents[x].regDegree = null;
                    }
                }
            }
        }
        static int findProgram(string input, List<DegreeProgram> allDegreeProgram)
        {
            for (int i = 0; i < allDegreeProgram.Count; i++)
            {
                if (allDegreeProgram[i].name == input)
                {
                    return i;
                }
            }
            return -1;
        }
        static void loadUserDataFromFile(List<User> allUsers)
        {
            string path = "C:\\Users\\HP\\Desktop\\Programs\\PF05\\SelfAssessment\\UsersData.txt";
            StreamReader file = new StreamReader(path);
            string line, name, pass, role;
            while(!file.EndOfStream)
            {
                line = file.ReadLine();
                string[] dataArray = line.Split(',');
                name = dataArray[0];
                pass = dataArray[1];
                role = dataArray[2];
                allUsers.Add(new User(name, pass, role));
            }
            file.Close();
        }
        static void addUserDatatoFile(List<Student> allStudents)
        {
            string path = "C:\\Users\\HP\\Desktop\\Programs\\PF05\\SelfAssessment\\UsersData.txt";
            StreamWriter file = new StreamWriter(path);
            for (int x = 0; x < allStudents.Count; x++)
            {
                if (allStudents[x].regDegree != null)
                {
                    file.WriteLine(allStudents[x].name + "," + allStudents[x].fscMarks + "," + "Student");
                }
            }
            file.Flush();
            file.Close();
        }
        static int findStudent(string input, List<Student> students)
        {
            int indexer = -1;
            for (int i = 0; i < students.Count; i++)
            {
                if (students[i].name == input)
                {
                    indexer = i;
                    break;
                }
            }
            return indexer;
        }
        static void updateAdmissionStatus()
        {
            string path = "C:\\Users\\HP\\Desktop\\Programs\\PF05\\SelfAssessment\\randomData.txt";
            StreamWriter file = new StreamWriter(path);
            file.WriteLine(admissionOpen);
            file.Flush();
            file.Close();
        }
        static void loadRandomData()
        {
            string path = "C:\\Users\\HP\\Desktop\\Programs\\PF05\\SelfAssessment\\randomData.txt";
            StreamReader file = new StreamReader(path);
            admissionOpen = bool.Parse(file.ReadLine());
            file.Close();
        }
    }
}