using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace StudentAPIDataAccessLayer
{

    public class StudentDTO
    {
        public StudentDTO(int id, string name, int age, int grade)
        {
            Id = id;
            Name = name;
            Age = age;
            Grade = grade;
        }


        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int Grade { get; set; }
    }

    public class StudentData
    {
        static string _ConnectionString = "Server=DESKTOP-U4T0EEF;\r\nDatabase=StudentsDB;\r\nIntegrated Security=True;\r\nTrustServerCertificate=True;\r\nEncrypt=False;";
        public static List<StudentDTO> GetStudents()
        {
            var studentsList = new List<StudentDTO>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetAllStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            studentsList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }
            return studentsList;
        }


        public static List<StudentDTO> GetPassedStudents()
        {
            var passedStudentsList = new List<StudentDTO>();
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetPassedStudents", connection))
                { 
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            passedStudentsList.Add(new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            ));
                        }
                    }
                }
            }
            return passedStudentsList;
        }

        public static Double GetAverageGrade()
        {
            double average = 0;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using(SqlCommand command = new SqlCommand("SP_GetAverageGrade", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        average = Convert.ToDouble(result);
                    else
                        average = 0; // or handle the case when there are no students
                }
            }
            return average;
        }

        public static int GetCountStudents()
        {
            int count = 0;
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetCountStudents", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        count = Convert.ToInt32(result);
                    else
                        count = 0; // or handle the case when there are no students
                }
            }
            return count;
        }

        public static StudentDTO GetStudentById(int id)
        {

            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetStudentById", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", id);
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new StudentDTO(
                                reader.GetInt32(reader.GetOrdinal("Id")),
                                reader.GetString(reader.GetOrdinal("Name")),
                                reader.GetInt32(reader.GetOrdinal("Age")),
                                reader.GetInt32(reader.GetOrdinal("Grade"))
                            );
                        }
                    }
                }
            }
            return null; // or throw an exception if student not found
        }

        public static int AddNewStudent(StudentDTO studentDTO)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_AddStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", studentDTO.Name);
                    command.Parameters.AddWithValue("@Age", studentDTO.Age);
                    command.Parameters.AddWithValue("@Grade", studentDTO.Grade);
                    var OutputParameterID = new SqlParameter("@NewStudentId", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(OutputParameterID);
                    connection.Open();
                    command.ExecuteNonQuery();
                    return (int)OutputParameterID.Value;
                }
            }
        }
            
        public static bool UpdateStudent(StudentDTO studentDTO)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand("SP_UpdateStudent", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@StudentId", studentDTO.Id);
                        command.Parameters.AddWithValue("@Name", studentDTO.Name);
                        command.Parameters.AddWithValue("@Age", studentDTO.Age);
                        command.Parameters.AddWithValue("@Grade", studentDTO.Grade);

                        connection.Open();
                        command.ExecuteNonQuery();

                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public static bool DeleteStudent(int StudentID)
        {
            using (SqlConnection connection = new SqlConnection(_ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_DeleteStudent", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@StudentId", StudentID);
                    connection.Open();
                    int rowAffected = (int)command.ExecuteScalar();
                    return (rowAffected > 0);
                }
            }
        }
        




    }
}
