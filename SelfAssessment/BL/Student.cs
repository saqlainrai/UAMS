using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SelfAssessment.BL
{
    public class Student
    {
        public string name;
        public int age;
        public int fscMarks;
        public int ecatMarks;
        public int preference;
        public List<string> preferredDegrees;   // list of preferences entered by student
        
        // values assigned after calculations
        public float merit;
        public List<Subject> regSubjects;
        public DegreeProgram regDegree = null;
        public Student(string name, int age, int fscMarks, int ecatMarks, int preference, List<string> listOfDegreesName)
        {
            this.name = name;
            this.age = age;
            this.fscMarks = fscMarks;
            this.ecatMarks = ecatMarks;
            this.preference = preference;
            preferredDegrees = listOfDegreesName;
            regSubjects = new List<Subject>();
        }
        public Student(Student s)
        {
            name = s.name;
            age = s.age;
            fscMarks = s.fscMarks;
            ecatMarks = s.ecatMarks;
            preference = s.preference;
            preferredDegrees = s.preferredDegrees;
            regSubjects = s.regSubjects;
            regDegree = s.regDegree;
            merit = s.merit;
        }
        public Student()
        {

        }
        public DegreeProgram returnDegree()
        {
            return regDegree;
        }
    }
}
