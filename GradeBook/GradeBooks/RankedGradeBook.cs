using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
	public class RankedGradeBook : BaseGradeBook 
	{
        public RankedGradeBook(string name) : base(name) {
        	Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade) {
        	if (Students.Count < 5) {
        		throw new System.InvalidOperationException("Not enough students");
        	}
        	var grades = Students.OrderByDescending(e => e.AverageGrade).Select(e => e.AverageGrade).ToList();
        	Students.Sort(delegate(Student A, Student B) {
        		if(A.AverageGrade > B.AverageGrade) return 1;
        		if(A.AverageGrade == B.AverageGrade) return 0;
        		else return -1;
        	});
        	// get the index of the first member larger than us
        	int i;
        	for (i = 0; i < grades.Count; i++) {
        		if (averageGrade < grades[i].AverageGrade) break;
        	}
        	// + 1 to account for zero indexing
        	double gradePercentile = (i+ 1) / (grades.Count + 1);
        	if(gradePercentile >= 0.8) return 'A';
        	else if(gradePercentile >= 0.6) return 'B';
        	else if(gradePercentile >= 0.4) return 'C';
        	else if(gradePercentile >= 0.2) return 'B';
        	else return 'F';
        }
	}
}