using System.Data;
using System.Reflection.Metadata.Ecma335;
using StudentAPIDataAccessLayer;
namespace StudentAPIBusinessLayer
{
    public class Student
    {
        public enum enMode { AddNew = 0, Update = 1 }
        public enMode Mode = enMode.AddNew;

        public StudentDTO StudentDTO {
            get { return (new StudentDTO(this.Id, this.Name, this.Age, this.Grade)); }
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }



        public Student(StudentDTO studentDTO, enMode mode = enMode.AddNew)
        {
            this.Id = studentDTO.Id;
            this.Name = studentDTO.Name;
            this.Age = studentDTO.Age;
            this.Grade = studentDTO.Grade;
            this.Mode = mode;
        }

        

        public bool _AddNewStudent()
        {
            this.Id = StudentData.AddNewStudent(StudentDTO);
            return (this.Id != -1);
        }


        public bool _UpdateStudent()
        {
            return StudentData.UpdateStudent(StudentDTO);
        }
        

        public static List<StudentDTO> GetStudents()
        {
            return StudentData.GetStudents();
        }

        public static List<StudentDTO> GetPassedStudents()
        {
            return StudentData.GetPassedStudents();
        }
        public static double GetAverageGrade()
        {
            return StudentData.GetAverageGrade();
        }
        public static int GetCountStudents()
        {
            return StudentData.GetCountStudents();
        }
        public static Student Find(int StudentID)
        {
            StudentDTO studentDTO = StudentData.GetStudentById(StudentID);
            if (studentDTO != null)
                return new Student(studentDTO, enMode.Update);

            return null;
            
        }
        public static bool DeleteStudent(int StudentID)
        {
            return StudentData.DeleteStudent(StudentID);
        }



        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewStudent())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;   
                case enMode.Update:
                    return _UpdateStudent();
                    
            }
            return false;
        }

    }
}
