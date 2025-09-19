using System;

namespace NotesSharingWebForms.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FilePath { get; set; }      
        public int UploadedBy { get; set; }
        public DateTime UploadTime { get; set; }
    }
}
