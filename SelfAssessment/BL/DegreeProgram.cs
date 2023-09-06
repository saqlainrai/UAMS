using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.BL
{
    public class DegreeProgram
    {
        public string name;
        public int duration;
        public int seats;
        public int noOfSubjects;
        public List<Subject> subjects = new List<Subject>();
        public List<Student> registeredStudents = new List<Student>();
        public List<Subject> returnSubjects()
        {
            return subjects;
        }
        public List<Student> returnRegisteredStudents()
        {
            return registeredStudents;
        }
        public DegreeProgram(string name, int duration, int seats, int noOfSubjects, List<Subject> subjects)
        {
            this.name = name;
            this.duration = duration;
            this.seats = seats;
            this.noOfSubjects = noOfSubjects;
            this.subjects = subjects;
        }
        public DegreeProgram(DegreeProgram degreeProgram)
        {
            name = degreeProgram.name;
            duration = degreeProgram.duration;
            seats = degreeProgram.seats;
            noOfSubjects = degreeProgram.noOfSubjects;
            subjects = degreeProgram.subjects;
        }
    }
}
