using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Core.Models
{
    public class OFile
    {
        public string Id { get; set; }  // מזהה ייחודי לקובץ ההזמנה
        public string Title { get; set; }  // שם הקובץ
        public string FileUrl { get; set; }  // כתובת ה-URL של הקובץ
        public DateTime CreatedAt { get; set; }  // תאריך יצירה
        public DateTime UpdatedAt { get; set; }  // תאריך עדכון אחרון
        public int UserId { get; set; }  // קשר למשתמש שהעלה את הקובץ
        public User User { get; set; }  //
    }
}
