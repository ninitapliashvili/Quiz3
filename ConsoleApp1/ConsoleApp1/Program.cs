using System.Collections.Generic;
using System.Runtime.InteropServices;

using System.Xml.Linq;
/*Საგნის კლასი უნდა შეიცავდეს საგნის დასახელებას, წინაპირობებეს (იმ საგნებს რომელიც არის წინაპირობა ახალი საგნის ასარჩევად), 
 * კრედიტის რაოდენობას და საგანზე მაქსიმალური სტუდენტების რაოდენობას. 
 */
public class Course
{
    public string Name { get; set; }
    public List<Course> Prerequisites = new List<Course>();
    public int Credits { get; set; }
    public int MaxStudents { get; set; }

    public Course(string name, List<Course> prerequisites, int credits, int maxStudents)
    {
        Name = name;
        Prerequisites = prerequisites;
        Credits = credits;
        MaxStudents = maxStudents;
    }

    public void AddPrerequisits(Course course)
    {
        Prerequisites.Add(course);
    }

    public override string ToString()
    {
        return $"{Name} - Prerequisites: {string.Join(", ", Prerequisites)}, Max Students: {MaxStudents}, credits: {Credits}";
    }
}

/*
 * Სტუდენტის კლასში უნდა არსეობდეს ინფორმაცია სტუდენტის სახელი გვარი პირადი ნომერი (პირადი ნომერი არ უნდა აღემატებოდეს 11 ციფრს), 
 * საგნების ჩამონათვალს და სემესტრების შესახებ ინფორმაციას.(ეს ცალკე კლასად შეგიძლიათ აღწეროთ). 
 * Შექმენით property რომელიც გამოტანს სახელს და გვარს ერთად. Სტუდენტს არ უნდა შეეძოს 35 GPA ზე მეტის აღება. 
 * Შექმენით მეთოდები რომელიც გამოიტანს სემესტრის ან სემესტრების საგნების ჩამონათვალს და ლექტორების შესახებ ინფორმაციას.  
 * Სტუდენტ კლასს ასევე უნდა ჰქონდეს მეთოდები რომელიც დაამატებს სემესტრს და ამ სემესტრის საგნებს. Ან კონკრეტულ სემესტრში დაამატებს საგანს.
 */
public class Semester
{
    public string Name;
    private List<Course> courses = new List<Course>();
    private List<Teacher> teachers = new List<Teacher>();

    public override string ToString()
    {
        return $"{Name} - courses: {string.Join(", ", courses)}, teachers: {string.Join(", ", teachers)}";
    }
    public Semester(string name)
    {
        Name = name;
    }

    public void AddCourse(Course course)
    {
        courses.Add(course);
    }

    public void AddTeacher(Teacher teacher)
    {
        teachers.Add(teacher);
    }

    public List<Course> GetCourses()
    {
        return courses;
    }
    public List<Teacher> GetTeachers()
    {
        return teachers;
    }
}
public class Student
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    private string personalNumber;
    public string PersonalNumber
    {
        get
        {
            return personalNumber;
        }
        set
        {
            if (value.Length == 11)
            {
                personalNumber = value;
            }
            else
            {
                throw new Exception("Personal number should have 11 digits.");
            }
        }
    }

    public string FullName => $"{FirstName} {LastName}";

    public Student(string _FirstName, string _LastName)
    {
        FirstName = _FirstName;
        LastName = _LastName;
    }

    private double _gpa;
    public double Gpa
    {
        get { return _gpa; }
        set
        {
            if (value > 35)
            {
                throw new ArgumentException("GPA can't be greater than 35.");
            }
            _gpa = value;
        }
    }
    public override string ToString()
    {
        return $"{FullName} - Personal number: {PersonalNumber}, Gpa: {Gpa}";
    }
    private List<Semester> semesters = new List<Semester>();

    private List<Teacher> teachers = new List<Teacher>();
    public void AddSemester(Semester semester)
    {
        semesters.Add(semester);
    }

    public void GetSemesterAndCourseInfo()
    {
        foreach (var semester in semesters)
        {
            Console.WriteLine($"semester: {semester.Name}");
            for (int i = 0; i < semester.GetCourses().Count; i++)
            {
                Console.WriteLine($"{i + 1}. {semester.GetCourses()[i]}");
            }
        }
    }



    public void AddCourseToSemester(int semesterIndex, Course course)
    {
        if (semesterIndex >= 0 && semesterIndex < semesters.Count)
        {
            semesters[semesterIndex].AddCourse(course);
        }
    }

    public List<Semester> GetSemesters()
    {
        return semesters;
    }

    public List<Course> GetCoursesForSemester(int semesterIndex)
    {
        if (semesterIndex >= 0 && semesterIndex < semesters.Count)
        {
            return semesters[semesterIndex].GetCourses();
        }

        return null;
    }

    public void GetTeacherInfo()
    {
        foreach (Teacher teacher in teachers)
        {
            Console.WriteLine($"Name: {teacher.FirstName} Department: {teacher.LastName} \n Courses: ");
            for (int i = 0; i < teacher.GetCourses().Count; i++)
            {
                Console.WriteLine($"\t {i + 1}. {teacher.GetCourses()[i]}.");
            }
        }
    }
}

