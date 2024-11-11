using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SchoolManagementSystem
{
    class Program
    {
        static List<Student> students = new List<Student>();
        static List<Teacher> teachers = new List<Teacher>();
        static string studentFile = "students.txt";
        static string teacherFile = "teachers.txt";

        static void Main(string[] args)
        {
            LoadData(); // Load existing data from files
            while (true)
            {
                Console.Clear();
                Console.WriteLine("===== School Management System =====");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Add Teacher");
                Console.WriteLine("3. View Students");
                Console.WriteLine("4. View Teachers");
                Console.WriteLine("5. Search Student");
                Console.WriteLine("6. Search Teacher");
                Console.WriteLine("7. Update Student");
                Console.WriteLine("8. Update Teacher");
                Console.WriteLine("9. Delete Student");
                Console.WriteLine("10. Calculate Grade");
                Console.WriteLine("11. Delete Teacher");
                Console.WriteLine("12. Save & Exit");
                Console.Write("Choose an option (1-12): ");
                
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": AddStudent(); break;
                    case "2": AddTeacher(); break;
                    case "3": ViewStudents(); break;
                    case "4": ViewTeachers(); break;
                    case "5": SearchStudent(); break;
                    case "6": SearchTeacher(); break;
                    case "7": UpdateStudent(); break;
                    case "8": UpdateTeacher(); break;
                    case "9": DeleteStudent(); break;
                    case "10": CalculateGrade(); break;
                    case "11": DeleteTeacher(); break;
                    case "12": SaveData(); return;
                    default: Console.WriteLine("Invalid option. Press Enter to try again."); Console.ReadLine(); break;
                }
            }
        }

        static void AddStudent()
        {
            Console.Clear();
            Console.Write("Enter Student ID: ");
            string id = Console.ReadLine();
            if (students.Exists(s => s.ID == id))
            {
                Console.WriteLine("Student ID already exists. Press Enter to try again.");
                var input = Console.ReadLine();
                return;
            }

            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Age: ");
            int age = int.TryParse(Console.ReadLine(), out age) ? age : 0;
            Console.Write("Enter Grade: ");
            string grade = Console.ReadLine();

            students.Add(new Student { ID = id, Name = name, Age = age, Grade = grade });
            Console.WriteLine("Student added successfully! Press Enter to continue.");
            Console.ReadLine();
        }

        static void AddTeacher()
        {
            Console.Clear();
            Console.Write("Enter Teacher ID: ");
            string id = Console.ReadLine();
            if (teachers.Exists(t => t.ID == id))
            {
                Console.WriteLine("Teacher ID already exists. Press Enter to try again.");
                Console.ReadLine();
                return;
            }

            Console.Write("Enter Teacher Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Subject: ");
            string subject = Console.ReadLine();

            teachers.Add(new Teacher { ID = id, Name = name, Subject = subject });
            Console.WriteLine("Teacher added successfully! Press Enter to continue.");
            Console.ReadLine();
        }

        static void ViewStudents()
        {
            Console.Clear();
            if (students.Count == 0)
            {
                Console.WriteLine("No students available.");
            }
            else
            {
                Console.WriteLine("===== Student List =====");
                foreach (var student in students)
                {
                    Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                    if (student.Scores.Count > 0)
                    {
                        Console.WriteLine($"Scores: {string.Join(", ", student.Scores)}");
                    }
                    else
                    {
                        Console.WriteLine("Scores: N/A");
                    }
                }
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void ViewTeachers()
        {
            Console.Clear();
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers available.");
            }
            else
            {
                Console.WriteLine("===== Teacher List =====");
                foreach (var teacher in teachers)
                {
                    Console.WriteLine($"ID: {teacher.ID}, Name: {teacher.Name}, Subject: {teacher.Subject}");
                }
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void SearchStudent()
        {
            Console.Write("Enter Student ID or Name to search: ");
            string query = Console.ReadLine();
            var results = students.FindAll(s => s.ID.Contains(query) || s.Name.Contains(query));

            if (results.Count == 0)
            {
                Console.WriteLine("No student found with the given ID or name.");
            }
            else
            {
                Console.WriteLine("===== Search Results =====");
                foreach (var student in results)
                {
                    Console.WriteLine($"ID: {student.ID}, Name: {student.Name}, Age: {student.Age}, Grade: {student.Grade}");
                    if (student.Scores.Count > 0)
                    {
                        Console.WriteLine($"Scores: {string.Join(", ", student.Scores)}");
                    }
                    else
                    {
                        Console.WriteLine("Scores: N/A");
                    }
                }
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void SearchTeacher()
        {
            Console.Write("Enter Teacher ID or Name to search: ");
            string query = Console.ReadLine();
            var results = teachers.FindAll(t => t.ID.Contains(query) || t.Name.Contains(query));

            if (results.Count == 0)
            {
                Console.WriteLine("No teacher found with the given ID or name.");
            }
            else
            {
                Console.WriteLine("===== Search Results =====");
                foreach (var teacher in results)
                {
                    Console.WriteLine($"ID: {teacher.ID}, Name: {teacher.Name}, Subject: {teacher.Subject}");
                }
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void UpdateStudent()
        {
            Console.Write("Enter Student ID to update: ");
            string id = Console.ReadLine();
            var student = students.Find(s => s.ID == id);

            if (student != null)
            {
                Console.Write("Enter new name: ");
                student.Name = Console.ReadLine();
                Console.Write("Enter new age: ");
                student.Age = int.TryParse(Console.ReadLine(), out int age) ? age : student.Age;
                Console.Write("Enter new grade: ");
                student.Grade = Console.ReadLine();

                Console.WriteLine("Do you want to update scores? (yes/no)");
                string updateScores = Console.ReadLine();
                if (updateScores.ToLower() == "yes")
                {
                    student.Scores.Clear();
                    Console.WriteLine("Enter student scores (enter 'done' when finished):");
                    while (true)
                    {
                        Console.Write("Enter score: ");
                        string score = Console.ReadLine();
                        if (score.ToLower() == "done") break;
                        if (int.TryParse(score, out int scoreValue))
                        {
                            student.Scores.Add(scoreValue);
                        }
                        else
                        {
                            Console.WriteLine("Invalid score. Try again.");
                        }
                    }
                }

                Console.WriteLine("Student updated successfully! Press Enter to continue.");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            Console.ReadLine();
        }

        static void UpdateTeacher()
        {
            Console.Write("Enter Teacher ID to update: ");
            string id = Console.ReadLine();
            var teacher = teachers.Find(t => t.ID == id);

            if (teacher != null)
            {
                Console.Write("Enter new name: ");
                teacher.Name = Console.ReadLine();
                Console.Write("Enter new subject: ");
                teacher.Subject = Console.ReadLine();

                Console.WriteLine("Teacher updated successfully! Press Enter to continue.");
            }
            else
            {
                Console.WriteLine("Teacher not found.");
            }
            Console.ReadLine();
        }

        static void DeleteStudent()
        {
            Console.Write("Enter Student ID to delete: ");
            string id = Console.ReadLine();
            var student = students.Find(s => s.ID == id);

            if (student != null)
            {
                students.Remove(student);
                Console.WriteLine("Student deleted successfully!");
            }
            else
            {
                Console.WriteLine("Student not found.");
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void DeleteTeacher()
        {
            Console.Write("Enter Teacher ID to delete: ");
            string id = Console.ReadLine();
            var teacher = teachers.Find(t => t.ID == id);

            if (teacher != null)
            {
                teachers.Remove(teacher);
                Console.WriteLine("Teacher deleted successfully!");
            }
            else
            {
                Console.WriteLine("Teacher not found.");
            }
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
        }

        static void SaveData()
        {
            // Save student data, including scores
            File.WriteAllLines(studentFile, students.ConvertAll(s => $"{s.ID},{s.Name},{s.Age},{s.Grade},{string.Join(",", s.Scores)}"));
            // Save teacher data
            File.WriteAllLines(teacherFile, teachers.ConvertAll(t => $"{t.ID},{t.Name},{t.Subject}"));
            Console.WriteLine("Data saved successfully! Press Enter to exit.");
            Console.ReadLine();
        }

        static void LoadData()
{
    if (File.Exists(studentFile))
    {
        var studentLines = File.ReadAllLines(studentFile);
        foreach (var line in studentLines)
        {
            // Skip empty lines
            if (string.IsNullOrWhiteSpace(line)) continue;

            var data = line.Split(',');

            // Validate and parse the student data
            if (data.Length >= 4) // Ensure there are enough fields to parse
            {
                var student = new Student
                {
                    ID = data[0],
                    Name = data[1],
                    Grade = data[3]  // Assuming grade is always available
                };

                // Try parsing the age field
                if (int.TryParse(data[2], out int age))
                {
                    student.Age = age;
                }
                else
                {
                    Console.WriteLine($"Invalid age format for student {student.Name}. Setting default age.");
                    student.Age = 0; // Default to 0 if parsing fails
                }

                // Load scores if available
                if (data.Length > 4)
                {
                    // Parse the scores and handle any non-numeric values
                    foreach (var score in data.Skip(4))
                    {
                        if (int.TryParse(score, out int scoreValue))
                        {
                            student.Scores.Add(scoreValue);
                        }
                        else
                        {
                            Console.WriteLine($"Invalid score '{score}' for student {student.Name}. Skipping.");
                        }
                    }
                }

                students.Add(student);
            }
            else
            {
                Console.WriteLine("Invalid student data format, skipping line.");
            }
        }
    }

    if (File.Exists(teacherFile))
    {
        var teacherLines = File.ReadAllLines(teacherFile);
        foreach (var line in teacherLines)
        {
            var data = line.Split(',');

            if (data.Length >= 3) // Ensure teacher data is valid
            {
                teachers.Add(new Teacher { ID = data[0], Name = data[1], Subject = data[2] });
            }
            else
            {
                Console.WriteLine($"Invalid teacher data format, skipping line.");
            }
        }
    }
}


        static void CalculateGrade()
        {
            Console.Clear();
            Console.WriteLine("===== Calculate Grade =====\n");

            foreach (var student in students)
            {
                if (student.Scores.Count > 0)
                {
                    double averageScore = student.Scores.Average();

                    // Assign letter grade based on the average score
                    student.Grade = GetGrade(averageScore);
                }
            }

            Console.WriteLine("Grades calculated successfully! Press Enter to continue.");
            Console.ReadLine();
        }

        // Method to convert average score to letter grade
        static string GetGrade(double averageScore)
        {
            if (averageScore >= 90) return "A";
            if (averageScore >= 80) return "B";
            if (averageScore >= 70) return "C";
            if (averageScore >= 60) return "D";
            return "F";
        }
    }

    class Student
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Grade { get; set; }
        public List<int> Scores { get; set; } = new List<int>();
    }

    class Teacher
    {        
        public string ID { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
    }
}

