using GradeBook.Enums;
using System;
using System.Collections.Generic;

namespace GradeBook.GradeBooks
{
	public class RankedGradeBook : BaseGradeBook 
	{
        public RankedGradeBook(string name, bool weighted) : base(name, weighted) {
        	Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade) {
        	if (Students.Count < 5) {
        		throw new System.InvalidOperationException("Not enough students");
        	}

            List<Student> localStudents = new List<Student>(Students);
            localStudents.Sort(delegate(Student A, Student B) {
                // Sorts descending
        		if(A.AverageGrade > B.AverageGrade) return -1;
        		if(A.AverageGrade == B.AverageGrade) return 0;
        		else return 1;
        	});
        	// get the index of the first member smaller than us
        	int i;
        	for(i = 0; i < localStudents.Count; i++) {
                if (averageGrade >= localStudents[i].AverageGrade) break;
        	}
        	// + 1 to account for zero indexing
        	double gradePercentile = (double)(i + 1) / (double)localStudents.Count;

            if (gradePercentile <= 0.2) return 'A';
        	else if(gradePercentile <= 0.4) return 'B';
        	else if(gradePercentile <= 0.6) return 'C';
        	else if(gradePercentile <= 0.8) return 'D';
        	else return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students.' when there were less than 5 students.");
                return;
            }

            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade.");
                return;
            }
            base.CalculateStudentStatistics(name);
        }
    }
}