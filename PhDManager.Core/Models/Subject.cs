namespace PhDManager.Core.Models
{
    public class Subject
    {
        public Guid SubjectId { get; set; }

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Semester { get; set; } = string.Empty;
        public int Credits { get; set; }

        public List<StudyProgram> StudyProgram { get; set; } = new List<StudyProgram>();
    }
}