/*
 * Მასწავლებლის კლასი უნდა შეიცავდეს მასწავლების სახელს და გვარს და იმ საგნების სიას რომელსაც ასწავლის. 
 * Ერთ მასწავლებელს არ უნდა შეეძლოს 3 ზე მეტი საგნის სწავლება. Კლასს განუსაზღვრეთ საგნის დამატების და შეცვლის მეთოდები.
 */

public class Teacher
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    private List<Course> courses = new List<Course>();

    public override string ToString()
    {
        return $"{FirstName} {LastName} - courses: {string.Join(", ", courses)}.";
    }

    public Teacher(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
    public void AddCourse(Course courseName)
    {
        if (courses.Count >= 3)
        {
            throw new Exception("You already teach 3 subjects.");
        }
        courses.Add(courseName);
    }

    public void EditCourse(int index, Course newCourseName)
    {
        courses[index] = newCourseName;
    }

    public List<Course> GetCourses()
    {
        return courses;
    }
}

/*
 * Შექმენით Custom Queue- ჯენერიკ იმპლემენტიაცია განუსაზღვრეთ კონსტრუქტორი პარამეტრიანი და უპარამეტროც. 
 * Უნდა შეიძებოდეს ქიუს ზომის შემოწმება ცარიელია თუ არა. Უნდა იყოს ელემენტის დამატების და წაშლის მეთოდები. 
 * Დანარჩენი მეთოდები შეგიძლიათ დაამატოთ სურვილისამებრ.
 */
class CustomQueue<T>
{
    private List<T> _items;

    public CustomQueue()
    {
        _items = new List<T>();
    }

    public CustomQueue(IEnumerable<T> collection)
    {
        _items = new List<T>(collection);
    }

    public int GetQLength
    {
        get { return _items.Count; }
    }

    public bool IsEmpty
    {
        get { return _items.Count == 0; }
    }

    public void AddElement(T item)
    {
        _items.Add(item);
    }

    public T DeleteElement(int ItemIndex)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException("Queue is empty.");
        }

        T item = _items[ItemIndex];
        _items.RemoveAt(ItemIndex);
        return item;
    }

}

/*
 * Შექმენით ინტერფეისი IShape რომელსაც ექნება ფართობის და პერიმეტრის გამოთვლის მეთოდები. 
 * Შექმენით პრიზმის, სამკუთხედის , მართკუთხედის, და ტრაპეციის კლასები. Ველები დაამატეთ საჭიროებისამებრ. 
 * Გააკეთეთ თითოეულ კლასში IShape-ის იმპლემენტაცია. 
 */
public interface IShape
{
    double GetArea();
    double GetPerimeter();
}

//პრიზმის კლასი:
public class Prism : IShape
{
    private readonly double _height;
    private readonly IShape _baseShape;

    public Prism(IShape baseShape, double height)
    {
        _baseShape = baseShape;
        _height = height;
    }

    public double GetArea()
    {
        return 2 * _baseShape.GetArea() + _baseShape.GetPerimeter() * _height;
    }

    public double GetPerimeter()
    {
        return _baseShape.GetPerimeter() + 2 * _baseShape.GetArea() / _height;
    }
}

//სამკუთხედი
public class Triangle : IShape
{
    public double Base { get; set; }
    public double Height { get; set; }
    public double Side1 { get; set; }
    public double Side2 { get; set; }
    public double Side3 { get; set; }

    public Triangle(double @base, double height, double side1, double side2, double side3)
    {
        Base = @base;
        Height = height;
        Side1 = side1;
        Side2 = side2;
        Side3 = side3;
    }

    public double GetArea()
    {
        return 0.5 * Base * Height;
    }

    public double GetPerimeter()
    {
        return Side1 + Side2 + Side3;
    }
}

//მართკუთხედი

public class Rectangle : IShape
{
    public double Length { get; set; }
    public double Width { get; set; }

    public Rectangle(double length, double width)
    {
        Length = length;
        Width = width;
    }

    public double GetArea()
    {
        return Length * Width;
    }

    public double GetPerimeter()
    {
        return 2 * (Length + Width);
    }
}

//ტრაპეცია

public class Trapezoid : IShape
{
    public double Base1 { get; set; }
    public double Base2 { get; set; }
    public double Height { get; set; }
    public double Side1 { get; set; }
    public double Side2 { get; set; }

    public Trapezoid(double base1, double base2, double height, double side1, double side2)
    {
        Base1 = base1;
        Base2 = base2;
        Height = height;
        Side1 = side1;
        Side2 = side2;
    }

    public double GetArea()
    {
        return 0.5 * (Base1 + Base2) * Height;
    }

    public double GetPerimeter()
    {
        return Base1 + Base2 + Side1 + Side2;
    }
}





