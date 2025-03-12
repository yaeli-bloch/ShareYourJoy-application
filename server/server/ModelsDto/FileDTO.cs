namespace Server.API.ModelsDto
{
    public class FileDTO
    {
        public string Id { get; set; }
        public string Title { get; set; }  // שם הקובץ
        public string FileUrl { get; set; }  // כתובת ה-URL של הקובץ
        public DateTime CreatedAt { get; set; }  // תאריך יצירה
        public DateTime UpdatedAt { get; set; }  // תאריך עדכון אחרון
        public int UserId { get; set; }  // קשר למשתמש שהעלה את הקובץ
    }
}
