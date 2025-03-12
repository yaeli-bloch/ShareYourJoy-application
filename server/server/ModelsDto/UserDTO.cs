namespace Server.API.ModelsDto
{
    public class UserDTO
    {
       public int Id { get; set; }
        public string FirstName { get; set; } // שם פרטי
        public string LastName { get; set; } // שם משפחה
        public string PreviousLastName { get; set; } // שם משפחה קודם (אם יש)
        public int NumberOfChildren { get; set; } // מספר ילדים
        public string Email { get; set; } // כתובת אימייל
        public DateTime CreatedAt { get; set; } // תאריך יצירה
        public DateTime UpdatedAt { get; set; } // תאריך עדכון אחרון
    }
}
