using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic;
using StudentAPIBusinessLayer;
using StudentAPIDataAccessLayer;
using Microsoft.AspNetCore.Http.HttpResults;

namespace StudentApi.Controllers
{
    [ApiController] // Marks the class as a Web API controller with enhanced features.
                    //  [Route("[controller]")] // Sets the route for this controller to "students", based on the controller name.
    [Route("api/Students")]

    public class StudentsController : ControllerBase // Declare the controller class inheriting from ControllerBase.
    {


        [HttpGet("All", Name = "GetAllStudents")] // Marks this method to respond to HTTP GET requests.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<IEnumerable<StudentDTO>> GetAllStudents() // Define a method to get all students.
        {
            List<StudentDTO> students = Student.GetStudents(); // Call the business layer to get the list of students.
            if (students == null || students.Count == 0)
            {
                return NotFound("No Students Found!");
            }
            return Ok(students);
        }

        [HttpGet("Passed", Name = "GetPassedStudents")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        // Method to get all students who passed
        public ActionResult<IEnumerable<Student>> GetPassedStudents()

        {
            List<StudentDTO> studentsList = Student.GetPassedStudents();
            if (studentsList.Count == 0)
            {
                return NotFound("No Passed Students Found!");
            }
            return Ok(studentsList);
        }
        [HttpGet("Count", Name = "Count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<int> GetCountStudents()
        {
            int count = Student.GetCountStudents();
            return Ok(count);
        }


        [HttpGet("AverageGrade", Name = "GetAverageGrade")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<double> GetAverageGrade()
        {
            int count = Student.GetCountStudents();
            //count = 0; // This line is likely a mistake; it resets the count to 0, which will always trigger the NotFound response.
            if (count == 0)
            {
                return NotFound("No students found!");
            }
            double average = Student.GetAverageGrade();
            return Ok(average);

        }



        [HttpGet("{id}", Name = "GetStudentById")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<Student> GetStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID. ID must be greater than zero.");
            }
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound($"Student with ID {id} not found.");
            }
            StudentDTO studentDTO = student.StudentDTO;
            return Ok(studentDTO);
        }

        ////for add new we use Http Post

        [HttpPost(Name = "AddStudent")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<StudentDTO> AddStudent(StudentDTO newStudentDTO)
        {
            if (newStudentDTO == null || string.IsNullOrEmpty(newStudentDTO.Name) || newStudentDTO.Age <= 0 || newStudentDTO.Grade < 0)
            {
                return BadRequest();
            }
            Student student = new Student(newStudentDTO);
            student.Save();
            newStudentDTO.Id = student.Id;

            return CreatedAtRoute("GetStudentById", new { id = newStudentDTO.Id }, newStudentDTO);

        }

        //here we use HttpDelete method
        [HttpDelete("{id}", Name = "DeleteStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest($"Not Accept ID {id}");

            if (Student.DeleteStudent(id))
                return Ok($"Student With StudentID {id} Deleted Successfully");
            else
                return NotFound($"No Student With StudentID {id}");



        }

        //here we use http put method for update
        [HttpPut("{id}", Name = "UpdateStudent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Student> UpdateStudent(int id, StudentDTO updatedStudentDTO)
        {
            if (id < 0 || string.IsNullOrEmpty(updatedStudentDTO.Name) || updatedStudentDTO.Age <= 0 || updatedStudentDTO.Grade < 0)
            {

                return BadRequest("Invalid Student Data");
            }
            Student student = Student.Find(id);
            if (student == null)
            {
                return NotFound($"No Student with Student ID {id}");
            }
            student.Name = updatedStudentDTO.Name;
            student.Age = updatedStudentDTO.Age;
            student.Grade = updatedStudentDTO.Grade;
            if(!student.Save())
            {
                return BadRequest("Invalid Student Data");
            }
            return Ok(student.StudentDTO);


        }



    }
}
