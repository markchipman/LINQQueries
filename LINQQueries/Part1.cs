using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQQueries {
    public static class Part1 {

        public static void Start() {
            var data = new Data();

            var students = data.Students;
            var subjects = data.Subjects;
            var exams = data.Exams;
            var results = data.Results;

            var query =
                from student in students
                where student.Years > 20
                select student;


            var method = students.Where(x => x.Years > 20);

            query.Print("Basic query");
            method.Print("Basic method");

            var query1 =
                from student in students
                where student.Years > 20 && student.Years <= 22
                select student;

            var method1 = students.Where(x => x.Years > 20 && x.Years <= 22);

            query1.Print("Query 1");
            method1.Print("Method 1");

            bool FilterFunc1(Subject s) => s.Year > 2015 && s.Year < 2017;

            var query2 =
                from subject in subjects
                where FilterFunc1(subject)
                select subject;

            var method2 = subjects.Where(FilterFunc1);

            query2.Print("Query 2");
            method2.Print("Method 2");

            var query3 =
                from student in students
                where student.Years > 20 && student.Years <= 22
                orderby student.Name
                select student;

            var method3 = students.Where(x => x.Years > 20 && x.Years <= 22).OrderBy(x => x.Name);

            query3.Print("Query 3");
            method3.Print("Method 3");

            var query4 =
                from student in students
                where student.Years > 20 && student.Years <= 22
                orderby student.Name descending 
                select student;

            var method4 = students.Where(x => x.Years > 20 && x.Years <= 22).OrderByDescending(x => x.Name);

            query4.Print("Query 4");
            method4.Print("Method 4");

            var query5 =
                from student in students
                where student.Years > 20 && student.Years <= 22
                orderby student.Name, student.Years
                select student;

            var method5 = students
                .Where(x => x.Years > 20 && x.Years <= 22)
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Years);

            query5.Print("Query 5");
            method5.Print("Method 5");


            var query6 =
                from student in students
                where student.Years > 20 && student.Years <= 22
                orderby student.Name descending , student.Years descending 
                select student;

            var method6 = students
                .Where(x => x.Years > 20 && x.Years <= 22)
                .OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Years);

            query6.Print("Query 6");
            method6.Print("Method 6");

            var query7 =
                (from student in students
                where student.Years > 20 && student.Years <= 22
                orderby student.Name descending, student.Years descending
                select student)
                .Reverse();

            var method7 = students
                .Where(x => x.Years > 20 && x.Years <= 22)
                .OrderByDescending(x => x.Name)
                .ThenByDescending(x => x.Years)
                .Reverse();

            query7.Print("Query 7");
            method7.Print("Method 7");

            var extMethod1 = students.Average(x => x.Years);

            extMethod1.Print("Extension Method 1");

            var extMethod2 = students.Where(x => x.Name.StartsWith("Stu")).Average(x => x.Years);

            extMethod2.Print("Extension Method 2");

            var extMethod3 = students.Single(x => x.Name == "Student 1");

            extMethod3.Print("Extension Method 3");

            var extMethod4 = results.Sum(x => x.Score);

            extMethod4.Print("Extension Method 4");

            var extMethod5 = results
                .Where(x => x.ExamId == exams.Single(y => y.Name == "Subject 1: Exam 1").Id)
                .Sum(x => x.Score);

            extMethod5.Print("Extension Method 5");

            var extMethod6 = results.Count(x => x.Score == 100);

            extMethod6.Print("Extension Method 6");

            var extMethod7 = results
                .Where(x => x.StudentId == students.Single(y => y.Name =="Student 1").Id)
                .Count(x => x.Score == 100);

            extMethod7.Print("Extension Method 7");

            var extMethod8 = students.All(x => !string.IsNullOrEmpty(x.Name));

            extMethod8.Print("Extension Method 8");

            var extMethod9 = students.Any(x => string.IsNullOrEmpty(x.Name));

            extMethod9.Print("Extension Method 9");

            var extMethod10 = students.Max(x => x.Years);

            extMethod10.Print("Extension Method 10");

            var extMethod11 = students.Min(x => x.Years);

            extMethod11.Print("Extension Method 11");

            var extMethod12 = students.First();

            extMethod12.Print("Extension Method 12");

            var extMethod13 = students.First(x => x.Name.StartsWith("Stu"));

            extMethod13.Print("Extension Method 13");

            var extMethod14 = students.Skip(5);

            extMethod14.Print("Extension Method 14");

            var extMethod15 = students.Take(5);

            extMethod15.Print("Extension Method 15");

            var extMethod16 = students.Skip(5).Take(5);

            extMethod16.Print("Extension Method 16");

            var extMethod17 = students.SkipWhile(x => x.Years < 19);

            extMethod17.Print("Extension Method 17");

            var extMethod18 = students.TakeWhile(x => x.Years < 19);

            extMethod18.Print("Extension Method 18");

            var extMethod19 = students.Select(x => x.Name).Union(subjects.Select(y => y.Name));

            extMethod19.Print("Extension Method 19");

            var extMethod20 = students.Select(x => x.Name).Concat(subjects.Select(y => y.Name));

            extMethod20.Print("Extension Method 20");

            Console.ReadKey();
        }

        #region Helpers

        private static void Print<T>(this T data, string name = "") {
            Console.WriteLine($"==== {name}");
            Console.WriteLine(data);
        }

        private static void Print<T>(this IOrderedEnumerable<T> data, string name = "") {
            Console.WriteLine($"==== {name}");
            foreach (var d in data) {
                Console.WriteLine(d);
            }
        }

        private static void Print<T>(this IEnumerable<T> data, string name = "") {
            Console.WriteLine($"==== {name}");
            foreach (var d in data) {
                Console.WriteLine(d);
            }
        }
        #endregion
    }



    #region Initializers

    class Data {
        public List<Student> Students = new List<Student> { };
        public List<Subject> Subjects = new List<Subject> { };
        public List<Exam> Exams = new List<Exam> { };
        public List<Result> Results = new List<Result> { };

        public Data() {
            for (var i = 0; i < 10; i++) {
                Students.Add(new Student($"Student {i}", 15 + i, $"City {i}"));
            }

            for (var i = 0; i < 10; i++) {
                Subjects.Add(new Subject($"Subject {i}", 2010 + i));
            }

            foreach (var subject in Subjects) {
                for (var i = 0; i < 4; i++) {
                    Exams.Add(new Exam(subject, $"{subject.Name}: Exam {i}"));
                }
            }

            var random = new Random();

            foreach (var exam in Exams) {
                foreach (var student in Students) {
                    Results.Add(new Result(student, exam, random.Next(0, 101)));
                }
            }
        }
    }

    #endregion

    #region Classes
    class Student {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Years { get; set; }
        public string City { get; set; }

        public Student(string name, int years, string city) {
            Id = Guid.NewGuid();
            Name = name;
            Years = years;
            City = city;
        }

        public override string ToString() {
            return $"[Student] Name: {Name}, Years: {Years}, City: {City}";
        }
    }

    class Subject {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public Subject(string name, int year) {
            Id = Guid.NewGuid();
            Name = name;
            Year = year;
        }

        public override string ToString() {
            return $"[Subject] Name: {Name}, Year: {Year}";
        }
    }

    class Enroll {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }

        public Enroll(Student student, Subject subject) {
            StudentId = student.Id;
            SubjectId = subject.Id;
        }

        public override string ToString() {
            return $"[Enroll] Student: {StudentId}, Subject: {SubjectId}";
        }
    }

    class Exam {
        public Guid Id { get; set; }
        public Guid SubjectId { get; set; }
        public string Name { get; set; }

        public Exam(Subject subject, string name) {
            Id = Guid.NewGuid();
            SubjectId = subject.Id;
            Name = name;
        }

        public override string ToString() {
            return $"[Exam] Name: {Name}, Subject: {SubjectId}";
        }
    }

    class Result {
        public Guid StudentId { get; set; }
        public Guid ExamId { get; set; }
        public int Score { get; set; }

        public Result(Student student, Exam exam, int score) {
            StudentId = student.Id;
            ExamId = exam.Id;
            Score = score;
        }

        public override string ToString() {
            return $"[Result] Student: {StudentId}, Exam: {ExamId}, Score: {Score}";
        }
    }
    #endregion
}
